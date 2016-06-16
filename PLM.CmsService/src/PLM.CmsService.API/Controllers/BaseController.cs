using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using PLM.CmsService.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsService.API.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected IConfiguration Config { get; }
        protected IRepository<CMSContent> CMSRepo { get; }

        public BaseController()
        {

        }
        protected BaseController(IConfiguration config, IRepository<CMSContent> repo)
        {
            this.Config = config;
            this.CMSRepo = repo;

        }

       
    }
}
