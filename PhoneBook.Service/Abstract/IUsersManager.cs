using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Service.Abstract
{
    public interface IUsersManager
    {

        List<Users> GetAll();
        List<UsersWithDetailResponse> GetAllWithDetail();
        UsersWithDetailResponse GetById(int UUID);
        BaseResponse Add(UserPostRequest request);
        BaseResponse Update(UserPostRequest request);
        BaseResponse Delete(int UUID);
    }
}
