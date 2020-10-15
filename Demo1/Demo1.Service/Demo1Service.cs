using Demo1.Core.EntityModel;
using Demo1.Core.Model;
using Demo1.Core.Repository;
using Demo1.Core.Service;
using System;
using System.Threading.Tasks;

namespace Demo1.Service
{
    public class Demo1Service : IDemoService
    {
        private readonly IRepository<Customer> _customerRepository;
        public Demo1Service(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Demo1Service()
        {
           
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customerRepository.FindSingleNoTracking(x => x.CustomerId == id);
        }

        public async Task<int> DeleteCustomer(Customer entity)
        {
            if (entity?.CustomerId == null)
            {
                throw new InvalidOperationException("Customer Id");
            }

            return await _customerRepository.DeleteAsync(entity);

        }

        public async Task<int> CreateCustomer(Customer entity)
        {
            if (entity?.CustomerName == null)
            {
                throw new InvalidOperationException("Customer Name");
            }


            return await _customerRepository.InsertAsync(entity);



        }

        public async Task<int> UpdateCustomer(Customer entity)
        {
            if (entity?.CustomerId == null)
            {
                throw new InvalidOperationException("Customer Id");
            }


            return await _customerRepository.UpdateAsync(entity);

        }
    }
}
