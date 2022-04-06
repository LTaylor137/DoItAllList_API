using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoItAllList_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoItAllListController : ControllerBase

    {
        static public List<ListItem> List = new List<ListItem>();

        // these items are re-added every time API is called upon.
        // public DoItAllListController()
        // {
        //     List.Add(new ListItem(1, "Apples"));
        //     List.Add(new ListItem(2, "Milk"));
        //     List.Add(new ListItem(3, "Cat Food"));
        // }

        // GET api/values
        [HttpGet("CreateList")]
        public List<ListItem> Get()
        {
            List.Add(new ListItem(1, "Apples"));
            List.Add(new ListItem(2, "Milk"));
            List.Add(new ListItem(3, "Cat Food"));
            // return new string[] { "value1", "value2" };
            return List;
        }

        // GET api/values
        // [HttpGet]
        // public List<ListItem> Get()
        // {
        //     // return new string[] { "value1", "value2" };
        //     return List;
        // }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Welcome to AspSolution.net";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
