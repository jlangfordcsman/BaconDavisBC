using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    public class ProjectRepository
    {
        private ICollection<Project> activeProjects;

        public ICollection<Project> ActiveProjects
        {
            get
            {
                if (activeProjects == null)
                {
                    activeProjects = new List<Project>();

                    using (var file = new FileStream(Paths.ActiveProjectsXml, FileMode.Open))
                    {
                        var reader = XmlReader.Create(file);

                        reader.ReadStartElement("ActiveProjects");

                        while (reader.IsStartElement("Project"))
                        {
                            reader.ReadStartElement("Project");

                            activeProjects.Add(LoadProjectFromXml(Guid.Parse(reader.GetAttribute("Id"));

                            reader.ReadEndElement();
                        }

                        reader.ReadEndElement();
                    }
                }

                return activeProjects;
            }
        }

        public Project LoadProjectFromXml(Guid id)
        {
            Project project = new Project();

            using (var file = new FileStream(Paths.GetProjectXmlFile(id), FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(file);

                reader.ReadStartElement("Project");

                project.Id = Guid.Parse(reader.GetAttribute("Id"));
                project.Name = reader.GetAttribute("Name");

                reader.ReadStartElement("Periods");

                while (reader.IsStartElement("Period"))
                {
                    project.Periods.Add(PeriodFromXml(reader));
                }

                reader.ReadEndElement();

                reader.ReadEndElement();
            }

            return project;
        }

        public void SaveActiveProjects(ICollection<Project> projects)
        {
            using (var file = new FileStream(Paths.ActiveProjectsXml, FileMode.Truncate))
            {
                var writer = XmlWriter.Create(file);

                writer.WriteStartElement("ActiveProjects");

                foreach (var project in projects)
                {
                    this.SaveProject(project);

                    writer.WriteStartElement("Project");
                    writer.WriteAttributeString("Id", project.Id.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        public void SaveProject(Project project)
        {
            if (project.Id.Equals(Guid.Empty))
            {
                project.Id = Guid.NewGuid();
            }

            using (var file = new FileStream(Paths.GetProjectXmlFile(project), FileMode.Truncate))
            {
                var writer = XmlWriter.Create(file);

                writer.WriteStartElement("Project");

                writer.WriteAttributeString("Id", project.Id.ToString());
                writer.WriteAttributeString("Name", project.Name);

                writer.WriteStartElement("Periods");

                foreach (var period in project.Periods)
                {
                    PeriodToXml(period, writer);
                }

                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void PeriodToXml(Period period, XmlWriter writer)
        {
            writer.WriteStartElement("Period");

            if (period.Start.HasValue)
            {
                writer.WriteAttributeString("Start", period.Start.ToString());
            }

            if (period.End.HasValue)
            {
                writer.WriteAttributeString("End", period.End.ToString());
            }

            writer.WriteStartElement("EmployeesInRoles");

            foreach (var employeeInRole in period.EmployeesInRoles)
            {
                EmployeeInRoleToXml(employeeInRole, writer);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private Period PeriodFromXml(XmlReader reader)
        {
            Period period = new Period();

            reader.ReadStartElement("Period");

            string buf = reader.GetAttribute("Start");

            if(!string.IsNullOrEmpty(buf))
            {
                period.Start = DateTime.Parse(buf);
            }

            buf = reader.GetAttribute("End");

            if (!string.IsNullOrEmpty(buf))
            {
                period.End = DateTime.Parse(buf);
            }
            reader.ReadEndElement();

            return period;
        }

        private void EmployeeInRoleToXml(EmployeeInRole employeeInRole, XmlWriter writer)
        {
            writer.WriteStartElement("EmployeeInRole");

            writer.WriteStartElement("Employee");

            writer.WriteAttributeString("Id", employeeInRole.Employee.Id.ToString());
            writer.WriteAttributeString("Name", employeeInRole.Employee.Name);

            writer.WriteEndElement();

            writer.WriteStartElement("Role");
            writer.WriteAttributeString("Id", employeeInRole.Role.Id.ToString());
            writer.WriteAttributeString("Name", employeeInRole.Role.Name);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
