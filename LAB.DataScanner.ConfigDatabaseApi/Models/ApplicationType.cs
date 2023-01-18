using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using LAB.DataScanner.ConfigDatabaseApi.Interfaces;

namespace LAB.DataScanner.ConfigDatabaseApi.Models
{
    public class ApplicationType
    {

        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeVersion { get; set; }
        public string ConfigTemplateJson { get; set; }

        public virtual ICollection<ApplicationInstance> ApplicationInstance { get; set; }

    }
}
