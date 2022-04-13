using GroupChat.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Interfaces
{
    public interface IGroupsService
    {
        Task<ResponseObject> CreateNewGroup(string groupName, long loggedMemberId);

        Task<ResponseObject> ChangeGroupName(string groupName, long groupId, long loggedMemberId);

        Task<ResponseObject> ChangeGroupStatus(long groupId, bool status, long loggedMemberId);

        Task<ResponseObject> ChangeGroupImage(long groupId, IFormFile groupImage);

        Task<List<GroupsModel>> GetGroupsInfo();

        Task<ResponseObject> AddGroupMember(long groupId, long memberId);

        Task<List<GroupMembersModel>> GetGroupMembers(long groupId);

        Task<ResponseObject> SaveGroupMessage(MessageModel objMessageModel, long loggedMemberId);

        Task<List<GroupMessagesModel>> GetGroupMessages(long groupId, int pageSize, int pageNumber);
    }
}
