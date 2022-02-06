using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Model.DataTransferObjects.Request
{
    public class CreateReportRequest
    {
        public int ReportId { get; set; }

        public string Location { get; set; }
    }
}
