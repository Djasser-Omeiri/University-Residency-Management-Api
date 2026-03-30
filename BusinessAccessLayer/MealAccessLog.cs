using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class MealAccessLog
    {

        public int AccessID { get; set; }
        public int CardID { get; set; }
        public int MealTypeID { get; set; }
        public DateTime AccessTime { get; set; }
        public MealAccessLogDTO MalDTO { get { return new MealAccessLogDTO(this.AccessID, this.CardID, this.MealTypeID, this.AccessTime); } }

        public MealAccessLog(MealAccessLogDTO MalDTO)
        {
            this.AccessID = MalDTO.AccessID;
            this.CardID = MalDTO.CardID;
            this.MealTypeID = MalDTO.MealTypeID;
            this.AccessTime = MalDTO.AccessTime;
        }

        private bool _AddMealAccessLog()
        {
            this.AccessID = MealAccessLogData.AddMealAccessLog(this.MalDTO);
            return AccessID != -1;
        }

        public static List<MealAccessLogDTO> GetAllMealAccessLogs()
        {
            return MealAccessLogData.GetAllMealAccessLogs();
        }

        public bool Save()
        {
            return _AddMealAccessLog();
        }
    }
}
