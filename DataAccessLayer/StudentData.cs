using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int RoomID { get; set; }

        public StudentDTO(int StudentID, string FirstName, string LastName, int Age, int RoomID)
        {
            this.StudentID = StudentID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Age = Age;
            this.RoomID = RoomID;
        }
    }
    public class StudentData
    {

    }
}
