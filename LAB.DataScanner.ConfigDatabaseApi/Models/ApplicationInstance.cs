using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using LAB.DataScanner.ConfigDatabaseApi.Interfaces;

namespace LAB.DataScanner.ConfigDatabaseApi.Models
{
    public class ApplicationInstance
    {

        [Key]
        public int InstanceId { get; set; }
        [Key]
        public int? TypeId { get; set; }
        public string InstanceName { get; set; }
        public string ConfigJson { get; set; }

        public virtual ApplicationType Type { get; set; }
        public virtual ICollection<Binding> BindingConsumerInstance { get; set; }
        public virtual ICollection<Binding> BindingPublisherInstance { get; set; }

    }
}
