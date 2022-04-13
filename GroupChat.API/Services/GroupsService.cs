using GroupChat.API.DataModels;
using GroupChat.API.Interfaces;
using GroupChat.ViewModels.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ILogger<GroupsService> _logger;
        private readonly GroupChatDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public GroupsService(GroupChatDbContext dbContext, ILogger<GroupsService> logger, IHostingEnvironment hostingEnvironment)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._hostingEnvironment = hostingEnvironment;
        }

        public async Task<ResponseObject> CreateNewGroup(string groupName, long loggedMemberId)
        {
            ResponseObject objResponse = new ResponseObject();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var checkGroupName = _dbContext.Groups.Where(g => g.GroupName == groupName).FirstOrDefault();

                    if(checkGroupName != null)
                    {
                        objResponse.StatusCode = 500;
                        objResponse.Message = "Group Name is already exists.";
                        return objResponse;
                    }

                    Groups objGroup = new Groups();
                    objGroup.GroupName = groupName.Trim();
                    objGroup.IsActive = true;
                    objGroup.CreatedBy = loggedMemberId;
                    objGroup.CreatedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                    _dbContext.Groups.Add(objGroup);

                    await _dbContext.SaveChangesAsync();

                    GroupMembers objGroupMember = new GroupMembers();
                    objGroupMember.GroupId = objGroup.GroupId;
                    objGroupMember.MemberId = loggedMemberId;
                    objGroupMember.IsActive = true;
                    objGroupMember.JoinedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                    _dbContext.GroupMembers.Add(objGroupMember);

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                    objResponse.StatusCode = 200;
                    objResponse.Message = "Group Name is created successfully.";
                    return objResponse;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.ToString());
                    objResponse.StatusCode = 500;
                    objResponse.Message = "Something went wront. Please try after sometime.";
                    return objResponse;
                }
            }
        }

        public async Task<ResponseObject> ChangeGroupName(string groupName, long groupId, long loggedMemberId)
        {
            ResponseObject objResponse = new ResponseObject();
            try
            {
                var checkGroupName = _dbContext.Groups.Where(g => g.GroupName == groupName && g.GroupId != groupId).FirstOrDefault();

                if (checkGroupName != null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "Group Name is already exists.";
                    return objResponse;
                }

                var existingGroup = _dbContext.Groups.Where(g => g.GroupId == groupId).FirstOrDefault();

                if(existingGroup == null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "Group Details doesn't exists.";
                    return objResponse;
                }

                existingGroup.GroupName = groupName.Trim();
                existingGroup.ModifiedBy = loggedMemberId;
                existingGroup.ModifiedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                await _dbContext.SaveChangesAsync();

                objResponse.StatusCode = 200;
                objResponse.Message = "Group Name updated Successfully.";
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                objResponse.StatusCode = 500;
                objResponse.Message = "Something went wront. Please try after sometime.";
                return objResponse;
            }
        }

        public async Task<ResponseObject> ChangeGroupStatus(long groupId, bool status, long loggedMemberId)
        {
            ResponseObject objResponse = new ResponseObject();
            try
            {
                var existingGroup = _dbContext.Groups.Where(g => g.GroupId == groupId).FirstOrDefault();

                if (existingGroup == null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "Group Details doesn't exists.";
                    return objResponse;
                }

                existingGroup.IsActive = status;
                existingGroup.ModifiedBy = loggedMemberId;
                existingGroup.ModifiedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                await _dbContext.SaveChangesAsync();

                objResponse.StatusCode = 200;
                objResponse.Message = "Group Status changed successfully.";
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                objResponse.StatusCode = 500;
                objResponse.Message = "Something went wront. Please try after sometime.";
                return objResponse;
            }
        }

        public async Task<ResponseObject> ChangeGroupImage(long groupId, IFormFile groupImage)
        {
            ResponseObject objResponse = new ResponseObject();
            try
            {
                if (groupImage.Length == 0)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "File Content is empty.";
                    return objResponse;
                }

                string folderName = "GroupImages";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string fileFolder = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }
                
                string fileExtension = Path.GetExtension(groupImage.FileName).ToLower();
                string newFileName = Guid.NewGuid().ToString() + fileExtension;
                string fullPath = Path.Combine(fileFolder, newFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await groupImage.CopyToAsync(stream);
                }

                var existingGroup = _dbContext.Groups.Where(g => g.GroupId == groupId).FirstOrDefault();

                existingGroup.GroupImageFileName = folderName + "/" + newFileName;

                await _dbContext.SaveChangesAsync();

                objResponse.StatusCode = 200;
                objResponse.Message = "Group Image changed successfully.";
                return objResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                objResponse.StatusCode = 500;
                objResponse.Message = "Something went wront. Please try after sometime.";
                return objResponse;
            }
        }

        public async Task<List<GroupsModel>> GetGroupsInfo()
        {
            try
            {
                var groupsInfo =  (from groups in _dbContext.Groups
                                  join members in _dbContext.Members on groups.CreatedBy equals members.MemberId
                                  select new GroupsModel
                                  {
                                      GroupId = groups.GroupId,
                                      GroupName = groups.GroupName,
                                      GroupImageFileName = groups.GroupImageFileName,
                                      CreatedMemberId = members.MemberId,
                                      CreatedMemberName = members.FullName,
                                      CreatedDateTime = groups.CreatedDateTime,
                                      IsActive = groups.IsActive
                                  }).ToList();
                return groupsInfo;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<ResponseObject> AddGroupMember(long groupId, long memberId)
        {
            ResponseObject objResponse = new ResponseObject();
            try
            {
                var checkGroup = _dbContext.Groups.Where(g => g.GroupId == groupId && g.IsActive == true).FirstOrDefault();

                if (checkGroup == null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "Group doesn't exists.";
                    return objResponse;
                }

                var checkGroupMember = _dbContext.GroupMembers.Where(g => g.GroupId == groupId && g.MemberId == memberId).FirstOrDefault();

                if (checkGroupMember != null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "This member already exists in this group.";
                    return objResponse;
                }

                GroupMembers objGroupMember = new GroupMembers();
                objGroupMember.GroupId = groupId;
                objGroupMember.MemberId = memberId;
                objGroupMember.IsActive = true;
                objGroupMember.JoinedDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                _dbContext.GroupMembers.Add(objGroupMember);

                await _dbContext.SaveChangesAsync();

                objResponse.StatusCode = 200;
                objResponse.Message = "Group Member added successfully.";
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                objResponse.StatusCode = 500;
                objResponse.Message = "Something went wront. Please try after sometime.";
                return objResponse;
            }
        }

        public async Task<List<GroupMembersModel>> GetGroupMembers(long groupId)
        {
            try
            {
                var groupMembers = (from grpMembers in _dbContext.GroupMembers
                                    join members in _dbContext.Members on grpMembers.MemberId equals members.MemberId
                                    where grpMembers.GroupId == groupId
                                    select new GroupMembersModel
                                    {
                                        MemberId = members.MemberId,
                                        FullName = members.FullName,
                                        Gender = ((Gender)members.Gender).ToString(),
                                        PhoneNumber = members.PhoneNumber,
                                        JoinedDateTime = grpMembers.JoinedDateTime,
                                        IsActive = grpMembers.IsActive
                                    }).ToList();
                return groupMembers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<ResponseObject> SaveGroupMessage(MessageModel objMessageModel, long loggedMemberId)
        {
            ResponseObject objResponse = new ResponseObject();
            try
            {
                var checkGroup = _dbContext.Groups.Where(g => g.GroupId == objMessageModel.GroupId && g.IsActive == true).FirstOrDefault();

                if (checkGroup == null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "Group doesn't exists.";
                    return objResponse;
                }

                var checkGroupMember = _dbContext.GroupMembers.Where(g => g.GroupId == objMessageModel.GroupId && g.MemberId == loggedMemberId).FirstOrDefault();

                if (checkGroupMember == null)
                {
                    objResponse.StatusCode = 500;
                    objResponse.Message = "You are not member of this group.";
                    return objResponse;
                }

                GroupMessages objMessage = new GroupMessages();
                objMessage.GroupId = objMessageModel.GroupId;
                objMessage.MessageSentBy = loggedMemberId;
                objMessage.Message = objMessageModel.Message;
                objMessage.MessageSentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);

                _dbContext.GroupMessages.Add(objMessage);

                await _dbContext.SaveChangesAsync();

                objResponse.StatusCode = 200;
                objResponse.Message = "Message Sent Successfully.";
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                objResponse.StatusCode = 500;
                objResponse.Message = "Something went wront. Please try after sometime.";
                return objResponse;
            }
        }

        public async Task<List<GroupMessagesModel>> GetGroupMessages(long groupId, int pageSize, int pageNumber)
        {
            try
            {
                var groupMessages = (from messages in _dbContext.GroupMessages
                                    join members in _dbContext.Members on messages.MessageSentBy equals members.MemberId
                                    where messages.GroupId == groupId orderby messages.MessageSentDateTime descending
                                    select new GroupMessagesModel
                                    {
                                        MemberId = members.MemberId,
                                        FullName = members.FullName,
                                        Gender = ((Gender)members.Gender).ToString(),
                                        PhoneNumber = members.PhoneNumber,
                                        MessageSentDateTime = messages.MessageSentDateTime,
                                        Message = messages.Message
                                    }).Skip(pageSize * (pageNumber-1)).Take(pageSize).ToList();
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