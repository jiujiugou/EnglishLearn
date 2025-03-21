﻿using EventBus;
using Identity.Repository;
using IdentityService.Domain;

namespace IdentityService.WebAPI.Event
{
    [EventName("IdentityService.User.Created")]
    public class UserCreatedEventHandler : JsonIntegrationEventHandler<UserCreatedEvent>
    {
        private readonly ISmsSender smsSender;

        public UserCreatedEventHandler(ISmsSender smsSender)
        {
            this.smsSender = smsSender;
        }

        public override Task HandleJson(string eventName, UserCreatedEvent? eventData)
        {
            //发送初始密码给被创建用户的手机
            return smsSender.SendAsync(eventData.PhoneNum, eventData.Password);
        }
    }
}
