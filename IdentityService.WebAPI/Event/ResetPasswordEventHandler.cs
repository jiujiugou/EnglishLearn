﻿using EventBus;
using Identity.Repository;
using IdentityService.Domain;

namespace IdentityService.WebAPI.Event
{
    [EventName("IdentityService.User.PasswordReset")]
    public class ResetPasswordEventHandler : JsonIntegrationEventHandler<ResetPasswordEvent>
    {
        private readonly ILogger<ResetPasswordEventHandler> logger;
        private readonly ISmsSender smsSender;

        public ResetPasswordEventHandler(ILogger<ResetPasswordEventHandler> logger, ISmsSender smsSender)
        {
            this.logger = logger;
            this.smsSender = smsSender;
        }

        public override Task HandleJson(string eventName, ResetPasswordEvent? eventData)
        {
            //发送密码给被用户的手机
            return smsSender.SendAsync(eventData.PhoneNum, eventData.Password);
        }
    }
}
