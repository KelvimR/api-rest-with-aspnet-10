using api_rest_with_aspnet_10.Models.Base;

namespace api_rest_with_aspnet_10.Repositories
{
    // Só posso extender a interface IRepository com uma classe que herda de BaseEntity, pois o repositório precisa de um Id para realizar as operações de CRUD.
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long Id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
    }
}
