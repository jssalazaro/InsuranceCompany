using InsuranceCompany.WebApi.DTOs.CustomerDTO;
using InsuranceCompnay.Application.Interfaces;
using InsuranceCompnay.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCompany.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly IApplication<Customer> _customer;

        public CustomerController(IApplication<Customer> customer)
        {
            _customer = customer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customer.GetAll());
        }

        [HttpPost]
        public IActionResult Save(CustomerDTO dto)
        {
            var c = new Customer()
            {
                Name = dto.Name,
                Address = dto.Address

            };
            return Ok(_customer.Save(c));
        }
    }
}
