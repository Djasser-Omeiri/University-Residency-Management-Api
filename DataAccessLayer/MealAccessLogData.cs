using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MealAccessLogDTO
    {
        public int AccessID { get; set; }
        public int CardID { get; set; }
        public int MealTypeID { get; set; }
        public DateTime AccessTime { get; set; }
        public MealAccessLogDTO(int AccessID, int CardID, int MealTypeID, DateTime AccessTime)
        {
            this.AccessID = AccessID;
            this.CardID = CardID;
            this.MealTypeID = MealTypeID;
            this.AccessTime = AccessTime;
        }
    }
    public class MealAccessLogData
    {
        public static int AddMealAccessLog(MealAccessLogDTO mealAccessLogDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddMealAccess", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CardID", mealAccessLogDto.CardID);
                command.Parameters.AddWithValue("@MealTypeID", mealAccessLogDto.MealTypeID);
                command.Parameters.AddWithValue("@AccessTime", mealAccessLogDto.AccessTime);
                var ReturnedValue = new SqlParameter("@ReturnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(ReturnedValue);
                connection.Open();
                command.ExecuteNonQuery();
                return (int)ReturnedValue.Value;
            }
        }
        public static List<MealAccessLogDTO> GetAllMealAccessLogs()
        {
            List<MealAccessLogDTO> MealAccessLogsList = new List<MealAccessLogDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllMealAccessLogs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MealAccessLogsList.Add(new MealAccessLogDTO(
                                reader.GetInt32(reader.GetOrdinal("AccessID")),
                                reader.GetInt32(reader.GetOrdinal("CardID")),
                                reader.GetInt32(reader.GetOrdinal("MealTypeID")),
                                reader.GetDateTime(reader.GetOrdinal("AccessTime"))
                            ));
                        }
                    }
                }
                return MealAccessLogsList;
            }
        }
    }
}
