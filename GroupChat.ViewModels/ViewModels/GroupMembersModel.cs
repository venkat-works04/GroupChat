using System;
using System.Collections.Generic;
using System.Text;

namespace GroupChat.ViewModels.ViewModels
{
    public class GroupMembersModel
    {
        public long MemberId { set; get; }

        public string FullName { set; get; }

        public string Gender { set; get; }

        public string PhoneNumber { set; get; }

        public DateTime JoinedDateTime { set; get; }

        public bool IsActive { set; get; }
    }
}
