namespace QuestMapperLib.Model
{
    public interface INPC
    {
        int Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsNull { get; }
    }
}
