using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WizCo.Domain.Enums;
using WizCo.Domain.Filters;
using WizCo.Infrastructure.Repositories;
using WizCo.Infrastructure.Tests.Builders;
using WizCo.Infrastructure.Tests.Fixtures;

namespace WizCo.Infrastructure.Tests.Repositories;

public class OrderRepositoryTests : BaseRepositoryTests
{
    private readonly OrderRepository _repository;

    public OrderRepositoryTests(WizCoDbContextFixture dbContextFixture) : base(dbContextFixture)
    {
        _repository = new OrderRepository(DbContextFixture.Context);
    }

    #region GetByFilter Tests

    [Fact]
    public void GetByFilter_WithoutFilters_ShouldReturnAllOrders()
    {
        // Arrange
        var orders = new[]
        {
            new OrderBuilder()
                .WithClientName("Client 1")
                .WithStatus(StatusOrder.New)
                .Build(),
            new OrderBuilder()
                .WithClientName("Client 2")
                .WithStatus(StatusOrder.Paid)
                .Build(),
            new OrderBuilder()
                .WithClientName("Client 3")
                .WithStatus(StatusOrder.Canceled)
                .Build()
        };

        DbContextFixture.Context.Orders.AddRange(orders);
        DbContextFixture.Context.SaveChanges();

        var filter = new OrderFilter();

        // Act
        var result = _repository.GetByFilter(filter).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().OnlyContain(o => o.Items != null);
    }

    [Theory]
    [InlineData("New", StatusOrder.New)]
    [InlineData("Paid", StatusOrder.Paid)]
    [InlineData("Canceled", StatusOrder.Canceled)]
    public void GetByFilter_WithStatusFilter_ShouldReturnOnlyOrdersWithThatStatus(string statusFilter, StatusOrder expectedStatus)
    {
        // Arrange
        var orders = new[]
        {
            new OrderBuilder().WithStatus(StatusOrder.New).Build(),
            new OrderBuilder().WithStatus(StatusOrder.Paid).Build(),
            new OrderBuilder().WithStatus(StatusOrder.Canceled).Build()
        };

        DbContextFixture.Context.Orders.AddRange(orders);
        DbContextFixture.Context.SaveChanges();

        var filter = new OrderFilter { Status = statusFilter };

        // Act
        var result = _repository.GetByFilter(filter).ToList();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().OnlyContain(o => o.Status == expectedStatus);
    }

