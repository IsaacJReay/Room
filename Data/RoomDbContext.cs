using Microsoft.EntityFrameworkCore;
// using System.Data.Entity;
using Room.Models;

namespace Room.Data
{
    public class RoomDbContext: DbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options): base (options)
        {
            
        }

        public DbSet<RoomModal> Rooms { get; set; } = default!;
        public DbSet<RoomTypeModal> RoomTypes { get; set; } = default!;

    }
}