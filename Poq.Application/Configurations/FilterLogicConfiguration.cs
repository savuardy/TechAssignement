using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Application.Configurations
{
    public class FilterLogicConfiguration
    {
        public const string Configuration = "FilterLogicConfiguration";
        public int SkipWords { get; set; }
        public int TakeWords { get; set; }
        public string? Highlighter { get; set; }
    }
}
