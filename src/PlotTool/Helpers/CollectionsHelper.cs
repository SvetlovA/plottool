using System;
using System.Collections.Generic;
using System.Linq;

namespace PlotTool.Helpers
{
    internal static class CollectionsHelper
    {
        public static IList<double> MergeCollections(IList<double> firstCollection, IList<double> secondCollection)
        {
            if (firstCollection == null)
            {
                throw new ArgumentNullException(nameof(firstCollection));
            }

            if (secondCollection == null)
            {
                throw new ArgumentNullException(nameof(secondCollection));
            }

            return firstCollection.Count >= secondCollection.Count
                ? firstCollection.Select((x, i) => x + (i < secondCollection.Count ? secondCollection[i] : default)).ToArray()
                : secondCollection.Select((x, i) => x + (i < firstCollection.Count ? firstCollection[i] : default)).ToArray();
        }

    }
}
