using System.Collections.Generic;

namespace br.fgv.ordering.service.Service
{
    public class BooksContext
    {
        public IEnumerable<ISorting> BookOrdenation { get; set; }
    }
}