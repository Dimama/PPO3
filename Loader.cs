using System.Collections.Generic;
using System.Xml;

namespace Lab3
{
    public abstract class Loader
    {
        public abstract List<Group> Load(string filename);
    }

    public class XMLLoader : Loader
    {
        public override List<Group> Load(string filename)
        {
            List<Group> Groups = new List<Group>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            foreach (XmlNode CurNode in doc.DocumentElement)
            {
                Group group = new Group();
                XmlElement GroupElement = (XmlElement)CurNode;
                XmlAttribute attr = GroupElement.GetAttributeNode("name");
                group.index = attr.InnerXml;

                var students = CurNode.ChildNodes.Item(0).ChildNodes;
                foreach (XmlNode node in students)
                {
                    Student student = new Student();
                    XmlElement StudentElement = (XmlElement)node;
                    student.Surname = StudentElement["surname"].InnerText;
                    student.Name = StudentElement["name"].InnerText;
                    student.Middlename = StudentElement["middleName"].InnerText;
                    student.Rating = int.Parse(StudentElement["rating"].InnerText);
                    if (StudentElement.Attributes["head"] != null)
                        student.Head = true;
                    else
                        student.Head = false;
                    group.AddStudent(student);
                }
                Groups.Add(group);
            }
            return Groups;
        }
    }
}
