using PhoneBook.Data.Contexts;
using PhoneBook.Data.Repositories.Abstract;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Service.Concrete
{
    public class UsersManager : IUsersManager
    {
        readonly IUsersDal _usersDal ; 
        readonly IUnitOfWork _unitOfWork; 
        readonly PBookContext _pbookContext;

        // Başlangıçta ihtiyaç duyacağımız bağımlılıkları yapıcı method'ta belirlememiz gerekiyor.
        // Hangi Data Access Layer lara ihtiyacın var belirle.
        public UsersManager(PBookContext pbookContext, IUsersDal usersDal, IUnitOfWork unitOfWork )
        { 
            _unitOfWork = unitOfWork;
            _usersDal = usersDal;
            _pbookContext = pbookContext; 
        }


    }
}
