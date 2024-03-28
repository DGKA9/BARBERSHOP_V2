using System.Linq.Expressions;

namespace BARBERSHOP_V2.Repository.CRUDRepo
{
    public interface IRepository<T> where T : class
    {
        //T GetById(int id);
        TDto GetById<TDto>(int id);
        public Task Add(T entity);
        Task UpdateProperties(int id, Action<T> updateAction);
        public Task Delete(int id);
        IEnumerable<TDto> GetAll<TDto>();
        IEnumerable<TDto> Search<TDto>(Expression<Func<T, bool>> predicate);
        IEnumerable<TDto> GetAll<TDto>(Expression<Func<T, bool>> filter = null);
    }
}
