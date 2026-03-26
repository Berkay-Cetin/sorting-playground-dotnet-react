using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Algorithms;

[SortDescription("Moves through the list like a garden gnome — steps forward if things look good, swaps and steps back if not.")]
public class GnomeSort
{
    public void Sort(ObservableList list)
    {
        int position = 0;
        while (position < list.Count)
        {
            if (position == 0 || list[position] >= list[position - 1])
                position++;
            else
            {
                list.Swap(position, position - 1);
                position--;
            }
        }
    }
}