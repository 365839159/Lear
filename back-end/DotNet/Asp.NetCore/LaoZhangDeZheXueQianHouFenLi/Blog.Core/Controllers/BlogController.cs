










using Blog.Core.IServices;
using Blog.Core.Model.Entitys;
using Blog.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET: api/Blog/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public List<Advertisement> Get(int id)
        {
            IAdvertisementServices advertisementServices = new AdvertisementServices();

            return advertisementServices.Query(d => d.Id == id);
        }
    }
}
