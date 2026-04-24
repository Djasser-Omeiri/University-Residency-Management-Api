using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public int Capacity { get; set; }
        public int RoomNumber { get; set; }

        public RoomDTO(int RoomID, int Capacity, int RoomNumber)
        {
            this.RoomID = RoomID;
            this.Capacity = Capacity;
            this.RoomNumber = RoomNumber;
        }
    }
    public class RoomData
    {
        public static int AddRoom(RoomDTO RoomDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddRoom", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Capacity", RoomDto.Capacity);
                command.Parameters.AddWithValue("@RoomNumber", RoomDto.RoomNumber);
                var returnParameter = new SqlParameter("@RoomID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnParameter);
                connection.Open();
                command.ExecuteNonQuery();
                return (int)returnParameter.Value;
            }
        }

        public static bool DeleteRoom(int RoomID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteRoom", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoomID", RoomID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected == 1;
            }
        }
        public static List<RoomDTO> GetAllRooms()
        {
            List<RoomDTO> RoomsList = new List<RoomDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllRooms", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoomsList.Add(new RoomDTO(
                                reader.GetInt32(reader.GetOrdinal("RoomID")),
                                reader.GetInt32(reader.GetOrdinal("Capacity")),
                                reader.GetInt32(reader.GetOrdinal("RoomNumber"))
                            ));

                        }
                    }
                }
                return RoomsList;
            }
        }
        public static RoomDTO GetRoomByID(int RoomID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetRoomByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoomID", RoomID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new RoomDTO(
                            reader.GetInt32(reader.GetOrdinal("RoomID")),
                            reader.GetInt32(reader.GetOrdinal("Capacity")),
                            reader.GetInt32(reader.GetOrdinal("RoomNumber"))
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static bool UpdateRoom(RoomDTO RoomDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateRoom", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoomID", RoomDto.RoomID);
                command.Parameters.AddWithValue("@Capacity", RoomDto.Capacity);
                command.Parameters.AddWithValue("@RoomNumber", RoomDto.RoomNumber);
                connection.Open();
                int AffectedRows=command.ExecuteNonQuery();
                return AffectedRows>0;
            }
        }


    }
}
