namespace Obviously.Shared
{
    public class Shared
    {
        public static T[] GetShuffledSubArray<T>(T[] array, int? numberOfItems = null)
        {
            // Default is retuning the whole array
            if (!numberOfItems.HasValue)
            {
                numberOfItems = array.Length;
            }

            if (numberOfItems > array.Length)
            {
                numberOfItems = array.Length;
            }

            // Fisher-Yates shuffle
            for (int i = 0; i < (numberOfItems - 1); i++)
            {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                var r = Random.Shared.Next(i, array.Length);
                // Swap the values
                (array[i], array[r]) = (array[r], array[i]);
            }
            var result = array[0..numberOfItems.Value];
            return result;
        }
    }
}
