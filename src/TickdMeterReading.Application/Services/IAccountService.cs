﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickdMeterReading.Application.ViewModels;

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
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAll();
        Task<AccountViewModel> GetById(int id);
        Task<AccountViewModel> Create(AccountViewModel taskViewModel);
        Task Delete(int id);
    }
}