    [Fact]
    public void GetByFilter_WithInvalidStatus_ShouldReturnAllOrders()
    {
        // Arrange
        var orders = new[]
        {
            new OrderBuilder().WithStatus(StatusOrder.New).Build(),
            new OrderBuilder().WithStatus(StatusOrder.Paid).Build()
        };

        DbContextFixture.Context.Orders.AddRange(orders);
        DbContextFixture.Context.SaveChanges();

        var filter = new OrderFilter { Status = "InvalidStatus" };

        // Act
        var result = _repository.GetByFilter(filter).ToList();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public void GetByFilter_WithEmptyStatus_ShouldReturnAllOrders()
    {
        // Arrange
        var orders = new[]
        {
            new OrderBuilder().WithStatus(StatusOrder.New).Build(),
            new OrderBuilder().WithStatus(StatusOrder.Paid).Build()
        };

        DbContextFixture.Context.Orders.AddRange(orders);
        DbContextFixture.Context.SaveChanges();

        var filter = new OrderFilter { Status = string.Empty };

        // Act
        var result = _repository.GetByFilter(filter).ToList();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public void GetByFilter_ShouldIncludeItems()
    {
        // Arrange
        var item1 = new ItemOrderBuilder()
            .WithProductName("Product 1")
            .WithAmount(2)
            .WithUnitPrice(10)
            .Build();

        var item2 = new ItemOrderBuilder()
            .WithProductName("Product 2")
            .WithAmount(1)
            .WithUnitPrice(20)
            .Build();

        var order = new OrderBuilder()
            .WithItems(new[] { item1, item2 })
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();

        var filter = new OrderFilter();

        // Act
        var result = _repository.GetByFilter(filter).ToList();

        // Assert
        result.Should().HaveCount(1);
        result.First().Items.Should().HaveCount(2);
        result.First().Items.Should().Contain(i => i.ProductName == "Product 1");
        result.First().Items.Should().Contain(i => i.ProductName == "Product 2");
    }

    [Fact]
    public void GetByFilter_ShouldReturnUntracked()
    {
        // Arrange
        var order = new OrderBuilder().Build();
        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();

        var filter = new OrderFilter();

        // Act
        var result = _repository.GetByFilter(filter).First();

        // Assert
        var entityEntry = DbContextFixture.Context.Entry(result);
        entityEntry.State.Should().Be(EntityState.Detached);
    }

    [Fact]
    public void GetByFilter_WithSort_ShouldApplySorting()
    {
        // Arrange
        var baseDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        
        var orders = new[]
        {
            new OrderBuilder()
                .WithClientName("Client A")
                .WithCreatedAt(baseDate)
                .Build(),
            new OrderBuilder()
                .WithClientName("Client B")
                .WithCreatedAt(baseDate.AddDays(1))
                .Build(),
            new OrderBuilder()
                .WithClientName("Client C")
                .WithCreatedAt(baseDate.AddDays(2))
                .Build()
        };

        DbContextFixture.Context.Orders.AddRange(orders);
        DbContextFixture.Context.SaveChanges();
        DbContextFixture.Context.ChangeTracker.Clear();

        var filter = new OrderFilter { Sort = "-CreatedAt" };

        // Act
        var result = _repository.GetByFilter(filter).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().BeInDescendingOrder(o => o.CreatedAt);
        result[0].ClientName.Should().Be("Client C");
        result[1].ClientName.Should().Be("Client B");
        result[2].ClientName.Should().Be("Client A");
    }

    #endregion

    #region GetByIdWithItemsAsync Tests

    [Fact]
    public async Task GetByIdWithItemsAsync_WithValidId_ShouldReturnOrderWithItems()
    {
        // Arrange
        var item = new ItemOrderBuilder()
            .WithProductName("Product Test")
            .WithAmount(3)
            .WithUnitPrice(15.50m)
            .Build();

        var order = new OrderBuilder()
            .WithClientName("Test Client")
            .WithItems(new[] { item })
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(orderId);
        result.ClientName.Should().Be("Test Client");
        result.Items.Should().HaveCount(1);
        result.Items.First().ProductName.Should().Be("Product Test");
    }

    [Fact]
    public async Task GetByIdWithItemsAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdWithItemsAsync(nonExistentId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdWithItemsAsync_ShouldReturnUntracked()
    {
        // Arrange
        var order = new OrderBuilder().Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        var entityEntry = DbContextFixture.Context.Entry(result);
        entityEntry.State.Should().Be(EntityState.Detached);
    }

    [Fact]
    public async Task GetByIdWithItemsAsync_WithMultipleItems_ShouldReturnAllItems()
    {
        // Arrange
        var items = new[]
        {
            new ItemOrderBuilder().WithProductName("Product 1").Build(),
            new ItemOrderBuilder().WithProductName("Product 2").Build(),
            new ItemOrderBuilder().WithProductName("Product 3").Build()
        };

        var order = new OrderBuilder()
            .WithItems(items)
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(3);
    }

    #endregion

    #region GetByIdWithItemsTrackingAsync Tests

    [Fact]
    public async Task GetByIdWithItemsTrackingAsync_WithValidId_ShouldReturnOrderWithItems()
    {
        // Arrange
        var item = new ItemOrderBuilder()
            .WithProductName("Tracked Product")
            .WithAmount(2)
            .WithUnitPrice(25.00m)
            .Build();

        var order = new OrderBuilder()
            .WithClientName("Tracked Client")
            .WithItems(new[] { item })
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsTrackingAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(orderId);
        result.ClientName.Should().Be("Tracked Client");
        result.Items.Should().HaveCount(1);
        result.Items.First().ProductName.Should().Be("Tracked Product");
    }

    [Fact]
    public async Task GetByIdWithItemsTrackingAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdWithItemsTrackingAsync(nonExistentId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdWithItemsTrackingAsync_ShouldReturnTrackedEntity()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithStatus(StatusOrder.New)
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsTrackingAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        var entityEntry = DbContextFixture.Context.Entry(result);
        entityEntry.State.Should().Be(EntityState.Unchanged);
    }

    [Fact]
    public async Task GetByIdWithItemsTrackingAsync_WhenModified_ShouldBeTracked()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithStatus(StatusOrder.New)
            .WithClientName("Original Name")
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsTrackingAsync(orderId);
        
        var property = result.GetType().GetProperty("ClientName");
        property?.SetValue(result, "Modified Name");

        // Assert
        var entityEntry = DbContextFixture.Context.Entry(result);
        entityEntry.State.Should().Be(EntityState.Modified);
    }

    [Fact]
    public async Task GetByIdWithItemsTrackingAsync_ItemsShouldBeTracked()
    {
        // Arrange
        var items = new[]
        {
            new ItemOrderBuilder().WithProductName("Item 1").Build(),
            new ItemOrderBuilder().WithProductName("Item 2").Build()
        };

        var order = new OrderBuilder()
            .WithItems(items)
            .Build();

        DbContextFixture.Context.Orders.Add(order);
        DbContextFixture.Context.SaveChanges();
        
        var orderId = order.Id;
        DbContextFixture.Context.ChangeTracker.Clear();

        // Act
        var result = await _repository.GetByIdWithItemsTrackingAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        
        foreach (var item in result.Items)
        {
            var itemEntry = DbContextFixture.Context.Entry(item);
            itemEntry.State.Should().Be(EntityState.Unchanged);
        }
    }

    #endregion
}
