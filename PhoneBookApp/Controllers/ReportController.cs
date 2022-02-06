using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data.Model.DataTransferObjects.Command;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Service.Abstract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PhoneBook.Core.Constants;

namespace PhoneBookApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Reports")]
    public class ReportController : Controller
    {
        IReportsManager _reportsManager;
        private readonly ICapPublisher _capBus;
        public ReportController(ICapPublisher capBus, IReportsManager reportsManager)
        {
            _capBus = capBus;
            _reportsManager = reportsManager;
        }

        [HttpPost("Add")]
        [SwaggerOperation(Summary = "Rapor Yaratma İşlemi", Description = "Rapor Yaratma İşlemi için kullanılır.")]
        public BaseResponse CreateReport(int personId)
        {  
            ReportResponse operationReport = _reportsManager.Add(personId);
            if (operationReport.Code == (int)ERRORCODES.SUCCESS)
            {
                _capBus.Publish("PhoneBook.Report.Create.DetailedLocationReport", new CreateDetailedLocationReportCommand() { ReportId = operationReport.Id,Location=operationReport.Location });
                
            }

            return operationReport;
        }

        [Route("GetAll")]
        [HttpGet]
        [SwaggerOperation(Summary = "Rapor listesini getir.", Description = "Tüm Rapor listesini getirmek için kullanılır.")]
        public List<Reports> GetAll()
        {
            List<Reports> result = _reportsManager.GetAll();
            return result;
        }

        [Route("GetById")]
        [HttpGet]
        [SwaggerOperation(Summary = "Bir raporu detayları ile birlikte getir.", Description = "Bir raporu detayları ile birlikte getirmek için kullanılır.")]
        public ReportContentReponse GetById(int reportId)
        {
            ReportContentReponse result = _reportsManager.GetById(reportId);
            return result;
        }
    }
}
