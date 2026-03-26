using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Has a 30% chance of doing nothing each step. Eventually sorts the list, but only when it feels like it.")]
public class ProcrastinationSort
{
    private static Random rnd = new Random();

    public void Sort(ObservableList list)
    {
        int maxAttempts = 500;
        int attempts = 0;

        while (!IsSorted(list) && attempts < maxAttempts)
        {
            if (rnd.NextDouble() < 0.3)
            {
                // %30 — ertelendi, sadece listeye bak
                for (int i = 0; i < list.Count; i++)
                {
                    int temp = list[i];
                    list[i] = temp;
                }
                attempts++;
                continue;
            }

            // %70 — bir adım at
            int index = rnd.Next(list.Count - 1);
            if (list[index] > list[index + 1])
                list.Swap(index, index + 1);

            attempts++;
        }
    }

    private bool IsSorted(ObservableList list)
    {
        for (int i = 0; i < list.Count - 1; i++)
            if (list[i] > list[i + 1]) return false;
        return true;
    }
}