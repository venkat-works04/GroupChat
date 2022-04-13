using GroupChat.API.Interfaces;
using GroupChat.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Controllers
{
    [EnableCors("AllowOrigin")]
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;
        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [Authorize]
        [HttpGet("api/CreateNewGroup/{groupName}")]
        public async Task<ActionResult<ResponseObject>> CreateNewGroup(string groupName)
        {
            long memberId = 0;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "MemberId"))
            {
                memberId = Convert.ToUInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "MemberId").Value);
            }

            var response = await _groupsService.CreateNewGroup(groupName.Trim(), memberId);
            return response;
        }

        [Authorize]
        [HttpGet("api/ChangeGroupName/{groupName}/{groupId}")]
        public async Task<ActionResult<ResponseObject>> ChangeGroupName(string groupName, long groupId)
        {
            long memberId = 0;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "MemberId"))
            {
                memberId = Convert.ToUInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "MemberId").Value);
            }

            var response = await _groupsService.ChangeGroupName(groupName, groupId, memberId);
            return response;
        }

        [Authorize]
        [HttpGet("api/ChangeGroupStatus/{groupId}/{status}")]
        public async Task<ActionResult<ResponseObject>> ChangeGroupStatus(long groupId, bool status)
        {
            long memberId = 0;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "MemberId"))
            {
                memberId = Convert.ToUInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "MemberId").Value);
            }

            var response = await _groupsService.ChangeGroupStatus(groupId, status, memberId);
            return response;
        }

        [Authorize]
        [HttpPost("~/api/ChangeGroupImage/{groupId}")]
        public async Task<ActionResult<ResponseObject>> ChangeGroupImage(long groupId, [FromForm] IFormFile file)
        {
            var response = await _groupsService.ChangeGroupImage(groupId, file);
            return response;
        }

        [Authorize]
        [HttpGet("api/GetGroupsInfo")]
        public async Task<List<GroupsModel>> GetGroupsInfo()
        {
            var response = await _groupsService.GetGroupsInfo();
            return response;
        }

        [Authorize]
        [HttpGet("api/AddGroupMember/{groupId}/{memberId}")]
        public async Task<ActionResult<ResponseObject>> AddGroupMember(long groupId, long memberId)
        {
            var response = await _groupsService.AddGroupMember(groupId, memberId);
            return response;
        }

        [Authorize]
        [HttpGet("api/GetGroupMembers/{groupId}")]
        public async Task<List<GroupMembersModel>> GetGroupMembers(long groupId)
        {
            var response = await _groupsService.GetGroupMembers(groupId);
            return response;
        }

        [Authorize]
        [HttpPost("api/SaveGroupMessage")]
        public async Task<ActionResult<ResponseObject>> SaveGroupMessage([FromBody] MessageModel objMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            long memberId = 0;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "MemberId"))
            {
                memberId = Convert.ToUInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "MemberId").Value);
            }

            var response = await _groupsService.SaveGroupMessage(objMessageModel, memberId);
            return response;
        }

        [Authorize]
        [HttpGet("api/GetGroupMessages/{groupId}/{pageSize}/{pageNumber}")]
        public async Task<List<GroupMessagesModel>> GetGroupMessages(long groupId, int pageSize, int pageNumber)
        {
            var response = await _groupsService.GetGroupMessages(groupId, pageSize, pageNumber);
            return response;
        }
    }
}