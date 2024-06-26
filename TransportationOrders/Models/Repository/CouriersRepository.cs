using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationOrders.Models.Entities;

namespace TransportationOrders.Models.Repository
{
    public class CouriersRepository
    {

        private readonly ApplicationDBContext _applicationDbContext;

        public CouriersRepository(ApplicationDBContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public async Task Add(Courier courier)
        {
            await _applicationDbContext.Couriers.AddAsync(courier);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Courier> Get(int id)
        {
            return await _applicationDbContext.Couriers.Where(courier => courier.Id == id).FirstOrDefaultAsync() ?? throw new Exception("there is courier with such Id");
        }

        public async Task<List<Courier>> GetRange(int startIndex, int count)
        {
            return await _applicationDbContext.Couriers.Take(count).OrderBy(p => p.Id).ToListAsync();
        }

        public void Update(Courier courier)
        {

            _applicationDbContext.Couriers.Update(courier);
            _applicationDbContext.SaveChanges();
        }

        public async Task Delete(int id)
        {
            _applicationDbContext.Couriers.Remove(await Get(id));
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
