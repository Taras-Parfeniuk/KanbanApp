using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using KanbanApp.Common.DTO;
using KanbanApp.BLL.Abstract;
using System.Linq;

namespace KanbanApp.Controllers
{
    [Produces("application/json")]
    [Route("api/cardlist")]
    public class CardListController : Controller
    {
        public CardListController(ICardService cardService, ICardListService cardListService)
        {
            _cardService = cardService;
            _cardListService = cardListService;
        }

        [Route("")]
        [HttpPost]
        public async Task<CardListDTO> CreateCardList([FromBody] CardListDTO cardlist)
        {
            return await _cardListService.CreateListAsync(cardlist);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task UpdateCardList(long id, [FromBody] CardListDTO cardlist)
        {
            await _cardListService.UpdateListAsync(cardlist);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task RemoveCardList(long id)
        {
            await _cardListService.RemoveListAsync(id);
        }

        [Route("")]
        [HttpGet]
        public async Task<List<long>> GetAllCardLists()
        {
            var lists = await _cardListService.GetCardListsAsync();

            return lists.Select(l => l.Id).ToList();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<CardListDTO> GetCardList(long id)
        {
            return await _cardListService.GetListByIdAsync(id);
        }

        [Route("{id}/cards")]
        [HttpGet]
        public async Task<List<long>> GetCards(long id)
        {
            var cards = await _cardService.GetCardsByListIdAsync(id);

            return cards.Select(c => c.Id).ToList();
        }

        private readonly ICardService _cardService;
        private readonly ICardListService _cardListService;
    }
}