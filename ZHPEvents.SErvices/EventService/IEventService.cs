using System;
using System.Collections.Generic;
using System.Text;
using ZHPEvents.ViewModels.Events;

namespace ZHPEvents.Services.EventService
{
    public interface IEventService
    {
        void AddAsync(AddEventViewModel model);
        void EditAsync(AddEventViewModel model);
        void DeleteAsync(AddEventViewModel model);
    }
}
