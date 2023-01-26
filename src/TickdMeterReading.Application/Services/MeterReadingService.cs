using TickdMeterReading.Application.Mappers;
using TickdMeterReading.Application.ViewModels;
using FluentMediator;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.MeterReadings;

namespace TickdMeterReading.Application.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IMeterReadingRepository _repository;
        private readonly IMeterReadingFactory _factory;
        private readonly MeterReadingViewModelMapper _mapper;
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public MeterReadingService(IMeterReadingRepository repository, MeterReadingViewModelMapper mapper, ITracer tracer, IMeterReadingFactory factory, IMediator mediator)
        {
            _repository = repository;
            _factory = factory;
            _tracer = tracer;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<MeterReadingViewModel> Create(MeterReadingViewModel viewModel)
        {
            using(var scope = _tracer.BuildSpan("Create_MeterReadingService").StartActive(true))
            {
                var newCommand = _mapper.NewMeterReadingCommand(viewModel);
                
                var result = await _mediator.SendAsync<MeterReading>(newCommand);

                return _mapper.ContstructFromEntity(result);
            }
        }
    }
}
