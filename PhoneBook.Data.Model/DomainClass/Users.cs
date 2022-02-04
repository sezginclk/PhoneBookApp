using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneBook.Data.Model.DomainClass
{
    public partial class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UUID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string Company { get; set; }

        public int ContactID { get; set; }
    }
}
