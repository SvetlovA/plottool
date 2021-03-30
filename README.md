# PlotTool
PlotTool is wrapper on [Plotly.NET](https://plotly.github.io/Plotly.NET/) library to simplify using and preparations data for plots. The original github repository of Plotly.Net https://github.com/plotly/Plotly.NET. PlotTool draw plots by files in directories. To use this tool you need to put files with plots coordinates to directories and use this directory paths like in example. PlotTool will draw trace for each file in directory and plot for each directory. Also tool can draw aggregated plot for each trace in original plot.

# Installation
Download and install the package from [NuGet](https://www.nuget.org/packages/PlotTool/)

# Getting Started
## Data Examples
PlotTool support any type of files that are match the patterns:

- Simple file template
```
0 10
10 20
20 30
30 40
40 50
50 60
```

- File with metadata
```
Plot Name
0 10
10 20
20 30
30 40
40 50
50 60
```

## Code Examples
```cs
using System;
using PlotTool;
using PlotTool.Console;

var plotManager = PlotManagerFactory.CreateFilePlotManager("<plot_name>", "<file_paths>");
await plotManager.DrawAll();
```

[Example project](https://github.com/SvetlovA/plottool/tree/master/src/PlotTool.Console)

# Library license
The library is available under the [MIT license](https://github.com/SvetlovA/plottool/blob/master/LICENSE).
