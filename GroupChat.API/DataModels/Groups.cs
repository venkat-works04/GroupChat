using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.DataModels
{
    public class Groups
    {
        [Key]
        public long GroupId { set; get; }

        public string GroupName { set; get; }

        public string GroupImageFileName {set; get;}

        public long CreatedBy { set; get; }

        public DateTime CreatedDateTime { set; get; }

        public long? ModifiedBy { set; get; }

        public DateTime? ModifiedDateTime { set; get; }

        public bool IsActive { set; get; }
    }
}
