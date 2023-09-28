namespace minapi_efcore
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public User User { get; set; }
    }
}
