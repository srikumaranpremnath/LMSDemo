using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainObjects
{
    public class Audit
    {
        
        public DateTime? CreatedDate { get; protected set; }
        public string CreatedBy { get; protected set; }

        public DateTime? EditedDate { get; protected set; }

        public string EditedBy { get; protected set; }



    }
}
