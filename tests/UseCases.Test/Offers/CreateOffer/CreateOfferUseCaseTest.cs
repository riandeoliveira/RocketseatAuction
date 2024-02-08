using Bogus;

using FluentAssertions;

using Moq;

using RocketseatAuction.API.Communication.Requests;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Services;
using RocketseatAuction.API.UseCases.Offers.CreateOffer;

using Xunit;

namespace UseCases.Test.Offers.CreateOffer;

public class CreateOfferUseCaseTest
{
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [Theory]
    public void Success(int itemId)
    {
        // ARRANGE
        var request = new Faker<RequestCreateOfferJson>()
            .RuleFor(request => request.Price, faker => faker.Random.Decimal(1, 700))
            .Generate();

        var offerRepositoryMock = new Mock<IOfferRepository>();
        var loggedUser = new Mock<ILoggedUser>();

        loggedUser.Setup(i => i.User()).Returns(new User());

        var useCase = new CreateOfferUseCase(offerRepositoryMock.Object, loggedUser.Object);

        // ACT
        var act = () => useCase.Execute(itemId, request);

        // ASSERT
        act.Should().NotThrow();
    }
}
