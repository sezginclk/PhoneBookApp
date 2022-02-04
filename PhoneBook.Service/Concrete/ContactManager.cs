using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Data.Repositories.Abstract;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PhoneBook.Service.Concrete
{
    class ContactManager : IContactManager
    {
        readonly IUsersDal _usersDal;
        readonly IContactsDal _contactsDal;
        readonly IUnitOfWork _unitOfWork;
        readonly PBookContext _pbookContext;

        // Başlangıçta ihtiyaç duyacağımız bağımlılıkları yapıcı method'ta belirlememiz gerekiyor.
        // Hangi Data Access Layer lara ihtiyacın var belirle.
        public ContactManager(PBookContext pbookContext, IUsersDal usersDal, IUnitOfWork unitOfWork, IContactsDal contactsDal)
        {
            _unitOfWork = unitOfWork;
            _usersDal = usersDal;
            _contactsDal = contactsDal;
            _pbookContext = pbookContext;
        }
        public List<Contacts> GetAll()
        {
            List<Contacts> contacts = new List<Contacts>();

            try
            {
                contacts = _contactsDal.Table.OrderBy(t=>t.UUID).ToList();
                return contacts;
            }
            catch (Exception)
            {
                return null;
            }
        }
         
        public List<Contacts> GetById(int UUID)
        {
            List<Contacts> contacts = new List<Contacts>();

            try
            {
                contacts = _contactsDal.Table.Where(t => t.UUID == UUID).ToList();
                return contacts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BaseResponse Add(ContactPostRequest request)
        {
            BaseResponse response = new BaseResponse();
            Contacts contact = new Contacts();

            try
            {
                if (request.Contact != "" && request.Contact != null)
                {
                    contact.Information = request.Contact;
                    contact.InformationType = (int)request.ContactType;
                    contact.UUID = request.UUID;

                    _contactsDal.Add(contact);
                    _unitOfWork.SaveChanges();

                    response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                    return response;
                }

                response.SetErrorToResponse(Core.Constants.ERRORCODES.CONTACTNOTFOUND);
                return response; 

            }
            catch (Exception)
            {

                return null;
            }
        }

        public BaseResponse Update(ContactPostRequest request)
        {

            BaseResponse response = new BaseResponse();
            Contacts contact = new Contacts();

            try
            {
                Contacts contactCheck = _contactsDal.Table.FirstOrDefault(t => t.UUID == request.UUID && t.InformationType == (int)request.ContactType && t.Information == request.Contact);

                if (contactCheck != null)
                {
                    contactCheck.Information = request.Contact;
                    contactCheck.InformationType = (int)request.ContactType;
                    contactCheck.UUID = request.UUID;

                    _contactsDal.Update(contactCheck);
                    _unitOfWork.SaveChanges();

                    response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
                    return response;
                }

                contact.Information = request.Contact;
                contact.InformationType = (int)request.ContactType;
                contact.UUID = request.UUID;

                _contactsDal.Add(contact);
                _unitOfWork.SaveChanges();


                response.SetErrorToResponse(Core.Constants.ERRORCODES.SUCCESS);
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
            List<Contacts> contacts = new List<Contacts>();

            try
            {
                contacts = _contactsDal.Table.Where(t => t.UUID == UUID).ToList();
                if (contacts.Count > 0)
                {
                    foreach (var contact in contacts)
                    {
                        _contactsDal.Delete(contact);
                    }


                    _unitOfWork.SaveChanges();
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
    }
}
