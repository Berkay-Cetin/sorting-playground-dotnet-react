using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Disassembles the list, reassembles it in the wrong order, makes a few mistakes, then mostly fixes them. One element always left over.")]
public class IkeaSort
{
    private static Random rnd = new Random();

    public void Sort(ObservableList list)
    {
        var parts = list.ToList();

        // 1. söküp parçalara ayır
        while (list.Count > 0)
            list.RemoveAt(0);

        // 2. talimatları yanlış oku — ters sırada ekle
        parts.Sort((a, b) => b.CompareTo(a));
        for (int i = 0; i < parts.Count; i++)
            list.Insert(0, parts[i]);

        // 3. birkaç elemanı ters tak
        int mistakes = rnd.Next(2, 5);
        for (int m = 0; m < mistakes; m++)
        {
            int i = rnd.Next(list.Count);
            int j = rnd.Next(list.Count);
            list.Swap(i, j);
        }

        // 4. neredeyse doğru sırala — insertion sort ile
        for (int i = 1; i < list.Count; i++)
        {
            int j = i;
            while (j > 0 && list[j] < list[j - 1])
            {
                list.Swap(j, j - 1);
                j--;
            }
        }
    }
}