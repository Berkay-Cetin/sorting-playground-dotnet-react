using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Each element votes on where it should go based on its neighbors. Majority rules. May never reach consensus.")]
public class DemocracySort
{
    public void Sort(ObservableList list)
    {
        int maxIterations = 1000;
        int iteration = 0;

        while (!IsSorted(list) && iteration < maxIterations)
        {
            iteration++;
            var moves = new Dictionary<int, int>(); // index → yeni index

            for (int i = 0; i < list.Count; i++)
            {
                int votesRight = 0; // sağa git oyu
                int votesLeft = 0;  // sola git oyu

                // komşulara sor
                for (int j = 0; j < list.Count; j++)
                {
                    if (j == i) continue;
                    if (list[j] < list[i]) votesRight++; // sen benden küçüksün, sağıma geç
                    else votesLeft++;
                }

                if (votesRight > votesLeft && i < list.Count - 1)
                    moves[i] = i + 1;
                else if (votesLeft > votesRight && i > 0)
                    moves[i] = i - 1;
            }

            // oylamaya göre taşın
            foreach (var move in moves)
            {
                if (move.Value < list.Count && move.Value >= 0)
                    list.Swap(move.Key, move.Value);
            }
        }
    }

    private bool IsSorted(ObservableList list)
    {
        for (int i = 0; i < list.Count - 1; i++)
            if (list[i] > list[i + 1]) return false;
        return true;
    }
}