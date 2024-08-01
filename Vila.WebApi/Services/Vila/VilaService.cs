
using Vila.WebApi.Context;
using Vila.WebApi.Paging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Vila.WebApi.Dtos;

namespace Vila.WebApi.Services.Vila
{
    public class VilaService : IVilaService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VilaService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool Create(Models.Vila model)
        {
            _context.Vilas.Add(model);
            return Save();
        }

        public bool delete(Models.Vila model)
        {
            _context.Vilas.Remove(model);
            return Save();
        }

        public List<Models.Vila> GetAll()
        {
            return _context.Vilas.ToList();
        }

        public Models.Vila GetById(int id)
        {
            return _context.Vilas.FirstOrDefault(v => v.VilaId == id);
        }

        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;

        public VilaPaging SearchVila(int pageId, string filter, int take)
        {
            IQueryable<Models.Vila> result = _context.Vilas.Include(x => x.Details);
             if (!string.IsNullOrEmpty(filter))
                result = result.Where(r =>
              r.Name.Contains(filter) ||
              r.State.Contains(filter) ||
              r.City.Contains(filter) ||
              r.Address.Contains(filter)
              );

            VilaPaging paging = new();
            paging.Generate(result, pageId, take);
            paging.Filter = filter;
            paging.Vilas = new();
            int skip = (pageId - 1) * take;
            var list = result.Skip(skip).Take(take).ToList();
            list.ForEach(x =>
            {
                paging.Vilas.Add(_mapper.Map<VilaSearchDto>(x));
            });
            return paging;

        }

        public bool Update(Models.Vila model)
        {
            _context.Vilas.Update(model);
            return Save();
        }
    }
}
