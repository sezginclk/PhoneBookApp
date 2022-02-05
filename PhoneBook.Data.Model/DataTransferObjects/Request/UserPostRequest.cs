using System;
using System.Collections.Generic;
using System.Text;
using static PhoneBook.Core.Constants;

namespace PhoneBook.Data.Model.DataTransferObjects.Request
{
    public class UserPostRequest
    {
        public int? UUID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public ContactType ContactType { get; set; }

        public string Contact { get; set; }
    }

}
