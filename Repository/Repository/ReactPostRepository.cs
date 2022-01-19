using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class ReactPostRepository : RepositoryBase<ReactPost>, IReactPostRepository
    {
        public ReactPostRepository(DataContext context) : base(context)
        {
        }

        public void CreateReact(ReactPost reactPost) => Create(reactPost);

        public void DeleteReact(ReactPost reactPost) => Delete(reactPost);
    }
}
