using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static PhoneBook.Core.Constants;

namespace PhoneBook.Data.Model.DomainClass
{
    public partial class Reports
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public ReportStatus Status { get; set; }
    }


}
