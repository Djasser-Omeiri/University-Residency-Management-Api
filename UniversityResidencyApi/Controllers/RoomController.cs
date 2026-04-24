using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccessLayer;
using DataAccessLayer;

namespace UniversityResidencyApi.Controllers
{
    [Route("api/Rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoomDTO> AddRoom(RoomDTO newRoom)
        {
            if (newRoom is null || newRoom.Capacity <= 0 || newRoom.RoomNumber <= 0)
            {
                return BadRequest("Room data is invalid.");
            }
            Room room = new Room(new RoomDTO(newRoom.RoomID, newRoom.Capacity, newRoom.RoomNumber));
            if (!room.Save())
            {
                return StatusCode(500, "Failed to add the room");
            }
            newRoom.RoomID = room.RoomID;
            return CreatedAtAction(nameof(GetRoom), new { id = newRoom.RoomID }, newRoom);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoomDTO> GetRoom(int id)
        {
            if (id < 1)
                return BadRequest("Invalid room ID.");

            Room room = Room.GetRoomByID(id);

            if (room == null)
                return NotFound("Room not found.");

            RoomDTO RoomDTO = room.RDTO;
            return Ok(RoomDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoomDTO>> GetAll() {
            List<RoomDTO> roomDto = Room.GetAllRooms();
            if (roomDto is null || roomDto.Count()==0)
            {
                return NotFound("No Rooms Found.");
            }
            return Ok(roomDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateRoom(int id, RoomDTO updatedRoom)
        {
            if (updatedRoom is null || updatedRoom.Capacity <= 0 || updatedRoom.RoomNumber <= 0)
            {
                return BadRequest("Room data is invalid.");
            }
            Room room = Room.GetRoomByID(id);
            if (room is null)
            {
                return NotFound();
            }
            room.Capacity = updatedRoom.Capacity;
            room.RoomNumber = updatedRoom.RoomNumber;
            if (!room.Save())
            {
                return StatusCode(500, "Failed to update the room");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteRoom(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid room ID.");
            }
            Room room = Room.GetRoomByID(id);
            if (room is null)
            {
                return NotFound();
            }
            if (!Room.DeleteRoom(id))
            {
                return StatusCode(500, "Failed to delete the room");
            }
            return NoContent();
        }


    }
}
