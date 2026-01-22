using System.Reflection;
using WizCo.Domain.Entities;
using WizCo.Domain.Enums;

namespace WizCo.Infrastructure.Tests.Builders;

public class OrderBuilder
{
    private string _clientName = "Client Default";
    private readonly List<ItemOrder> _items = [];
    private StatusOrder? _status;
    private DateTimeOffset? _createdAt;
    private Guid? _id;

    public OrderBuilder WithClientName(string clientName)
    {
        _clientName = clientName;
        return this;
    }

    public OrderBuilder WithItem(ItemOrder item)
    {
        _items.Add(item);
        return this;
    }

    public OrderBuilder WithItems(IEnumerable<ItemOrder> items)
    {
        _items.AddRange(items);
        return this;
    }

    public OrderBuilder WithStatus(StatusOrder status)
    {
        _status = status;
        return this;
    }

    public OrderBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public OrderBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public Order Build()
    {
        var items = _items.Any() 
            ? _items 
            : new List<ItemOrder> { new ItemOrderBuilder().Build() };

        var order = new Order(_clientName, items);

        if (_id.HasValue)
        {
            SetPrivateField(order, "<Id>k__BackingField", _id.Value);
        }

        if (_status.HasValue)
        {
            SetPrivateField(order, "<Status>k__BackingField", _status.Value);
        }

        if (_createdAt.HasValue)
        {
            SetPrivateField(order, "<CreatedAt>k__BackingField", _createdAt.Value);
        }

        return order;
    }

    private static void SetPrivateField<T>(object obj, string fieldName, T value)
    {
        var field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (field != null)
        {
            field.SetValue(obj, value);
        }
    }

    public static implicit operator Order(OrderBuilder builder)
    {
        return builder.Build();
    }
}
