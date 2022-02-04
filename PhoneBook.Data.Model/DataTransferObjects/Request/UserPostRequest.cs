using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Model.DataTransferObjects.Request
{
    public class UserPostRequest
    {
        public int? UUID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }
    }

}
