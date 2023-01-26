using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.Application.ViewModels
{
    public class MeterReadingViewModel
    {
        public MeterReadingViewModel(MeterReadingId id, AccountId accountId, MeterReadingDateTime dateTime, MeterReadValue value) 
        { 
            Id = id.ToString();
            AccountId = accountId.Value;
            MeterReadingDateTime = dateTime.Value;
            MeterReadValue = value.Value;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public DateTime MeterReadingDateTime { get; set; }

        [Required]
        public string MeterReadValue { get; set; }
    }
}
