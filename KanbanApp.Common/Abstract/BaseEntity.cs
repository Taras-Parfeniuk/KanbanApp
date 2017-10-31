using System;
using System.Collections.Generic;
using System.Text;

namespace KanbanApp.Common.Abstract
{
    public abstract class BaseEntity<TKey> where TKey : IComparable
    {
        public TKey Id { get; set; }
    }
}
