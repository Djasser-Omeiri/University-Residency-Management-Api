using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class MealType
    {
        public enum eMode { AddNew, Update };
        eMode Mode = eMode.AddNew;
        public int MealTypeID { get; set; }
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public MealTypeDTO MDTO { get { return new MealTypeDTO(this.MealTypeID, this.Name, this.StartTime, this.EndTime); } }
        public MealType(MealTypeDTO MDTO, eMode Mode = eMode.AddNew)
        {
            this.MealTypeID = MDTO.MealTypeID;
            this.Name = MDTO.Name;
            this.StartTime = MDTO.StartTime;
            this.EndTime = MDTO.EndTime;
            this.Mode = Mode;
        }

        private bool _AddMealType()
        {
            this.MealTypeID = MealTypeData.AddMealType(this.MDTO);
            return this.MealTypeID != 0;
        }
        private bool _UpdateMealType()
        {
            return MealTypeData.UpdateMealType(this.MDTO);
        }
        public static bool DeleteMealType(int MealTypeID)
        {
            return MealTypeData.DeleteMealType(MealTypeID);
        }
        public static List<MealTypeDTO> GetAllMealTypes()
        {
            return MealTypeData.GetAllMealTypes();
        }
        public static MealType GetMealTypeByID(int MealTypeID)
        {
            MealTypeDTO MDTO = MealTypeData.GetMealTypeByID(MealTypeID);
            if (MDTO != null)
                return new MealType(MDTO, eMode.Update);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case eMode.AddNew:
                    if (_AddMealType())
                    {
                        Mode = eMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case eMode.Update:
                    return _UpdateMealType();
                default:
                    return false;
            }
        }

    }
}
