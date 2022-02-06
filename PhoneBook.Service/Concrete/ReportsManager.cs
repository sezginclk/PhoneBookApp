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

namespace PhoneBook.Service.Concrete
{
    public class ReportsManager : IReportsManager
    {
        readonly IReportsDal _reportsDal;
        readonly IReportContentDal _reportContentDal;
        readonly IContactsDal _contactsDal;
        readonly IUnitOfWork _unitOfWork;
        readonly PBookContext _pbookContext;
        readonly IContactManager _contactManager;

        // Başlangıçta ihtiyaç duyacağımız bağımlılıkları yapıcı method'ta belirlememiz gerekiyor.
        // Hangi Data Access Layer lara ihtiyacın var belirle.
        public ReportsManager(IReportsDal reportsDal, PBookContext pbookContext, IUnitOfWork unitOfWork, IContactsDal contactsDal, IContactManager contactManager, IReportContentDal reportContentDal)
        {
            _reportsDal = reportsDal;
            _unitOfWork = unitOfWork;
            _contactsDal = contactsDal;
            _pbookContext = pbookContext;
            _contactManager = contactManager;
            _reportContentDal = reportContentDal;
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
                    response.LocationX = reportContent.LocationX;
                    response.LocationY = reportContent.LocationY;
                    response.NumberInLocation = reportContent.NumberInLocation;
                    response.PersonInLocation = reportContent.PersonInLocation;

                }

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ReportResponse Add()
        {
            ReportResponse response = new ReportResponse();
            Reports report = new Reports();

            try
            {
                report.Date = DateTime.Now;
                report.Status = Core.Constants.ReportStatus.Initial;

                _reportsDal.Add(report);
                _unitOfWork.SaveChanges();

                response.Id = report.Id;

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
    }
}
