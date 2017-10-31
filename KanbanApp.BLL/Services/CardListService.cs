using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using KanbanApp.BLL.Abstract;
using KanbanApp.Common.Entities;
using KanbanApp.DAL.Abstract;
using KanbanApp.Common.DTO;

namespace KanbanApp.BLL.Services
{
    public class CardListService : ICardListService
    {
        public CardListService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<CardListDTO> CreateListAsync(CardListDTO cardList)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var entity = new CardList
            {
                Title = cardList.Title
            };

            unitOfWork.CardListRepository.Create(entity);

            await unitOfWork.SaveAsync();

            cardList.Id = entity.Id;

            return cardList;
        }

        public async Task<List<CardListDTO>> GetCardListsAsync()
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var entities = await unitOfWork.CardListRepository.GetAllAsync();

            return entities.Select(e => new CardListDTO
            {
                Id = e.Id,
                Title = e.Title
            }).ToList();
        }

        public async Task<CardListDTO> GetListByIdAsync(long id)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var entity = await unitOfWork.CardListRepository.GetByIdAsync(id);

            return new CardListDTO
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public async Task RemoveListAsync(long id)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            await unitOfWork.CardListRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateListAsync(CardListDTO cardList)
        {
            var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

            var entity = await unitOfWork.CardListRepository.GetByIdAsync(cardList.Id);

            entity.Title = cardList.Title;

            unitOfWork.CardListRepository.Update(entity);

            await unitOfWork.SaveAsync();
        }

        private IUnitOfWorkFactory _unitOfWorkFactory;
    }
}
