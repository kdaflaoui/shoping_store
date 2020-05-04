using ShoppingStore.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ShoppingStore.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        DbSet<Tbl_Entity> _dbSet;
        private dbShoppingStoreEntities _dbEntity;

        public GenericRepository(dbShoppingStoreEntities _DbEntity)
        {
            _dbEntity = _DbEntity;
            _dbSet = _DbEntity.Set<Tbl_Entity>();
        }

        public void Add(Tbl_Entity entity)
        {
            _dbSet.Add(entity);
            _dbEntity.SaveChanges();
        }

        public IEnumerable<Tbl_Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllRecordsQuerable()
        {
            return _dbSet;
        }

        public IEnumerable<Tbl_Entity> GetElements()
        {
            return _dbSet.ToList();
        }

        public Tbl_Entity GetFirstOrDefault(int recordID)
        {
            return _dbSet.Find(recordID);
        }

        public Tbl_Entity GetFirstOrDefaultByParameters(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public int GetRecordsCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<Tbl_Entity> GetRecordsToShow(int pageNo, int pageSize, int currentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }

        public IEnumerable<Tbl_Entity> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _dbEntity.Database.SqlQuery<Tbl_Entity>(query, parameters).ToList();
            }
            else
            {
                return _dbEntity.Database.SqlQuery<Tbl_Entity>(query).ToList();
            }
        }

        public void InactiveAndDelteMarkByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(Tbl_Entity entity)
        {
            if (_dbEntity.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void RemoveByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            Tbl_Entity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            _dbSet.Remove(entity);
        }

        public void RemoveRangeByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            List<Tbl_Entity> entities = _dbSet.Where(wherePredict).ToList();
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }

        }

        public void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Updtae(Tbl_Entity entity)
        {
            _dbSet.Attach(entity);
            _dbEntity.Entry(entity).State = EntityState.Modified;
            _dbEntity.SaveChanges();
        }
    }
}
