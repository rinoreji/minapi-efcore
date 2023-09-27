using Microsoft.EntityFrameworkCore;
using minapi_efcore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/todoitems", () => "Hello World!");

app.MapGet("/", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.MapGet("/add", async (TodoDb db) =>
{
    for (int i = 2; i < 1000; i++)
    {

        await db.Todos.AddAsync(new Todo
        {
            Id = i,
            IsComplete = false,
            Name = Guid.NewGuid().ToString()
        });
    }
    await db.SaveChangesAsync();
    return await db.Todos.ToListAsync();
});

app.Run();
