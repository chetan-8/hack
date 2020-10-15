using AutoMapper;
using Demo1.Core.Business;
using Demo1.Core.EntityModel;
using Demo1.Core.Model;
using Demo1.Core.Service;
using System;
using System.Threading.Tasks;

namespace Demo1.Business
{
    public class DemoBusiness : IDemoBusiness
    {
        private readonly IDemoService _demoService;
        
        public DemoBusiness(IDemoService demoService)
        {
            _demoService = demoService;
            
        }

        public async Task<CustomerModel> GetCustomerById(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(nameof(id));
            }

            var result = await _demoService.GetCustomerById(id);

            return result==null ? new CustomerModel() :
                new CustomerModel { CustomerId = result.CustomerId, CustomerName = result.CustomerName };
            
        }
        public async Task<int> DeleteCustomer(CustomerModel model)
        {
            if (model?.CustomerId == null)
            {
                throw new InvalidOperationException("Customer Id");
            }
            var customerEntity =
                 new Customer { CustomerId = model.CustomerId, CustomerName = model.CustomerName };
            return await _demoService.DeleteCustomer(customerEntity);

        }

        public async Task<int> CreateCustomer(CustomerModel model)
        {
            if (model?.CustomerName == null)
            {
                throw new InvalidOperationException("Customer Name");
            }
            var customerEntity =
               new Customer { CustomerId = model.CustomerId, CustomerName = model.CustomerName };
            return await _demoService.CreateCustomer(customerEntity);


        }

        public async Task<int> UpdateCustomer(CustomerModel model)
        {
            if (model?.CustomerId == null)
            {
                throw new InvalidOperationException("Customer Id");
            }
            var customerEntity =
              new Customer { CustomerId = model.CustomerId, CustomerName = model.CustomerName };
            return await _demoService.UpdateCustomer(customerEntity);


        }

    }
}
