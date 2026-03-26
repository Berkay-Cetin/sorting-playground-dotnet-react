using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

public class MiracleSort
{

    public void Sort(ObservableList list)
    {
        while (!IsSorted(list))
        {
            Thread.Sleep(1000);

            CheckList(list);
        }
    }
    private void CheckList(ObservableList list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            list[i] = temp;
        }
    }

    private bool IsSorted(ObservableList list)
    {
        for (int i = 0; i < list.Count - 1; i++)
            if (list[i] > list[i + 1]) return false;
        return true;
    }
}