using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Model.DataTransferObjects.DTO
{

    public partial class ReportContentDTO
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public int PersonInLocation { get; set; }

        public int NumberInLocation { get; set; }
    }
}
