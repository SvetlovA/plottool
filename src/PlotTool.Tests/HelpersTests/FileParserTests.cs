using System.Collections.Generic;
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
        [TestCaseSource(nameof(ParseAsyncPositiveTestData))]
        public async Task ParseAsyncPositiveTest(string[] testFolderPaths, string[] expectedResultPaths)
        {
            var actualResults = await FileParser.ParseAsync(new InputPlotData
            {
                PlotFilesDirectoryPaths = testFolderPaths
            });

            var expectedResults = expectedResultPaths.Select(GetExpectedPlotView);

            Assert.AreEqual(actualResults.Count(), expectedResults.Count());

            var orderedActualResults = actualResults.OrderBy(x => x.PlotName).ToArray();
            var orderedExpectedResults = expectedResults.OrderBy(x => x.PlotName).ToArray();

            for (var i = 0; i < orderedActualResults.Length; i++)
            {
                Assert.That(orderedActualResults[i], Is.EqualTo(orderedExpectedResults[i]).Using(new PlotViewComparer()));
            }
        }

        public static IEnumerable<TestCaseData> ParseAsyncPositiveTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "TestPlotFolder/TestPlotFiles1" }, new [] { "TestPlotResults/TestPlotFiles1Result.json" })
                    .SetName("ParseAsync single folder test");

                yield return new TestCaseData(new[] { "TestPlotFolder" }, new [] { "TestPlotResults/TestPlotFolderResult.json" })
                    .SetName("ParseAsync nested folder test");

                yield return new TestCaseData(new[] { "TestPlotFolder/TestPlotFiles1", "TestPlotFolder/TestPlotFiles2" },
                        new [] { "TestPlotResults/TestPlotFiles1Result.json", "TestPlotResults/TestPlotFiles2Result.json" })
                    .SetName("ParseAsync multi folder test");
            }
        }

        private static PlotView GetExpectedPlotView(string expectedPlotResultsPath)
        {
            var plotViewJson = File.ReadAllText(expectedPlotResultsPath);

            return JsonSerializer.Deserialize<PlotView>(plotViewJson);
        }
    }
}
