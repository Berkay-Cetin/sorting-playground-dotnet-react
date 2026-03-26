using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Believes the list is already sorted. Checks. If wrong, moves one element and believes again. Repeat until reality complies.")]
public class FlatEarthSort
{
    private static Random rnd = new Random();

    public void Sort(ObservableList list)
    {
        while (true)
        {
            // listeye inan — gözlemle
            bool believeItsSorted = true;
            for (int i = 0; i < list.Count - 1; i++)
            {
                // list.Highlight(i);
                if (list[i] > list[i + 1])
                {
                    believeItsSorted = false;
                    break;
                }
            }

            // inanç yeterliyse bitir
            if (believeItsSorted) return;

            // inanç yetersiz — bir eleman taşı ve tekrar inan
            int index = rnd.Next(list.Count - 1);
            if (list[index] > list[index + 1])
                list.Swap(index, index + 1);
        }
    }
}