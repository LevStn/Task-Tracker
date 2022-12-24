using Task_Tracker.BusinessLayer.Models;

namespace Task_Tracker.BusinessLayer.Services
{
    public interface ICustomFildService
    {
        public Task<int> AddCustomFild(CustomFildModel customFild);
        public Task DeleteCustomFild(int id);
    }
}