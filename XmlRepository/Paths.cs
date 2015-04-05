using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlRepository
{
    public static class Paths
    {
        private static readonly string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private const string appRoot = "BillingsConcrete";

        private const string baconDavisRoot = "BaconDavis";
        private const string employeesRoot = "Employees";

        private const string projects = "Projects";

        private const string activeProjectsXml = "ActiveProjects.xml";
        private const string activeEmployeesXml = "ActiveEmployees.xml";

        private const string rolesXml = "Roles.xml";
        private const string selectedProjectXml = "SelectedProject.xml";

        public static string AppRoot
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.root, Paths.appRoot);


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public static string BaconDavisRoot
        {
            get
            {
                string path = string.Format(@"{0}\{1}\", Paths.AppRoot, Paths.baconDavisRoot);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public static string EmployeesRoot
        {
            get
            {
                string path = string.Format(@"{0}\{1}\", Paths.AppRoot, Paths.employeesRoot);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public static string ActiveProjectsXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.BaconDavisRoot, Paths.activeProjectsXml);

                if (!File.Exists(path))
                {
                    using (File.Create(path)) { };
                }

                return path;
            }
        }

        public static string ProjectsFolder
        {
            get
            {
                string path = string.Format(@"{0}\{1}\", Paths.BaconDavisRoot, Paths.projects);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public static string GetProjectXmlFile(Project project)
        {
            return GetProjectXmlFile(project.Id);
        }

        public static string GetProjectXmlFile(Guid id)
        {
            string path = string.Format(@"{0}\{1}.xml", Paths.ProjectsFolder, id.ToString());

            if (!File.Exists(path))
            {
                using (File.Create(path)) { };
            }

            return path;
        }

        public static string GetEmployeeXmlFile(Guid id)
        {
            string path = string.Format(@"{0}\{1}.xml", Paths.EmployeesRoot, id.ToString());

            if (!File.Exists(path))
            {
                using (File.Create(path)) { };
            }

            return path;
        }

        public static string EmployeeXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.EmployeesRoot, Paths.activeEmployeesXml);

                if (!File.Exists(path))
                {
                    using (File.Create(path)) { }
                }

                return path;
            }
        }

        public static string RolesXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.BaconDavisRoot, Paths.rolesXml);

                if (!File.Exists(path))
                {
                    using (File.Create(path)) { }
                }

                return path;
            }
        }

        public static string SelectedProjectXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.BaconDavisRoot, Paths.selectedProjectXml);

                if (!File.Exists(path))
                {
                    using (File.Create(path)) { };
                }

                return path;
            }
        }
    }
}
