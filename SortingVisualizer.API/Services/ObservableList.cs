namespace SortingVisualizer.API.Services;

public class ObservableList
{
    private readonly List<int> _inner;
    private readonly Func<List<int>, int[], Task> _onChanged;

    public ObservableList(List<int> initial, Func<List<int>, int[], Task> onChanged)
    {
        _inner = new List<int>(initial);
        _onChanged = onChanged;
    }

    public int Count => _inner.Count;

    public int this[int index]
    {
        get => _inner[index];
        set
        {
            _inner[index] = value;
            _onChanged(new List<int>(_inner), new[] { index }).GetAwaiter().GetResult();
        }
    }

    public void Swap(int i, int j)
    {
        (_inner[i], _inner[j]) = (_inner[j], _inner[i]);
        _onChanged(new List<int>(_inner), new[] { i, j }).GetAwaiter().GetResult();
    }

    public void RemoveAt(int index)
    {
        _inner.RemoveAt(index);
        _onChanged(new List<int>(_inner), Array.Empty<int>()).GetAwaiter().GetResult();
    }

    public void Insert(int index, int value)
    {
        _inner.Insert(index, value);
        _onChanged(new List<int>(_inner), new[] { index }).GetAwaiter().GetResult();
    }

    public void Add(int value)
    {
        _inner.Add(value);
        _onChanged(new List<int>(_inner), new[] { _inner.Count - 1 }).GetAwaiter().GetResult();
    }

    public List<int> ToList() => new List<int>(_inner);
}