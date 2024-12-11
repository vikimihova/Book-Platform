using Moq;
using MockQueryable;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.Services;
using BookPlatform.Core.ViewModels.Book;
using System.Linq.Expressions;

namespace BookPlatform.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        private Book book1;
        private Book book2;
        private Book book3;
        private ICollection<string> allBooksTitles;
        private ICollection<string> validBooksTitles;
        private IEnumerable<Book> booksData;
        private IEnumerable<Book> booksDataEmpty;

        private Author author1;
        private Author author2;
        private Author author3;

        private Genre genre1;
        private Genre genre2;
        private Genre genre3;

        private Mock<IRepository<Book, Guid>> bookRepository;

        [SetUp]
        public void SetUp()
        {
            this.author1 = new Author()
            {
                Id = Guid.Parse("D8A5AC42-01B0-49AB-A1B1-447B99D1768B"),
                FirstName = "Richard",
                LastName = "Adams",
                FullName = "Richard Adams",
                IsDeleted = false,                
            };

            this.author2 = new Author()
            {
                Id = Guid.Parse("3512D0C8-C7DE-49B1-990C-6048A08DB9AE"),
                FirstName = "Herman",
                LastName = "Melville",
                FullName = "Herman Melville",
                IsDeleted = true,
            };

            this.author3 = new Author()
            {
                Id = Guid.Parse("51717429-E61D-4E99-87A0-A6F4977979B3"),
                FirstName = "Jane",
                LastName = "Austen",
                FullName = "Jane Austen",
                IsDeleted = false,
            };

            this.genre1 = new Genre()
            {
                Id = Guid.Parse("2AE2A8E2-27B1-42AE-ABA2-0C4722F86704"),
                Name = "Animals",
                IsDeleted = false
            };

            this.genre2 = new Genre()
            {
                Id = Guid.Parse("61F871F9-1E36-4F20-BE48-4749C171DA7E"),
                Name = "Historical Fiction",
                IsDeleted = false
            };

            this.genre3 = new Genre()
            {
                Id = Guid.Parse("B0823402-4DDA-4B81-B587-915749F2605B"),
                Name = "Philosophy",
                IsDeleted = false
            };

            this.book1 = new Book()
            {
                Id = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                Title = "Watership Down",
                PublicationYear = 1972,
                AuthorId = Guid.Parse("D8A5AC42-01B0-49AB-A1B1-447B99D1768B"),
                Author = this.author1,
                GenreId = Guid.Parse("2AE2A8E2-27B1-42AE-ABA2-0C4722F86704"),
                Genre = this.genre1,
                Description = "Set in England's Downs, a once idyllic rural landscape, this stirring tale of adventure, courage and survival follows a band of very special creatures on their flight from the intrusion of man and the certain destruction of their home. Led by a stouthearted pair of friends, they journey forth from their native Sandleford Warren through the harrowing trials posed by predators and adversaries, to a mysterious promised land and a more perfect society.",
                ImageUrl = "/images/watership-down.jpg",
                AverageRating = 0,
                IsDeleted = false
            };

            this.book2 = new Book()
            {
                Id = Guid.Parse("E56C08FF-9BFE-4C49-8B76-25A31A0959AD"),
                Title = "Moby Dick",
                PublicationYear = 1851,
                AuthorId = Guid.Parse("3512D0C8-C7DE-49B1-990C-6048A08DB9AE"),
                Author = this.author2,
                GenreId = Guid.Parse("61F871F9-1E36-4F20-BE48-4749C171DA7E"),
                Genre = this.genre2,
                Description = "So Melville wrote of his masterpiece, one of the greatest works of imagination in literary history. In part, Moby-Dick is the story of an eerily compelling madman pursuing an unholy war against a creature as vast and dangerous and unknowable as the sea itself. But more than just a novel of adventure, more than an encyclopaedia of whaling lore and legend, the book can be seen as part of its author's lifelong meditation on America. Written with wonderfully redemptive humour, Moby-Dick is also a profound inquiry into character, faith, and the nature of perception.",
                ImageUrl = "/images/moby-dick.jpg",
                AverageRating = 1.5,
                IsDeleted = false
            };

            this.book3 = new Book()
            {
                Id = Guid.Parse("C4362229-1D19-47CC-ACC4-3CDC96FE358D"),
                Title = "Pride and Prejudice",
                PublicationYear = 1813,
                AuthorId = Guid.Parse("51717429-E61D-4E99-87A0-A6F4977979B3"),
                Author = this.author3,
                GenreId = Guid.Parse("61F871F9-1E36-4F20-BE48-4749C171DA7E"),
                Genre = this.genre2,
                Description = "Since its immediate success in 1813, Pride and Prejudice has remained one of the most popular novels in the English language. Jane Austen called this brilliant work \"her own darling child\" and its vivacious heroine, Elizabeth Bennet, \"as delightful a creature as ever appeared in print.\" The romantic clash between the opinionated Elizabeth and her proud beau, Mr. Darcy, is a splendid performance of civilized sparring. And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England.",
                ImageUrl = "/images/pride-and-prejudice.jpg",
                AverageRating = 4,
                IsDeleted = true
            };

            this.allBooksTitles = new List<string>()
            {
                "Watership Down",
                "Moby Dick",
                "Pride and Prejudice"
            };

            this.validBooksTitles = new List<string>()
            {
                "Watership Down",
                "Moby Dick"
            };

            this.booksData = new List<Book>()
            {
                book1,
                book2,
                book3
            };

            this.booksDataEmpty = new List<Book>();

            this.bookRepository = new Mock<IRepository<Book, Guid>>();
        }

        // IndexGetAllAsync
        [Test]
        public async Task IndexGetAllAsyncPositive()
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllAsync();

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(3));
            foreach (var book in booksActual)
            {
                Assert.IsTrue(this.allBooksTitles.Contains(book.Title));
            }
        }

        [Test]
        public async Task IndexGetAllAsyncEmptyPositive()
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksDataEmpty.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllAsync();

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(0));
        }

        // IndexGetAllRandomAsync
        [Test]
        public async Task IndexGetAllAsyncNoArgumentsPositive()
        {
            BookIndexViewModelWrapper model = new BookIndexViewModelWrapper();

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllRandomAsync(model);

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(2));

            foreach (var book in booksActual)
            {
                Assert.IsTrue(this.validBooksTitles.Contains(book.Title));
            }
        }

        [Test]
        public async Task IndexGetAllAsyncSearchInputFoundPositive()
        {
            BookIndexViewModelWrapper model = new BookIndexViewModelWrapper();
            model.SearchInput = "down";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllRandomAsync(model);

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(1));  
            foreach (var book in booksActual)
            {
                Assert.That(book.Title, Is.EqualTo(book1.Title));
            }
        }

        [Test]
        public async Task IndexGetAllAsyncSearchInputNotFoundPositive()
        {
            BookIndexViewModelWrapper model = new BookIndexViewModelWrapper();
            model.SearchInput = "anna";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllRandomAsync(model);

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(2));
            foreach (var book in booksActual)
            {
                Assert.IsTrue(this.validBooksTitles.Contains(book.Title));
            }
        }

        [Test]
        public async Task IndexGetAllAsyncFilterFoundPositive()
        {
            BookIndexViewModelWrapper model = new BookIndexViewModelWrapper();
            model.GenreFilter = "Animals";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllRandomAsync(model);

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(1));
            foreach (var book in booksActual)
            {
                Assert.That(book.Title, Is.EqualTo(book1.Title));
            }
        }

        [Test]
        public async Task IndexGetAllAsyncFilterNotFoundNegative()
        {
            BookIndexViewModelWrapper model = new BookIndexViewModelWrapper();
            model.GenreFilter = "Philosophy";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            IEnumerable<BookIndexViewModel> booksActual = await bookService.IndexGetAllRandomAsync(model);

            Assert.IsNotNull(booksActual);
            Assert.That(booksActual.Count(), Is.EqualTo(0));
        }

        // GetBookDetailsAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D")]
        public async Task GetBookDetailsPositive(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);


            BookDetailsViewModel bookActual = await bookService.GetBookDetailsAsync(bookId);

            Assert.IsNotNull(bookActual);
            Assert.That(bookActual.Id.ToLower(), Is.EqualTo(book1.Id.ToString().ToLower()));
            Assert.That(bookActual.Title.ToLower(), Is.EqualTo(book1.Title.ToString().ToLower()));
            Assert.That(bookActual.PublicationYear, Is.EqualTo(book1.PublicationYear));
            Assert.That(bookActual.Author.ToLower(), Is.EqualTo(book1.Author.FullName.ToLower()));
            Assert.That(bookActual.Genre.ToLower(), Is.EqualTo(book1.Genre.Name.ToLower()));
            Assert.That(bookActual.Description.ToLower(), Is.EqualTo(book1.Description.ToLower()));
            Assert.That(bookActual.AverageRating, Is.EqualTo(book1.AverageRating));
            Assert.That(bookActual.ImageUrl, Is.EqualTo(book1.ImageUrl));
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E")]
        public async Task GetBookNonExistentBookNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                BookDetailsViewModel bookActual = await bookService.GetBookDetailsAsync(bookId);
            });
        }

        [Test]
        [TestCase("BF6C727C-B40F--60-10E")]
        public async Task GetBookDetailsInvalidBookIdNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BookDetailsViewModel bookActual = await bookService.GetBookDetailsAsync(bookId);
            });
        }

        // AddBookAsync
        [Test]
        public async Task AddBookPositive()
        {
            AddBookInputModel model = new AddBookInputModel();
            model.Title = "Shardik";
            model.Description = "Description";
            model.AuthorId = "D8A5AC42-01B0-49AB-A1B1-447B99D1768B";
            model.GenreId = "2AE2A8E2-27B1-42AE-ABA2-0C4722F86704";
            model.PublicationYear = 1950;
            model.ImageUrl = "/images/shardik.jpg";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            bool result = await bookService.AddBookAsync(model);

            Assert.True(result);
        }

        [Test]
        public async Task AddBookEmptyModelNegative()
        {
            AddBookInputModel model = new AddBookInputModel();

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await bookService.AddBookAsync(model);
            });
        }

        [Test]
        [TestCase("D8A5AC42-01B0--A1B1-44B", "2AE2A8E2-27B1-42AE-ABA2-0C4722F86704")]
        [TestCase("D8A5AC42-01B0-49AB-A1B1-447B99D1768B", "2AE2A8E2-27B1--ABA2-")]
        [TestCase("D8A5AC42-0-49AB-A1B1-447B99D1768B", "2AE2A8E2-27B1--0C4722F86704")]
        public async Task AddBookInvalidIdsNegative(string authorId, string genreId)
        {
            AddBookInputModel model = new AddBookInputModel();
            model.Title = "Shardik";
            model.Description = "Description";
            model.AuthorId = authorId;
            model.GenreId = genreId;
            model.PublicationYear = 1950;
            model.ImageUrl = "/images/shardik.jpg";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await bookService.AddBookAsync(model);
            });
        }

        [Test]
        public async Task AddBookExistentBookNegative()
        {
            AddBookInputModel model = new AddBookInputModel();
            model.Title = "Watership Down";
            model.Description = "Set in England's Downs, a once idyllic rural landscape, this stirring tale of adventure, courage and survival follows a band of very special creatures on their flight from the intrusion of man and the certain destruction of their home. Led by a stouthearted pair of friends, they journey forth from their native Sandleford Warren through the harrowing trials posed by predators and adversaries, to a mysterious promised land and a more perfect society.";
            model.AuthorId = "D8A5AC42-01B0-49AB-A1B1-447B99D1768B";
            model.GenreId = "2AE2A8E2-27B1-42AE-ABA2-0C4722F86704";
            model.PublicationYear = 1972;
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await bookService.AddBookAsync(model);
            });
        }

        // EditBookAync
        [Test]
        public async Task EditBookPositive()
        {
            EditBookInputModel model = new EditBookInputModel();
            model.Title = "Watership Down";
            model.Description = "Set in England's Downs, a once idyllic rural landscape, this stirring tale of adventure, courage and survival follows a band of very special creatures on their flight from the intrusion of man and the certain destruction of their home. Led by a stouthearted pair of friends, they journey forth from their native Sandleford Warren through the harrowing trials posed by predators and adversaries, to a mysterious promised land and a more perfect society.";
            model.AuthorId = "D8A5AC42-01B0-49AB-A1B1-447B99D1768B";
            model.GenreId = "2AE2A8E2-27B1-42AE-ABA2-0C4722F86704";
            model.PublicationYear = 1973;
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            bool result = await bookService.EditBookAsync(model);

            Assert.True(result);
        }

        [Test]
        public async Task EditBookNonExistentNegative()
        {
            EditBookInputModel model = new EditBookInputModel();
            model.Title = "Shardik";
            model.Description = "Description";
            model.AuthorId = "D8A5AC42-01B0-49AB-A1B1-447B99D1768B";
            model.GenreId = "2AE2A8E2-27B1-42AE-ABA2-0C4722F86704";
            model.PublicationYear = 1950;
            model.ImageUrl = "/images/shardik.jpg";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await bookService.EditBookAsync(model);
            });
        }

        [Test]
        [TestCase("D8A5AC42-01B0--A1B1-44B", "2AE2A8E2-27B1-42AE-ABA2-0C4722F86704")]
        [TestCase("D8A5AC42-01B0-49AB-A1B1-447B99D1768B", "2AE2A8E2-27B1--ABA2-")]
        [TestCase("D8A5AC42-0-49AB-A1B1-447B99D1768B", "2AE2A8E2-27B1--0C4722F86704")]
        public async Task EditBookInvalidIdsNegative(string authorId, string genreId)
        {
            EditBookInputModel model = new EditBookInputModel();
            model.Title = "Shardik";
            model.Description = "Description";
            model.AuthorId = authorId;
            model.GenreId = genreId;
            model.PublicationYear = 1950;
            model.ImageUrl = "/images/shardik.jpg";

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await bookService.EditBookAsync(model);
            });
        }

        [Test]
        public async Task EditBookEmptyModelNegative()
        {
            EditBookInputModel model = new EditBookInputModel();

            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await bookService.EditBookAsync(model);
            });
        }

        // SoftDeleteBookAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D")]
        public async Task SoftDeletePositive(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            bool result = await bookService.SoftDeleteBookAsync(bookId);

            Assert.True(result);
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D")]
        public async Task SoftDeleteNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book3);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            bool result = await bookService.SoftDeleteBookAsync(bookId);

            Assert.False(result);
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-4-358D")]
        public async Task SoftDeleteInvalidBookIdNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book3);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);            

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await bookService.SoftDeleteBookAsync(bookId);
            });
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E")]
        public async Task SoftDeleteNonExistentBookNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await bookService.SoftDeleteBookAsync(bookId);
            });
        }

        // IncludeBookAsync
        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D")]        
        public async Task IncludePositive(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book3);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            bool result = await bookService.IncludeBookAsync(bookId);

            Assert.True(result);
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D")]
        public async Task IncludeNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            bool result = await bookService.IncludeBookAsync(bookId);

            Assert.False(result);
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-4-358D")]
        public async Task IncludeInvalidBookIdNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book3);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await bookService.IncludeBookAsync(bookId);
            });
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E")]
        public async Task IncludeNonExistentBookNegative(string bookId)
        {
            // Repositories        
            IQueryable<Book> booksQueryable = booksData.AsQueryable().BuildMock();
            this.bookRepository
                .Setup(r => r.GetAllAttached())
                .Returns(booksQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await bookService.IncludeBookAsync(bookId);
            });
        }

        // GenerateEditBookInputModelAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D")]
        public async Task GenerateEditBookInputModel(string bookId)
        {
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            EditBookInputModel modelActual = await bookService.GenerateEditBookInputModelAsync(bookId);

            Assert.IsNotNull(modelActual);
            Assert.That(modelActual.Title.ToLower(), Is.EqualTo(this.book1.Title.ToLower()));
            Assert.That(modelActual.AuthorId.ToLower(), Is.EqualTo(this.book1.AuthorId.ToString().ToLower()));
            Assert.That(modelActual.GenreId.ToLower(), Is.EqualTo(this.book1.GenreId.ToString().ToLower()));
            Assert.That(modelActual.PublicationYear, Is.EqualTo(this.book1.PublicationYear));
            Assert.That(modelActual.ImageUrl, Is.EqualTo(this.book1.ImageUrl));
            Assert.That(modelActual.Description, Is.EqualTo(this.book1.Description));
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-C-1D")]
        public async Task GenerateEditBookInputModelInvalidBookId(string bookId)
        {
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                EditBookInputModel modelActual = await bookService.GenerateEditBookInputModelAsync(bookId);
            });
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E")]
        public async Task GenerateEditBookInputModelNonExistentBook(string bookId)
        {
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IBookService bookService = new BookService(bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                EditBookInputModel modelActual = await bookService.GenerateEditBookInputModelAsync(bookId);
            });
        }
    }
}
