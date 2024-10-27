using Application.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace UnitTest.Services
{
    public class AuctionServiceTest
    {
        private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
        private readonly IMapper _mapper;
        private readonly AuctionService _auctionService;

        public AuctionServiceTest()
        {
            _auctionRepositoryMock = new Mock<IAuctionRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Auction, AuctionReadDTO>();
                cfg.CreateMap<AuctionCreateDTO, Auction>();
                cfg.CreateMap<AuctionUpdateDTO, Auction>();
            });
            _mapper = config.CreateMapper();

            _auctionService = new AuctionService(_auctionRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAuctionsAsync_ReturnsAllAuctions()
        {
            // Arrange
            var auctions = new List<Auction>
            {
                new Auction { Id = Guid.NewGuid(), Title = "Auction1", StartingPrice = 100, UserId = Guid.NewGuid(), CategoryId = Guid.NewGuid() },
                new Auction { Id = Guid.NewGuid(), Title = "Auction2", StartingPrice = 200, UserId = Guid.NewGuid(), CategoryId = Guid.NewGuid() }
            };
            _auctionRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(auctions);

            // Act
            var result = await _auctionService.GetAllAuctionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(auctions.Count, result.Count());
            foreach (var auction in auctions)
            {
                Assert.Contains(result, a => a.Title == auction.Title && a.StartingPrice == auction.StartingPrice);
            }
        }

        [Fact]
        public async Task GetAuctionByIdAsync_ReturnsCorrectAuction()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var auction = new Auction { Id = auctionId, Title = "Test", StartingPrice = 150, UserId = Guid.NewGuid(), CategoryId = Guid.NewGuid() };
            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync(auction);

            // Act
            var result = await _auctionService.GetAuctionByIdAsync(auctionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(auction.Title, result.Title);
            Assert.Equal(auction.StartingPrice, result.StartingPrice);
        }

        [Fact]
        public async Task GetAuctionByIdAsync_ReturnsNullForNonExistentAuction()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((Auction)null);

            // Act
            var result = await _auctionService.GetAuctionByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAuctionAsync_AddsNewAuction()
        {
            // Arrange
            var auctionCreateDto = new AuctionCreateDTO
            {
                Title = "Shkelzen",
                Description = "Description",
                StartingPrice = 500,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                UserId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid()
            };
            var photos = new List<IFormFile>();

            // Act
            var result = await _auctionService.CreateAuctionAsync(auctionCreateDto, photos);

            // Assert
            _auctionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Auction>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(auctionCreateDto.Title, result.Title);
            Assert.Equal(auctionCreateDto.StartingPrice, result.StartingPrice);
        }

        [Fact]
        public async Task UpdateAuctionAsync_UpdatesAuctionDetails()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var auctionUpdateDto = new AuctionUpdateDTO { Title = "Updated Auction", Description = "Updated Description", StartingPrice = 600 };
            var existingAuction = new Auction { Id = auctionId, Title = "Original Auction", StartingPrice = 300, UserId = Guid.NewGuid(), CategoryId = Guid.NewGuid() };
            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync(existingAuction);

            // Act
            var result = await _auctionService.UpdateAuctionAsync(auctionId, auctionUpdateDto);

            // Assert
            Assert.True(result);
            _auctionRepositoryMock.Verify(repo => repo.UpdateAsync(existingAuction), Times.Once);
            Assert.Equal(auctionUpdateDto.Title, existingAuction.Title);
            Assert.Equal(auctionUpdateDto.StartingPrice, existingAuction.StartingPrice);
        }

        [Fact]
        public async Task UpdateAuctionAsync_ReturnsFalseWhenAuctionDoesNotExist()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var auctionUpdateDto = new AuctionUpdateDTO { Title = "Doesnt exist Auction", StartingPrice = 400 };
            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync((Auction)null);

            // Act
            var result = await _auctionService.UpdateAuctionAsync(auctionId, auctionUpdateDto);

            // Assert
            Assert.False(result);
            _auctionRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAuctionAsync_DeletesAuction()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var existingAuction = new Auction { Id = auctionId, Title = "Auction to Delete" };
            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(auctionId)).ReturnsAsync(existingAuction);

            // Act
            var result = await _auctionService.DeleteAuctionAsync(auctionId);

            // Assert
            Assert.True(result);
            _auctionRepositoryMock.Verify(repo => repo.DeleteAsync(auctionId), Times.Once);
        }

        [Fact]
        public async Task DeleteAuctionAsync_ReturnsFalseWhenAuctionDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _auctionRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((Auction)null);

            // Act
            var result = await _auctionService.DeleteAuctionAsync(nonExistentId);

            // Assert
            Assert.False(result);
            _auctionRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task SearchAuctionsAsync_ReturnsMatchingAuctions()
        {
            // Arrange
            var searchTerm = "Auction";
            var auctions = new List<Auction>
            {
                new Auction { Id = Guid.NewGuid(), Title = "Auction 1", Description = "First Auction" },
                new Auction { Id = Guid.NewGuid(), Title = "Auction 2", Description = "Second Auction" }
            };
            _auctionRepositoryMock.Setup(repo => repo.SearchAuctionsAsync(searchTerm)).ReturnsAsync(auctions);

            // Act
            var result = await _auctionService.SearchAuctionsAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(auctions.Count, result.Count());
            foreach (var auction in auctions)
            {
                Assert.Contains(result, a => a.Title == auction.Title);
            }
        }
    }
}
