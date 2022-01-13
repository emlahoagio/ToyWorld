using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(DataContext context) : base(context)
        {
        }
    }
}
