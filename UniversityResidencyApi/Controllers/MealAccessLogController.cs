using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using BusinessAccessLayer;

namespace UniversityResidencyApi.Controllers
{
    [Route("api/MealAccessLogs")]
    [ApiController]
    public class MealAccessLogController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MealAccessLogDTO> AddMealAccessLog(MealAccessLogDTO newMealAccessLog)
        {
            if (newMealAccessLog is null)
            {
                return BadRequest("Meal Access Log is null.");
            }
            MealAccessLog mealAccessLog = new MealAccessLog(new MealAccessLogDTO(newMealAccessLog.AccessID, newMealAccessLog.CardID, newMealAccessLog.MealTypeID, newMealAccessLog.AccessTime));
            if (!mealAccessLog.Save())
                return StatusCode(500, "An error occurred while saving the meal access log.");

            newMealAccessLog.AccessID = mealAccessLog.AccessID;

            return Ok(newMealAccessLog);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<MealAccessLogDTO>> GetAllMealAccessLogs()
        {
            var mealAccessLogs = MealAccessLog.GetAllMealAccessLogs();
            if (mealAccessLogs is null)
            {
                return NotFound("No meal access logs found.");
            }
            return Ok(mealAccessLogs);
        }
    }
}
