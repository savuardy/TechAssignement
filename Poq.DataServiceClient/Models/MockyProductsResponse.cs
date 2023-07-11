using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.DataSourceClient.Models
{
    public class MockyProductsResponse
    {
        public IEnumerable<MockyProductResponse> Products { get; set; }
    }
}
