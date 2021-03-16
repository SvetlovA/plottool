using System.Collections.Generic;

namespace PlotTool.Entities
{
    public class PlotView
    {
        public string PlotName { get; set; }
        public ICollection<TraceView> Traces { get; set; }
    }
}
