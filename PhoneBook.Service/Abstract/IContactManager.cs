using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Service.Abstract
{
    interface IContactManager
    {
        List<Contacts> GetAll();
        List<Contacts> GetById(int UUID);
        BaseResponse Add(ContactPostRequest request);
        BaseResponse Update(ContactPostRequest request);
        BaseResponse Delete(int UUID);
    }
}
