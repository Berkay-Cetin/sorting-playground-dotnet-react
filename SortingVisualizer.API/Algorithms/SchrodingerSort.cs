using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("The list is simultaneously sorted and unsorted until observed. Collapses into a sorted state eventually, given enough universes.")]
public class SchrodingerSort
{
    private static Random rnd = new Random();

    public void Sort(ObservableList list)
    {
        while (true)
        {
            // dalga fonksiyonu — liste hem sıralı hem değil
            for (int i = 0; i < list.Count; i++)
                // list.Highlight(i);

                // gözlemle — dalga fonksiyonu çöker
                if (IsSorted(list)) return; // şanslı evren ✨

            // şanssız evren — karıştır, tekrar dene
            Shuffle(list);
        }
    }

    private void Shuffle(ObservableList list)
    {
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