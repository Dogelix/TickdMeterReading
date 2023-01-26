using FluentMediator;
using System.Threading.Tasks;
using TickdMeterReading.Application.Exceptions;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.Commands;
using TickdMeterReading.Domain.Accounts.Events;

namespace TickdMeterReading.Application.Handlers
{
    public class AccountCommandHandler
    {
        private readonly Domain.Accounts.IAccountFactory _accountFactory;
        private readonly IAccountsRepository _accountRepository;
        private readonly IMediator _mediator;

        public AccountCommandHandler(IAccountsRepository accountsRepository, Domain.Accounts.IAccountFactory accountFactory, IMediator mediator)
        {
            _accountRepository = accountsRepository;
            _accountFactory = accountFactory;
            _mediator = mediator;
        }

        public async Task<Account> HandleNewAccount(CreateNewAccountCommand createNewAccountCommand)
        {
            var account = _accountFactory.Create(
                    accountId: new Domain.Accounts.ValueObjects.AccountId(createNewAccountCommand.AccountId),
                    firstName: new Domain.Accounts.ValueObjects.FirstName(createNewAccountCommand.FirstName),
                    lastName: new Domain.Accounts.ValueObjects.LastName(createNewAccountCommand.LastName)
                );

            var accountCreated = await _accountRepository.Add(account);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(new AccountCreatedEvent(accountCreated.AccountId.Value, accountCreated.FirstName.Value, accountCreated.LastName.Value));

            return accountCreated;
        }

        public async Task HandleDeleteAccount(RemoveAccountCommand removeAccountCommand)
        {
            var account = await _accountRepository.FindById(removeAccountCommand.AccountId);

            if(account == null)
            {
                throw new DataNotFoundRepositoryException($"Account with id {removeAccountCommand.AccountId} does not exist");
            }

            await _accountRepository.Remove(removeAccountCommand.AccountId);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(new AccountDeletedEvent(account.AccountId.Value, account.FirstName.Value, account.LastName.Value));
        }
    }
}
