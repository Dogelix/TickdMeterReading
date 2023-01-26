using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.Commands;
using TickdMeterReading.Domain.MeterReadings;
using TickdMeterReading.Domain.MeterReadings.Commands;

namespace TickdMeterReading.Application.Mappers
{
    public class MeterReadingViewModelMapper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public MeterReadingViewModelMapper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IEnumerable<MeterReadingViewModel> ContstructFromEntityList(IEnumerable<MeterReading> entityList)
        {
            var viewModels = entityList.Select(x => new MeterReadingViewModel(
                    x.Id,
                    x.AccountId,
                    x.MeterReadingDateTime,
                    x.MeterReadValue
                ));

            return viewModels;
        }

        public MeterReadingViewModel ContstructFromEntity(MeterReading entity)
        {
            return new MeterReadingViewModel(entity.Id, entity.AccountId, entity.MeterReadingDateTime, entity.MeterReadValue);
        }

        public AddMeterReadingCommand NewMeterReadingCommand(MeterReadingViewModel viewModel)
        {
            return new AddMeterReadingCommand(viewModel.Id, viewModel.AccountId, viewModel.MeterReadingDateTime, viewModel.MeterReadValue);
        }
    }
}
