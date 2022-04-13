using GroupChat.API.DataModels;
using GroupChat.API.Interfaces;
using GroupChat.ViewModels.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly ILogger<MemberService> _logger;
        private readonly GroupChatDbContext _dbContext;

        public MemberService(GroupChatDbContext dbContext, ILogger<MemberService> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public bool ValidateMemberPhoneNumber(string phoneNumber)
        {
            bool isMemberPhoneNumberExists = false;
            try
            {
                var member = _dbContext.Members.Where(u => u.PhoneNumber == phoneNumber.Trim()).FirstOrDefault();

                if (member != null)
                {
                    isMemberPhoneNumberExists = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return isMemberPhoneNumberExists;
        }

        public async Task<bool> ValidateMemberEmail(string email)
        {
            bool isMemberEmailExists = false;
            try
            {
                var member = _dbContext.Members.Where(u => u.EmailId == email.Trim()).FirstOrDefault();

                if (member != null)
                {
                    isMemberEmailExists = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return isMemberEmailExists;
        }

        public async Task<bool> ValidateMemberUsername(string username)
        {
            bool isMemberUsernameExists = false;
            try
            {
                var user = _dbContext.UserLogins.Where(u => u.UserName == username.Trim()).FirstOrDefault();

                if (user != null)
                {
                    isMemberUsernameExists = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return isMemberUsernameExists;
        }

        public async Task<bool> MemberRegistration(MemberCreateModel objMemberModel)
        {
            bool isRegistrationSuccess = false;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Members objMember = new Members();
                    objMember.FullName = objMemberModel.FullName.Trim();
                    objMember.DateOfBirth = objMemberModel.DateOfBirth;
                    objMember.Gender = Convert.ToInt16(objMemberModel.Gender);
                    objMember.PhoneNumber = objMemberModel.PhoneNumber.Trim();
                    objMember.EmailId = objMemberModel.EmailId.Trim();
                    objMember.IsActive = true;
                    objMember.CreatedBy = 0;
                    objMember.CreatedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                    _dbContext.Members.Add(objMember);

                    await _dbContext.SaveChangesAsync();

                    UserLogins objLogin = new UserLogins();
                    objLogin.UserName = objMemberModel.Username;
                    objLogin.Password = objMemberModel.Password;
                    objLogin.IsActive = true;
                    objLogin.MemberId = objMember.MemberId;

                    _dbContext.UserLogins.Add(objLogin);

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                    isRegistrationSuccess = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.ToString());
                }
            }
            return isRegistrationSuccess;
        }

        public async Task<bool> UpdateMemberInformation(long memberId, MemberUpdateModel objMemberModel, long loggedUserId)
        {
            bool isUpdateSuccess = false;

            try
            {
                var existingMember = _dbContext.Members.Where(m => m.MemberId == memberId).FirstOrDefault();

                if(existingMember == null)
                {
                    return isUpdateSuccess;
                }
                
                existingMember.FullName = objMemberModel.FullName.Trim();
                existingMember.DateOfBirth = objMemberModel.DateOfBirth;
                existingMember.Gender = Convert.ToInt16(objMemberModel.Gender);
                existingMember.PhoneNumber = objMemberModel.PhoneNumber.Trim();
                existingMember.EmailId = objMemberModel.EmailId.Trim();
                existingMember.ModifiedBy = loggedUserId;
                existingMember.ModifiedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                await _dbContext.SaveChangesAsync();

                isUpdateSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            
            return isUpdateSuccess;
        }

        public async Task<MemberInfoResponseModel> GetMemberInformation(long memberId)
        {
            MemberInfoResponseModel objMemberInfo = new MemberInfoResponseModel();

            try
            {
                var existingMember = _dbContext.Members.Where(m => m.MemberId == memberId).FirstOrDefault();

                if (existingMember == null)
                {
                    return null;
                }

                objMemberInfo.MemberId = existingMember.MemberId;
                objMemberInfo.FullName = existingMember.FullName.Trim();
                objMemberInfo.DateOfBirth = existingMember.DateOfBirth;
                objMemberInfo.Gender = ((Gender)existingMember.Gender).ToString();
                objMemberInfo.PhoneNumber = existingMember.PhoneNumber.Trim();
                objMemberInfo.EmailId = existingMember.EmailId.Trim();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return objMemberInfo;
        }

        public async Task<bool> ChangeMemberStatus(long memberId, bool status, long loggedUserId)
        {
            bool isChangeStatusSuccess = false;

            try
            {
                var existingMember = _dbContext.Members.Where(m => m.MemberId == memberId).FirstOrDefault();

                var existingUser = _dbContext.UserLogins.Where(u => u.MemberId == memberId).FirstOrDefault();

                if (existingMember == null)
                {
                    return isChangeStatusSuccess;
                }

                existingMember.IsActive = status;
                existingMember.ModifiedBy = loggedUserId;
                existingMember.ModifiedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                existingUser.IsActive = status;

                await _dbContext.SaveChangesAsync();

                isChangeStatusSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return isChangeStatusSuccess;
        }

        public async Task<List<MemberInfoResponseModel>> GetMembers(int pageSize, int pageNumber)
        {
            try
            {
                var groupMessages = (from members in _dbContext.Members 
                                     select new MemberInfoResponseModel
                                     {
                                         MemberId = members.MemberId,
                                         FullName = members.FullName,
                                         Gender = ((Gender)members.Gender).ToString(),
                                         PhoneNumber = members.PhoneNumber,
                                         EmailId = members.EmailId,
                                     }).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return groupMessages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}