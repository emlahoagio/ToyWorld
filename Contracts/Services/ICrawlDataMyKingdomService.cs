using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface ICrawlDataMyKingdomService
    {
        public List<String> GetListLink(string url);
        public Task<Toy> GetToyDetail(string url);

    }
}
