

namespace Vila.WebApi.Services.Vila
{
    public interface IVilaService
    {
        List<Model.Vila> GetAll();
        Model.Vila GetById(int id);
        bool Create(Model.Vila model);
        bool Update(Model.Vila model);
        bool delete(Model.Vila model);
        bool Save();
    }
}
