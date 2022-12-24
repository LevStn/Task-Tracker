using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer.Repositories;

public interface ICustomFildRepository
{
    public Task<int> AddCustomFild(CustomFildEntity customFild);
    public Task DeleteCustomFild(CustomFildEntity customFild);
    public Task<CustomFildEntity?> GetCustomFildById(int id);
}
