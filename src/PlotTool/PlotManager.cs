using System.Linq;
using Plotly.NET;
using PlotTool.Entities;

namespace PlotTool
{
    internal class PlotManager
    {
        public static async void DrawAll()
        {
            var plotViews = await FileParser.ParseAsync();

            foreach (var plotView in plotViews)
            {
                GenericChart.combine(plotView.Traces.Select(GetTrace))
                    .Show();
            }
        }

        private static GenericChart.GenericChart GetTrace(TraceView traceView) =>
            Chart.Line<double, double, StyleParam.DrawingStyle, int>(traceView.X, traceView.Y, traceView.TraceName, Dash: StyleParam.DrawingStyle.Solid, Width: 2)
                .WithTraceName(traceView.TraceName, true)
                .WithX_AxisStyle("Time", Showgrid: false, Showline: true)
                .WithY_AxisStyle("Events", Showgrid: false, Showline: true);
    }
}
