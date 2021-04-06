using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using PlotTool.Entities;
using PlotTool.Helpers;
using PlotTool.Tests.Comparers;

namespace PlotTool.Tests.HelpersTests
{
    [TestFixture]
    public class FileParserTests
    {
        [Test]
        public async Task ParseAsyncSingleDirectoryPositiveTest()
        {
            var actualResult = await FileParser.ParseAsync(new InputPlotData
            {
                PlotFilesDirectoryPaths = new[] { "TestPlotFiles" }
            });

            var expectedResult = GetExpectedPlotView("TestPlotResults/TestPlotResult.json");

            Assert.AreEqual(actualResult.Count(), 1);
            Assert.That(actualResult.First(), Is.EqualTo(expectedResult).Using(new PlotViewComparer()));
        }

        private static PlotView GetExpectedPlotView(string expectedPlotResultsPath)
        {
            var plotViewJson = File.ReadAllText(expectedPlotResultsPath);

            return JsonSerializer.Deserialize<PlotView>(plotViewJson);
        }
    }
}
