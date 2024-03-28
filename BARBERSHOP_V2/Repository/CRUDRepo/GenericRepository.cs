using System.Linq.Expressions;
using AutoMapper;
using BARBERSHOP_V2.Data;

namespace BARBERSHOP_V2.Repository.CRUDRepo
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly BarberShopContext _context;
        private readonly IMapper _mapper;


        public GenericRepository(BarberShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public TDto GetById<TDto>(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"Không tìm thấy đối tượng với ID {id}.");
            }
            return MapToDto<TDto>(entity);
        }

        public IEnumerable<TDto> GetAll<TDto>()
        {
            var entities = _context.Set<T>().ToList();
            return entities.Select(entity => MapToDto<TDto>(entity));
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateProperties(int id, Action<T> updateAction)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                updateAction(entity);
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
                _context.Set<T>().Remove(entity);
        }


        private TDto MapToDto<TDto>(T entity)
        {
            return _mapper.Map<TDto>(entity);
        }

        public IEnumerable<TDto> Search<TDto>(Expression<Func<T, bool>> predicate)
        {
            var entities = _context.Set<T>().Where(predicate).ToList();
            return entities.Select(entity => MapToDto<TDto>(entity));
        }

        public IEnumerable<TDto> GetAll<TDto>(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = query.ToList();
            return entities.Select(entity => MapToDto<TDto>(entity));
        }
    }

}
