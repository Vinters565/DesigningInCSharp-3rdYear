using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Dto;

namespace UI.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly ApiClient apiClient;
        private List<AttributeMetadata>? cachedAttributes;

        public AttributeRepository(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<List<AttributeMetadata>> GetAttributesAsync()
        {
            if (cachedAttributes == null)
            {
                cachedAttributes = await apiClient.GetEventAttributesAsync();
            }
            return cachedAttributes;
        }
    }
}
