using System;
using PlotTool;
using PlotTool.Repositories.Implementation;
using PlotTool.Services.Implementation;

Console.WriteLine("PlotTool Starting...");

var plotManager = new PlotManager(new PlotViewService(new FilePlotViewRepository()));
await plotManager.DrawAll();

Console.WriteLine("PlotTool Finish...");