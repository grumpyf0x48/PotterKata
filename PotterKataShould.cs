using NFluent;
using NUnit.Framework;

namespace PotterKataTest;

public class PotterKataShould
{
    [Test]
    public void cost_8_Euro_when_user_buys_a_book()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        double price = basket.Price();
        Check.That(price).IsEqualTo(8);
    }

    [Test]
    public void cost_16_Euro_when_user_buys_two_items_of_the_same_book()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume1);
        double price = basket.Price();
        Check.That(price).IsEqualTo(16);
    }

    [Test]
    public void have_5_percent_discount_when_buys_two_items_of_different_books()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume2);
        double price = basket.Price();
        Check.That(price).IsEqualTo(2 * 8 * 0.95);
    }

    [Test]
    public void have_10_percent_discount_when_three_items_of_different_books()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume2);
        basket.Add(PotterTittle.Volume3);
        double price = basket.Price();
        Check.That(price).IsEqualTo(3 * 8 * 0.9);
    }
    
    [Test]
    public void have_20_percent_discount_when_four_items_of_different_books()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume2);
        basket.Add(PotterTittle.Volume3);
        basket.Add(PotterTittle.Volume4);
        double price = basket.Price();
        Check.That(price).IsEqualTo(4 * 8 * 0.8);
    }
    
    [Test]
    public void have_20_percent_discount_when_five_items_of_different_books()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume2);
        basket.Add(PotterTittle.Volume3);
        basket.Add(PotterTittle.Volume4);
        basket.Add(PotterTittle.Volume5);
        double price = basket.Price();
        Check.That(price).IsEqualTo(5 * 8 * 0.75);
    }

    [Test]
    public void have_sample_basket_costs_51_20_euro()
    {
        var basket = new Basket();
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume1);
        basket.Add(PotterTittle.Volume2);
        basket.Add(PotterTittle.Volume2);
        basket.Add(PotterTittle.Volume3);
        basket.Add(PotterTittle.Volume3);
        basket.Add(PotterTittle.Volume4);
        basket.Add(PotterTittle.Volume5);
        var price = basket.Price();
        Check.That(price).IsEqualTo(51.2);
    }
}

public class Basket
{
    private Dictionary<PotterTittle, int> books = new();
    private Dictionary<int, double> discounts = new()
    {
        [1] = 1,
        [2] = 0.95,
        [3] = 0.9,
        [4] = 0.8,
        [5] = 0.75
    };

    public void Add(PotterTittle potterTittle)
    {
        if (books.ContainsKey(potterTittle))
        {
            books[potterTittle] += 1;
        }
        else
        {
            books[potterTittle] = 1;
        }
    }

    public double ComputeDiscountedPrice(int differentBookCount)
    {
        return 8 * differentBookCount * discounts[differentBookCount];
    }

    public double Price()
    {
        double price = 0;
        
        Dictionary<PotterTittle, int> updatedBook = new(books);
        while (books.Count > 0)
        {
            price += ComputeDiscountedPrice(updatedBook.Keys.Count);
            foreach (var (key, value) in books)
            {
                if (value > 1)
                {
                    updatedBook[key] = value - 1;
                }
                else
                {
                    updatedBook.Remove(key);
                }

                books = updatedBook;
            }
        }

        return price;
    }
}

public enum PotterTittle
{
    Volume1,
    Volume2,
    Volume3,
    Volume4,
    Volume5
}