﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.UoW
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T : class;
    }
}
