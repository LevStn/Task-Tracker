using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.BusinessLayer.Checkers
{
    public interface ICheckerService
    {
        public void CheckIfProjectEmpty(ProjectEntity? project, int id);
        public void CheckIfTaskEmpty(TaskEntity? task, long id);
        public void CheckIfCustomFildEmpty(CustomFildEntity? customFild, int id);
    }
}