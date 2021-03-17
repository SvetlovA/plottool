using System.Collections.Generic;

namespace PlotTool.Entities
{
    public class TraceView
    {
        public string TraceName { get; set; }
        public IList<double> X { get; set; }
        public IList<double> Y { get; set; }
    }
}
