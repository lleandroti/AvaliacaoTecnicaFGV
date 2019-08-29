using br.fgv.ordering.service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using br.fgv.ordering.service.Model;
using br.fgv.ordering.service.Exceptions;

namespace br.fgv.ordering.service
{
    /// <summary>
    /// Serviço de ordenação de livros
    /// </summary>
    public class OrderingService : IBooksOrderer
    {
        private readonly BooksContext _booksContext;

        public OrderingService(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public IEnumerable<Book> Order(IEnumerable<Book> books)
        {
            if (books == null)
                throw new OrdenationException(Resources.Textos.AListaDeLivrosNaoPodeSerNula);

            if (_booksContext.BookOrdenation == null)
                throw new OrdenationException(Resources.Textos.OrdenacaoNaoDefinida);

            if (!books.Any())
                return books;

            if (!_booksContext.BookOrdenation.Any())
                return new List<Book>();

            var ordenacoes = _booksContext.BookOrdenation.Reverse();

            return ordenacoes.Aggregate(books,
                (current, sortingColumn) =>
                    current.OrderBy(sortingColumn.ColumnName + (sortingColumn.Ascending ? "" : " descending")));
        }
    }
}