using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.fgv.ordering.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using br.fgv.ordering.service.Model;
using br.fgv.ordering.service.Service;
using br.fgv.ordering.service.Exceptions;

namespace br.fgv.ordering.service.Tests
{
    [TestClass()]
    public class OrderingServiceTests
    {
        private IList<Book> _books;

        public OrderingServiceTests()
        {
            _books = GetBooks();
        }

        private Book[] GetBooks()
        {
            return new[]
            {
                new Book
                {
                    Titulo = "Java How to Program",
                    Autor = "Deitel & Deitel",
                    Edicao = 2007
                },
                new Book
                {
                    Titulo = "Patterns of Enterprise Application Architecture",
                    Autor = "Martin Fowler",
                    Edicao = 2002
                },
                new Book
                {
                    Titulo = "Head First Design Patterns",
                    Autor = "Elisabeth Freeman",
                    Edicao = 2004
                },
                new Book
                {
                    Titulo = "Internet & World Wide Web: How to Program",
                    Autor = "Deitel & Deitel",
                    Edicao = 2007
                }
            };
        }

        /// <summary>
        /// Ordenação por título ascendente
        /// </summary>
        [TestMethod()]
        public void OrdenacaoTituloAscendente()
        {
            var settings = new BooksContext();
            settings.BookOrdenation = new List<Sorting> { new Sorting { ColumnName = "Titulo" } };

            var servico = new OrderingService(settings);

            var livrosOrdenados = servico.Order(_books);

            Assert.IsNotNull(livrosOrdenados);

            Assert.AreEqual("Head First Design Patterns", livrosOrdenados.First().Titulo);
            Assert.AreEqual("Internet & World Wide Web: How to Program", livrosOrdenados.ElementAt(1).Titulo);
            Assert.AreEqual("Head First Design Patterns", livrosOrdenados.ElementAt(2).Titulo);
            Assert.AreEqual("Patterns of Enterprise Application Architecture", livrosOrdenados.ElementAt(3).Titulo);
        }

        /// <summary>
        /// Ordenação por autor ascendente e título descendente
        /// </summary>
        [TestMethod()]
        public void OrdenacaoAutorAscendenteETituloDescendente()
        {
            var settings = new BooksContext();
            settings.BookOrdenation = new List<Sorting>
            {
                new Sorting { ColumnName = "Autor" },
                new Sorting { ColumnName = "Titulo", Ascending = false }
            };

            var servico = new OrderingService(settings);

            var livrosOrdenados = servico.Order(_books);

            Assert.IsNotNull(livrosOrdenados);

            Assert.AreEqual("Java How to Program", livrosOrdenados.First().Titulo);
            Assert.AreEqual("Internet & World Wide Web: How to Program", livrosOrdenados.ElementAt(1).Titulo);
            Assert.AreEqual("Head First Design Patterns", livrosOrdenados.ElementAt(2).Titulo);
            Assert.AreEqual("Patterns of Enterprise Application Architecture", livrosOrdenados.ElementAt(3).Titulo);
        }

        /// <summary>
        /// Ordenação por edição descendente, autor descendente e título ascendente
        /// </summary>
        [TestMethod()]
        public void OrdenacaoEdicaoDescendenteAutorDescendenteETituloAscendente()
        {
            var settings = new BooksContext();
            settings.BookOrdenation = new List<Sorting>
            {
                new Sorting { ColumnName = "Edicao", Ascending = false },
                new Sorting { ColumnName = "Autor", Ascending = false },
                new Sorting { ColumnName = "Titulo"}
            };

            var servico = new OrderingService(settings);

            var livrosOrdenados = servico.Order(_books);

            Assert.IsNotNull(livrosOrdenados);

            Assert.AreEqual("Internet & World Wide Web: How to Program", livrosOrdenados.First().Titulo);
            Assert.AreEqual("Java How to Program", livrosOrdenados.ElementAt(1).Titulo);
            Assert.AreEqual("Head First Design Patterns", livrosOrdenados.ElementAt(2).Titulo);
            Assert.AreEqual("Patterns of Enterprise Application Architecture", livrosOrdenados.ElementAt(3).Titulo);
        }

        /// <summary>
        /// Excessão para ordenação não definida
        /// </summary>
        [TestMethod()]
        public void OrdenacaoNaoDefinida()
        {
            try
            {
                var settings = new BooksContext();

                var servico = new OrderingService(settings);

                var livrosOrdenados = servico.Order(_books);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(OrdenationException));
            }
        }

        /// <summary>
        /// Resultado vazio quando sem ordenação
        /// </summary>
        [TestMethod()]
        public void OrdenacaoVazia()
        {
            var settings = new BooksContext();
            settings.BookOrdenation = new List<Sorting> { new Sorting() };

            var servico = new OrderingService(settings);

            var livrosOrdenados = servico.Order(_books);

            Assert.IsFalse(livrosOrdenados.Any());
        }
    }
}