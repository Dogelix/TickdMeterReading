using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickdMeterReading.Application.ViewModels;

namespace TickdMeterReading.Application.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAll();
        Task<AccountViewModel> GetById(int id);
        Task<AccountViewModel> Create(AccountViewModel taskViewModel);
        Task Delete(int id);
    }
}
