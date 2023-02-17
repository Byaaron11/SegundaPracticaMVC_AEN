using SegundaPracticaMVC_AEN.Models;

namespace SegundaPracticaMVC_AEN.Repositories
{
    public interface IRepository
    {
        public List<Comic> GetComics();
        public void InsertarComic(string nombre, string imagen, string descripcion);
    }
}
