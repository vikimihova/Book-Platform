using Moq;
using MockQueryable;
using System.Linq.Expressions;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

using BookPlatform.Core.Services;
using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;

namespace BookPlatform.Tests
{
    [TestFixture]
    public class CharacterServiceTests
    {
        private Book book1;
        private Book book2;
        private Book book3;
        private Character character1;
        private Character character2;
        private Character character3;
        private Character character4;
        private BookCharacter bookCharacter;
        private BookCharacter bookCharacter2;
        private ICollection<string> allCharacterNames;
        private ICollection<string> validCharacterNames;
        private IEnumerable<BookCharacter> bookCharactersData;

        private Mock<IRepository<Character, Guid>> characterRepository;
        private Mock<IRepository<BookCharacter, object>> bookCharacterRepository;
        private Mock<IRepository<Book, Guid>> bookRepository;

        [SetUp]
        public void Setup()
        {
            this.book1 = new Book()
            {
                Id = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                Title = "Watership Down",
                PublicationYear = 1972,
                AuthorId = Guid.Parse("D8A5AC42-01B0-49AB-A1B1-447B99D1768B"),
                GenreId = Guid.Parse("2AE2A8E2-27B1-42AE-ABA2-0C4722F86704"),
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
                GenreId = Guid.Parse("61F871F9-1E36-4F20-BE48-4749C171DA7E"),
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
                GenreId = Guid.Parse("61F871F9-1E36-4F20-BE48-4749C171DA7E"),
                Description = "Since its immediate success in 1813, Pride and Prejudice has remained one of the most popular novels in the English language. Jane Austen called this brilliant work \"her own darling child\" and its vivacious heroine, Elizabeth Bennet, \"as delightful a creature as ever appeared in print.\" The romantic clash between the opinionated Elizabeth and her proud beau, Mr. Darcy, is a splendid performance of civilized sparring. And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England.",
                ImageUrl = "/images/pride-and-prejudice.jpg",
                AverageRating = 4,
                IsDeleted = true
            };

            this.character1 = new Character()
            {
                Id = Guid.Parse("598D60B4-AEFF-41FC-83C5-353E796271EE"),
                Name = "Bluebell",
                IsDeleted = false,
                IsSubmittedByUser = false,
            };

            this.character2 = new Character()
            {
                Id = Guid.Parse("FD7A69D4-FA5C-4380-8788-4216BFEF4C69"),
                Name = "Holly",
                IsDeleted = true,
                IsSubmittedByUser = false,
            };

            this.character3 = new Character()
            {
                Id = Guid.Parse("7BAA1ECA-BCDA-4006-AE48-61F07441E56F"),
                Name = "Hazel",
                IsDeleted = false,
                IsSubmittedByUser = false,
            };

            this.character4 = new Character()
            {
                Id = Guid.Parse("DA2B1F1C-DC8D-4D81-A664-F1A21550EE24"),
                Name = "Jane Bennet",
                IsDeleted = false,
                IsSubmittedByUser = false,
            };

            this.bookCharacter = new BookCharacter()
            {
                BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                Book = this.book1,
                CharacterId = Guid.Parse("598D60B4-AEFF-41FC-83C5-353E796271EE"),
                Character = this.character1,
                IsDeleted = false,
                IsSubmittedByUser = false,
            };

            this.bookCharacter2 = new BookCharacter()
            {
                BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                Book = this.book1,
                CharacterId = Guid.Parse("FD7A69D4-FA5C-4380-8788-4216BFEF4C69"),
                Character = this.character2,
                IsDeleted = true,
                IsSubmittedByUser = false,
            };

            this.allCharacterNames = new List<string>()
            {
                "Bluebell",
                "Holly",
                "Hazel",
                "Jane Bennet"
            };

            this.validCharacterNames = new List<string>()
            {
                "Bluebell",
                "Hazel",
                "Jane Bennet"
            };

            this.bookCharactersData = new List<BookCharacter>()
            {
                new BookCharacter()
                {
                    BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                    Book = this.book1,
                    CharacterId = Guid.Parse("598D60B4-AEFF-41FC-83C5-353E796271EE"),
                    Character = this.character1,
                    IsDeleted = false,
                    IsSubmittedByUser = false,
                },
                new BookCharacter()
                {
                    BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                    Book = this.book1,
                    CharacterId = Guid.Parse("FD7A69D4-FA5C-4380-8788-4216BFEF4C69"),
                    Character = this.character2,
                    IsDeleted = false,
                    IsSubmittedByUser = false,
                },
                new BookCharacter()
                {
                    BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                    Book = this.book1,
                    CharacterId = Guid.Parse("7BAA1ECA-BCDA-4006-AE48-61F07441E56F"),
                    Character = this.character3,
                    IsDeleted = false,
                    IsSubmittedByUser = false,
                },
                new BookCharacter()
                {
                    BookId = Guid.Parse("C4362229-1D19-47CC-ACC4-3CDC96FE358D"),
                    Book = this.book3,
                    CharacterId = Guid.Parse("DA2B1F1C-DC8D-4D81-A664-F1A21550EE24"),
                    Character = this.character4,
                    IsDeleted = false,
                    IsSubmittedByUser = false,
                }
            };
                       
            this.characterRepository = new Mock<IRepository<Character, Guid>>();
            this.bookCharacterRepository = new Mock<IRepository<BookCharacter, object>>();
            this.bookRepository = new Mock<IRepository<Book, Guid>>();
        }

        // GetCharactersIndexAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D")]
        public async Task GetCharactersIndexExistingBookWithCharacters(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            IEnumerable<CharacterIndexViewModel> charactersActual = await characterService.GetCharactersIndexAsync(bookId);

            Assert.IsNotNull(charactersActual);
            Assert.That(charactersActual.Count(), Is.EqualTo(3));
            foreach (var character in charactersActual)
            {
                Assert.IsTrue(this.allCharacterNames.Contains(character.Name));
            }
        }


        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD")]
        public async Task GetCharactersIndexExistingBookWithNoCharacters(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            IEnumerable<CharacterIndexViewModel> charactersActual = await characterService.GetCharactersIndexAsync(bookId);

            Assert.IsNotNull(charactersActual);
            Assert.That(charactersActual.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D")]
        public async Task GetCharactersIndexDeletedBookWithCharacters(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book3.Id))
                .ReturnsAsync(this.book3);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            IEnumerable<CharacterIndexViewModel> charactersActual = await characterService.GetCharactersIndexAsync(bookId);

            Assert.IsNotNull(charactersActual);
            Assert.That(charactersActual.Count(), Is.EqualTo(1));
            foreach (var character in charactersActual)
            {
                Assert.IsTrue(this.allCharacterNames.Contains(character.Name));
            }
        }

