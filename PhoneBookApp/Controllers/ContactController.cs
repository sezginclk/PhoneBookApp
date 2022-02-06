using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Service.Abstract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApp.Controllers
{

    [Produces("application/json")]
    [Route("api/Contact")]
    public class ContactController : Controller
    {
        IContactManager _contactManager; 
        public ContactController(IContactManager contactManager )
        {
            _contactManager = contactManager; 
        } 

        [Route("Add")]
        [HttpPost]
        [SwaggerOperation(Summary = "İletişim bilgisi ekleme işlemi", Description = "İletişim bilgisi işlemi için kullanılır")]
        public BaseResponse Add([FromBody] ContactPostRequest request)
        {
            BaseResponse result = _contactManager.Add(request);
            return result;
        }

        [Route("Update")]
        [HttpPatch]
        [SwaggerOperation(Summary = "İletişim bilgisi güncelleme işlemi", Description = "İletişim bilgisi güncelleme işlemi için kullanılır")]
        public BaseResponse Update([FromBody] ContactPostRequest request)
        {
            BaseResponse result = _contactManager.Update(request);
            return result;
        }

        [Route("Delete")]
        [HttpDelete]
        [SwaggerOperation(Summary = "İletişim bilgisi silme işlemi", Description = "İletişim bilgisi silme işlemi için kullanılır.")]
        public BaseResponse Delete([FromBody] int UUID)
        {
            BaseResponse result = _contactManager.Delete(UUID);
            return result;
        }
    }
}
