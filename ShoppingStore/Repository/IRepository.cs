using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Repository
{
    public interface IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        IEnumerable<Tbl_Entity> GetAllRecords();
        IQueryable<Tbl_Entity> GetAllRecordsQuerable();
        int GetRecordsCount();
        void Add(Tbl_Entity entity);
        void Updtae(Tbl_Entity entity);
        void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict);
        Tbl_Entity GetFirstOrDefault(int recordID);
        void Remove(Tbl_Entity entity);
        void RemoveByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict);
        void RemoveRangeByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict);
        void InactiveAndDelteMarkByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict);
        Tbl_Entity GetFirstOrDefaultByParameters(Expression<Func<Tbl_Entity, bool>> wherePredict);
        IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict);
        IEnumerable<Tbl_Entity> GetResultBySqlProcedure(string query, params Object[] parameters);
        IEnumerable<Tbl_Entity> GetRecordsToShow(
                int pageNo,
                int pageSize,
                int currentPage,
                Expression<Func<Tbl_Entity, bool>> wherePredict,
                Expression<Func<Tbl_Entity, int>> orderByPredict
            );
        IEnumerable<Tbl_Entity> GetElements();
    }
}
