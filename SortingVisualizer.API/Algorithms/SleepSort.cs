using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Each element sleeps for a duration equal to its value. The first to wake up is the smallest. Parallelism as a sorting strategy.")]
public class SleepSort
{

    public void Sort(ObservableList list)
    {
        var sorted = new List<int>();
        var lockObj = new object();

        var tasks = new List<Task>();

        for (int i = 0; i < list.Count; i++)
        {
            int value = list[i];
            tasks.Add(Task.Run(() =>
            {
                Thread.Sleep(value * 50);
                lock (lockObj)
                {
                    sorted.Add(value);
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());

        for (int i = 0; i < sorted.Count; i++)
            list[i] = sorted[i];
    }
}