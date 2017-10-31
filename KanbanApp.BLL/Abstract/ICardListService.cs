using System.Collections.Generic;
using System.Threading.Tasks;

using KanbanApp.Common.DTO;

namespace KanbanApp.BLL.Abstract
{
    public interface ICardListService
    {
        Task<CardListDTO> CreateListAsync(CardListDTO cardList);
        Task<CardListDTO> GetListByIdAsync(long id);
        Task<List<CardListDTO>> GetCardListsAsync();
        Task RemoveListAsync(long id);
        Task UpdateListAsync(CardListDTO cardList);
    }
}
