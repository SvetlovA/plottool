using System;
using System.Collections.Generic;
using System.Linq;
using PlotTool.Entities;

namespace PlotTool.Tests.Comparers
{
    internal class PlotViewComparer : IComparer<PlotView>
    {
        public int Compare(PlotView x, PlotView y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            if (x.Traces.Count != y.Traces.Count) return -1;

            var traceViewComparer = new TraceViewComparer();
            var traceViewCompareResult =
                x.Traces.Select((traceView, i) => traceViewComparer.Compare(traceView, y.Traces[i]));
            if (traceViewCompareResult.Any(result => result != 0)) return -1;

            return string.Compare(x.PlotName, y.PlotName, StringComparison.Ordinal);
        }
    }
}
