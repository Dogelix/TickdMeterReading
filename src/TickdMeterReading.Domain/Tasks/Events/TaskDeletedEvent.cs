using System;
using System.Collections.Generic;
using System.Text;

namespace TickdMeterReading.Domain.Tasks.Events
{
    public class TaskDeletedEvent : TaskEvent
    {
        public TaskDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
