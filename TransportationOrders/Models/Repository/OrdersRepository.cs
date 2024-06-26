using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TransportationOrders.Models.Entities;

namespace TransportationOrders.Models.Repository
{
    public class OrdersRepository
    {

        private readonly ApplicationDBContext _applicationDbContext;

        public OrdersRepository(ApplicationDBContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public async Task<Order> Add(Order order)
        {
            var res = await _applicationDbContext.Orders.AddAsync(order);
            
            await _applicationDbContext.SaveChangesAsync();
            
            return res.Entity;
        }

        public async Task<Order> Get(int id)
        {
            return await _applicationDbContext.Orders.Where(order => order.Id == id).FirstOrDefaultAsync() ?? throw new Exception("there is no order with such id");
        }

        public async Task<List<Order>> GetRange(int startIndex, int count)
        {
            return await _applicationDbContext.Orders.Take(count).OrderBy(p => p.Id).ToListAsync();
        }

        public async Task Update(Order order)
        {
            _applicationDbContext.Orders.Update(order);
            
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetRangeByPattern(string pattern, int maxCount)
        {
            return await _applicationDbContext.Orders.AsQueryable().WhereLike(pattern).Take(maxCount).ToListAsync();
        }
        
        public async Task Delete(int id)
        {
            _applicationDbContext.Orders.Remove(await Get(id));

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
