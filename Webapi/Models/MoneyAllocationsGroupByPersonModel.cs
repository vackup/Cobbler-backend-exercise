using System.Collections.Generic;

namespace Webapi.Models
{
    public class MoneyAllocationsGroupByPersonModel
    {
        public string PersonName { get; set; }

        public List<string> ProjectNames { get; set; }
    }
}