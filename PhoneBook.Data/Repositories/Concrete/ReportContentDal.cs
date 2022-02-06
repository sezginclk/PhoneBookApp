using PhoneBook.Data.Contexts;
using PhoneBook.Data.Model.DomainClass;
using PhoneBook.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Repositories.Concrete
{
    public class ReportContentDal : EFRepository<ReportContent>, IReportContentDal
    {
        public ReportContentDal(PBookContext pBookContext) : base(pBookContext)
        {
        }
    }
}
