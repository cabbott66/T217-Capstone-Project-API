using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories
{
    public class ProjectRepository
    {
        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();

        public Project GetProject(int id)
        {
            Project project = _context.Projects.Where(x => x.ProjectID == id).FirstOrDefault();

            if (project == null)
            {
                project = new Project();
            }
            return project;
        }
    }
}
