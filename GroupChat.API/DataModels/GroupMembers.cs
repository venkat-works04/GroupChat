using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.DataModels
{
    public class GroupMembers
    {
        [Key]
        public long GroupMemberId { set; get; }

        public long GroupId { get; set; }

        public long MemberId { set; get; }

        public DateTime JoinedDateTime { set; get; }

        public bool IsActive { set; get; }
    }
}