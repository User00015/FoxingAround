using System;

namespace MVC5App.Services
{
    public class RandomNumber : IRandomNumber
    {
        public int Next(int quantity)
        {
           return new Random().Next(quantity / 2, quantity); 
        }
    }

    public class MockRandomNumber : IRandomNumber
    {
        public int Next(int quantity)
        {
            return quantity;
        }
    }

    public interface IRandomNumber
    {
        int Next(int quantity);
    }
}