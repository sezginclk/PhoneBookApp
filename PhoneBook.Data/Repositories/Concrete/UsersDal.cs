using System;
using System.Collections.Generic;
using System.Text;
using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Data.Repositories.Abstract;

namespace PhoneBook.Data.Repositories.Concrete
{
    public class UsersDal : EFRepository<Users>, IUsersDal
    {
        public UsersDal(PBookContext pBookContext) : base(pBookContext)
        {
        }
    }
}
