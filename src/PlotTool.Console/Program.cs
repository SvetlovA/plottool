using System;
using PlotTool;
using PlotTool.Console;

Console.WriteLine("PlotTool Starting...");

var plotManager = PlotManagerFactory.CreateFilePlotManager(AppSettings.Instance.PlotName, AppSettings.Instance.PlotPaths);
await plotManager.DrawAll();

Console.WriteLine("PlotTool Finish...");