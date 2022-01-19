using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IReactPostRepository
    {
        void CreateReact(ReactPost reactPost);
        void DeleteReact(ReactPost reactPost);
    }
}
