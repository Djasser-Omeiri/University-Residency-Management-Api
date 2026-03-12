using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CardDTO
    {

        public int CardID { get; set; }
        public int StudentID { get; set; }
        public bool IsActive { get; set; }
        public string CardNumber { get; set; }
        public CardDTO(int CardID, int StudentID, bool IsActive, string CardNumber)
        {
            this.CardID = CardID;
            this.StudentID = StudentID;
            this.IsActive = IsActive;
            this.CardNumber = CardNumber;
        }
    }
    public class CardData
    {
        public static int AddCard(CardDTO CardDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddCard", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", CardDto.StudentID);
                command.Parameters.AddWithValue("@IsActive", CardDto.IsActive);
                command.Parameters.AddWithValue("@CardNumber", CardDto.CardNumber);
                var OutputVar = new SqlParameter("@CardID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(OutputVar);
                connection.Open();
                command.ExecuteNonQuery();
                return (int)OutputVar.Value;
            }
        }
        public static bool UpdateCard(CardDTO CardDto)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateCard", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CardID", CardDto.CardID);
                command.Parameters.AddWithValue("@StudentID", CardDto.StudentID);
                command.Parameters.AddWithValue("@CardNumber", CardDto.CardNumber);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
        }
        public static bool DeleteCard(int CardID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteCard", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CardID", CardID);
                int AffectedRows = (int)command.ExecuteScalar();
                return AffectedRows == 1;
            }
        }

        public static List<CardDTO> GetAllCards()
        {
            var CardList = new List<CardDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllCards", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CardList.Add(new CardDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("CardID")),
                                    reader.GetInt32(reader.GetOrdinal("StudentID")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.GetString(reader.GetOrdinal("CardNumber"))
                                ));

                        }
                    }
                }
                return CardList;
            }
        }

        public static CardDTO GetCardByID(int CardID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetCardByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardID", CardID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CardDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("CardID")),
                                    reader.GetInt32(reader.GetOrdinal("StudentID")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.GetString(reader.GetOrdinal("CardNumber"))
                                );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public static List<CardDTO> GetCardsByStudentID(int StudentID)
        {
            var CardList = new List<CardDTO>();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetCardsByStudentID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", StudentID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CardList.Add(new CardDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("CardID")),
                                    reader.GetInt32(reader.GetOrdinal("StudentID")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.GetString(reader.GetOrdinal("CardNumber"))
                                ));
                        }
                    }
                }
                return CardList;
            }
        }

        public static CardDTO GetCardByCardNumber(int CardNumber)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetCardByCardNumber", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CardNumber", CardNumber);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CardDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("CardID")),
                                reader.GetInt32(reader.GetOrdinal("StudentID")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                reader.GetString(reader.GetOrdinal("CardNumber"))
                            );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

    }
}
