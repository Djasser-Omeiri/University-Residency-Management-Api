using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class Room
    {
        public enum eMode { AddNew, Update };
        eMode Mode = eMode.AddNew;

        public RoomDTO RDTO { get { return new RoomDTO(this.RoomID, this.Capacity, this.RoomNumber); } }

        public int RoomID { get; set; }
        public int Capacity { get; set; }
        public int RoomNumber { get; set; }

        public Room(RoomDTO RDTO, eMode Mode = eMode.AddNew)
        {
            this.RoomID = RDTO.RoomID;
            this.Capacity = RDTO.Capacity;
            this.RoomNumber = RDTO.RoomNumber;
            this.Mode = Mode;
        }

        private bool _AddRoom()
        {
            this.RoomID = RoomData.AddRoom(this.RDTO);
            return this.RoomID != -1;
        }

        private bool _UpdateRoom()
        {
            return RoomData.UpdateRoom(this.RDTO);
        }

        public static bool DeleteRoom(int RoomID)
        {
            return RoomData.DeleteRoom(RoomID);
        }

        public static Room GetRoomByID(int RoomID)
        {
            RoomDTO room = RoomData.GetRoomByID(RoomID);
            if (room != null)
                return new Room(room, eMode.Update);
            else
                return null;

        }

        public static List<RoomDTO> GetAllRooms()
        {
            return RoomData.GetAllRooms();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.AddNew:
                    if (_AddRoom())
                    {
                        Mode = eMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case eMode.Update:
                    return _UpdateRoom();
                default: return false;
            }
        }
    }
}
