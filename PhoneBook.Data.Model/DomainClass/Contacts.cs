using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Data.Model.DomainClass
{
    public partial class Contacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }

        public int UUID { get; set; }

        public int InformationType { get; set; }

        [StringLength(500)]
        public string Information { get; set; }
    }
}
