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
        private static ICollection<Project> activeProjects;

        public ICollection<Project> ActiveProjects
        {
            get
            {
                if (activeProjects == null)
                {
                    activeProjects = new List<Project>();

                    using (var file = new FileStream(Paths.ActiveProjectsXml, FileMode.Open))
                    {
                        using (var reader = XmlReader.Create(file))
                        {

                            try
                            {
                                if (reader.IsStartElement("ActiveProjects"))
                                {
                                    reader.ReadStartElement("ActiveProjects");

                                    while (reader.IsStartElement("Project"))
                                    {
                                        activeProjects.Add(GetProject(Guid.Parse(reader.GetAttribute("Id"))));
                                        reader.ReadStartElement("Project");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // todo: figure out something else to do here rather than swallowing an exception
                            }
                        }
                    }
                }

                return activeProjects;
            }
        }


        public Project GetProject(Guid id)
        {
            Project project = new Project();

            using (var file = new FileStream(Paths.GetProjectXmlFile(id), FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(file))
                {
                    project.ReadFromXml(reader);
                }
            }

            return project;
        }

        public void SaveActiveProjects()
        {
            using (var file = new FileStream(Paths.ActiveProjectsXml, FileMode.Truncate))
            {
                using (var writer = XmlWriter.Create(file))
                {

                    writer.WriteStartElement("ActiveProjects");

                    foreach (var project in this.ActiveProjects)
                    {
                        this.SaveProject(project);

                        writer.WriteStartElement("Project");
                        writer.WriteAttributeString("Id", project.Id.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
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
                using (var writer = XmlWriter.Create(file))
                {
                    project.WriteToXml(writer);
                }
            }
        }

        public Project GetSelectedProject()
        {
            Guid? selectedProjectId = null;

            Project project = null;

            using (var file = new FileStream(Paths.SelectedProjectXml, FileMode.Open))
            {
                using (var reader = XmlReader.Create(file))
                {

                    try
                    {
                        reader.MoveToContent();
                        if (reader.IsStartElement("SelectedProject"))
                        {
                            selectedProjectId = Guid.Parse(reader.GetAttribute("Id"));
                            reader.ReadStartElement("SelectedProject");
                        }
                    }
                    catch(Exception ex)
                    {
                        project = AddProject("Unnamed Project");
                    }
                }
            }

            if (project == null)
            {
                project = ActiveProjects.FirstOrDefault(p => p.Id == selectedProjectId);
            }
            else
            {
                SetSelectedProject(project);
            }

            return project;
        }

        public void SetSelectedProject(Project project)
        {
            using (var file = new FileStream(Paths.SelectedProjectXml, FileMode.Truncate))
            {
                using (var writer = XmlWriter.Create(file))
                {
                    writer.WriteStartElement("SelectedProject");
                    writer.WriteAttributeString("Id", project.Id.ToString());
                    writer.WriteEndElement();
                }
            }
        }

        public Project AddProject(string name)
        {
            Project project = new Project()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            ActiveProjects.Add(project);

            SaveActiveProjects();

            return project;
        }
    }
}
