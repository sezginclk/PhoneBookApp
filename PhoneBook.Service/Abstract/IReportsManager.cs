using PhoneBook.Data.Model.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Service.Abstract
{
    public interface IReportsManager
    {
        BaseResponse Add();
        BaseResponse Update();
    }
}
