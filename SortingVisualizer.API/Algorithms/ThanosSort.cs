using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

public class ThanosSort
{
    private static Random random = new Random();

    public void Sort(ObservableList list)
    {
        while (!IsSorted(list))
        {
            int removeCount = list.Count / 2;
            for (int i = 0; i < removeCount; i++)
            {
                int index = random.Next(list.Count);
                list.RemoveAt(index);  // ← otomatik track
            }
        }
    }

    private bool IsSorted(ObservableList list)
    {
        for (int i = 0; i < list.Count - 1; i++)
            if (list[i] > list[i + 1]) return false;
        return true;
    }
}