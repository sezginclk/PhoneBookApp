using System;
using System.Collections.Generic;
using System.Text;
using static PhoneBook.Core.Constants;

namespace PhoneBook.Data.Model.DataTransferObjects.Request
{
    public class ReportRequest
    {
        public int ReportId { get; set; }
        public ReportStatus Status { get; set; }
    }
}
