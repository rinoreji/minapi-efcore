﻿using Microsoft.EntityFrameworkCore;

namespace minapi_efcore
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<User> Users => Set<User>();
    }
}
