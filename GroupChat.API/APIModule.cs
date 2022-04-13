using Autofac;
using GroupChat.API.Interfaces;
using GroupChat.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChat.API
{
    public class APIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemberService>().As<IMemberService>().InstancePerLifetimeScope();
            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupsService>().As<IGroupsService>().InstancePerLifetimeScope();
        }
    }
}