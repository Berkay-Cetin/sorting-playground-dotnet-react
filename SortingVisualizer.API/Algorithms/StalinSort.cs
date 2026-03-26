using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Any element that is out of order is simply removed. The remaining elements are, by definition, sorted.")]
public class StalinSort
{
    public void Sort(ObservableList list)
    {
        int i = 1;
        while (i < list.Count)
        {
            if (list[i] < list[i - 1])
                list.RemoveAt(i);
            else
                i++;
        }
    }
}