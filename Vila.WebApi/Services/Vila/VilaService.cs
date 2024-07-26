
using Vila.WebApi.Context;

namespace Vila.WebApi.Services.Vila
{
    public class VilaService : IVilaService
    {

        private readonly DataContext _context;

        public VilaService(DataContext context)
        {
            _context = context;
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


        public bool Update(Models.Vila model)
        {
            _context.Vilas.Update(model);
            return Save();
        }
    }
}
