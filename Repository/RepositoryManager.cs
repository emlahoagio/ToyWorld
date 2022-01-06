using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private IToyRepository _toyRepository;
        private IBrandRepository _brandRepository;
        private ITypeRepository _typeRepository;

        public RepositoryManager(DataContext context)
        {
            _context = context;
        }

        public IToyRepository Toy
        {
            get
            {
                if(_toyRepository == null)
                {
                    _toyRepository = new ToyRepository(_context);
                }
                return _toyRepository;
            }
        }

        public ITypeRepository Type
        {
            get
            {
                if(_typeRepository == null)
                {
                    _typeRepository = new TypeRepository(_context);
                }
                return _typeRepository;
            }
        }
        public IBrandRepository Brand
        {
            get
            {
                if(_brandRepository == null)
                {
                    _brandRepository = new BrandRepository(_context);
                }
                return _brandRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
