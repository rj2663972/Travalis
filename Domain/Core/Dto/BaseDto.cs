using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Dto
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public bool IsTransient() => this.Id == default(Int32);
    }
}
