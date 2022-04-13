using GroupChat.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseModel> ValidateLogin(LoginModel LoginRequest);
    }
}
