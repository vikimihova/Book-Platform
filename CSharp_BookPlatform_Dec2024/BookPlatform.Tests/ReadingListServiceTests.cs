using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.Services;
using BookPlatform.Core.ViewModels.Book;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Core.ViewModels.Character;
using System.Net;
using BookPlatform.Core.ViewModels.ApplicationUser;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookPlatform.Tests
{
    [TestFixture]
    public class ReadingListServiceTests
    {
        private Rating rating1;
        private Rating rating2;
        private ReadingStatus readingStatus;
        private ReadingStatus readingStatus2;

        private Book book1;
        private Book book2;
        private Book book3;

        private ICollection<string> allBooksTitles;
        private ICollection<string> validBooksTitles;
        private IEnumerable<Book> booksData;
        private IEnumerable<Book> booksDataEmpty;
        private IEnumerable<Review> reviewsData;
        private IEnumerable<BookApplicationUser> bookApplicationUsersData;
        private IEnumerable<ApplicationUser> applicationUsersData;

        private Character character1;
        private Character character2;

        private Author author1;
        private Author author2;
        private Author author3;

        private Genre genre1;
        private Genre genre2;
        private Genre genre3;

        private ApplicationUser applicationUser1;
        private ApplicationUser applicationUser2;
        private ApplicationUser applicationUser3;

        private BookApplicationUser bookApplicationUser1;
        private BookApplicationUser bookApplicationUser2;

        private Review review1;
        private Review review2;
        private Review review3;

        private Mock<IRepository<Book, Guid>> bookRepository;
        private Mock<IRepository<Review, Guid>> reviewRepository;
        private Mock<IRepository<BookApplicationUser, object>> bookApplicationUserRepository;
        private Mock<UserManager<ApplicationUser>> userManager;

        [SetUp]
        public void SetUp()
        {
            this.rating1 = new Rating()
            {
                Id = 5,
                RatingDescription = "Amazing",
            };

            this.rating2 = new Rating()
            {
                Id = 2,
                RatingDescription = "So-so",
            };

            this.readingStatus = new ReadingStatus()
            {
                Id = 1,
                StatusDescription = "Read",
            };

            this.readingStatus2 = new ReadingStatus()
            {
                Id = 2,
                StatusDescription = "Currently Reading",
            };

            this.applicationUser1 = new ApplicationUser()
            {
                Id = Guid.Parse("420AE570-CD14-4A8A-8E69-7B846C47AF2D"),
                UserName = "Viki",
                NormalizedUserName = "VIKI",
                Email = "viki@abv.bg",
                NormalizedEmail = "VIKI@ABV.BG",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEAFwvVOfURX5M1yTfvebUnLkKfz5V1DibJuYFoYyiMBDJ4e8KTFuVF4BFEvNmBV7yg==",
                SecurityStamp = "N5FYLDTT5Z44ZMGNMN5KA6ESN6JGMQJX",
                ConcurrencyStamp = "5629e43d-0c59-4012-8eb1-241a48921875",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LastLogin = new DateTime(2024, 12, 11),
                Friends = new List<ApplicationUser>() 
                {
                    this.applicationUser2
                }
            };

            this.applicationUser2 = new ApplicationUser()
            {
                Id = Guid.Parse("01695D06-3FA1-484F-8C14-F537D7A7A2F2"),
                UserName = "Emona",
                NormalizedUserName = "EMONA",
                Email = "emona@abv.bg",
                NormalizedEmail = "EMONA@ABV.BG",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEAFwvVOfURX5M1yTfvebUnLkKfz5V1DibJuYFoYyiMBDJ4e8KTFuVF4BFEvNmBV7yg==",
                SecurityStamp = "N5FYLDTT5Z44ZMGNMN5KA6ESN6JGMQJX",
                ConcurrencyStamp = "5629e43d-0c59-4012-8eb1-241a48921875",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LastLogin = new DateTime(2024, 12, 11),
                Friends = new List<ApplicationUser>()
                {
                    this.applicationUser1
                }
            };

            this.applicationUser3 = new ApplicationUser()
            {
                Id = Guid.Parse("9E09A5A2-F769-48A7-BF8A-E7A0A702FA31"),
                UserName = "Strahil",
                NormalizedUserName = "STRAHIL",
                Email = "strahil@abv.bg",
                NormalizedEmail = "STRAHIL@ABV.BG",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEAFwvVOfURX5M1yTfvebUnLkKfz5V1DibJuYFoYyiMBDJ4e8KTFuVF4BFEvNmBV7yg==",
                SecurityStamp = "N5FYLDTT5Z44ZMGNMN5KA6ESN6JGMQJX",
                ConcurrencyStamp = "5629e43d-0c59-4012-8eb1-241a48921875",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LastLogin = new DateTime(2024, 12, 11),
                Friends = new List<ApplicationUser>(),
            };

            this.applicationUsersData = new List<ApplicationUser>()
            {
                this.applicationUser1,
                this.applicationUser2,
                this.applicationUser3,
            };

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
                IsDeleted = false
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

            this.bookApplicationUser1 = new BookApplicationUser()
            {
                BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                Book = this.book1,
                ApplicationUserId = Guid.Parse("420AE570-CD14-4A8A-8E69-7B846C47AF2D"),
                ApplicationUser = this.applicationUser1,
                RatingId = this.rating1.Id,
                Rating = this.rating1,
                DateStarted = null,
                DateFinished = null,
                ReadingStatusId = this.readingStatus.Id,
                ReadingStatus = this.readingStatus,
                CharacterId = this.character1.Id,
                Character = this.character1,
                DateAdded = new DateTime(2024, 01, 01),
                IsDeleted = false,
            };

            this.bookApplicationUser2 = new BookApplicationUser()
            {
                BookId = this.book2.Id,
                Book = this.book2,
                ApplicationUserId = Guid.Parse("420AE570-CD14-4A8A-8E69-7B846C47AF2D"),
                ApplicationUser = this.applicationUser1,
                RatingId = this.rating2.Id,
                Rating = this.rating2,
                DateStarted = null,
                DateFinished = null,
                ReadingStatusId = this.readingStatus2.Id,
                ReadingStatus = this.readingStatus2,
                CharacterId = null,
                Character = null,
                DateAdded = new DateTime(2024, 01, 03),
                IsDeleted = false,
            };

            this.review1 = new Review()
            {
                Id = Guid.Parse("EA5C2F38-4823-4021-86FE-0E340E0A8BA6"),
                Content = "It's a classic.",
                CreatedOn = new DateTime(2022, 08, 11),
                ModifiedOn = null,
                BookId = Guid.Parse("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D"),
                ApplicationUserId = Guid.Parse("420AE570-CD14-4A8A-8E69-7B846C47AF2D"),
                IsDeleted = false,
            };

            this.review2 = new Review()
            {
                Id = Guid.Parse("D766411B-9D93-4853-A2A9-1F03BBFD23AC"),
                Content = "Not bad.",
                CreatedOn = new DateTime(2024, 11, 02),
                ModifiedOn = null,
                BookId = Guid.Parse("E56C08FF-9BFE-4C49-8B76-25A31A0959AD"),
                ApplicationUserId = Guid.Parse("420AE570-CD14-4A8A-8E69-7B846C47AF2D"),
                IsDeleted = false,
            };

            this.reviewsData = new List<Review>()
            {
                this.review1,
                this.review2,
            };

            this.bookApplicationUsersData = new List<BookApplicationUser>()
            {
                this.bookApplicationUser1,
                this.bookApplicationUser2,
            };

            var store = new Mock<IUserStore<ApplicationUser>>();
            this.userManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            this.bookRepository = new Mock<IRepository<Book, Guid>>();
            this.reviewRepository = new Mock<IRepository<Review, Guid>>();
            this.bookApplicationUserRepository = new Mock<IRepository<BookApplicationUser, object>>();            
        }

        // GetUserReadingListByUserIdAsync
        [Test]
        [TestCase("420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetUserReadingListByUserIdAsyncPositive(string userId)
        {
            ReadingListPaginatedViewModel inputModel = new ReadingListPaginatedViewModel();

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(applicationUser1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);


            IEnumerable<ReadingListViewModel> bookApplicationUsersActual = await readingListService.GetUserReadingListByUserIdAsync(userId, inputModel);

            Assert.IsNotNull(bookApplicationUsersActual);
            Assert.That(bookApplicationUsersActual.Count(), Is.EqualTo(2));
            foreach (var bookApplicationUserViewModel in bookApplicationUsersActual)
            {
                Assert.IsTrue(this.allBooksTitles.Contains(bookApplicationUserViewModel.BookTitle));
            }
        }

        [Test]
        [TestCase("420AE570-CD14-4A8A--7B846C47AF2D")]
        public async Task GetReadingListInvalidUserIdNegative(string userId)
        {
            ReadingListPaginatedViewModel inputModel = new ReadingListPaginatedViewModel();

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(applicationUser1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IEnumerable<ReadingListViewModel> bookApplicationUsersActual = await readingListService.GetUserReadingListByUserIdAsync(userId, inputModel);
            });
        }

        [Test]
        [TestCase("4BFD099A-762D-4744-BBD0-EA820AC643D5")]
        public async Task GetReadingListNonExistentUserNegative(string userId)
        {
            ReadingListPaginatedViewModel inputModel = new ReadingListPaginatedViewModel();

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.userManager
                .Setup(um => um.FindByIdAsync(applicationUser1.Id.ToString()))
                .ReturnsAsync(applicationUser1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                IEnumerable<ReadingListViewModel> bookApplicationUsersActual = await readingListService.GetUserReadingListByUserIdAsync(userId, inputModel);
            });
        }        

        // AddBookToUserReadingListAsync

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "4BFD099A-762D-4744-BBD0-EA820AC643D5", 2)]        
        public async Task AddNewBookToReadingListPositive(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book3.Id))
                .ReturnsAsync(this.book3);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);

            Assert.True(result );            
        }

        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD", "4BFD099A-762D-4744-BBD0-EA820AC643D5", 3)]
        public async Task AddBookToNewReadingListPositive(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book2.Id))
                .ReturnsAsync(this.book2);
            
            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);

            Assert.True(result);
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 3)]
        public async Task AddReadBookToNewReadingListNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);

            Assert.False(result);
        }

        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 2)]
        public async Task AddBookToSameReadingListNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser2);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);

            Assert.False(result);
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 2)]
        public async Task AddNonExistentBookToReadingListNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser2);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);


            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);
            });           
        }

        [Test]
        [TestCase("C4362229-1D19--C4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 2)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570--4A8A-9-7B846C47AF2D", 2)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 0)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 4)]
        public async Task AddBookToReadingListInvalidInputNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser2);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book2.Id))
                .ReturnsAsync(this.book2);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);


            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);
            });
        }

        // AddBookToUserReadingListReadAsync
        [Test]
        public async Task AddNewBookToReadingListReadPositive()
        {
            string userId = this.applicationUser1.Id.ToString();
            ReadingListAddInputModel model = new ReadingListAddInputModel();
            model.BookId = this.book3.Id.ToString();
            model.BookTitle = this.book3.Title;
            model.Rating = 3;
            model.ReadingStatus = 1;
            model.DateFinished = null;
            model.CharacterId = this.character1.Id.ToString();
            model.Review = "Interesting";
            model.ImageUrl = this.book3.ImageUrl;


            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book3.Id))
                .ReturnsAsync(this.book3);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.AddBookToUserReadingListReadAsync(model, userId);

            Assert.True(result);
        }

        // EditBookInReadingListAsync
        [Test]
        public async Task EditBookInReadingListReadPositive()
        {
            string userId = this.applicationUser1.Id.ToString();
            ReadingListEditInputModel model = new ReadingListEditInputModel();
            model.BookId = this.book1.Id.ToString();
            model.BookTitle = this.book1.Title;
            model.Rating = 3;
            model.ReadingStatus = 1;
            model.DateFinished = null;
            model.CharacterId = this.character1.Id.ToString();
            model.Review = null;
            model.ImageUrl = this.book1.ImageUrl;


            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            this.reviewRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.EditInReadingListAsync(model, userId);

            Assert.True(result);
        }

        [Test]
        [TestCase("420AE570-CD14-4A8A-47AF2D", "BF6C727C-B40F-48C1-8C60-117DC4343E0E", "598D60B4-AEFF-41FC-83C5-353E796271EE")]
        [TestCase("420AE570-CD14-4A8A-8E69-7B846C47AF2D", "BF6C727C-B40F-48C1--117DC4343E0E", "598D60B4-AEFF-41FC-83C5-353E796271EE")]
        [TestCase("420AE570-CD14-4A8A-8E69-7B846C47AF2D", "BF6C727C-B40F-48C1-8C60-117DC4343E0E", "598D60B4--41F-83C5-353E796271EE")]
        public async Task EditBookInReadingListInvalidInputNegative(string userId, string bookId, string characterId)
        {
            string importedUserId = userId;
            ReadingListEditInputModel model = new ReadingListEditInputModel();
            model.BookId = bookId;
            model.BookTitle = this.book1.Title;
            model.Rating = 3;
            model.ReadingStatus = 1;
            model.DateFinished = null;
            model.CharacterId = characterId;
            model.Review = null;
            model.ImageUrl = this.book1.ImageUrl;


            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            this.reviewRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await readingListService.EditInReadingListAsync(model, importedUserId);
            });        
        }

        [Test]
        public async Task EditBookInReadingListNonExistentBookApplicationUserNegative()
        {
            string userId = "4BFD099A-762D-4744-BBD0-EA820AC643D5";
            ReadingListEditInputModel model = new ReadingListEditInputModel();
            model.BookId = this.book1.Id.ToString();
            model.BookTitle = this.book1.Title;
            model.Rating = 3;
            model.ReadingStatus = 1;
            model.DateFinished = null;
            model.CharacterId = this.character1.Id.ToString();
            model.Review = null;
            model.ImageUrl = this.book1.ImageUrl;


            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            this.reviewRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                bool result = await readingListService.EditInReadingListAsync(model, userId);
            });
        }

        // RemoveBookFromUserReadingListAsync
        [Test]
        public async Task DeleteBookApplicationUserPositive()
        {
            string bookId = this.book1.Id.ToString();
            string userId = "420AE570-CD14-4A8A-8E69-7B846C47AF2D";

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            this.reviewRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.RemoveBookFromUserReadingListAsync(bookId, userId);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteNonExistentBookApplicationUserNegative()
        {
            string bookId = this.book3.Id.ToString();
            string userId = "420AE570-CD14-4A8A-8E69-7B846C47AF2D";

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            this.reviewRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.RemoveBookFromUserReadingListAsync(bookId, userId);

            Assert.False(result);
        }

        [Test]
        [TestCase("420AE570-CD14-4A8A-47AF2D", "BF6C727C-B40F-48C1-8C60-117DC4343E0E")]
        [TestCase("420AE570-CD14-4A8A-8E69-7B846C47AF2D", "BF6C727C-B40F-48C1--117DC4343E0E")]
        public async Task DeleteBookApplicationUserInvalidInputNegative(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            this.reviewRepository
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns(Task.CompletedTask);

            this.reviewRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                bool result = await readingListService.RemoveBookFromUserReadingListAsync(bookId, userId);
            });
        }

        // GetCurrentReadingStatusAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusPositive(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            ReadingStatus result = await readingListService.GetCurrentReadingStatusAsync(bookId, userId);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusNegative(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            ReadingStatus result = await readingListService.GetCurrentReadingStatusAsync(bookId, userId);

            Assert.IsNull(result);
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-60-43E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14--8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusInvalidInputNegative(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                ReadingStatus result = await readingListService.GetCurrentReadingStatusAsync(bookId, userId);
            });            
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusNonExistentBookNegative(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ReadingStatus result = await readingListService.GetCurrentReadingStatusAsync(bookId, userId);
            });
        }

        [Test]
        [TestCase("420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusDeletedBookNegative(string userId)
        {
            string bookId = this.book3.Id.ToString();
            this.book3.IsDeleted = true;

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ReadingStatus result = await readingListService.GetCurrentReadingStatusAsync(bookId, userId);
            });
        }

        // GetCurrentReadingStatusDescriptionAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusDescriptionPositive(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            string result = await readingListService.GetCurrentReadingStatusDescriptionAsync(bookId, userId);

            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo("Read"));
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetCurrentReadingStatusDescriptionNegative(string bookId, string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            string result = await readingListService.GetCurrentReadingStatusDescriptionAsync(bookId, userId);

            Assert.IsNull(result);
        }

        // CheckIfBookAlreadyReadAsync
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task CheckIfBookAlreadyReadPositive(string bookId, string userId, int readingStatusId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid bookGuid = Guid.Parse(bookId);

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.CheckIfBookAlreadyReadAsync(bookGuid, userGuid, readingStatusId);

            Assert.True(result);
        }

        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 2)]
        public async Task CheckIfBookAlreadyReadNegative(string bookId, string userId, int readingStatusId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid bookGuid = Guid.Parse(bookId);

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookApplicationUserRepository
                .Setup(r => r.AddAsync(It.IsAny<BookApplicationUser>()))
                .Returns(Task.CompletedTask);

            this.bookApplicationUserRepository
                .Setup(r => r.UpdateAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookApplicationUserRepository
                .Setup(r => r.DeleteAsync(It.IsAny<BookApplicationUser>()))
                .ReturnsAsync(true);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            bool result = await readingListService.CheckIfBookAlreadyReadAsync(bookGuid, userGuid, readingStatusId);

            Assert.False(result);
        }

        // UpdateRatingAsync
        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD")]
        public async Task UpdateRatingPositive(string bookId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);
            
            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book2);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            await readingListService.UpdateBookRating(bookId);

            this.bookRepository.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E")]
        public async Task UpdateRatingNonExistentBook(string bookId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await readingListService.UpdateBookRating(bookId);
            });
        }

        [Test]
        [TestCase("E56C08FF-9BFE-4C49-8B76-25A31A0959AD")]
        public async Task UpdateRatingDeletedBook(string bookId)
        {
            this.book2.IsDeleted = true;

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await readingListService.UpdateBookRating(bookId);
            });
        }

        [Test]
        [TestCase("E56C08FF-9B-4C49-59AD")]
        public async Task UpdateRatingInvalidInput(string bookId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.Id == book1.Id))
                .ReturnsAsync(book1);

            this.bookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await readingListService.UpdateBookRating(bookId);
            });
        }

        // GenerateAddInputModelAsync
        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateAddInputModelPositive(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book3);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            ReadingListAddInputModel modelActual = await readingListService.GenerateAddInputModelAsync(bookId, userId, readingStatusId);

            Assert.IsNotNull(modelActual);
            Assert.That(modelActual.BookId.ToLower(), Is.EqualTo(this.book3.Id.ToString().ToLower()));
            Assert.That(modelActual.BookTitle.ToLower(), Is.EqualTo(this.book3.Title.ToLower()));
            Assert.That(modelActual.ReadingStatus, Is.EqualTo(readingStatusId));
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateAddInputModelNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            ReadingListAddInputModel modelActual = await readingListService.GenerateAddInputModelAsync(bookId, userId, readingStatusId);

            Assert.IsNull(modelActual);
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateAddInputModelNonExistentBookNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ReadingListAddInputModel modelActual = await readingListService.GenerateAddInputModelAsync(bookId, userId, readingStatusId);
            });    
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateAddInputModelNonDeletedBookNegative(string bookId, string userId, int readingStatusId)
        {
            this.book1.IsDeleted = true;

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ReadingListAddInputModel modelActual = await readingListService.GenerateAddInputModelAsync(bookId, userId, readingStatusId);
            });
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-6FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420A70-CD14--8E69-7B846C47AF2D", 1)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 0)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 4)]
        public async Task GenerateAddInputModelInvalidInputNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book3);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                ReadingListAddInputModel modelActual = await readingListService.GenerateAddInputModelAsync(bookId, userId, readingStatusId);
            });
        }

        // GenerateEditInputModel
        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateEditInputModelPositive(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<BookApplicationUser, bool>>>()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book1);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            ReadingListEditInputModel modelActual = await readingListService.GenerateEditInputModelAsync(bookId, userId, readingStatusId);

            Assert.IsNotNull(modelActual);
            Assert.That(modelActual.BookId.ToLower(), Is.EqualTo(this.book1.Id.ToString().ToLower()));
            Assert.That(modelActual.BookTitle.ToLower(), Is.EqualTo(this.book1.Title.ToLower()));
            Assert.That(modelActual.Rating, Is.EqualTo(this.bookApplicationUser1.RatingId));
            Assert.That(modelActual.CharacterId, Is.EqualTo(this.bookApplicationUser1.CharacterId.ToString()));
            Assert.That(modelActual.ImageUrl, Is.EqualTo(this.book1.ImageUrl));
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateEditInputModelNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book3);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            ReadingListEditInputModel modelActual = await readingListService.GenerateEditInputModelAsync(bookId, userId, readingStatusId);

            Assert.IsNull(modelActual);
        }

        [Test]
        [TestCase("BF6C727C-B40F-48C1-8C60-117DC4343E0E", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateEditInputModelNonExistentBookNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book3.Id))
                .ReturnsAsync(this.book3);

            IQueryable<Review> reviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.reviewRepository
                .Setup(r => r.GetAllAttached())
                .Returns(reviewsQueryable);

            this.reviewRepository
                .Setup(r => r.FirstOrDefaultAsync(re => re.BookId == review1.BookId &&
                                                        re.ApplicationUserId == review1.ApplicationUserId))
                .ReturnsAsync(review1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ReadingListEditInputModel modelActual = await readingListService.GenerateEditInputModelAsync(bookId, userId, readingStatusId);
            });
        }

        [Test]
        [TestCase("624E1A1A-2BE9-4A2D-A22C-184A83E94D1D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        public async Task GenerateEditInputModelDeletedBookNegative(string bookId, string userId, int readingStatusId)
        {
            this.book1.IsDeleted = true;

            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(this.book1.Id))
                .ReturnsAsync(this.book1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                ReadingListEditInputModel modelActual = await readingListService.GenerateEditInputModelAsync(bookId, userId, readingStatusId);
            });
        }

        [Test]
        [TestCase("C4362229-1D19-47CC-ACC4-6FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 1)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420A70-CD14--8E69-7B846C47AF2D", 1)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 0)]
        [TestCase("C4362229-1D19-47CC-ACC4-3CDC96FE358D", "420AE570-CD14-4A8A-8E69-7B846C47AF2D", 4)]
        public async Task GenerateEditInputModelInvalidInputNegative(string bookId, string userId, int readingStatusId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.bookApplicationUserRepository
                .Setup(r => r.FirstOrDefaultAsync(b => b.BookId == book1.Id &&
                                                       b.ApplicationUserId.ToString().ToLower() == userId.ToLower()))
                .ReturnsAsync(bookApplicationUser1);

            this.bookRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(this.book3);



            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                ReadingListEditInputModel modelActual = await readingListService.GenerateEditInputModelAsync(bookId, userId, readingStatusId);
            });
        }

        // GetTotalBooksCountPerUserAsync
        [Test]
        [TestCase("420AE570-CD14-4A8A-8E69-7B846C47AF2D")]
        public async Task GetTotalBooksCountPerUserPositive(string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(applicationUser1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);


            int resultActual = await readingListService.GetTotalBooksCountPerUserAsync(userId);

            Assert.IsNotNull(resultActual);
            Assert.That(resultActual, Is.EqualTo(2));
        }

        [Test]
        [TestCase("420AE570-CD14--8E69-F2D")]
        public async Task GetTotalBooksCountPerUserInvalidUserIdNegative(string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(applicationUser1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                int resultActual = await readingListService.GetTotalBooksCountPerUserAsync(userId);
            });
        }

        [Test]
        [TestCase("4BFD099A-762D-4744-BBD0-EA820AC643D5")]
        public async Task GetTotalBooksCountPerUserNonExistentUserNegative(string userId)
        {
            // Repositories        
            IQueryable<BookApplicationUser> bookApplicationUsersQueryable = bookApplicationUsersData.AsQueryable().BuildMock();
            this.bookApplicationUserRepository
                .Setup(r => r.GetAllAttached())
                .Returns(bookApplicationUsersQueryable);

            this.userManager
                .Setup(um => um.FindByIdAsync(this.applicationUser1.Id.ToString()))
                .ReturnsAsync(applicationUser1);

            // Service
            IReadingListService readingListService = new ReadingListService(
                bookRepository.Object,
                reviewRepository.Object,
                bookApplicationUserRepository.Object,
                userManager.Object);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                int resultActual = await readingListService.GetTotalBooksCountPerUserAsync(userId);
            });
        }
    }
}
