using Contracts;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private IJwtSupport _jwtSupport;
        private IFirebaseSupport _firebaseSupport;
        private IToyRepository _toyRepository;
        private IBrandRepository _brandRepository;
        private ITypeRepository _typeRepository;
        private IAccountRepository _accountRepository;

        public RepositoryManager(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IFirebaseSupport Firebase
        {
            get
            {
                if(_firebaseSupport == null)
                {
                    _firebaseSupport = new FirebaseServices(_configuration);
                }
                return _firebaseSupport;
            }
        }

        public IJwtSupport Jwt
        {
            get
            {
                if(_jwtSupport == null)
                {
                    _jwtSupport = new JwtServices(_configuration);
                }
                return _jwtSupport;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if(_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_context, _jwtSupport, _firebaseSupport);
                }
                return _accountRepository;
            }
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
