using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.Services;
using BookPlatform.Core.ViewModels.Book;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using BookPlatform.Core.ViewModels.ReadingList;

namespace BookPlatform.Tests
{
    [TestFixture]
    public class ReadingListServiceTests
    {
        private Rating rating1;
        private Rating rating2;
        private ReadingStatus readingStatus;

        private Book book1;
        private Book book2;
        private Book book3;

        private ICollection<string> allBooksTitles;
        private ICollection<string> validBooksTitles;
        private IEnumerable<Book> booksData;
        private IEnumerable<Book> booksDataEmpty;
        private IEnumerable<Review> reviewsData;
        private IEnumerable<BookApplicationUser> bookApplicationUsersData;

        private Character character1;
        private Character character2;

        private Author author1;
        private Author author2;
        private Author author3;

        private Genre genre1;
        private Genre genre2;
        private Genre genre3;

        private ApplicationUser applicationUser1;
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
                Id = 3,
                StatusDescription = "Read",
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
                ReadingStatusId = this.readingStatus.Id,
                ReadingStatus = this.readingStatus,
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
    }
}
