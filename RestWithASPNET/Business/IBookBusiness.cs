using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Business
{
    public interface IBookBusiness
    {
        List<Book> FindAll();
        Book FindById(int id);
        Book Create(Book book);
        Book Update(Book book);
        void Delete(int id);
    }
}