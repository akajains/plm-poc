using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PLM.CmsService.Data;
using System;
using Microsoft.AspNet.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsService.API.Controllers
{
    
    public class ContentController : BaseController
    {
        //IRepository<CMSContent> repo;
        public ContentController(IConfiguration config, IRepository<CMSContent> repo) : base(config,repo)
        {
          //  this.repo = repo;
        }


        // GET: api/content
        [HttpGet]
        [Authorize]
        public IEnumerable<CMSContent> Get()
        {
            var list = CMSRepo.List;
            return list;
        }



        [HttpPost]
        public void Post([FromBody]CMSContent value)
        {
            CMSContent values = new CMSContent() { content = new ContentBlock() { contentId = new Random().Next(100, 1000), description = "Sample description " + DateTime.Now, text = "Sample text " + DateTime.Now, title = "Sample title " + DateTime.Now } };
            CMSRepo.Add(values);
        }


    }
}
