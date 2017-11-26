﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface IErrorRepository:IRepository<Error>
    {

    }
   public class ErrorRepository:RepositoryBase<Error>,IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

    }
}