using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using LAB.DataScanner.ConfigDatabaseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LAB.DataScanner.ConfigDatabaseApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("[controller]")]
    [ApiController]
    public class ApplicationTypeController : BaseController<ApplicationType>
    {
        private readonly BaseContext _context;

        //injecting context
        public ApplicationTypeController(BaseContext _context)
        {
            this._context = _context;
        }

        //overriding generic dbset with our model set
        protected override DbSet<ApplicationType> Entities => this._context.ApplicationTypes;

    }
}
