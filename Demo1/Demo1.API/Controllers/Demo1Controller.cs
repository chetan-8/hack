using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo1.Core.Business;
using Demo1.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] //This will handle authentication for API
    public class Demo1Controller : ControllerBase
    {

        private readonly IDemoBusiness _demoBusiness;
        public Demo1Controller(IDemoBusiness demoBusiness)
        {
            _demoBusiness = demoBusiness;
        }

        /// <summary>
        /// Gets Customer.
        /// </summary>
        /// <returns>Get a Customer</returns>
        [HttpGet]
        [Route("getcustomerbyid")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
           
                return Ok(await _demoBusiness.GetCustomerById(Id));
          
        }

        /// <summary>
        /// Gets Customer.
        /// </summary>
        /// <returns>Get a Customer</returns>
        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(CustomerModel model)
        {

            return Ok(await _demoBusiness.DeleteCustomer(model));

        }

        /// <summary>
        /// Gets Customer.
        /// </summary>
        /// <returns>Get a Customer</returns>
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerModel model)
        {

            return Ok(await _demoBusiness.CreateCustomer(model));

        }

        /// <summary>
        /// Gets Customer.
        /// </summary>
        /// <returns>Get a Customer</returns>
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerModel model)
        {

            return Ok(await _demoBusiness.UpdateCustomer(model));

        }

    }
}
