using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccessLayer;

namespace UniversityResidencyApi.Controllers
{
    [Route("api/MealTypes")]
    [ApiController]
    public class MealTypeController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MealTypeDTO> AddMealType(MealTypeDTO newMealType)
        {
            if (newMealType is null)
            {
                return BadRequest("Meal type data is null.");
            }
            MealType mealType = new MealType(new MealTypeDTO(newMealType.MealTypeID, newMealType.Name, newMealType.StartTime, newMealType.EndTime));
            if (!mealType.Save())
                return BadRequest("Failed to create meal type.");

            newMealType.MealTypeID = mealType.MealTypeID;
            return CreatedAtAction(nameof(GetMealType), new { id = mealType.MealTypeID }, mealType.MDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MealTypeDTO> GetMealType(int id)
        {
            if (id < 1)
            {
                return BadRequest("The id is invalid.");
            }

            MealType mealType = MealType.GetMealTypeByID(id);
            if (mealType is null)
            {
                return NotFound("Meal type not found.");
            }
            MealTypeDTO mealTypeDTO = mealType.MDTO;

            return Ok(mealTypeDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MealTypeDTO>> GetAllMealTypes()
        {
            List<MealTypeDTO> mealTypes = MealType.GetAllMealTypes();
            if (mealTypes is null || mealTypes.Count == 0)
            {
                return NotFound("No meal types found.");
            }
            return Ok(mealTypes);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult UpdateMealType(int id, MealTypeDTO updatedMealType)
        {
            {
                if (updatedMealType is null || string.IsNullOrEmpty(updatedMealType.Name))
                {
                    return BadRequest("Invalid meal type data.");
                }
                MealType existingMealType = MealType.GetMealTypeByID(id);
                if (existingMealType is null)
                {
                    return NotFound("Meal type not found.");
                }
                existingMealType.Name = updatedMealType.Name;
                existingMealType.StartTime = updatedMealType.StartTime;
                existingMealType.EndTime = updatedMealType.EndTime;
                if (!existingMealType.Save())
                    return BadRequest("Failed to update meal type.");
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteMealType(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid meal type ID.");
            }
            MealType existingMealType = MealType.GetMealTypeByID(id);
            if (existingMealType is null)
            {
                return NotFound("Meal type not found.");
            }
            if (!MealType.DeleteMealType(id))
                return BadRequest("Failed to delete meal type.");
            return NoContent();
        }


    }
}
