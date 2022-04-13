using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroupChat.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupChat.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace GroupChat.API.Controllers
{
    [EnableCors("AllowOrigin")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("api/ValidateMemberPhoneNumber/{phoneNumber}")]
        public async Task<ActionResult<bool>> ValidateMemberPhoneNumber(string phoneNumber)
        {
            var response = _memberService.ValidateMemberPhoneNumber(phoneNumber.Trim());
            return response;
        }

        [HttpGet("api/ValidateMemberEmail/{email}")]
        public async Task<ActionResult<bool>> ValidateMemberEmail(string email)
        {
            var response = await _memberService.ValidateMemberEmail(email.Trim());
            return response;
        }

        [HttpGet("api/ValidateMemberUsername/{username}")]
        public async Task<ActionResult<bool>> ValidateMemberUsername(string username)
        {
            var response = await _memberService.ValidateMemberUsername(username.Trim());
            return response;
        }

        [HttpPost("api/MemberRegistration")]
        public async Task<ActionResult<bool>> MemberRegistration([FromBody] MemberCreateModel objMemberModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _memberService.MemberRegistration(objMemberModel);
            return response;
        }

        [Authorize]
        [HttpPut("api/UpdateMemberInformation/{memberId}")]
        public async Task<ActionResult<bool>> UpdateMemberInformation(long memberId, [FromBody] MemberUpdateModel objMemberModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            long userId = 0;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "UserId"))
            {
                userId = Convert.ToUInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            }

            var response = await _memberService.UpdateMemberInformation(memberId, objMemberModel, userId);
            return response;
        }

        [Authorize]
        [HttpGet("api/GetMemberInformation/{memberId}")]
        public async Task<ActionResult<MemberInfoResponseModel>> GetMemberInformation(long memberId)
        {
            var response = await _memberService.GetMemberInformation(memberId);
            return response;
        }

        [Authorize]
        [HttpGet("api/ChangeMemberStatus/{memberId}/{status}")]
        public async Task<ActionResult<bool>> ChangeMemberStatus(long memberId, bool status)
        {
            long userId = 0;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "UserId"))
            {
                userId = Convert.ToUInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            }

            var response = await _memberService.ChangeMemberStatus(memberId, status, userId);
            return response;
        }

        [Authorize]
        [HttpGet("api/GetMembers/{pageSize}/{pageNumber}")]
        public async Task<List<MemberInfoResponseModel>> GetMembers(int pageSize, int pageNumber)
        {
            var response = await _memberService.GetMembers(pageSize, pageNumber);
            return response;
        }
    }
}