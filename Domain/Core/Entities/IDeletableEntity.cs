using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Entities
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; }
    }
}
