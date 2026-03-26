using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Randomly shuffles the list until it happens to be sorted. Statistically guaranteed to finish. Eventually.")]
public class BogoSort
{

    public void Sort(ObservableList list)
    {
        while (!IsSorted(list))
        {
            Shuffle(list);
        }
    }

    public void Shuffle(ObservableList list)
    {
        var rnd = new Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            list.Swap(n, k);
        }
    }

    private bool IsSorted(ObservableList list)
    {
        for (int i = 0; i < list.Count - 1; i++)
            if (list[i] > list[i + 1]) return false;
        return true;
    }
}