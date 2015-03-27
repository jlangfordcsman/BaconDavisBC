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
        private const string appRoot = "BaconDavisBC";
        private const string projects = "Projects";

        private const string activeProjectsXml = "ActiveProjects.xml";
        private const string employeesXml = "Employees.xml";
        private const string rolesXml = "Roles.xml";

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

        public static string ActiveProjectsXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.AppRoot, Paths.activeProjectsXml);

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                return path;
            }
        }

        public static string ProjectsFolder
        {
            get
            {
                string path = string.Format(@"{0}\{1}\", Paths.AppRoot, Paths.projects);

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
                File.Create(path);
            }

            return path;
        }

        public static string EmployeeXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.AppRoot, Paths.employeesXml);

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                return path;
            }
        }

        public static string RolesXml
        {
            get
            {
                string path = string.Format(@"{0}\{1}", Paths.AppRoot, Paths.rolesXml);

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                return path;
            }
        }
    }
}
