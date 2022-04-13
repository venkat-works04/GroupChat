using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GroupChat.ViewModels.ViewModels
{
    public class GroupMessagesModel
    {
        public long MemberId { set; get; }

        public string FullName { set; get; }

        public string Gender { set; get; }

        public string PhoneNumber { set; get; }

        public string Message { set; get; }

        public DateTime MessageSentDateTime { set; get; }
    }

    public class MessageModel
    {
        [Required]
        public long GroupId { set; get; }

        [Required]
        public string Message { set; get; }
    }
}
