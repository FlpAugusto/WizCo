using WizCo.Domain.Entities;

namespace WizCo.Infrastructure.Tests.Builders;

public class ItemOrderBuilder
{
    private string _productName = "Product Default";
    private int _amount = 1;
    private decimal _unitPrice = 10.00m;

    public ItemOrderBuilder WithProductName(string productName)
    {
        _productName = productName;
        return this;
    }

    public ItemOrderBuilder WithAmount(int amount)
    {
        _amount = amount;
        return this;
    }

    public ItemOrderBuilder WithUnitPrice(decimal unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public ItemOrder Build()
    {
        return new ItemOrder(_productName, _amount, _unitPrice);
    }

    public static implicit operator ItemOrder(ItemOrderBuilder builder)
    {
        return builder.Build();
    }
}
