using System.Drawing;
using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Only operation allowed: flip. Finds the largest element, flips it to the top, then flips it into place. Like sorting a stack of pancakes with a spatula.")]
public class PancakeSort
{

    public void Sort(ObservableList list)
    {
        for (int size = list.Count; size > 1; size--)
        {
            int maxIndex = FindMax(list, size);

            if (maxIndex == size - 1) continue;

            if (maxIndex != 0)
                Flip(list, maxIndex);

            Flip(list, size - 1);
        }
    }
    private int FindMax(ObservableList list, int size)
    {
        int maxIndex = 0;
        for (int i = 1; i < size; i++)
            if (list[i] > list[maxIndex])
                maxIndex = i;
        return maxIndex;
    }

    private void Flip(ObservableList list, int k)
    {
        int left = 0, right = k;
        while (left < right)
        {
            list.Swap(left, right);
            left++;
            right--;
        }
    }
}