namespace Caliburn.Micro
{
    /// <summary>
    /// Represents a tuple with three items.
    /// </summary>
    /// <typeparam name="TItem1"></typeparam>
    /// <typeparam name="TItem2"></typeparam>
    /// <typeparam name="TItem3"></typeparam>
    public class Tuple<TItem1, TItem2, TItem3>
    {
        /// <summary>
        /// Gets or Sets the first item.
        /// </summary>
        public TItem1 Item1 { get; private set; }

        /// <summary>
        /// Gets or Sets the second item.
        /// </summary>
        public TItem2 Item2 { get; private set; }

        /// <summary>
        /// Gets or Sets the third item.
        /// </summary>
        public TItem3 Item3 { get; private set; }

        /// <summary>
        /// Creates an instance of the tuple.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        public Tuple(TItem1 item1, TItem2 item2, TItem3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }
}