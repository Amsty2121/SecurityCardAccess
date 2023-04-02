using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SyncEntity
    {
        public Guid Id { get; set; }
        public DateTime LastChangeAt { get; set; }
        public string JsonData { get; set; }
        public string SyncType { get; set; }
        public string Origin { get; set; }
        public string OperationUrl { get; set; }
    }
}
