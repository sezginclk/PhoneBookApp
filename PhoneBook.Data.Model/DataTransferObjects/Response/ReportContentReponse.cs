using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Model.DataTransferObjects.Response
{
    public class ReportContentReponse : BaseResponse
    {
        public string Location { get; set; }

        public int PersonInLocation { get; set; }

        public int NumberInLocation { get; set; }

        public DateTime Date { get; set; }

        public Core.Constants.ReportStatus Status { get; set; }
    }
}
