using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using KanbanApp.BLL.Abstract;
using KanbanApp.Common.Entities;
using KanbanApp.DAL.Abstract;
using KanbanApp.Common.DTO;

namespace KanbanApp.BLL.Services
{
    public class CardService : ICardService
    {
        public CardService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<CardDTO> CreateCardAsync(CardDTO card)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var cardList = await unitOfWork.CardListRepository.GetByIdAsync(card.ListId);

            var entity = new Card
            {
                CardList = cardList,
                Title = card.Title,
                Description = string.Empty,
                Position = card.Position
            };

           unitOfWork.CardRepository.Create(entity);

            await unitOfWork.SaveAsync();

            card.Id = entity.Id;

            return card;
        }

        public async Task<CardDTO> GetCardByIdAsync(long id)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var entity = await unitOfWork.CardRepository.Query
                .Include(e => e.CardList)
                .FirstOrDefaultAsync(e => e.Id == id);

            return new CardDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ListId = entity.CardList.Id
            };
        }

        public async Task<List<CardDTO>> GetCardsByListIdAsync(long id)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            return await unitOfWork.CardRepository.Query
                .Include(e => e.CardList)
                .Where(e => e.CardList.Id == id)
                .Select(e => new CardDTO
                {
                    Id = e.Id,
                    ListId = e.CardList.Id,
                    Position = e.Position,
                    Title = e.Title
                })
                .ToListAsync();
        }

        public async Task MoveCardAsync(CardDTO card)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var cardList = await unitOfWork.CardListRepository.GetByIdAsync(card.ListId);

            var entity = await unitOfWork.CardRepository.Query
                .Include(e => e.CardList)
                .FirstOrDefaultAsync(e => e.Id == card.Id);

            entity.CardList = cardList;
            entity.Position = card.Position;

           unitOfWork.CardRepository.Update(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task RemoveCardAsync(long id)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            await unitOfWork.CardRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateCardAsync(CardDTO card)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var entity = await unitOfWork.CardRepository.GetByIdAsync(card.Id);

            entity.Title = card.Title;
            entity.Description = card.Description;

           unitOfWork.CardRepository.Update(entity);

            await unitOfWork.SaveAsync();
        }

        private IUnitOfWorkFactory _unitOfWorkFactory;
    }
}
