﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IErrorService
    {
        Error Create(Error error);
        void Save();
    }
    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;
        public ErrorService(IErrorRepository errorRepository,IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;

        }
        public Error Create(Model.Models.Error error)
        {
           return this._errorRepository.Add(error);

        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }
    }
}