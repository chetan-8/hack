using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo1.Api.Tests
{
    public class ServiceTypeTests : IClassFixture<BaseClass>
    {
        private readonly HttpClient _client;

        public ServiceTypeTests(BaseClass baseclass)
        {
            _client = baseclass.Client;
        }

        [Fact]
        public async Task TestCreateCustomer()
        {

            // Arrange
            var request = new
            {
                Url = "/api/Demo1/CreateCustomer",
                Body = new
                {
                    CustomerName = "Chetan"
                }
            };


            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            Assert.Contains("1", content);
        }


        [Fact]
        public async Task TestGetCustomer()
        {

            // The endpoint or route of the controller action.         
            // Arrange
            var url = "/api/Demo1/getcustomerbyid?id=1";
            // Act
            var httpResponse = await _client.GetAsync(url);
            // Assert
            httpResponse.EnsureSuccessStatusCode();
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Demo1.Core.Model.CustomerModel customer = 
             JsonConvert.DeserializeObject<Demo1.Core.Model.CustomerModel>(stringResponse);          
          
            Assert.Contains("Chetan", customer?.CustomerName?.ToString());
        }

      

        [Fact]
        public async Task TestUpdateCustomer()
        {

            // Arrange
            var request = new
            {
                Url = "/api/Demo1/UpdateCustomer",
                Body = new
                {
                    CustomerId = 2,
                    CustomerName = "Chetan_1"
                }
            };

            // Act
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            Assert.Contains("1", content);
        }

        
    }
}
