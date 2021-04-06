using System;
using System.Collections.Generic;
using System.Linq;
using PlotTool.Entities;

namespace PlotTool.Tests.Comparers
{
    internal class TraceViewComparer : IComparer<TraceView>
    {
        public int Compare(TraceView x, TraceView y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            if (!x.X.SequenceEqual(y.X)) return -1;
            if (!x.Y.SequenceEqual(y.Y)) return -1;
            return string.Compare(x.TraceName, y.TraceName, StringComparison.Ordinal);
        }
    }
}
