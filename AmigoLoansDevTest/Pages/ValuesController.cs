using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmigoLoansDevTest.Pages
{
    [Route("api")]
    public class ValuesController : Controller
    {

        private Logic logic = new Logic();

        public ValuesController()
        {
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return logic.APIGetID(id.ToString());
        }
    }
}
