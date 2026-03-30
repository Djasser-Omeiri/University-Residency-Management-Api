using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class Card
    {
        public enum eMode { AddNew, Update }
        eMode Mode = eMode.AddNew;
        public int CardID { get; set; }
        public int StudentID { get; set; }
        public bool IsActive { get; set; }
        public string CardNumber { get; set; }

        public CardDTO CDTO
        { get { return new CardDTO(this.CardID, this.StudentID, this.IsActive, this.CardNumber); } }

        public Card(CardDTO cardDTO, eMode mode = eMode.AddNew)
        {
            this.CardID = cardDTO.CardID;
            this.StudentID = cardDTO.StudentID;
            this.IsActive = cardDTO.IsActive;
            this.CardNumber = cardDTO.CardNumber;
            this.Mode = mode;
        }

        private bool _AddCard()
        {
            this.CardID = CardData.AddCard(this.CDTO);
            return this.CardID != -1;
        }

        private bool _UpdateCard()
        {
            return CardData.UpdateCard(this.CDTO);
        }
        public static bool DeleteCard(int CardID)
        {
            return CardData.DeleteCard(CardID);
        }

        public static List<CardDTO> GetAllCards()
        {
            return CardData.GetAllCards();
        }

        public static Card GetCardByCardID(int CardID)
        {
            CardDTO cardDTO = CardData.GetCardByID(CardID);
            if (cardDTO != null)
            {
                return new Card(cardDTO, eMode.Update);
            }
            else
            {
                return null;
            }
        }

        public static List<CardDTO> GetCardByStudentID(int StudentID)
        {
            return CardData.GetCardsByStudentID(StudentID);
        }

        public static Card GetCardByCardNumber(string CardNumber)
        {
            CardDTO cardDTO = CardData.GetCardByCardNumber(CardNumber);
            if (cardDTO != null)
            {
                return new Card(cardDTO, eMode.Update);
            }
            else
            {
                return null;
            }
        }


        public bool Save()
        {
            switch (Mode)
            {
                case eMode.AddNew:
                    if (_AddCard())
                    {
                        Mode = eMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case eMode.Update:
                    return _UpdateCard();
                default:
                    return false;
            }
        }


    }
}
