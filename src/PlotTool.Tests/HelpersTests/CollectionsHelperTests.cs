using System;
using System.Collections.Generic;
using NUnit.Framework;
using PlotTool.Helpers;

namespace PlotTool.Tests.HelpersTests
{
    [TestFixture]
    public class CollectionsHelperTests
    {
        [Test]
        [TestCaseSource(nameof(MergeCollectionsPositiveTestCaseData))]
        public IList<double> MergeCollectionsPositiveTest(IList<double> firstCollection, IList<double> secondCollection)
        {
            return CollectionsHelper.MergeCollections(firstCollection, secondCollection);
        }

        public static IEnumerable<TestCaseData> MergeCollectionsPositiveTestCaseData
        {
            get
            {
                yield return new TestCaseData(new[] { 1d, 2d, 3d }, new[] { 1d, 2d, 3d })
                    .Returns(new[] { 2d, 4d, 6d })
                    .SetName("Test merge collections with same count");

                yield return new TestCaseData(new[] { 1d, 2d, 3d, 4d }, new[] { 1d, 2d, 3d })
                    .Returns(new[] { 2d, 4d, 6d, 4d })
                    .SetName("Test merge collections firs count > second count");

                yield return new TestCaseData(new[] { 1d, 2d, 3d }, new[] { 1d, 2d, 3d, 4d })
                    .Returns(new[] { 2d, 4d, 6d, 4d })
                    .SetName("Test merge collections second count > firs count");

                yield return new TestCaseData(Array.Empty<double>(), Array.Empty<double>())
                    .Returns(Array.Empty<double>())
                    .SetName("Test merge empty collections");

                yield return new TestCaseData(Array.Empty<double>(), new[] { 1d, 2d, 3d })
                    .Returns(new[] { 1d, 2d, 3d })
                    .SetName("Test merge one empty collections");
            }
        }

        [Test]
        [TestCaseSource(nameof(MergeCollectionsNegativeTestCaseData))]
        public void MergeCollectionsNegativeTest(IList<double> firstCollection, IList<double> secondCollection)
        {
            Assert.Throws(Is.AssignableTo<Exception>(), () => CollectionsHelper.MergeCollections(firstCollection, secondCollection));
        }

        public static IEnumerable<TestCaseData> MergeCollectionsNegativeTestCaseData
        {
            get
            {
                yield return new TestCaseData(null, null)
                    .SetName("Test merge null collections");

                yield return new TestCaseData(null, new[] { 1d, 2d, 3d })
                    .SetName("Test merge first null collection");

                yield return new TestCaseData(new[] { 1d, 2d, 3d }, null)
                    .SetName("Test merge second null collection");
            }
        }
    }
}
