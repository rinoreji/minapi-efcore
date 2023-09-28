using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using minapi_efcore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<TodoDb>(opt => opt.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=miniapi-todo;Trusted_Connection=True;"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/list", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.MapGet("/add", async (TodoDb db) =>
{
    var userGen = GenData.GetUserGenerator();
    var todoGen = GenData.GetTodoGenerator(userGen.Generate(10));
    var todos = todoGen.Generate(100000);
    await db.Todos.AddRangeAsync(todos);

    await db.SaveChangesAsync();
    return await db.Todos.ToListAsync();
});

await EnsureDb(app.Services, app.Logger);

app.Run();


async Task EnsureDb(IServiceProvider services, ILogger logger)
{
    logger.LogInformation("Ensuring database exists at connection string '{connectionString}'");

    using var db = services.CreateScope().ServiceProvider.GetRequiredService<TodoDb>();
    //var sql = $@"CREATE TABLE IF NOT EXISTS Todos (
    //              {nameof(Todo.Id)} INTEGER PRIMARY KEY AUTOINCREMENT,
    //              {nameof(Todo.Title)} TEXT NOT NULL,
    //              {nameof(Todo.IsComplete)} INTEGER DEFAULT 0 NOT NULL CHECK({nameof(Todo.IsComplete)} IN (0, 1))
    //             );";
    //await db.ExecuteAsync(sql);
    await db.Database.EnsureCreatedAsync();
    //await db.Database.MigrateAsync();
}
