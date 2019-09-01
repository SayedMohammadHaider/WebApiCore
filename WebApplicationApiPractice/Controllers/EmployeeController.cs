using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationApiPractice.Model;

namespace WebApplicationApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public List<Employee> Get()
        {
            List<Employee> employee = new List<Employee> {
                new Employee{ Id=1,Name="Haider",Address="Wilson Garden"},
                new Employee{ Id=2,Name="Asif",Address="Lakkasandra"},
                new Employee{ Id=3,Name="Amir",Address="BTM 1st stage"}
            };
            return employee.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var emp = Get().Where(x => x.Id == id).FirstOrDefault();
            return emp;
        }

        [HttpPost]
        public List<Employee> Post(Employee employee)
        {
            var emp = Get();
            emp.Add(employee);
            return emp;
        }

        [HttpPut]
        public Employee Put(Employee employee)
        {
            var emp = Get().Where(x => x.Id == employee.Id).FirstOrDefault();
            emp = employee;
            return emp;
        }

        [HttpDelete("{id}")]
        public List<Employee> Delete(int id)
        {
            var emp = Get();
            var emp1 = emp.Where(note => note.Id != id).ToList();
            return emp1;
        }
    }
}