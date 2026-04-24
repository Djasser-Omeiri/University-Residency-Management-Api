using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MealTypeDTO
    {
        public int MealTypeID { get; set; }
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public MealTypeDTO(int MealTypeID, string Name, TimeOnly StartTime, TimeOnly EndTime)
        {
            this.MealTypeID = MealTypeID;
            this.Name = Name;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }
    }
    public class MealTypeData
    {
        public static int AddMealType(MealTypeDTO mealAccessLogDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddMealType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", mealAccessLogDto.Name);
                command.Parameters.AddWithValue("@StartTime", mealAccessLogDto.StartTime);
                command.Parameters.AddWithValue("@EndTime", mealAccessLogDto.EndTime);
                var ReturnedValue = new SqlParameter("@MealTypeID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(ReturnedValue);
                connection.Open();
                command.ExecuteNonQuery();
                return (int)ReturnedValue.Value;
            }
        }

        public static bool DeleteMealType(int MealTypeID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteMealType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MealTypeID", MealTypeID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected == 1;
            }
        }

        public static List<MealTypeDTO> GetAllMealTypes()
        {
            List<MealTypeDTO> MealTypesList = new List<MealTypeDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllMealTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MealTypesList.Add(new MealTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("MealTypeID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("StartTime"))),
                                TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("EndTime")))
                            ));
                        }
                    }
                }
                return MealTypesList;
            }
        }

        public static MealTypeDTO GetMealTypeByID(int MealTypeID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetMealTypeByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MealTypeID", MealTypeID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new MealTypeDTO(
                            reader.GetInt32(reader.GetOrdinal("MealTypeID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("StartTime"))),
                            TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("EndTime")))
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static bool UpdateMealType(MealTypeDTO mealTypeDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateMealType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MealTypeID", mealTypeDto.MealTypeID);
                command.Parameters.AddWithValue("@Name", mealTypeDto.Name);
                command.Parameters.AddWithValue("@StartTime", mealTypeDto.StartTime);
                command.Parameters.AddWithValue("@EndTime", mealTypeDto.EndTime);
                connection.Open();
                int AffectedRows = command.ExecuteNonQuery();
                return AffectedRows > 0;
            }
        }
    }
}
