
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

        public bool Create(Model.Vila model)
        {
            _context.Vilas.Add(model);
            return Save();
        }

        public bool delete(Model.Vila model)
        {
            _context.Vilas.Remove(model);
            return Save();
        }

        public List<Model.Vila> GetAll()
        {
            return _context.Vilas.ToList();
        }

        public Model.Vila GetById(int id)
        {
            return _context.Vilas.FirstOrDefault(v => v.VilaId == id); 
        }

        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;


        public bool Update(Model.Vila model)
        {
            _context.Vilas.Update(model);
            return Save();
        }
    }
}
