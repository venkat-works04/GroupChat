using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.DataModels
{
    public class Members
    {
        [Key]
        public long MemberId { set; get; }

        public string FullName { set; get; }

        public DateTime DateOfBirth { set; get; }

        public short Gender { set; get; }

        public string PhoneNumber { set; get; }

        public string EmailId { set; get; }

        public bool? IsActive { set; get; }

        public long CreatedBy { set; get; }

        public DateTime CreatedDateTime { set; get; }

        public long? ModifiedBy { set; get; }

        public DateTime? ModifiedDateTime { set; get; } 
    }
}
