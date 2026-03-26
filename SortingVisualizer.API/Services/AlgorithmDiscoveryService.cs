using System.Reflection;

namespace SortingVisualizer.API.Services;

public class AlgorithmDiscoveryService
{
    public List<string> GetAvailableAlgorithms()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == "SortingVisualizer.API.Algorithms"
                     && t.IsClass
                     && !t.IsAbstract
                     && t.GetMethod("Sort") != null)
            .Select(t => t.Name)
            .OrderBy(n => n)
            .ToList();
    }

    public async Task RunAlgorithm(string algorithmName, ObservableList list)
    {
        var type = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == algorithmName
                              && t.Namespace == "SortingVisualizer.API.Algorithms");

        if (type == null)
            throw new ArgumentException($"Algorithm '{algorithmName}' not found.");

        var instance = Activator.CreateInstance(type);
        var method = type.GetMethod("Sort");

        if (method == null)
            throw new InvalidOperationException($"Sort method not found on '{algorithmName}'.");

        var result = method.Invoke(instance, new object[] { list });

        if (result is Task task)
            await task;
    }
}