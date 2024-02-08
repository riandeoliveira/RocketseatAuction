using RocketseatAuction.API.UseCases.Auctions.GetCurrent;
using FluentAssertions;
using Xunit;
using Moq;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using Bogus;
using RocketseatAuction.API.Enums;

namespace UseCases.Test.Auctions.GetCurrent;

public class GetCurrentAuctionUseCaseTest
{
    [Fact]
    public void Success()
    {
        // ARRANGE
        var entity = new Faker<Auction>()
            .RuleFor(auction => auction.Id, faker => faker.Random.Number(1, 700))
            .RuleFor(auction => auction.Name, faker => faker.Lorem.Word())
            .RuleFor(auction => auction.Starts, faker => faker.Date.Past())
            .RuleFor(auction => auction.Ends, faker => faker.Date.Future())
            .RuleFor(auction => auction.Items, (faker, auction) =>
            [
                new()
                {
                    Id = faker.Random.Number(1, 700),
                    Name = faker.Commerce.ProductName(),
                    Brand = faker.Commerce.Department(),
                    BasePrice = faker.Random.Decimal(50, 1000),
                    Condition = faker.PickRandom<Condition>(),
                    AuctionId = auction.Id
                }
            ]).Generate();

        var auctionRepositoryMock = new Mock<IAuctionRepository>();

        auctionRepositoryMock.Setup(i => i.GetCurrent()).Returns(entity);

        var useCase = new GetCurrentAuctionUseCase(auctionRepositoryMock.Object);

        // ACT
        var auction = useCase.Execute();

        // ASSERT
        auction.Should().NotBeNull();
        auction.Id.Should().Be(entity.Id);
        auction.Name.Should().Be(entity.Name);
    }
}
