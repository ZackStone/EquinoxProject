using Equinox.Domain.Events;
using Equinox.Domain.Interfaces.Buckets;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Equinox.Domain.EventHandlers
{
    public class CustomerEventHandler :
        INotificationHandler<CustomerRegisteredEvent>,
        INotificationHandler<CustomerUpdatedEvent>,
        INotificationHandler<CustomerRemovedEvent>
    {
        private readonly ICustomerBucket _customersBucket;

        public CustomerEventHandler(ICustomerBucket customersBucket)
        {
            _customersBucket = customersBucket;
        }

        public async Task Handle(CustomerUpdatedEvent message, CancellationToken cancellationToken)
        {
            await _customersBucket.DefaultCollection.UpsertAsync(message.Id.ToString(), message);
        }

        public async Task Handle(CustomerRegisteredEvent message, CancellationToken cancellationToken)
        {
            await _customersBucket.DefaultCollection.InsertAsync(message.Id.ToString(), message);
        }

        public async Task Handle(CustomerRemovedEvent message, CancellationToken cancellationToken)
        {
            await _customersBucket.DefaultCollection.RemoveAsync(message.Id.ToString());
        }
    }
}