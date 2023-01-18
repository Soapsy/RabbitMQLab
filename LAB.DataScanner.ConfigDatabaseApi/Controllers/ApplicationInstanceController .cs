using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LAB.DataScanner.ConfigDatabaseApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData;
using System.Linq.Expressions;

namespace LAB.DataScanner.ConfigDatabaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class ApplicationInstanceController : BaseController<ApplicationInstance>
    {
        private readonly BaseContext _context;

        //injecting context
        public ApplicationInstanceController(BaseContext _context)
        {
            this._context = _context;
        }
        protected override DbSet<ApplicationInstance> Entities => this._context.ApplicationInstances;
    }
}
