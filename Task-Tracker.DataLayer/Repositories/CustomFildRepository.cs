using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer.Repositories
{
    public class CustomFildRepository : ICustomFildRepository
    {
        private readonly TaskTrackerContext _context;

        public CustomFildRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public async Task<int> AddCustomFild(CustomFildEntity customFild)
        {
            customFild.Task = await _context.Tasks.FirstOrDefaultAsync(t=>t.Id == customFild.Task.Id);
            await _context.CustomFilds.AddAsync(customFild);
            await _context.SaveChangesAsync();

            return customFild.Id;
        }

        public async Task DeleteCustomFild(CustomFildEntity customFild)
        {            
            _context.CustomFilds.Remove(customFild);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomFildEntity?> GetCustomFildById(int id) => await _context.CustomFilds
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
