using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static int AddStudent(StudentDTO StudentDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", StudentDto.FirstName);
                command.Parameters.AddWithValue("@LastName", StudentDto.LastName);
                command.Parameters.AddWithValue("@Age", StudentDto.Age);
                command.Parameters.AddWithValue("@RoomID", StudentDto.RoomID);

                var OutputParameter = new SqlParameter("@StudentID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(OutputParameter);
                connection.Open();
                command.ExecuteNonQuery();
                return (int)OutputParameter.Value;
            }
        }
        public static bool UpdateStudent(StudentDTO StudentDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", StudentDto.StudentID);
                command.Parameters.AddWithValue("@FirstName", StudentDto.FirstName);
                command.Parameters.AddWithValue("@LastName", StudentDto.LastName);
                command.Parameters.AddWithValue("@Age", StudentDto.Age);
                command.Parameters.AddWithValue("@RoomID", StudentDto.RoomID);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
        }
        public static bool DeleteStudent(int StudentID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", StudentID);
                connection.Open();
                int AffectedRows = (int)command.ExecuteScalar();
                return AffectedRows == 1;
            }
        }
        public static List<StudentDTO> GetAllStudents()
        {
            var StudentsList = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("StudentID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("RoomID"))
                                ));
                        }
                    }
                }
                return StudentsList;
            }
        }
        public static StudentDTO GetStudentByID(int StudentID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetStudentByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", StudentID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new StudentDTO(
                            reader.GetInt32(reader.GetOrdinal("StudentID")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetInt32(reader.GetOrdinal("Age")),
                            reader.GetInt32(reader.GetOrdinal("RoomID"))
                            );
                    }
                    else
                    {
                        return null;
                    }
                }
            }

        }
        public static List<StudentDTO> GetStudentsByRoomID(int RoomID)
        {
            var StudentsList = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetStudentsByRoomID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoomID", RoomID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StudentsList.Add(new StudentDTO(
                            reader.GetInt32(reader.GetOrdinal("StudentID")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetInt32(reader.GetOrdinal("Age")),
                            reader.GetInt32(reader.GetOrdinal("RoomID"))
                            ));
                    }
                }
                return StudentsList;
            }
        }
    }
}
