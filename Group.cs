using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Group
    {
        public String index { set; get; }
        public List<Student> Students { set; get; }

        public Group()
        {
            Students = new List<Student>(); 
        }

        public Group(Group group)
        {
            index = group.index;
            Students = new List<Student>();
            foreach (Student student in group.Students)
                Students.Add(student);
        }

        public int StudentsCount()
        {
            return Students.Count;
        }

        public String GetHead()
        {
            String res = "None";
            foreach (Student student in Students)
                if (student.Head)
                    res = student.Surname;
            return res;
        }
        public int MaxRating()
        {
            if (StudentsCount() == 0)
                return 0;
            int max = Students[0].Rating;
            foreach(Student student in Students)
            {
                if (student.Rating > max)
                    max = student.Rating;
            }
            return max;
        }

        public int MinRating()
        {
            if (StudentsCount() == 0)
                return 0;
            int min = Students[0].Rating;
            foreach (Student student in Students)
            {
                if (student.Rating < min)
                    min = student.Rating;
            }
            return min;
        }

        public float AverageRating()
        {
            if (StudentsCount() == 0)
                return 0;

            float SumRating = 0;
            foreach (Student student in Students)
                SumRating += student.Rating;
            return SumRating / StudentsCount();
        }

        public void DeleteStudent(int num)
        {
            Students.Remove(Students[num]);
        }

        public void AddStudent(Student Std)
        {
            Students.Add(Std);
        }
    }
}
