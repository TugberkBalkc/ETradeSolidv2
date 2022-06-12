using ETrade.Core.DataAccess.Concrete.EntityFramework;
using ETrade.DataAccess.Abstract.Address;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.Address
{
    public class EfAddressQueryRepository : EfQueryRepositoryBase<Entities.Concrete.Address>, IAddressQueryRepository
    {
        private DbContext _context => this.GetContext;
        public EfAddressQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Entities.Concrete.Address> GetAddressesByUser(Core.Entities.Concrete.User user)
        {
            var result = from userAddress in _context.Set<Entities.Concrete.UserAddress>()
                         join address in _context.Set<Entities.Concrete.Address>()
                         on userAddress.AddressId equals address.Id
                         where userAddress.UserId == user.Id
                         select new Entities.Concrete.Address
                         {
                             Id = address.Id,
                             City = address.City,
                             District = address.District,
                             Street = address.Street,
                             PostalCode = address.PostalCode,
                             CreatedDate = address.CreatedDate,
                             UpdatedDate = address.UpdatedDate,
                             IsActive = address.IsActive,
                             IsDeleted = address.IsDeleted
                         };

            return result;
        }

        public IQueryable<Entities.Concrete.Address> GetAddressesByUserId(int userId)
        {
            var result = from userAddress in _context.Set<Entities.Concrete.UserAddress>()
                         join address in _context.Set<Entities.Concrete.Address>()
                         on userAddress.AddressId equals address.Id
                         where userAddress.UserId == userId
                         select new Entities.Concrete.Address
                         {
                             Id = address.Id,
                             City = address.City,
                             District = address.District,
                             Street = address.Street,
                             PostalCode = address.PostalCode,
                             CreatedDate = address.CreatedDate,
                             UpdatedDate = address.UpdatedDate,
                             IsActive = address.IsActive,
                             IsDeleted = address.IsDeleted
                         };

            return result;
        }
    }
}
