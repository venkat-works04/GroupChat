using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GroupChat.ViewModels.ViewModels
{
    public class MemberCreateModel
    { 
        [Required]
        public string FullName { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { set; get; }

        [Required]
        public Gender Gender { set; get; }

        [Required]
        public string PhoneNumber { set; get; }

        public string EmailId { set; get; }

        [Required]
        public string Username { set; get; }

        [Required]
        public string Password { set; get; }
    }

    public class MemberUpdateModel
    {
        [Required]
        public string FullName { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { set; get; }

        [Required]
        public Gender Gender { set; get; }

        [Required]
        public string PhoneNumber { set; get; }

        public string EmailId { set; get; }
    }

    public class MemberInfoResponseModel
    {
        public long MemberId { set; get; }

        public string FullName { set; get; }

        public DateTime DateOfBirth { set; get; }

        public string Gender { set; get; }

        public string PhoneNumber { set; get; }

        public string EmailId { set; get; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}
