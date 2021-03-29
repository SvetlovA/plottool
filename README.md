# PlotTool
PlotTool is wrapper on [Plotly.NET](https://plotly.github.io/Plotly.NET/) library to simplify using and preparations data for plots. The original github repository of Plotly.Net https://github.com/plotly/Plotly.NET.

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

## Code Examples
```cs
using System;
using PlotTool;
using PlotTool.Console;

var plotManager = PlotManagerFactory.CreateFilePlotManager("<file_path>");
await plotManager.DrawAll();
```

[Example project](https://github.com/SvetlovA/plottool/tree/master/src/PlotTool.Console)

# Library license
The library is available under the [MIT license](https://github.com/SvetlovA/plottool/blob/master/LICENSE).
