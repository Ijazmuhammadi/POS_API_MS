using Microsoft.EntityFrameworkCore;
using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API
{
    public class Customer_Repo : ICustomer_Interface
    {
        private readonly AppDbContext appDb;

        public Customer_Repo(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
           
            var result = await appDb.customers.AddAsync(customer);
            await appDb.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCustomer(int customerId)
        {
            var result = await appDb.customers
            .FirstOrDefaultAsync(e => e.CustomerId == customerId);

            if (result != null)
            {
                appDb.customers.Remove(result);
                await appDb.SaveChangesAsync();
            }
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            return await appDb.customers
               .FirstOrDefaultAsync(e => e.CustomerId == customerId);
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await appDb.customers
               .FirstOrDefaultAsync(e => e.Email == email);

        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await appDb.customers.ToListAsync();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var result = await appDb.customers
                  .FirstOrDefaultAsync(e => e.CustomerId == customer.CustomerId);

            if (result != null)
            {
                result.FullName = customer.FullName;
                result.Email = customer.Email;
                result.ContactNumber = customer.ContactNumber;
                result.Address = customer.Address;
           
                await appDb.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
