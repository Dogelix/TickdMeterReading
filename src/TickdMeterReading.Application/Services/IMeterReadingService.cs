using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickdMeterReading.Application.ViewModels;

namespace TickdMeterReading.Application.Services
{
    public interface IMeterReadingService
    {
        Task<MeterReadingViewModel> Create(MeterReadingViewModel taskViewModel);
    }
}
