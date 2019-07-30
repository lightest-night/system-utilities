using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightestNight.System.Utilities.Extensions
{
    public static class ExtendsEnumerable
    {
        /// <summary>
        /// Determines whether an <see cref="IEnumerable{T}" /> is either null, or empty
        /// </summary>
        /// <param name="target">The <see cref="IEnumerable{T}" /> to check</param>
        /// <typeparam name="T">The Type the <see cref="IEnumerable{T}" /> holds</typeparam>
        /// <returns>Boolean denoting whether the <see cref="IEnumerable{T}" /> is null or empty</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> target)
            => target == null || !target.Any();

        /// <summary>
        /// Executes a task asynchronously over an <see cref="IEnumerable{T}" />
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to iterate over</param>
        /// <param name="body">The function to call iteratively</param>
        /// <param name="dop">The degrees of parallelism to use</param>
        /// <typeparam name="T">The Type the <see cref="IEnumerable{T}" /> holds</typeparam>
        public static Task ForEach<T>(this IEnumerable<T> source, Func<T, Task> body, int dop = 1)
            => Task.WhenAll(Partitioner.Create(source).GetPartitions(dop).Select(async partition =>
            {
                using (partition)
                {
                    while (partition.MoveNext())
                        await body(partition.Current);
                }
            }));
    }
}