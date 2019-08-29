using br.fgv.ordering.service.Model;
using System.Collections.Generic;

namespace br.fgv.ordering.service.Service
{
    public interface IBooksOrderer
    {
        IEnumerable<Book> Order(IEnumerable<Book> books);
    }
}