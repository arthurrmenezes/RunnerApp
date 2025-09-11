using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.UnitTests.Domain.BoundedContexts.TrainingContext;

public class TrainingTest
{
    [Fact]
    public void Factory_WithValidParameters_ShouldCreateTraining()
    {
        // Arrange
        var location = LocationType.GYM;
        var distance = 5.0;
        var duration = TimeSpan.FromMinutes(30);
        var date = DateTime.Now;
        var accountId = IdValueObject.Factory(Guid.NewGuid());

        // Act
        var training = Training.Factory(
            location: location,
            distance: distance,
            duration: duration,
            date: date,
            accountId: accountId);
        
        // Assert
        Assert.NotNull(training);
        Assert.Equal(location, training.Location);
        Assert.Equal(distance, training.Distance);
        Assert.Equal(duration, training.Duration);
        Assert.Equal(date, training.Date);
        Assert.Equal(accountId.ToString(), training.AccountId.ToString());
        Assert.NotEqual(Guid.Empty, training.AccountId.Id);
    }

    [Fact]
    public void Factory_WithInvalidLocation_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Training.Factory(
            location: (LocationType) 999,
            distance: 5,
            duration: TimeSpan.FromMinutes(30),
            date: DateTime.Now,
            accountId: IdValueObject.Factory(Guid.NewGuid())));
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(301)]
    public void Factory_WithInvalidDistance_ShouldThrowArgumentException(double distance)
    {
        Assert.Throws<ArgumentException>(() => Training.Factory(
            location: LocationType.GYM,
            distance: distance,
            duration: TimeSpan.FromMinutes(30),
            date: DateTime.Now,
            accountId: IdValueObject.Factory(Guid.NewGuid())));
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(4.99)]
    [InlineData(86401)]
    public void Factory_WithInvalidDuration_ShouldThrowArgumentException(double duration)
    {
        Assert.Throws<ArgumentException>(() => Training.Factory(
            location: LocationType.GYM,
            distance: 2.0,
            duration: TimeSpan.FromSeconds(duration),
            date: DateTime.Now,
            accountId: IdValueObject.Factory(Guid.NewGuid())));
    }

    [Fact]
    public void Factory_WithFutureDate_ShouldThrowArgumentException()
    {
        var date = DateTime.Now.AddDays(1);

        Assert.Throws<ArgumentException>(() => Training.Factory(
            location: LocationType.PARK,
            distance: 2,
            duration: TimeSpan.FromMinutes(2),
            date: date,
            accountId: IdValueObject.Factory(Guid.NewGuid())));
    }

    [Fact]
    public void Factory_UtcNow_ShouldSetCreatedAtToUtcNow()
    {
        var dateBefore = DateTime.UtcNow;

        var training = Training.Factory(
            location: LocationType.GYM,
            distance: 5,
            duration: TimeSpan.FromMinutes(10),
            date: dateBefore,
            accountId: IdValueObject.Factory(Guid.NewGuid()));

        var dateAfter = DateTime.UtcNow;

        Assert.True(training.CreatedAt >= dateBefore && training.CreatedAt <= dateAfter);
        Assert.Equal(DateTimeKind.Utc, training.CreatedAt.Kind);
        Assert.Equal(DateTimeKind.Utc, training.Date.Kind);
    }

    [Fact]
    public void UpdateTrainingDetails_WithAllFieldsChanging_ShouldUpdateAllFieldsSuccessfully()
    {
        // Arrange
        var training = Training.Factory(
            location: LocationType.GYM,
            distance: 5,
            duration: TimeSpan.FromMinutes(30),
            date: DateTime.Now,
            accountId: IdValueObject.Factory(Guid.NewGuid()));

        var newLocation = LocationType.PARK;
        var newDistance = 10;
        var newDuration = TimeSpan.FromMinutes(60);
        var newDate = DateTime.UtcNow;

        // Act
        training.UpdateTrainingDetails(
            location: newLocation,
            distance: newDistance,
            duration: newDuration,
            date: newDate);

        // Assert
        Assert.Equal(newLocation, training.Location);
        Assert.Equal(newDistance, training.Distance);
        Assert.Equal(newDuration, training.Duration);
        Assert.Equal(newDate, training.Date);
    }


    [Fact]
    public void UpdateTrainingDetails_WithPartialFields_ShouldUpdateOnlyProvidedFields()
    {
        // Arrange
        var training = Training.Factory(
            location: LocationType.GYM,
            distance: 5,
            duration: TimeSpan.FromMinutes(30),
            date: DateTime.Now,
            accountId: IdValueObject.Factory(Guid.NewGuid()));

        var newDistance = 10;
        
        // Act
        training.UpdateTrainingDetails(
            location: null,
            distance: newDistance,
            duration: null,
            date: null);

        // Assert
        Assert.Equal(newDistance, training.Distance);
        Assert.Equal(LocationType.GYM, training.Location);
    }

    [Theory]
    [InlineData((LocationType) 0, -10, -10)]
    [InlineData((LocationType) 10, 0, 0)]
    [InlineData((LocationType) 10, 301, 4.99)]
    public void UpdateTrainingDetails_WithInvalidInputData_ShouldThrowArgumentException(LocationType location, double distance, double duration)
    {
        var training = Training.Factory(
            location: LocationType.GYM,
            distance: 5,
            duration: TimeSpan.FromMinutes(30),
            date: DateTime.Now,
            accountId: IdValueObject.Factory(Guid.NewGuid()));

        var futureDate = DateTime.Now.AddHours(1);

        Assert.Throws<ArgumentException>(() => training.UpdateTrainingDetails(
            location: location,
            distance: distance,
            duration: TimeSpan.FromSeconds(duration),
            date: futureDate));
    }

    [Fact]
    public void UpdateTrainingDetails_WithNoFields_DoNotUpdateAnyFields()
    {
        // Arrange
        var location = LocationType.STREET;
        var distance = 5;
        var duration = TimeSpan.FromMinutes(30);
        var date = DateTime.Now;
        var accountId = IdValueObject.Factory(Guid.NewGuid());

        var training = Training.Factory(
            location: location,
            distance: distance,
            duration: duration,
            date: date,
            accountId: accountId);

        // Act
        training.UpdateTrainingDetails(
            location: null,
            distance: null,
            duration: null,
            date: null);

        // Assert
        Assert.Equal(location, training.Location);
        Assert.Equal(distance, training.Distance);
        Assert.Equal(duration, training.Duration);
        Assert.Equal(date, training.Date);
        Assert.Equal(accountId, training.AccountId);
    }
}
