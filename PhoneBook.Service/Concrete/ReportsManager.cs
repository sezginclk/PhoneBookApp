using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Data.Repositories.Abstract;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using static PhoneBook.Core.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using PhoneBook.Data.Model.DataTransferObjects.DTO;

namespace PhoneBook.Service.Concrete
{
    public class ReportsManager : IReportsManager
    {
        readonly IReportsDal _reportsDal;
        readonly IReportContentDal _reportContentDal;
        readonly IContactsDal _contactsDal;
        readonly IUsersDal _usersDal;
        readonly IUnitOfWork _unitOfWork;
        readonly PBookContext _pbookContext;
        readonly IContactManager _contactManager;

        // Başlangıçta ihtiyaç duyacağımız bağımlılıkları yapıcı method'ta belirlememiz gerekiyor.
        // Hangi Data Access Layer lara ihtiyacın var belirle.
        public ReportsManager(IReportsDal reportsDal, PBookContext pbookContext, IUnitOfWork unitOfWork, IContactsDal contactsDal, IContactManager contactManager, IReportContentDal reportContentDal, IUsersDal usersDal)
        {
            _reportsDal = reportsDal;
            _unitOfWork = unitOfWork;
            _contactsDal = contactsDal;
            _pbookContext = pbookContext;
            _contactManager = contactManager;
            _reportContentDal = reportContentDal;
            _usersDal = usersDal;
        }

        public List<Reports> GetAll()
        {
            List<Reports> reports = new List<Reports>();

            try
            {
                reports = _reportsDal.Table.ToList();
                return reports;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ReportContentReponse GetById(int reportId)
        {
            ReportContent reportContent = new ReportContent();
            Reports report = new Reports();
            ReportContentReponse response = new ReportContentReponse();

            try
            {

                report = _reportsDal.Table.Where(t => t.Id == reportId).FirstOrDefault();
                response.Date = report.Date;
                response.Status = report.Status;


                //tamamlanmayan raporun içeriğini alamayız.
                if (response.Status == Core.Constants.ReportStatus.Completed)
                {
                    reportContent = _reportContentDal.Table.Where(t => t.Id == reportId).FirstOrDefault();
                    response.Location = reportContent.ExcelLocation;

                }

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ReportResponse Add(int personId)
        {
            ReportResponse response = new ReportResponse();
            Reports report = new Reports();

            try
            {
                report.Date = DateTime.Now;
                report.Status = Core.Constants.ReportStatus.Initial;

                _reportsDal.Add(report);
                _unitOfWork.SaveChanges();

                response.Location = _contactsDal.Table.Where(t => t.UUID == personId && t.InformationType == (int)ContactType.Location).FirstOrDefault().Information;
                response.Id = report.Id;

                //kşiye ait konum bilgisi yoksa.
                if (response.Location==null || response.Location=="") {
                    response.SetErrorToResponse(Core.Constants.ERRORCODES.LOCATIONNOTFOUND);
                    return response;
                }

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                return response;

            }
            catch (Exception)
            {

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SYSTEMERROR);
                return response;
            }
        }

        public BaseResponse Update(ReportRequest request)
        {
            ReportResponse response = new ReportResponse();
            Reports report = new Reports();

            try
            {

                report = _reportsDal.Table.Where(t => t.Id == request.ReportId).FirstOrDefault();

                report.Status = request.Status;

                _reportsDal.Update(report);
                _unitOfWork.SaveChanges();

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                return response;

            }
            catch (Exception)
            {

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SYSTEMERROR);
                return response;
            }
        }

        public BaseResponse CreateReport(CreateReportRequest request)
        {
            BaseResponse response = new BaseResponse();
            ReportContentDTO reportContent = new ReportContentDTO();
            List<Contacts> contacts = new List<Contacts>();
            List<Contacts> allContacts = new List<Contacts>();
            List<int> personNumbers = new List<int>();
            int numberCount = 0;

            try
            {
                allContacts = _contactsDal.Table.ToList();
                contacts = _contactsDal.Table.Where(t => t.Information == request.Location).ToList();

                reportContent.Id = request.ReportId;
                reportContent.Location = request.Location;


                //kişileri ayırt etmek için group by ve içeriği UUID olarak kullandık.
                reportContent.PersonInLocation = contacts.GroupBy(t => t.UUID).Count();


                //konumda olan kişilerin listesi alındı
                personNumbers = contacts.Where(t => t.Information == request.Location).Select(t => t.UUID).Distinct().ToList();
                foreach (var personNumber in personNumbers)
                {
                    //liste içerisinde dönülüp kişilere ait telefon numrası sayıları alındı.
                    numberCount += allContacts.Where(t => t.UUID == personNumber && t.InformationType == (int)Core.Constants.ContactType.PhoneNumber).GroupBy(t => t.Information).Count();
                }

                reportContent.NumberInLocation = numberCount;



                //excel işlemleri
                var excel = new Excel.Application();

                var workBooks = excel.Workbooks;
                var workBook = workBooks.Add();
                var workSheet = (Excel.Worksheet)excel.ActiveSheet;


                workSheet.Cells[1, "Konum"] = reportContent.Location;
                workSheet.Cells[1, "Rehbere Kayıtlı Kişi Sayısı"] = reportContent.PersonInLocation;
                workSheet.Cells[1, "Rehbere Kayıtlı Telefon Numarası Sayısı"] = reportContent.NumberInLocation;

                // report Id si ile isimlendirilerek kaydediyor.
                workBook.SaveAs(Directory.GetCurrentDirectory() + "\\" + $"{reportContent.Id}", Excel.XlFileFormat.xlOpenXMLWorkbook);


                //ReportId ve Excel Konumu kaydedliyor..
                ReportContent reportResult = new ReportContent();
                reportResult.Id = reportContent.Id;
                reportResult.ExcelLocation = Directory.GetCurrentDirectory() + "\\" + $"{reportContent.Id}"+ ".xls";
                _reportContentDal.Add(reportResult);
                _unitOfWork.SaveChanges();


                //işlemin sonucuna göre raporlar tablosu güncelleniyor.
                _reportsDal.Update(new Reports() { Id = request.ReportId, Status = Core.Constants.ReportStatus.Completed });




                response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                return response;

            }
            catch (Exception)
            {

                //işlemin sonucuna göre raporlar tablosu güncelleniyor.
                _reportsDal.Update(new Reports() { Id = request.ReportId, Status = Core.Constants.ReportStatus.Error }); 
                response.SetErrorToResponse(Core.Constants.ERRORCODES.SYSTEMERROR);
                return response;
            }
        }
    }
}
