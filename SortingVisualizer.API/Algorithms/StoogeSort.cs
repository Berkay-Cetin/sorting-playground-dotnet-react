using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Sorts the first 2/3, then the last 2/3, then the first 2/3 again. Yes, really. O(n^2.7).")]
public class StoogeSort
{
    public void Sort(ObservableList list)
    {
        StoogeSortRange(list, 0, list.Count - 1);
    }

    private void StoogeSortRange(ObservableList list, int low, int high)
    {
        if (low >= high) return;

        if (list[low] > list[high])
            list.Swap(low, high);

        if (high - low + 1 > 2)
        {
            int t = (high - low + 1) / 3;

            StoogeSortRange(list, low, high - t);        // ilk 2/3
            StoogeSortRange(list, low + t, high);        // son 2/3
            StoogeSortRange(list, low, high - t);        // ilk 2/3 tekrar
        }
    }
}