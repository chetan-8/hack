using Demo1.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Core.Business
{
   public interface IDemoBusiness
    {
        Task<CustomerModel> GetCustomerById(int id);
        Task<int> DeleteCustomer(CustomerModel model);
        Task<int> CreateCustomer(CustomerModel model);
        Task<int> UpdateCustomer(CustomerModel model);
    }
}
