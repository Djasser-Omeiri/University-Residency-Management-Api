using System;
using System.Collections.Generic;
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
    }
}
