using System;

namespace Lab6_team1
{
    [Serializable]
    public class Product
    {
        public string productName { get; set; }
        public int productCount { get; set; }
        public double productInitialPrice { get; set; }
        public double productSellingPrice { get; set; }
        public Product(string Name = "", int Count = 0, double InitialPrice = 0.0, double SellingPrice = 0.0)
        {
            productName = Name;
            productCount = Count;
            productInitialPrice = InitialPrice;
            productSellingPrice = SellingPrice;
        }
        public Product() { }
        public override string ToString()
        {
            return "Name: " + productName 
                + " | Initial price: " + productInitialPrice 
                + " | Selling price: " + productSellingPrice 
                + " | Count: "+ productCount;
        }
    }


}
