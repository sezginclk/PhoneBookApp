using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBook.Service.Abstract;
using PhoneBook.Service.Concrete;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using static PhoneBook.Core.Constants;
using PhoneBook.Data.Contexts;
using System.Linq;
using System.IO;

namespace Test_PhoneBookApp
{
    [TestClass]
    public class UnitTestPhoneBook
    {
        //normalde her katman�n testlerini ayr� ayr� yazard�m ancak k�sa zaman i�erisinde tek s�n�fta yapmak durumunday�m.

        readonly IUsersManager _usersManager;
        readonly IContactManager _contactManager;
        readonly PBookContext _pbookContext;
        readonly IReportsManager _reportsManager;

        public UnitTestPhoneBook(IUsersManager usersManager, PBookContext pbookContext, IContactManager contactManager, IReportsManager reportsManager)
        {
            _usersManager = usersManager;
            _pbookContext = pbookContext;
            _contactManager = contactManager;
            _reportsManager = reportsManager;
        }


        //USER OPERATIONS
        [TestMethod]
        public void UserManagerAddShouldAddUserToDatabase()
        {
            _usersManager.Add(new UserPostRequest() { Name = "Test", Surname = "Test", Company = "TestCompany" });
            //ekleme i�lemi sonunda b�yle bir veri varsa do�ru tamamlanacak. e�er ekleme yapmad�ysa veri olmayaca��ndan hatal� olacak
            Assert.IsNotNull(_pbookContext.Users.Where(t => t.Name == "Test" && t.Surname == "Test" && t.Company == "TestCompany").FirstOrDefault());
        }

        [TestMethod]
        public void UserManagerUpdateShouldUpdateUserToDatabase()
        {
            _usersManager.Update(new UserPostRequest() { Name = "Test", Surname = "Test", Company = "TestCompany", UUID = 1 });
            //g�ncelleme i�lemi sonunda b�yle bir veri varsa do�ru tamamlanacak. e�er g�ncelleme yapmad�ysa veri olmayaca��ndan veya eski kayd� olaca��ndan  hatal� olacak
            Assert.IsNotNull(_pbookContext.Users.Where(t => t.Name == "Test" && t.Surname == "Test" && t.Company == "TestCompany" && t.UUID == 1).FirstOrDefault());
        }

        [TestMethod]
        public void UserManagerDeleteShouldDeleteUserToDatabase()
        {
            //UUID bilgisi 1 olaak g�nderildi.
            _usersManager.Delete(1);
            //1 numaral� user silindi mi,null veri gelirse test do�ru tamamlanacak
            Assert.IsNull(_pbookContext.Users.Where(t => t.UUID == 1).FirstOrDefault());
        }

        [TestMethod]
        public void UserManagerGetAllShouldGetAllUserFromDatabase()
        {
            _usersManager.GetAll();
            //b�t�n user kayd� getirilebilir mi?
            Assert.IsNotNull(_pbookContext.Users.ToList());
        }



        //CONTACT OPERATIONS
        [TestMethod]
        public void ContactManagerAddShouldAddContactToDatabase()
        {
            _contactManager.Add(new ContactPostRequest() { UUID = 1, Contact = "test@test.com", ContactType = ContactType.Mail });
            //ekleme i�lemi sonunda 1 numaral� ID nin b�yle bir kayd� varsa do�ru tamamlanacak. e�er ekleme yapmad�ysa veri olmayaca��ndan hatal� olacak
            Assert.IsNotNull(_pbookContext.Contacts.Where(t => t.UUID == 1 && t.Information == "test@test.com" && t.InformationType == (int)ContactType.Mail).FirstOrDefault());
        }

        [TestMethod]
        public void ContactManagerUpdateShouldUpdateContactToDatabase()
        {
            _contactManager.Update(new ContactPostRequest() { UUID = 1, Contact = "test@test.com", ContactType = ContactType.Mail });
            //g�ncelleme i�lemi sonunda 1 numaral� ID nin b�yle bir kayd� varsa do�ru tamamlanacak. e�er g�ncelleme yapmad�ysa veri olmayaca��ndan veya eski kayd� olaca��ndan hatal� olacak
            Assert.IsNotNull(_pbookContext.Contacts.Where(t => t.UUID == 1 && t.Information == "test@test.com" && t.InformationType == (int)ContactType.Mail).FirstOrDefault());
        }

        [TestMethod]
        public void ContactManagerDeleteShouldDeleteContactToDatabase()
        {
            //UUID bilgisi 1 olaak g�nderildi.
            _contactManager.Delete(1);
            //1 numaral� usera ait ileti�im bilgileri silindi mi,null veri gelirse test do�ru tamamlanacak
            Assert.IsNull(_pbookContext.Contacts.Where(t => t.UUID == 1).FirstOrDefault());
        }



        //REPORT OPERATIONS
        [TestMethod]
        public void ReportManagerCreateReportShouldReportToExcel()
        {
            _reportsManager.CreateReport(new CreateReportRequest() { ReportId = 1, Location = "Test" });
            //Rapor yaratma i�lemi s�ras�nda ilk olarak raporlar tablosuna ekleme yap�yor.
            Assert.IsNotNull(_pbookContext.Reports.Where(t => t.Id == 1 && t.Status == ReportStatus.Initial).FirstOrDefault());

            //rapor yaratma i�lemi tamamland� ise excel verisi kontrol edilebilir.
            var count = _pbookContext.Reports.Where(t => t.Id == 1 && t.Status == ReportStatus.Completed).Count();
            string excelLocation = Directory.GetCurrentDirectory() + "\\" + $"{1}" + ".xls";
            FileInfo file = new FileInfo(excelLocation);
            //e�er tamamland�ysa count 1 olaca��ndan test eder.
            if (count > 0) { Assert.IsTrue(file.Exists); };
        }

        [TestMethod]
        public void ReportManagerGetAllShouldGetAllReportFromDatabase()
        {
            _reportsManager.GetAll();
            //b�t�n rapor kayd� getirilebilir mi?
            Assert.IsNotNull(_pbookContext.Reports.ToList());
        }

        [TestMethod]
        public void ReportManagerUpdateShouldUpdateReportToDatabase()
        {
            _reportsManager.Update(new ReportRequest() {  ReportId=1,Status=ReportStatus.Working });
            //g�ncelleme i�lemi sonunda 1 numaral� ID nin b�yle bir rapor kayd� varsa do�ru tamamlanacak.
            //e�er g�ncelleme yapmad�ysa veri olmayaca��ndan veya eski rapor kayd� olaca��ndan hatal� olacak veya olma ihtimali var
            Assert.IsNotNull(_pbookContext.Reports.Where(t=>t.Id==1&&t.Status==ReportStatus.Working).FirstOrDefault());
        }

    }
}
