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
            // Lấy danh sách FloorId theo BlockId
            var floorId = await _context.FloorOfBlocks
                .Where(x => x.BlockId == id)
                .Select(fob => fob.FloorId)
                .ToListAsync();

            // Lấy danh sách các tầng tương ứng, sắp xếp theo FloorOrder và chuyển sang FloorDto
            var floors = await _context.Floors
                .Where(f => floorId.Contains(f.Id))
                .OrderBy(f => f.FloorOrder)  // Sắp xếp theo cột FloorOrder
                .Select(f => new FloorDto    // Chuyển đổi sang FloorDto
                {
                    Id = f.Id,
                    FloorCode = f.FloorCode,
                    FloorName = f.FloorName,
                    FloorOrder = f.FloorOrder  // Nếu FloorOrder là kiểu số, chuyển đổi sang chuỗi
                })
                .ToListAsync();

            return floors;
        }
    }
}
