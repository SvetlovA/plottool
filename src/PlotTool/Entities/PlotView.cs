using System.Collections.Generic;

namespace PlotTool.Entities
{
    internal class PlotView
    {
        public string PlotName { get; set; }
        public IList<TraceView> Traces { get; set; }
    }
}
