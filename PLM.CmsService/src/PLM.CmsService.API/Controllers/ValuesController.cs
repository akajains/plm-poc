﻿using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using PLM.CmsService.Data;

namespace CmsService.API.Controllers
{
    
    public class ValuesController : BaseController
    {
        public ValuesController(IConfiguration config, IRepository<CMSContent> repo) : base(config, repo)
        {
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            //MongoConnection con = new MongoConnection(config);
          
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
