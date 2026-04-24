using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccessLayer;

namespace UniversityResidencyApi.Controllers
{
    [Route("api/Cards")]
    [ApiController]
    public class CardController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CardDTO> AddCard(CardDTO newCard)
        {
            if (newCard == null || string.IsNullOrWhiteSpace(newCard.CardNumber) || newCard.StudentID < 1)
            {
                return BadRequest("Invalid Card data");
            }

            Card card = new Card(new CardDTO(newCard.CardID, newCard.StudentID, newCard.IsActive, newCard.CardNumber));
            if (!card.Save())
            {
                return StatusCode(500, "Failed to add Card");
            }
            newCard.CardID = card.CardID;

            return CreatedAtAction(nameof(GetByCardID), new { CardID = newCard.CardID }, newCard);
        }

        [HttpGet("{CardID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CardDTO> GetByCardID(int CardID)
        {
            if (CardID < 1)
            {
                return BadRequest("The card id is invalid.");
            }

            Card card = Card.GetCardByCardID(CardID);
            if (card == null)
            {
                return NotFound();
            }
            CardDTO cardDTO = card.CDTO;

            return Ok(cardDTO);
        }

        [HttpGet("ByStudentID{StudentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<CardDTO> GetByStudentID(int StudentID)
        {
            if (StudentID < 1)
            {
                return BadRequest("The student id is invalid.");
            }
            List<CardDTO> cardDto = Card.GetCardByStudentID(StudentID);
            if (cardDto == null)
            {
                return NotFound();
            }
            return Ok(cardDto);
        }

        [HttpGet("ByCardNumber{CardNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<CardDTO> GetByCardNumber(string CardNumber)
        {
            if (string.IsNullOrEmpty(CardNumber))
            {
                return BadRequest("The card number is invalid.");
            }
            Card card = Card.GetCardByCardNumber(CardNumber);
            if (card == null)
            {
                return NotFound();
            }
            CardDTO cardDTO = card.CDTO;
            return Ok(cardDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CardDTO>> GetAllCards()
        {
            List<CardDTO> cardDtos = Card.GetAllCards();
            if (cardDtos == null || cardDtos.Count == 0)
            {
                return NotFound("No Cards Found");
            }
            return Ok(cardDtos);
        }

        [HttpPut("{CardID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateCard(int CardID, CardDTO updatedCard)
        {
            if (CardID < 1 || updatedCard == null || CardID != updatedCard.CardID)
            {
                return BadRequest("Invalid Card data");
            }
            Card existingCard = Card.GetCardByCardID(CardID);
            if (existingCard == null)
            {
                return NotFound();
            }
            existingCard.StudentID = updatedCard.StudentID;
            existingCard.IsActive = updatedCard.IsActive;
            existingCard.CardNumber = updatedCard.CardNumber;
            if (!existingCard.Save())
            {
                return StatusCode(500, "Failed to update Card");
            }
            return NoContent();
        }

        [HttpDelete("{CardID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCard(int CardID)
        {
            if (CardID < 1)
            {
                return BadRequest("The card id is invalid.");
            }
            if (Card.DeleteCard(CardID))
            {
                return NoContent();
            }
            else
            {
                return NotFound("The card was not found.");
            }
        }


    }

}
