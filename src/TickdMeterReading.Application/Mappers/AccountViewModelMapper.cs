using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.Commands;

namespace TickdMeterReading.Application.Mappers
{
    public class AccountViewModelMapper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountViewModelMapper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IEnumerable<AccountViewModel> ContstructFromEntityList(IEnumerable<Account> entityList)
        {
            var viewModels = entityList.Select(x => new AccountViewModel(
                    x.AccountId,
                    x.FirstName,
                    x.LastName
                ));

            return viewModels;
        }

        public AccountViewModel ContstructFromEntity(Account entity)
        {
            return new AccountViewModel(entity.AccountId, entity.FirstName, entity.LastName);
        }

        public CreateNewAccountCommand NewAccountCommand(AccountViewModel viewModel)
        {
            int newId;

            if(!int.TryParse(viewModel.AccountId, out newId)) throw new ArgumentException("Account ID is not an integer.", nameof(viewModel.AccountId));

            return new CreateNewAccountCommand(newId, viewModel.FirstName, viewModel.LastName);
        }

        public RemoveAccountCommand RemoveAccountCommand(int id) 
        { 
            return new RemoveAccountCommand(id);
        }
    }
}
