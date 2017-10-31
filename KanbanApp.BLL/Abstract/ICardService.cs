using System.Collections.Generic;
using System.Threading.Tasks;

using KanbanApp.Common.DTO;

namespace KanbanApp.BLL.Abstract
{
    public interface ICardService
    {
        Task<CardDTO> GetCardByIdAsync(long id);
        Task<CardDTO> CreateCardAsync(CardDTO card);
        Task<List<CardDTO>> GetCardsByListIdAsync(long id);
        Task RemoveCardAsync(long id);
        Task MoveCardAsync(CardDTO card);
        Task UpdateCardAsync(CardDTO card);
    }
}
