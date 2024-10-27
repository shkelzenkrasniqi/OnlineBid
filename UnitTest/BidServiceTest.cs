using Application.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using Moq;
using Xunit;

namespace UnitTest.Services
{
    public class BidServiceTest
    {
        private readonly Mock<IBidRepository> _bidRepositoryMock;
        private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
        private readonly IMapper _mapper;
        private readonly BidService _bidService;

        public BidServiceTest()
        {
            _bidRepositoryMock = new Mock<IBidRepository>();
            _auctionRepositoryMock = new Mock<IAuctionRepository>();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bid, BidReadDTO>();
                cfg.CreateMap<BidCreateDTO, Bid>();
            });
            _mapper = config.CreateMapper();

            _bidService = new BidService(_bidRepositoryMock.Object, _mapper, _auctionRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllBidsAsync_ReturnsAllBids()
        {
            // Arrange
            var bids = new List<Bid>
            {
                new Bid { Id = Guid.NewGuid(), Amount = 100, BidTime = DateTime.UtcNow, UserId = Guid.NewGuid(), AuctionId = Guid.NewGuid() },
                new Bid { Id = Guid.NewGuid(), Amount = 200, BidTime = DateTime.UtcNow, UserId = Guid.NewGuid(), AuctionId = Guid.NewGuid() }
            };
            _bidRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bids);

            // Act
            var result = await _bidService.GetAllBidsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bids.Count, result.Count());
        }

        [Fact]
        public async Task GetBidByIdAsync_ReturnsCorrectBid()
        {
            // Arrange
            var bidId = Guid.NewGuid();
            var bid = new Bid { Id = bidId, Amount = 150, BidTime = DateTime.UtcNow, UserId = Guid.NewGuid(), AuctionId = Guid.NewGuid() };
            _bidRepositoryMock.Setup(repo => repo.GetByIdAsync(bidId)).ReturnsAsync(bid);

            // Act
            var result = await _bidService.GetBidByIdAsync(bidId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bid.Amount, result.Amount);
        }

        [Fact]
        public async Task CreateBidAsync_AddsNewBidIfValid()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var auction = new Auction { Id = auctionId, StartingPrice = 100, CurrentPrice = 150,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            };
            var bidCreateDto = new BidCreateDTO { Amount = 200, BidTime = DateTime.UtcNow, UserId = Guid.NewGuid(), AuctionId = auctionId };

            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync(auction);
            _bidRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Bid>()));

            // Act
            var result = await _bidService.CreateBidAsync(bidCreateDto);

            // Assert
            _auctionRepositoryMock.Verify(repo => repo.UpdateAsync(auction), Times.Once);
            _bidRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Bid>()), Times.Once);
            Assert.Equal(bidCreateDto.Amount, result.Amount);
        }

        [Fact]
        public async Task CreateBidAsync_ThrowsExceptionIfAuctionNotActive()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var auction = new Auction { Id = auctionId,
                StartDate = DateTime.UtcNow.AddHours(4),  
                EndDate = DateTime.UtcNow.AddDays(1)
            };
            var bidCreateDto = new BidCreateDTO { Amount = 200, AuctionId = auctionId };

            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync(auction);

            // Actand Assert
            await Assert.ThrowsAsync<Exception>(async () => await _bidService.CreateBidAsync(bidCreateDto));
            _bidRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Bid>()), Times.Never);
        }

        [Fact]
        public async Task CreateBidAsync_ThrowsExceptionIfBidAmountTooLow()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var auction = new Auction { Id = auctionId, StartingPrice = 100, CurrentPrice = 150,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            };
            var bidCreateDto = new BidCreateDTO { Amount = 100, AuctionId = auctionId };

            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync(auction);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await _bidService.CreateBidAsync(bidCreateDto));
            _bidRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Bid>()), Times.Never);
        }

        [Fact]
        public async Task DeleteBidAsync_DeletesBidIfExists()
        {
            // Arrange
            var bidId = Guid.NewGuid();
            var bid = new Bid { Id = bidId };
            _bidRepositoryMock.Setup(repo => repo.GetByIdAsync(bidId)).ReturnsAsync(bid);

            // Act
            var result = await _bidService.DeleteBidAsync(bidId);

            // Assert
            Assert.True(result);
            _bidRepositoryMock.Verify(repo => repo.DeleteAsync(bidId), Times.Once);
        }

        [Fact]
        public async Task DeleteBidAsync_ReturnsFalseIfBidDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _bidRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((Bid)null);

            // Act
            var result = await _bidService.DeleteBidAsync(nonExistentId);

            // Assert
            Assert.False(result);
            _bidRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task GetAllBidsForAuctionAsync_ReturnsBidsForGivenAuction()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var bids = new List<Bid>
            {
                new Bid { Id = Guid.NewGuid(), Amount = 100, AuctionId = auctionId, BidTime = DateTime.UtcNow },
                new Bid { Id = Guid.NewGuid(), Amount = 200, AuctionId = auctionId, BidTime = DateTime.UtcNow }
            };
            _bidRepositoryMock.Setup(repo => repo.GetAllForAuctionAsync(auctionId)).ReturnsAsync(bids);

            // Act
            var result = await _bidService.GetAllBidsForAuctionAsync(auctionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bids.Count, result.Count());
            foreach (var bid in bids)
            {
                Assert.Contains(result, b => b.Amount == bid.Amount && b.AuctionId == bid.AuctionId);
            }
        }
    }
}
