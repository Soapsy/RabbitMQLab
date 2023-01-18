using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using LAB.DataScanner.ConfigDatabaseApi.Interfaces;

namespace LAB.DataScanner.ConfigDatabaseApi.Models
{
    public class Binding
    {
        [Key]
        public int PublisherInstanceId { get; set; }
        [Key]
        public int ConsumerInstanceId { get; set; }

        public virtual ApplicationInstance ConsumerInstance { get; set; }
        public virtual ApplicationInstance PublisherInstance { get; set; }

    }
}