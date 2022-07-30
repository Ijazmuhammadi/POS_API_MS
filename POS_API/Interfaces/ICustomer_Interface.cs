using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Interfaces
{
    public interface ICustomer_Interface
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int CustomerId);
        Task<Customer> GetCustomerByEmail(string email);
        Task<Customer> AddCustomer(Customer Customer);
        Task<Customer> UpdateCustomer(Customer Customer);
        Task DeleteCustomer(int CustomerId);
    }
}
