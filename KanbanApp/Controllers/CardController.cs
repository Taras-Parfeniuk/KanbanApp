using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using KanbanApp.Common.DTO;
using KanbanApp.BLL.Abstract;

namespace KanbanApp.Controllers
{
    [Produces("application/json")]
    [Route("api/card")]
    public class CardController : Controller
    {
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [Route("")]
        [HttpPost]
        public async Task<CardDTO> CreateCard([FromBody] CardDTO card)
        {
            return await _cardService.CreateCardAsync(card);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task UpdateCard(long id, [FromBody] CardDTO card)
        {
            await _cardService.UpdateCardAsync(card);
        }

        [Route("{id}/move")]
        [HttpPut]
        public async Task MoveCard(long id, [FromBody] CardDTO card)
        {
            await _cardService.MoveCardAsync(card);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task RemoveCard(long id)
        {
            await _cardService.RemoveCardAsync(id);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<CardDTO> GetCard(long id)
        {
            return await _cardService.GetCardByIdAsync(id); 
        }

        private readonly ICardService _cardService;
    }
}