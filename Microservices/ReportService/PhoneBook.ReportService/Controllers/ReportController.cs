using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Model.DataTransferObjects.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.ReportService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }


        [CapSubscribe("PhoneBook.Report.Create.DetailedLocationReport")]
        public void CreateDetailedLocationReport(CreateDetailedLocationReportCommand command)
        {

        }
    }
}
