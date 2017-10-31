using StructureMap;

using KanbanApp.BLL.Abstract;
using KanbanApp.BLL.Services;

namespace KanbanApp.BLL.Infrastructure
{
    public class BLLStructureMapRegistry : Registry
    {
        public BLLStructureMapRegistry()
        {
            For<ICardService>().Use<CardService>();
            For<ICardListService>().Use<CardListService>();
        }
    }
}
