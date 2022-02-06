using PhoneBook.Data.Model.DataTransferObjects.Request;
using PhoneBook.Data.Model.DataTransferObjects.Response;
using PhoneBook.Data.Model.DomainClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Service.Abstract
{
    public interface IReportsManager
    {
        List<Reports> GetAll();
        //List<UsersWithDetailResponse> GetAllWithDetail();
        ReportContentReponse GetById(int reportId);
        ReportResponse Add();
        BaseResponse Update(ReportRequest request);
        BaseResponse CreateReport(CreateReportRequest request);
    }
}
