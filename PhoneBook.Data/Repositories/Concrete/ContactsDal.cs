using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Repositories.Concrete
{
    public class ContactsDal : EFRepository<Contacts>, IContactsDal
    {
        public ContactsDal(PBookContext pBookContext) : base(pBookContext)
        {
        }
    }
}
