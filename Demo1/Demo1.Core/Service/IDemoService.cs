using Demo1.Core.EntityModel;
using Demo1.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Core.Service
{
   public interface IDemoService
    {
        Task<Customer> GetCustomerById(int id);
        Task<int> DeleteCustomer(Customer model);
        Task<int> CreateCustomer(Customer model);
        Task<int> UpdateCustomer(Customer model);
    }
}
