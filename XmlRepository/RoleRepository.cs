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
    public class RoleRepository
    {
        private static ICollection<EmployeeRole> roles;
        private static RoleRepository defaultRepository;

        internal static RoleRepository Default
        {
            get
            {
                if (defaultRepository == null)
                {
                    defaultRepository = new RoleRepository();
                }

                return defaultRepository;
            }
        }

        public ICollection<EmployeeRole> Roles
        {
            get
            {
                if (roles == null)
                {
                    roles = new List<EmployeeRole>();
                    LoadRolesFromXml();
                }

                return roles;
            }
        }

        public EmployeeRole CreateRole(string name)
        {
            var role = new EmployeeRole()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            roles.Add(role);
            SaveRolesToXml();

            return role;
        }

        public void LoadRolesFromXml()
        {
            using (var file = new FileStream(Paths.RolesXml, FileMode.Open))
            {
                if (file.Length > 0)
                {
                    using (var reader = XmlReader.Create(file))
                    {

                        reader.MoveToContent();

                        reader.ReadStartElement("Roles");

                        while (reader.IsStartElement("Role"))
                        {
                            var role = new EmployeeRole();
                            role.ReadFromXml(reader);
                        }

                        reader.ReadEndElement();
                    }
                }
            }
        }

        public void SaveRolesToXml()
        {
            using (var file = new FileStream(Paths.RolesXml, FileMode.Truncate))
            {
                using (var writer = XmlWriter.Create(file))
                {
                    writer.WriteStartElement("Roles");

                    foreach (var role in this.Roles)
                    {
                        role.WriteToXml(writer);
                    }

                    writer.WriteEndElement();
                }
            }
        }
    }
}

