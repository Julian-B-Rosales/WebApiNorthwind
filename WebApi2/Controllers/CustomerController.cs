using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace WebApi2.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly NorthwindContext _context;
        public CustomerController(NorthwindContext context)
        {
            _context = context;
        }

        //GET /api/customer
        [HttpGet]
        public IEnumerable<Customer> get()
        {
            List<Customer> customers = (from c in _context.Customers
                                        select c).ToList();

            return customers;
        }

        //GET /api/customer/customerId
        [HttpGet("{customerId}")]
        public Customer get(string customerId)
        {
            Customer customer = (from c in _context.Customers
                                 where c.CustomerId == customerId
                                 select c).SingleOrDefault();

            return customer;
        }

        //GET /api/customer/companyName/contactName
        [HttpGet("{companyName}/{contactName}")]
        public dynamic get(string companyName, string contactName)
        {
            dynamic customers = (from c in _context.Customers
                                 where c.CompanyName == companyName && c.ContactName == contactName
                                 select new { c.CompanyName, c.ContactName, c.ContactTitle, c.Phone });

            return customers;
        }

        //PUT /api/customer/id
        [HttpPut("{id}")]
        public ActionResult put(string id, [FromBody] Customer customer)
        {

            if (customer.CustomerId != id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/customer/id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok();
        }

    }
}
