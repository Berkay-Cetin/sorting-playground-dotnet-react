using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Sorts the list, then anxiously re-checks everything from the beginning five times just to be sure.")]
public class AnxietySort
{
    public void Sort(ObservableList list)
    {
        for (int round = 0; round < 5; round++)
        {
            // sırala
            BubblePass(list);

            // "acaba doğru mu?" diye baştan tara
            for (int i = 0; i < list.Count; i++)
            {
                int temp = list[i];
                list[i] = temp;
            }
        }

        BubblePass(list);
    }

    private void BubblePass(ObservableList list)
    {
        bool swapped = true;
        while (swapped)
        {
            swapped = false;
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i] > list[i + 1])
                {
                    list.Swap(i, i + 1);
                    swapped = true;
                }
            }
        }
    }
}