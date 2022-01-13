﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface ICrawlDataJapanFigureServices
    {
        Task<IEnumerable<Toy>> getToy(string crawlLink);
    }
}
