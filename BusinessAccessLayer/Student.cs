using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class Student
    {
        public enum eMode { AddNew, Update };
        public eMode Mode = eMode.AddNew;
        public StudentDTO SDTO { get { return new StudentDTO(this.StudentID, this.FirstName, this.LastName, this.Age, this.RoomID); } }

        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int RoomID { get; set; }
        public Student(StudentDTO SDTO, eMode Mode = eMode.AddNew)
        {
            this.StudentID = SDTO.StudentID;
            this.FirstName = SDTO.FirstName;
            this.LastName = SDTO.LastName;
            this.Age = SDTO.Age;
            this.RoomID = SDTO.RoomID;
            this.Mode = Mode;
        }
        private bool _AddStudent()
        {
            this.StudentID = StudentData.AddStudent(this.SDTO);
            return this.StudentID != -1;
        }
        private bool _UpdateStudent()
        {
            return StudentData.UpdateStudent(this.SDTO);
        }

        public static List<StudentDTO> GetAllStudents()
        {
            return StudentData.GetAllStudents();
        }
        public static Student GetStudentByID(int StudentID)
        {
            StudentDTO SDTO = StudentData.GetStudentByID(StudentID);
            if (SDTO != null)
                return new Student(SDTO, eMode.Update);
            else
                return null;
        }
        public static bool DeleteStudent(int StudentID)
        {
            return StudentData.DeleteStudent(StudentID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case eMode.AddNew:
                    if (_AddStudent())
                    {
                        Mode = eMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case eMode.Update:
                    return _UpdateStudent();
                default: return false;
            }

        }
    }
}
