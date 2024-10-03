using Microsoft.EntityFrameworkCore;

namespace sem_5_24_25_043.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

}