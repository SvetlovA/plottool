using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.FSharp.Core;
using Plotly.NET;
using PlotTool.Entities;
using PlotTool.Services;

namespace PlotTool
{
    public class PlotManager
    {
        private readonly IPlotViewService _plotViewService;

        public PlotManager(IPlotViewService plotViewService)
        {
            _plotViewService = plotViewService ?? throw new ArgumentNullException(nameof(plotViewService));
        }

        public async Task DrawAll() => await Task.WhenAll(DrawAllPlotViews(), DrawAggregatedPlot());

        public async Task DrawAllPlotViews()
        {
            var plotViews = await _plotViewService.GetAllPlots();
            DrawCombinedTraces(plotViews);
        }

        public async Task DrawAggregatedPlot()
        {
            var aggregatedPlotViews = await _plotViewService.GetAggregatedPlots();
            DrawCombinedTraces(aggregatedPlotViews);
        }

        private static void DrawCombinedTraces(IEnumerable<PlotView> plotViews)
        {
            foreach (var plotView in plotViews)
            {
                GenericChart.combine(plotView.Traces.Select(GetTrace))
                    .WithLayout(Layout.init<object, object, object, object, object, object, object>(
                        plotView.PlotName,
                        null,
                        null,
                        null,
                        null,
                        1500,
                        600,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null))
                    .ShowWithDescription(new ChartDescription.Description(plotView.PlotName, ""));
            }
        }

        private static GenericChart.GenericChart GetTrace(TraceView traceView) =>
            Chart.Line<double, double, StyleParam.DrawingStyle, int>(traceView.X, traceView.Y, traceView.TraceName, Dash: StyleParam.DrawingStyle.Solid, Width: 1000)
                .WithTraceName(traceView.TraceName, true)
                .WithX_AxisStyle("Time", Showgrid: false, Showline: true)
                .WithY_AxisStyle("Events", Showgrid: false, Showline: true);
    }
}
