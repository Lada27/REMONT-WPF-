using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH
{
    public class ProjectsClass
    {
        public ProjectsClass(int projectId, string projectName)
        {
            ProjectId = projectId;
            ProjectName = projectName;
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        
    }
}
