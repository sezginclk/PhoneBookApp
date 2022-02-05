using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Repositories.Abstract;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Service.Concrete
{
    public class ReportsManager:IReportsManager
    {
        readonly IReportsDal _reportsDal ;
        readonly IUsersDal _usersDal;
        readonly IContactsDal _contactsDal;
        readonly IUnitOfWork _unitOfWork;
        readonly PBookContext _pbookContext;
        readonly IContactManager _contactManager;

        // Başlangıçta ihtiyaç duyacağımız bağımlılıkları yapıcı method'ta belirlememiz gerekiyor.
        // Hangi Data Access Layer lara ihtiyacın var belirle.
        public ReportsManager(IReportsDal reportsDal ,PBookContext pbookContext, IUsersDal usersDal, IUnitOfWork unitOfWork, IContactsDal contactsDal, IContactManager contactManager)
        {
            _reportsDal = reportsDal;
            _unitOfWork = unitOfWork;
            _usersDal = usersDal;
            _contactsDal = contactsDal;
            _pbookContext = pbookContext;
            _contactManager = contactManager;
        }

        public BaseResponse Add()
        {
            throw new NotImplementedException();
        }

        public BaseResponse Update()
        {
            throw new NotImplementedException();
        }
    }
}
