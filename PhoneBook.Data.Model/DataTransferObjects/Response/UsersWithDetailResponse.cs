using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Model.DataTransferObjects.Response
{
    public class UsersWithDetailResponse
    {
        public int UUID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public List<Contacts> Contacts { get; set; } = new List<Contacts>();
    }
}
