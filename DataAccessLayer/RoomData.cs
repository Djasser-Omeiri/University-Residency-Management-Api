using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomDTO
    {
        int RoomID { get; set; }
        int Capacity { get; set; }
        int RoomNumber { get; set; }

        public RoomDTO(int RoomID, int Capacity, int RoomNumber)
        {
            this.RoomID = RoomID;
            this.Capacity = Capacity;
            this.RoomNumber = RoomNumber;
        }
    }
    public class RoomData
    {
    }
}
