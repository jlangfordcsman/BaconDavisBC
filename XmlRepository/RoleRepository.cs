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
                var reader = XmlReader.Create(file);

                reader.ReadStartElement("Roles");

                while (reader.IsStartElement("Role"))
                {
                    var role = new EmployeeRole();

                    reader.ReadStartElement("Role");

                    role.Id = Guid.Parse(reader.GetAttribute("Id"));
                    role.Name = reader.GetAttribute("Name");

                    reader.ReadEndElement();
                }
                reader.ReadEndElement();
            }
        }

        public void SaveRolesToXml()
        {
            using (var file = new FileStream(Paths.RolesXml, FileMode.Truncate))
            {
                var writer = XmlWriter.Create(file);

                writer.WriteStartElement("Roles");

                foreach (var role in this.Roles)
                {
                    writer.WriteStartElement("Role");

                    writer.WriteAttributeString("Id", role.Id.ToString());
                    writer.WriteAttributeString("Name", role.Name);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
    }
}

