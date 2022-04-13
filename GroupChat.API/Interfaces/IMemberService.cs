using GroupChat.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Interfaces
{
    public interface IMemberService
    {
        bool ValidateMemberPhoneNumber(string phoneNumber);

        Task<bool> ValidateMemberEmail(string email);

        Task<bool> ValidateMemberUsername(string username);

        Task<bool> MemberRegistration(MemberCreateModel objMemberModel);

        Task<bool> UpdateMemberInformation(long memberId, MemberUpdateModel objMemberModel, long loggedUserId);

        Task<MemberInfoResponseModel> GetMemberInformation(long memberId);

        Task<bool> ChangeMemberStatus(long memberId, bool status, long loggedUserId);

        Task<List<MemberInfoResponseModel>> GetMembers(int pageSize, int pageNumber);
    }
}
