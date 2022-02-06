using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Model.DataTransferObjects.Response
{
    public class ReportResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Location { get; set; }
    }
}
