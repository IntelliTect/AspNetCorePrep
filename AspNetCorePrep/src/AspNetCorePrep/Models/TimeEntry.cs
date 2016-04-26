using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePrep.Models
{
    public class TimeEntry
    {
        public int TimeEntryId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime Date { get; set; }

        public decimal Hours { get; set; }
    }
}
