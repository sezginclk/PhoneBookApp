using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Data.Repositories.Abstract;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBook.Service.Concrete
{
    public class UsersManager : IUsersManager
    {
        readonly IUsersDal _usersDal;
        readonly IContactsDal _contactsDal;
        readonly IUnitOfWork _unitOfWork;
        readonly PBookContext _pbookContext;
        readonly IContactManager _contactManager;

        // Başlangıçta ihtiyaç duyacağımız bağımlılıkları yapıcı method'ta belirlememiz gerekiyor.
        // Hangi Data Access Layer lara ihtiyacın var belirle.
        public UsersManager(PBookContext pbookContext, IUsersDal usersDal, IUnitOfWork unitOfWork, IContactsDal contactsDal, IContactManager contactManager)
        {
            _unitOfWork = unitOfWork;
            _usersDal = usersDal;
            _contactsDal = contactsDal;
            _pbookContext = pbookContext;
            _contactManager = contactManager;
        }

        public List<Users> GetAll()
        {
            List<Users> users = new List<Users>();

            try
            {
                users = _usersDal.Table.ToList();
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<UsersWithDetailResponse> GetAllWithDetail()
        {
            List<UsersWithDetailResponse> response = new List<UsersWithDetailResponse>();


            try
            {
                List<Users> users = new List<Users>();
                users = _usersDal.Table.ToList();
                foreach (var user in users)
                {

                    UsersWithDetailResponse usersWithDetail = new UsersWithDetailResponse();
                    usersWithDetail.Name = user.Name;
                    usersWithDetail.Surname = user.Surname;
                    usersWithDetail.Company = user.Company;


                    List<Contacts> contacts = new List<Contacts>();
                    contacts = _contactsDal.Table.Where(t => t.UUID == user.UUID).ToList();
                    if (contacts.Count > 0)
                    {
                        foreach (var contact in contacts)
                        {
                            usersWithDetail.Contacts.Add(contact);
                        }
                    }
                }

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Users GetById(int UUID)
        {
            Users users = new Users();

            try
            {
                users = _usersDal.Table.Where(t => t.UUID == UUID).FirstOrDefault();
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BaseResponse Add(UserPostRequest request)
        {
            BaseResponse response = new BaseResponse();
            Users user = new Users();
            Contacts contact = new Contacts();

            try
            {

                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Company = request.Company;

                _usersDal.Add(user);
                _unitOfWork.SaveChanges();



                //kişiye ait iletişim bilgileri eklenecek
                if (request.Contact != "" && request.Contact != null)
                {
                    ContactPostRequest contactPostRequest = new ContactPostRequest();
                    contactPostRequest.ContactType = request.ContactType;
                    contactPostRequest.Contact = request.Contact;
                    contactPostRequest.UUID = user.UUID;
                    _contactManager.Add(contactPostRequest);
                }


                response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                return response;

            }
            catch (Exception)
            {

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SYSTEMERROR);
                return response;
            }
        }

        public BaseResponse Update(UserPostRequest request)
        {
            BaseResponse response = new BaseResponse();
            Contacts contact = new Contacts();

            try
            {
                Users userCheck = _usersDal.Table.FirstOrDefault(t => t.UUID == request.UUID);

                if (userCheck != null)
                {
                    userCheck.Name = request.Name;
                    userCheck.Surname = request.Surname;
                    userCheck.Company = request.Company;

                    _usersDal.Update(userCheck);
                    _unitOfWork.SaveChanges();



                    //kişiye ait iletişim blgileri güncellenecek
                    if (request.Contact != "" && request.Contact != null)
                    {
                        ContactPostRequest contactPostRequest = new ContactPostRequest();
                        contactPostRequest.ContactType = request.ContactType;
                        contactPostRequest.Contact = request.Contact;
                        contactPostRequest.UUID = userCheck.UUID;
                        _contactManager.Update(contactPostRequest);
                    }


                    response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                    return response;
                }


                response.SetErrorToResponse(Core.Constants.ERRORCODES.USERNOTFOUND);
                return response;

            }
            catch (Exception)
            {

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SYSTEMERROR);
                return response;
            }
        }

        public BaseResponse Delete(int UUID)
        {
            BaseResponse response = new BaseResponse();
            Users user = new Users();
            List<Contacts> contacts = new List<Contacts>();

            try
            {
                user = _usersDal.Table.FirstOrDefault(t => t.UUID == UUID);

                if (user != null)
                {
                    _usersDal.Delete(user);
                    _unitOfWork.SaveChanges();

                    //kişiye ait iletişim blgileri silinecek
                    _contactManager.Delete(user.UUID);

                    response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                    return response;
                }


                response.SetErrorToResponse(Core.Constants.ERRORCODES.USERNOTFOUND);
                return response;

            }
            catch (Exception)
            {

                response.SetErrorToResponse(Core.Constants.ERRORCODES.SYSTEMERROR);
                return response;
            }
        }
    }
}
