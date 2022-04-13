using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.DataModels
{
    public class GroupMessages
    {
        [Key]
        public long GroupMessageId { set; get; }

        public long GroupId { set; get; }

        public long MessageSentBy { set; get; }

        public string Message { set; get; }

        public DateTime MessageSentDateTime { set; get; }
    }
}