﻿using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;

namespace Appointify.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {
        }
    }
}
