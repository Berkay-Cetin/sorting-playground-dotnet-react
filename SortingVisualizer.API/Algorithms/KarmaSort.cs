using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Large elements have accumulated bad karma and are sent to the back as punishment. Essentially selection sort with a moral framework.")]
public class KarmaSort
{
    public void Sort(ObservableList list)
    {
        for (int punished = list.Count - 1; punished > 0; punished--)
        {
            // en büyüğü bul — kötü karma biriktirmiş
            int maxIndex = 0;
            for (int i = 1; i <= punished; i++)
            {
                // list.Highlight(i); // suçluyu arıyoruz
                if (list[i] > list[maxIndex])
                    maxIndex = i;
            }

            if (maxIndex != punished)
                list.Swap(maxIndex, punished);
        }
    }
}