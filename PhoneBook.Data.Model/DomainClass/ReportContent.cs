using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneBook.Data.Model.DomainClass
{
    public partial class ReportContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public int PersonInLocation { get; set; }

        public int NumberInLocation { get; set; }
    }
}
