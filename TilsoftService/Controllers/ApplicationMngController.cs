using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [RoutePrefix("api/applicationmng")]
    public class ApplicationMngController : ApiController
    {
        [HttpPost]
        [Route("clearcache")]
        public IHttpActionResult ClearCache()
        {
            Library.CacheHelper.ClearCache();
            return Ok("All cache are clear");
        }
    }
}
