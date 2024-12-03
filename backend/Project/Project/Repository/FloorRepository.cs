using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class FloorRepository : IFloorRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public FloorRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }
        public async Task<List<FloorDto>> GetFloorByBlockId(string id)
        {
            
            var floorId = await _context.FloorOfBlocks
                .Where(x => x.BlockId == id)
                .Select(fob => fob.FloorId)
                .ToListAsync();

          
            var floors = await _context.Floors
                .Where(f => floorId.Contains(f.Id))
                .OrderBy(f => f.FloorOrder)  
                .Select(f => new FloorDto    
                {
                    Id = f.Id,
                    FloorCode = f.FloorCode,
                    FloorName = f.FloorName,
                    FloorOrder = f.FloorOrder  
                })
                .ToListAsync();

            return floors;
        }
    }
}
