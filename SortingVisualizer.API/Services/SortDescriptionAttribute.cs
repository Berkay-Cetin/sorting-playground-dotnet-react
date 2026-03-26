namespace SortingVisualizer.API.Services;

[AttributeUsage(AttributeTargets.Class)]
public class SortDescriptionAttribute : Attribute
{
    public string Description { get; }

    public SortDescriptionAttribute(string description)
    {
        Description = description;
    }
}