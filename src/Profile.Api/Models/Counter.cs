namespace Profile.Api.Models;

public class Counter
{
    private int _quantity;

    public Counter()
    {
        _quantity = 0;
    }

    public int Quantity
    {
        get => _quantity;
    }
    
    public void Increment()
    {
        _quantity++;
    }

    public void Decrement()
    {
        _quantity--;
    }
}