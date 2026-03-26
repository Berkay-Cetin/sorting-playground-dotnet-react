using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Divide and conquer, but make it slow on purpose. Provably the least efficient sorting algorithm that still terminates.")]
public class SlowSort
{
    public void Sort(ObservableList list)
    {
        SlowSortRange(list, 0, list.Count - 1);
    }

    private void SlowSortRange(ObservableList list, int low, int high)
    {
        if (low >= high) return;

        int mid = (low + high) / 2;

        SlowSortRange(list, low, mid);
        SlowSortRange(list, mid + 1, high);

        if (list[mid] > list[high])
            list.Swap(mid, high);

        SlowSortRange(list, low, high - 1);
    }
}