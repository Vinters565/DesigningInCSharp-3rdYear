using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Dto;

namespace UI.Repository
{
    interface IAttributeRepository
    {
        Task<List<AttributeMetadata>> GetAttributesAsync();
    }
}
