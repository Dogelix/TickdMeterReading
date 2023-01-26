using TickdMeterReading.Application.Mappers;
using TickdMeterReading.Application.ViewModels;
using FluentMediator;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts;

/*
 * Application service is that layer which initializes and oversees interaction 
 * between the domain objects and services. The flow is generally like this: 
 * get domain object (or objects) from repository, execute an action and put it 
 * (them) back there (or not). It can do more - for instance it can check whether 
 * a domain object exists or not and throw exceptions accordingly. So it lets the 
 * user interact with the application (and this is probably where its name originates 
 * from) - by manipulating domain objects and services. Application services should 
 * generally represent all possible use cases.
 */

namespace TickdMeterReading.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountsRepository _accountRepository;
        private readonly IAccountFactory _accountFactory;
        private readonly AccountViewModelMapper _accountViewModelMapper;
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public AccountService(IAccountsRepository accountRepository, AccountViewModelMapper accountViewModelMapper, ITracer tracer, IAccountFactory accountFactory, IMediator mediator)
        {
            _accountRepository = accountRepository;
            _accountViewModelMapper = accountViewModelMapper;
            _tracer = tracer;
            _accountFactory = accountFactory;
            _mediator = mediator;
        }

        public async Task<AccountViewModel> Create(AccountViewModel viewModel)
        {
            using(var scope = _tracer.BuildSpan("Create_AccountService").StartActive(true))
            {
                var newCommand = _accountViewModelMapper.NewAccountCommand(viewModel);
                
                var result = await _mediator.SendAsync<Account>(newCommand);

                return _accountViewModelMapper.ContstructFromEntity(result);
            }
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            using (var scope = _tracer.BuildSpan("Delete_AccountService").StartActive(true))
            {
                var deleteTaskCommand = _accountViewModelMapper.RemoveAccountCommand(id);
                await _mediator.PublishAsync(deleteTaskCommand);
            }
        }

        public async Task<IEnumerable<AccountViewModel>> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAll_AccountService").StartActive(true))
            {
                var accountEntities = await _accountRepository.FindAll();

                return _accountViewModelMapper.ContstructFromEntityList(accountEntities);
            }
        }

        public async Task<AccountViewModel> GetById(int id)
        {
            using (var scope = _tracer.BuildSpan("GetById_AccountService").StartActive(true))
            {
                var taskEntity = await _accountRepository.FindById(id);

                return _accountViewModelMapper.ContstructFromEntity(taskEntity);
            }
        }
    }
}
