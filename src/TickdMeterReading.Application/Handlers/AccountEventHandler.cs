using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.Events;

namespace TickdMeterReading.Application.Handlers
{
    public class AccountEventHandler
    {
        public async Task HandleTaskCreatedEvent(AccountCreatedEvent accountCreatedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }

        public async Task HandleTaskDeletedEvent(AccountDeletedEvent accountDeletedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }
    }
}
