using System.Collections.Generic;

namespace PlotTool.Entities
{
    public class TraceView
    {
        public string TraceName { get; set; }
        public ICollection<double> X { get; set; }
        public ICollection<double> Y { get; set; }
    }
}
