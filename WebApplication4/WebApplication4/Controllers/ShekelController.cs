using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ShekelController : ApiController
    {
        private IList<GroupResult> groupResults;
        //שאלה 1
        public IHttpActionResult Get()
        {
            var ctx = new ShekelContext();

            groupResults = (from p in ctx.Groups
                           from f in ctx.Factories.Where(x=> x.groupCode == p.groupCode).DefaultIfEmpty()
                           from m in ctx.FactoriesToCustomer.Where(y => y.factoryCode == f.factoryCode).DefaultIfEmpty()
                            from a in ctx.Customers.Where(z => z.customerId == m.customerId).DefaultIfEmpty()
                           select new GroupResult
                           {
                               groupCode = p.groupCode,
                               groupName =  p.groupName,
                               CustomerID = a != null ?  a.customerId : string.Empty,
                               Name = a != null ? a.name : string.Empty
                           }).ToList();

            if (groupResults.Count == 0)
            {
                return NotFound();
            }
            return Ok(groupResults);
        }
        //שאלה 2
        public void PostProduct(NewCustomer newCustomer)
        {
            var ctx = new ShekelContext();
            Customers customer = new Customers(){ customerId = newCustomer.CustomerID, name = newCustomer.Name, address = newCustomer.Address, phone = newCustomer.Phone };
            FactoriesToCustomer f = new FactoriesToCustomer() { factoryCode = newCustomer.factoryCode, groupCode = newCustomer.groupCode, customerId = newCustomer.CustomerID };
            customer.FactoriesToCustomer = new List<FactoriesToCustomer>();
            customer.FactoriesToCustomer.Add(f);
            ctx.Customers.Add(customer);
            ctx.SaveChanges();
        }

    }
}
