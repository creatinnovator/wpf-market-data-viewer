using System;
using System.Collections.Generic;

namespace MarketDataViewer.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Executes action for each item in the collection
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="items">Items/collection to iterate></param>
        /// <param name="action">Action to perform for each item</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
