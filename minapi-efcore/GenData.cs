using Bogus;

namespace minapi_efcore
{
    public class GenData
    {

        public static Faker<Todo> GetTodoGenerator(IEnumerable<User> users)
        {
            return new Faker<Todo>()
                .RuleFor(t => t.IsComplete, f => f.Random.Bool())
                .RuleFor(t => t.Id, _ => Guid.NewGuid())
                .RuleFor(t=>t.User, f=>f.PickRandom(users))
                .RuleFor(t => t.Name, f => f.Lorem.Sentence());
        }

        public static Faker<User> GetUserGenerator()
        {
            return new Faker<User>()
                .RuleFor(t => t.Id, _ => Guid.NewGuid())
                .RuleFor(t => t.Name, f => f.Name.FirstName());
        }
    }
}
