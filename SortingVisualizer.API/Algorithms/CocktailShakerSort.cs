using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Bubble sort in both directions. Shakes large elements to the right and small elements to the left each pass.")]
public class CocktailShakerSort
{
    public void Sort(ObservableList list)
    {
        int left = 0;
        int right = list.Count - 1;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;

            for (int i = left; i < right; i++)
            {
                if (list[i] > list[i + 1])
                {
                    list.Swap(i, i + 1);
                    swapped = true;
                }
            }
            right--;

            if (!swapped) break;

            swapped = false;

            for (int j = right; j > left; j--)
            {
                if (list[j] < list[j - 1])
                {
                    list.Swap(j, j - 1);
                    swapped = true;
                }
            }
            left++;

            if (!swapped) break;

            swapped = false;
        }
    }
}