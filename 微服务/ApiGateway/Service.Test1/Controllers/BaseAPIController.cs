using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Service.Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        protected readonly ILog log;
        public BaseAPIController(IConfiguration configuration)
        {
            log = LogManager.GetLogger(configuration["Log4netRepositoryName"], "loger");
        }
    }
}