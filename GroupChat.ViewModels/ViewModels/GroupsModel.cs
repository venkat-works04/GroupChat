using System;
using System.Collections.Generic;
using System.Text;

namespace GroupChat.ViewModels.ViewModels
{
    public class GroupsModel
    {
        public long GroupId { set; get; }

        public string GroupName { set; get; }

        public string GroupImageFileName { set; get; }

        public long CreatedMemberId { set; get; }

        public string CreatedMemberName { set; get; }

        public DateTime CreatedDateTime { set; get; }

        public bool IsActive { set; get; }
    }
}
