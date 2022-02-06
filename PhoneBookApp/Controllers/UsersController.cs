using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data.Model.DataTransferObjects.Command;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Service.Abstract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PhoneBook.Core.Constants;

namespace PhoneBookApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        IUsersManager _userManager;
        private readonly ICapPublisher _capBus;
        public UsersController(IUsersManager userManager, ICapPublisher capBus)
        {
            _userManager = userManager;
            _capBus = capBus;
        }


        [Route("GetAll")]
        [HttpGet]
        [SwaggerOperation(Summary = "User listesini getir.", Description = "Tüm user listesini getirmek için kullanılır.")]
        public List<Users> GetAll()
        {
            List<Users> result = _userManager.GetAll();
            return result;
        }

        [Route("GetAllWithDetail")]
        [HttpGet]
        [SwaggerOperation(Summary = "User listesini detayları ile birlikte getir.", Description = "Tüm user listesini detayları ile birlikte getirmek için kullanılır.")]
        public List<UsersWithDetailResponse> GetAllWithDetail()
        {
            List<UsersWithDetailResponse> result = _userManager.GetAllWithDetail();
            return result;
        }


        [Route("GetById")]
        [HttpGet]
        [SwaggerOperation(Summary = "Bir Userı detayları ile birlikte getir.", Description = "Bir userı detayları ile birlikte getirmek için kullanılır.")]
        public UsersWithDetailResponse GetById([FromBody] int UUID)
        {
            UsersWithDetailResponse result = _userManager.GetById(UUID);
            return result;
        }

        [Route("Add")]
        [HttpPost]
        [SwaggerOperation(Summary = "User ekleme işlemi", Description = "User ekleme işlemi için kullanılır")]
        public BaseResponse Add([FromBody] UserPostRequest request)
        {
            BaseResponse result = _userManager.Add(request);
            return result;
        }

        [Route("Update")]
        [HttpPatch]
        [SwaggerOperation(Summary = "User güncelleme işlemi", Description = "User güncelleme işlemi için kullanılır")]
        public BaseResponse Update([FromBody] UserPostRequest request)
        {
            BaseResponse result = _userManager.Update(request);
            return result;
        }

        [Route("Delete")]
        [HttpDelete]
        [SwaggerOperation(Summary = "User silme işlemi", Description = "User silme işlemi için kullanılır.")]
        public BaseResponse Delete([FromBody] int UUID)
        {
            BaseResponse result = _userManager.Delete(UUID);
            return result;
        }

    }
}