        [Test]
        [TestCase("E1FDFD97-C84B-4560-B367-32FE121E35B6")]
        public async Task GetCharactersIndexNonExistentBook(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);


            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                IEnumerable<CharacterIndexViewModel> charactersActual = await characterService.GetCharactersIndexAsync(bookId);
            });
        }

        [Test]
        [TestCase("E1FDFD97-4B-4560-B36-32FE121E35B")]
        public async Task GetCharactersIndexInvalidBookId(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);


            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IEnumerable<CharacterIndexViewModel> charactersActual = await characterService.GetCharactersIndexAsync(bookId);
            });
        }

        // GetCharactersAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D")]
        public async Task GetCharactersExistentBookWithCharacters(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            ICollection<SelectCharacterViewModel> charactersActual = await characterService.GetCharactersAsync(bookId);

            Assert.IsNotNull(charactersActual);
            Assert.That(charactersActual.Count(), Is.EqualTo(2));
            foreach (var character in charactersActual)
            {
                Assert.IsTrue(this.validCharacterNames.Contains(character.Name));
            }
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D")]
        public async Task GetCharactersDeletedBookWithCharacters(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book3.Id))
                .ReturnsAsync(this.book3);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            ICollection<SelectCharacterViewModel> charactersActual = await characterService.GetCharactersAsync(bookId);

            Assert.IsNotNull(charactersActual);
            Assert.That(charactersActual.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD")]
        public async Task GetCharactersExistentBookWithNoCharacters(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            ICollection<SelectCharacterViewModel> charactersActual = await characterService.GetCharactersAsync(bookId);

            Assert.IsNotNull(charactersActual);
            Assert.That(charactersActual.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase("E1FDFD97-C84B-4560-B367-32FE121E35B6")]
        public async Task GetCharactersNonExistentBook(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);


            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ICollection<SelectCharacterViewModel> charactersActual = await characterService.GetCharactersAsync(bookId);
            });
        }

        [Test]
        [TestCase("E1FDFD97-4B-4560-B36-32FE121E35B")]
        public async Task GetCharactersInvalidBookId(string bookId)
        {
            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);


            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                ICollection<SelectCharacterViewModel> charactersActual = await characterService.GetCharactersAsync(bookId);
            });
        }

        // AddCharacterAsync
        [Test]
        public async Task AddCharacterPositive()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Blackberry";
            model.BookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == character1.Name))
                .ReturnsAsync(character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.AddCharacterAsync(model);

            Assert.True(result);
        }

        [Test]
        public async Task AddCharacterNegative()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Bluebell";
            model.BookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(bookCharacter);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Character, bool>>>()))
                .ReturnsAsync(character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.AddCharacterAsync(model);

            Assert.False(result);
        }

        [Test]
        public async Task AddCharacterEmptyModel()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == "Bluebell"))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.AddCharacterAsync(model);
            });
        }

        [Test]
        public async Task AddCharacterInvalidBookId()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Blackberry";
            model.BookId = "624E1A1A-2BE9-4D-A2C-184A83E94";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == "Bluebell"))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.AddCharacterAsync(model);
            });
        }

        [Test]
        public async Task AddCharacterNonExistentBook()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Blackberry";
            model.BookId = "E1FDFD97-C84B-4560-B367-32FE121E35B6";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == "Bluebell"))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.AddCharacterAsync(model);
            });
        }

        // AddCharacterByAdminAsync
        [Test]
        public async Task AddCharacterByAdminPositive()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Blackberry";
            model.BookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == character1.Name))
                .ReturnsAsync(character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.AddCharacterByAdminAsync(model);

            Assert.True(result);
        }

        [Test]
        public async Task AddCharacterByAdminNegative()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Bluebell";
            model.BookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(bookCharacter);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Character, bool>>>()))
                .ReturnsAsync(character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.AddCharacterByAdminAsync(model);

            Assert.False(result);
        }

        [Test]
        public async Task AddCharacterByAdminEmptyModel()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == "Bluebell"))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.AddCharacterByAdminAsync(model);
            });
        }

        [Test]
        public async Task AddCharacterByAdminInvalidBookId()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Blackberry";
            model.BookId = "624E1A1A-2BE9-4D-A2C-184A83E94";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == "Bluebell"))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.AddCharacterByAdminAsync(model);
            });
        }

        [Test]
        public async Task AddCharacterByAdminNonExistentBook()
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Name = "Blackberry";
            model.BookId = "E1FDFD97-C84B-4560-B367-32FE121E35B6";
            model.BookTitle = "Watership Down";
            model.ImageUrl = "/images/watership-down.jpg";

            // Repositories        
            IQueryable<BookCharacter> bookCharactersQueryable = bookCharactersData.AsQueryable().BuildMock();
            this.bookCharacterRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookCharactersQueryable);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.FirstOrDefaultAsync(c => c.Name == "Bluebell"))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.AddCharacterByAdminAsync(model);
            });
        }

        // SoftDeleteCharacterAsync
        [Test]
        public async Task SoftDeleteCharacterPositive()
        {
            string characterId = "598D60B4-AEFF-41FC-83C5-353E796271EE";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character1.Id))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);

            Assert.True(result);
        }

        [Test]
        public async Task SoftDeleteCharacterNegative()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-4216BFEF4C69";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);

            Assert.False(result);
        }

        [Test]
        public async Task SoftDeleteCharacterInvalidCharacterId()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-421";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task SoftDeleteCharacterInvalidBookId()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-4216BFEF4C69";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task SoftDeleteCharacterNonExistentBook()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-4216BFEF4C69";
            string bookId = "E1FDFD97-C84B-4560-B367-32FE121E35B6";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task SoftDeleteCharacterNonExistentCharacter()
        {
            string characterId = "AFEBFC0B-5A4A-4454-9FC6-107B4F951E35";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task SoftDeleteCharacterNonExistentBookCharacter()
        {
            string characterId = "DA2B1F1C-DC8D-4D81-A664-F1A21550EE24";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(bc => bc.BookId == bookCharacter2.BookId &&
                                                        bc.CharacterId == bookCharacter2.CharacterId))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.SoftDeleteCharacterAsync(characterId, bookId);
            });
        }

        // IncludeCharacterAsync
        [Test]
        public async Task IncludeCharacterPositive()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-4216BFEF4C69";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";            

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.IncludeCharacterAsync(characterId, bookId);

            Assert.True(result);
        }

        [Test]
        public async Task IncludeCharacterNegative()
        {
            string characterId = "598D60B4-AEFF-41FC-83C5-353E796271EE";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character1.Id))
                .ReturnsAsync(this.character1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            bool result = await characterService.IncludeCharacterAsync(characterId, bookId);

            Assert.False(result);
        }

        [Test]
        public async Task IncludeCharacterInvalidCharacterId()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-421";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.IncludeCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task IncludeCharacterInvalidBookId()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-4216BFEF4C69";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await characterService.IncludeCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task IncludeCharacterNonExistentBook()
        {
            string characterId = "FD7A69D4-FA5C-4380-8788-4216BFEF4C69";
            string bookId = "E1FDFD97-C84B-4560-B367-32FE121E35B6";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.IncludeCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task IncludeCharacterNonExistentCharacter()
        {
            string characterId = "AFEBFC0B-5A4A-4454-9FC6-107B4F951E35";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookCharacter, bool>>>()))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.IncludeCharacterAsync(characterId, bookId);
            });
        }

        [Test]
        public async Task IncludeCharacterNonExistentBookCharacter()
        {
            string characterId = "DA2B1F1C-DC8D-4D81-A664-F1A21550EE24";
            string bookId = "624E1A1A-2BE9-4A2D-A22C-184A83E94D1D";

            // Repositories        
            this.bookCharacterRepository
                .Setup(r => r.FirstOrDefaultAsync(bc => bc.BookId == bookCharacter2.BookId &&
                                                        bc.CharacterId == bookCharacter2.CharacterId))
                .ReturnsAsync(this.bookCharacter2);

            this.bookCharacterRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookCharacter>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.characterRepository
                .Setup(r => r.GetByIdAsync(this.character2.Id))
                .ReturnsAsync(this.character2);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await characterService.IncludeCharacterAsync(characterId, bookId);
            });
        }

        // GenerateAddCharacterInputModelAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", 3)]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", null)]
        public async Task GenerateAddCharacterInputModel(string bookId, int? readingStatusId = null)
        {         
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            AddCharacterInputModel modelActual = await characterService.GenerateAddCharacterInputModelAsync(bookId, readingStatusId);
                
            Assert.IsNotNull(modelActual);
            Assert.That(modelActual.BookId.ToLower(), Is.EqualTo(this.book1.Id.ToString().ToLower()));
            Assert.That(modelActual.BookTitle.ToLower(), Is.EqualTo(this.book1.Title.ToLower()));
            Assert.That(modelActual.ImageUrl, Is.EqualTo(this.book1.ImageUrl));
            Assert.That(modelActual.ReadingStatusId, Is.EqualTo(readingStatusId));
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-18", 3)]
        public async Task GenerateAddCharacterInputModelInvalidBookId(string bookId, int? readingStatusId = null)
        {       
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);            

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                AddCharacterInputModel model = await characterService.GenerateAddCharacterInputModelAsync(bookId, readingStatusId);
            });
        }

        [Test]
        [TestCase("E1FDFD97-C84B-4560-B367-32FE121E35B6", 3)]
        public async Task GenerateAddCharacterInputModelNonExistentBook(string bookId, int? readingStatusId = null)
        {
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                AddCharacterInputModel model = await characterService.GenerateAddCharacterInputModelAsync(bookId, readingStatusId);
            });
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", 3)]
        public async Task GenerateAddCharacterInputModelDeletedBook(string bookId, int? readingStatusId = null)
        {
            // Repositories      
            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            ICharacterService characterService = new CharacterService(
                characterRepository.Object,
                bookCharacterRepository.Object,
                bookRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                AddCharacterInputModel model = await characterService.GenerateAddCharacterInputModelAsync(bookId, readingStatusId);
            });
        }
    }
}