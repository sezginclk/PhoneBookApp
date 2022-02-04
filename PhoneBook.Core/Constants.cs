using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Core
{
    public class Constants
    {



        public enum ERRORCODES
        {
            SUCCESS = 0,
            SYSTEMERROR = 1,
            USERNOTFOUND = 2,
            CONTACTNOTFOUND = 3
        }


        public static Dictionary<int, string> ErrorCodeData = new Dictionary<int, string>
        {
            {(int)ERRORCODES.SUCCESS,"İşlem Başarılı." },
            {(int)ERRORCODES.SYSTEMERROR,"Sistemde Hata Oluştu!" },
            {(int)ERRORCODES.USERNOTFOUND,"Kullanıcı bulunamadı!" },
            {(int)ERRORCODES.CONTACTNOTFOUND,"İletişim Bilgisi bulunamadı!" },
    };
    }
}
