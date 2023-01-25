using ITnCare.CM.Entities;
using ITnCare.CM.Framework;
using Microsoft.EntityFrameworkCore;

namespace ITnCare.CM.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _dbContext;

        public CustomerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.FromSqlRaw("SELECT [Id], [TradingID], [Origin] FROM [Customers]").AsNoTracking().ToListAsync(cancellationToken); ;
        }
    }
}
