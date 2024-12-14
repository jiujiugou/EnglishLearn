using EventBus;
using MediaEncoder.Domain.Events;
using MediatR;

namespace MediaEncoder.WebAPI.EventHandler
{
    class EncodingItemCreatedEventHandler : INotificationHandler<EncodingItemCreatedEvent>
    {
        private readonly IEventBus eventBus;

        public EncodingItemCreatedEventHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task Handle(EncodingItemCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
