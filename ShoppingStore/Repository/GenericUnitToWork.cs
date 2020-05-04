using ShoppingStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingStore.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private dbShoppingStoreEntities _dbEntity = new dbShoppingStoreEntities();

        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(_dbEntity);
        }

        public void SaveChanges()
        {
            _dbEntity.SaveChanges();
        }

        protected virtual void Dispose(bool _disposing)
        {
            if (!this._disposed)
            {
                if (_disposing)
                {
                    _dbEntity.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
    }
}