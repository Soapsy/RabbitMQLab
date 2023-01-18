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
    internal class BindingController : BaseController<Binding>
    {
        private readonly BaseContext _context;

        //injecting context
        public BindingController(BaseContext _context)
        {
            this._context = _context;
        }

        //passing our context as Entity
        protected override DbSet<Binding> Entities => this._context.Bindings;

        public SingleResult<Binding> Get([FromODataUri] string publisherId, [FromODataUri] string consumerId)
        {
            IQueryable<Binding> getView = Entities.Where(c => c.PublisherInstanceId.ToString() == publisherId && c.ConsumerInstanceId.ToString() == consumerId);
            return SingleResult.Create<Binding>(getView);
        }
    }
}
