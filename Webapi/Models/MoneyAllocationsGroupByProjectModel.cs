using System.Collections.Generic;

namespace Webapi.Models
{
    public class MoneyAllocationsGroupByProjectModel
    {
        public string ProjectName { get; set; }

        public List<string> PersonsNames { get; set; }
    }
}