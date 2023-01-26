using FluentMediator;
using System.Threading.Tasks;
using TickdMeterReading.Application.Exceptions;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.Commands;
using TickdMeterReading.Domain.Accounts.Events;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings;
using TickdMeterReading.Domain.MeterReadings.Commands;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.Application.Handlers
{
    public class MeterReadingCommandHandler
    {
        private readonly IMeterReadingFactory _factory;
        private readonly IMeterReadingRepository _repository;
        private readonly IMediator _mediator;

        public MeterReadingCommandHandler(IMeterReadingRepository repository, IMeterReadingFactory factory, IMediator mediator)
        {
            _repository = repository;
            _factory = factory;
            _mediator = mediator;
        }

        public async Task<MeterReading> HandleNewMeterReading(AddMeterReadingCommand command)
        {
            var account = _factory.Create(
                    new AccountId(command.AccountId),
                    new MeterReadingDateTime(command.MeterReadingDateTime),
                    new MeterReadValue(command.MeterReadValue)
                );

            var result = await _repository.Add(account);

            return result;
        }
    }
}
