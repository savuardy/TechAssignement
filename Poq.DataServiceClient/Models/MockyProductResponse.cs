using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.DataSourceClient.Models
{
    public class MockyProductResponse
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public IEnumerable<string> Sizes { get; set; }
        public string Description { get; set; }
    }
}
