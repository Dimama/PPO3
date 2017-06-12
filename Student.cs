using System;


namespace Lab3
{
    public class Student
    {        
        public String Surname { get; set; }
        public String Name { get; set; }
        public String Middlename { get; set; }
        public int Rating { get; set; }
        public Boolean Head { get; set; }

        public Student() { }

        public Student(String S_name, String _Name, String M_name, int R, Boolean _Head)
        {
            Surname = S_name;
            Name = _Name;
            Middlename = M_name;
            Rating = R;
            Head = _Head;
        }

        public Student(Student student)
        {
            Surname = student.Surname;
            Name = student.Name;
            Middlename = student.Middlename;
            Rating = student.Rating;
            Head = student.Head;
        }
    }
}
