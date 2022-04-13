using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.DataModels
{
    public class UserLogins
    {
        [Key]
        public long UserId { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        public string ForgotPassword { set; get; }

        public DateTime? LastLogin { set; get; }

        public bool IsActive { set; get; }

        public long MemberId { set; get; }
    }
}