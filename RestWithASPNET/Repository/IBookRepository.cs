using RestWithASPNET.Model;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Book Disabled(long id);
    }
}