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
        //normalde her katmanýn testlerini ayrý ayrý yazardým ancak kýsa zaman içerisinde tek sýnýfta yapmak durumundayým.

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
            //ekleme iþlemi sonunda böyle bir veri varsa doðru tamamlanacak. eðer ekleme yapmadýysa veri olmayacaðýndan hatalý olacak
            Assert.IsNotNull(_pbookContext.Users.Where(t => t.Name == "Test" && t.Surname == "Test" && t.Company == "TestCompany").FirstOrDefault());
        }

        [TestMethod]
        public void UserManagerUpdateShouldUpdateUserToDatabase()
        {
            _usersManager.Update(new UserPostRequest() { Name = "Test", Surname = "Test", Company = "TestCompany", UUID = 1 });
            //güncelleme iþlemi sonunda böyle bir veri varsa doðru tamamlanacak. eðer güncelleme yapmadýysa veri olmayacaðýndan veya eski kaydý olacaðýndan  hatalý olacak
            Assert.IsNotNull(_pbookContext.Users.Where(t => t.Name == "Test" && t.Surname == "Test" && t.Company == "TestCompany" && t.UUID == 1).FirstOrDefault());
        }

        [TestMethod]
        public void UserManagerDeleteShouldDeleteUserToDatabase()
        {
            //UUID bilgisi 1 olaak gönderildi.
            _usersManager.Delete(1);
            //1 numaralý user silindi mi,null veri gelirse test doðru tamamlanacak
            Assert.IsNull(_pbookContext.Users.Where(t => t.UUID == 1).FirstOrDefault());
        }

        [TestMethod]
        public void UserManagerGetAllShouldGetAllUserFromDatabase()
        {
            _usersManager.GetAll();
            //bütün user kaydý getirilebilir mi?
            Assert.IsNotNull(_pbookContext.Users.ToList());
        }



        //CONTACT OPERATIONS
        [TestMethod]
        public void ContactManagerAddShouldAddContactToDatabase()
        {
            _contactManager.Add(new ContactPostRequest() { UUID = 1, Contact = "test@test.com", ContactType = ContactType.Mail });
            //ekleme iþlemi sonunda 1 numaralý ID nin böyle bir kaydý varsa doðru tamamlanacak. eðer ekleme yapmadýysa veri olmayacaðýndan hatalý olacak
            Assert.IsNotNull(_pbookContext.Contacts.Where(t => t.UUID == 1 && t.Information == "test@test.com" && t.InformationType == (int)ContactType.Mail).FirstOrDefault());
        }

        [TestMethod]
        public void ContactManagerUpdateShouldUpdateContactToDatabase()
        {
            _contactManager.Update(new ContactPostRequest() { UUID = 1, Contact = "test@test.com", ContactType = ContactType.Mail });
            //güncelleme iþlemi sonunda 1 numaralý ID nin böyle bir kaydý varsa doðru tamamlanacak. eðer güncelleme yapmadýysa veri olmayacaðýndan veya eski kaydý olacaðýndan hatalý olacak
            Assert.IsNotNull(_pbookContext.Contacts.Where(t => t.UUID == 1 && t.Information == "test@test.com" && t.InformationType == (int)ContactType.Mail).FirstOrDefault());
        }

        [TestMethod]
        public void ContactManagerDeleteShouldDeleteContactToDatabase()
        {
            //UUID bilgisi 1 olaak gönderildi.
            _contactManager.Delete(1);
            //1 numaralý usera ait iletiþim bilgileri silindi mi,null veri gelirse test doðru tamamlanacak
            Assert.IsNull(_pbookContext.Contacts.Where(t => t.UUID == 1).FirstOrDefault());
        }



        //REPORT OPERATIONS
        [TestMethod]
        public void ReportManagerCreateReportShouldReportToExcel()
        {
            _reportsManager.CreateReport(new CreateReportRequest() { ReportId = 1, Location = "Test" });
            //Rapor yaratma iþlemi sýrasýnda ilk olarak raporlar tablosuna ekleme yapýyor.
            Assert.IsNotNull(_pbookContext.Reports.Where(t => t.Id == 1 && t.Status == ReportStatus.Initial).FirstOrDefault());

            //rapor yaratma iþlemi tamamlandý ise excel verisi kontrol edilebilir.
            var count = _pbookContext.Reports.Where(t => t.Id == 1 && t.Status == ReportStatus.Completed).Count();
            string excelLocation = Directory.GetCurrentDirectory() + "\\" + $"{1}" + ".xls";
            FileInfo file = new FileInfo(excelLocation);
            //eðer tamamlandýysa count 1 olacaðýndan test eder.
            if (count > 0) { Assert.IsTrue(file.Exists); };
        }

        [TestMethod]
        public void ReportManagerGetAllShouldGetAllReportFromDatabase()
        {
            _reportsManager.GetAll();
            //bütün rapor kaydý getirilebilir mi?
            Assert.IsNotNull(_pbookContext.Reports.ToList());
        }

        [TestMethod]
        public void ReportManagerUpdateShouldUpdateReportToDatabase()
        {
            _reportsManager.Update(new ReportRequest() {  ReportId=1,Status=ReportStatus.Working });
            //güncelleme iþlemi sonunda 1 numaralý ID nin böyle bir rapor kaydý varsa doðru tamamlanacak.
            //eðer güncelleme yapmadýysa veri olmayacaðýndan veya eski rapor kaydý olacaðýndan hatalý olacak veya olma ihtimali var
            Assert.IsNotNull(_pbookContext.Reports.Where(t=>t.Id==1&&t.Status==ReportStatus.Working).FirstOrDefault());
        }

    }
}
