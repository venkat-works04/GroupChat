using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API.DataModels
{
    public class GroupChatDbContext : DbContext
    {
        public GroupChatDbContext(DbContextOptions<GroupChatDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Members> Members { set; get; }

        public virtual DbSet<UserLogins> UserLogins { set; get; }

        public virtual DbSet<Groups> Groups { set; get; }

        public virtual DbSet<GroupMembers> GroupMembers { set; get; }

        public virtual DbSet<GroupMessages> GroupMessages { set; get; }
    }
}
