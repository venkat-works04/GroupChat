using System;
using System.Collections.Generic;
using System.Text;

namespace GroupChat.ViewModels.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public int Status { set; get; }

        public string Message { set; get; }

        public string JWToken { set; get; }

        public LoginUserInformation UserInformation { set; get; }

        public LoginResponseModel()
        {
            Status = 0;
            Message = string.Empty;
            UserInformation = new LoginUserInformation();
            JWToken = string.Empty;
        }
    }

    public class LoginUserInformation
    {
        public long UserId { set; get; }

        public string FullName { set; get; }

        public long MemberId { set; get; }
    }
}
