﻿using EventBus;
using MediaEncoder.Domain.Events;
using MediatR;

namespace MediaEncoder.WebAPI.EventHandler
{
    class EncodingItemCompletedEventHandler : INotificationHandler<EncodingItemCompletedEvent>
    {
        private readonly IEventBus eventBus;

        public EncodingItemCompletedEventHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task Handle(EncodingItemCompletedEvent notification, CancellationToken cancellationToken)
        {
            //把转码任务状态变化的领域事件，转换为集成事件发出
            eventBus.Publish("MediaEncoding.Completed", notification);
            return Task.CompletedTask;
        }
    }
}
