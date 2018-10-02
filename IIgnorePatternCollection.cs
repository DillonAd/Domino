namespace domino
{
    public interface IIgnorePatternCollection
    {
        bool ShouldIgnore(string fileName);
    }
}
