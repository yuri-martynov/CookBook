namespace DataAccess
{
    public interface IDishDataAccess
    {
        IDish Get(string id);
    }

    public interface IDish
    {
        string Title { get; }
        string Recipe { get; }
    }
}