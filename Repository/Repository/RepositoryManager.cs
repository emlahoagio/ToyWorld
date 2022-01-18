﻿using Contracts;
using Contracts.Repositories;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private IJwtSupport _jwtSupport;
        private IToyRepository _toyRepository;
        private IBrandRepository _brandRepository;
        private ITypeRepository _typeRepository;
        private IAccountRepository _accountRepository;
        private IGroupRepository _groupRepository;
        private IContestRepository _contestRepository;
        private IImageRepository _imageRepository;
        private IPostRepository _postRepository;

        public RepositoryManager(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IAccountRepository Account
        {
            get
            {
                if(_accountRepository == null)
                {
                    if(_jwtSupport == null)
                    {
                        _jwtSupport = new JwtServices(_configuration);
                    }
                    _accountRepository = new AccountRepository(_context, _jwtSupport);
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
                    _toyRepository = new ToyRepository(_context, _configuration, this);
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
        public IGroupRepository Group
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GroupRepository(_context);
                }
                return _groupRepository;
            }
        }

        public IContestRepository Contest
        {
            get
            {
                if(_contestRepository == null)
                {
                    _contestRepository = new ContestRepository(_context);
                }
                return _contestRepository;
            }
        }

        public IImageRepository Image
        {
            get
            {
                if(_imageRepository == null)
                {
                    _imageRepository = new ImageRepository(_context);
                }
                return _imageRepository;
            }
        }

        public IPostRepository Post
        {
            get
            {
                if(_postRepository == null)
                {
                    _postRepository = new PostRepository(_context);
                }
                return _postRepository;
            }
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}