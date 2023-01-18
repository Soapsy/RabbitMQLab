using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using LAB.DataScanner.ConfigDatabaseApi.Models;
using Microsoft.EntityFrameworkCore;
using LAB.DataScanner.ConfigDatabaseApi.Interfaces;
using System.Linq.Expressions;

namespace LAB.DataScanner.ConfigDatabaseApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("[controller]")]
    [ApiController]

    // [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class BaseController<T> : ODataController where T : class
    {
        //Entities to override with controller-specific ones
        protected virtual DbSet<T> Entities { get; set; }

        private readonly BaseContext _context;

        [HttpGet("get")]
        [EnableQuery]
        public virtual SingleResult<T> Get([FromODataUri] Expression<Func<T, bool>> filter)
        {
            IQueryable<T> getView = Entities.Where(filter);
            return SingleResult.Create<T>(getView);
        }

        [HttpPost("post")]
        [EnableQuery]
        public virtual IActionResult Post([FromODataUri] T entity)
        {
            Entities.Add(entity);
            _context.SaveChanges();
            return Created(entity);
        }

        [HttpPatch("patch")]
        [EnableQuery]
        public virtual IActionResult Patch([FromODataUri] Expression<Func<T, bool>> filter, Delta<T> entityDelta)
        {
            var view = Entities.FirstOrDefault(filter);
            entityDelta.Patch(view);
            _context.SaveChanges();
            return Updated(entityDelta);
        }

        [HttpDelete("delete")]
        [EnableQuery]
        public virtual IActionResult Delete([FromODataUri] Expression<Func<T, bool>> filter)
        {
            var view = Entities.FirstOrDefault(filter);
            _context.SaveChanges();
            return Ok(Entities.Remove(view));
        }


        //вариант реализации через if и сравнение типов
         /*var result = 
         var typeOfT = typeof(T).FullName;

         /*if (typeOfT.Equals(typeof(ApplicationInstance)))
             {

             }
         IQueryable<T> result = Context.Find()
         return SingleResult.Create<T>(result);
         */
    }
}
