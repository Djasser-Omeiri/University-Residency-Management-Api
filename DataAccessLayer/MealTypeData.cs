using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MealTypeDTO
    {
        public int MealTypeID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public MealTypeDTO(int MealTypeID, string Name, DateTime StartTime, DateTime EndTime)
        {
            this.MealTypeID = MealTypeID;
            this.Name = Name;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }
    }
    public class MealTypeData
    {
    }
}
