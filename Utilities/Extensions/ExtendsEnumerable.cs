using System.Collections.Generic;
using System.Linq;

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
    }
}