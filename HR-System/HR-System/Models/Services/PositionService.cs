using HR_System.Data;
using HR_System.Models.Interfaces;

namespace HR_System.Models.Services
{
    public class PositionService : IPosition
    {
        private readonly AppDbContext _context;

        public PositionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Position> PostPosition(Position position)
        {
            var pos = new Position()
            {
                Id = position.Id,
                Name = position.Name
            };
            await _context.AddAsync(pos);
            await _context.SaveChangesAsync();
            return pos;
        }
    }
}
