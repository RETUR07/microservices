using Microsoft.EntityFrameworkCore;
using AuthorizationAPI.Entities.Models;
using AuthorizationAPI.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        protected RepositoryContext RepositoryContext;
        private DbSet<User> RepContextSet;

        public UserRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            RepContextSet = RepositoryContext.Set<User>();
        }
    }
}
