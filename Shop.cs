using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Lab6_team1
{
    [Serializable]
    public class Shop : INotifyPropertyChanged
    {
        private Random rand = new Random();
        private ObservableCollection<Client> clientsList = new ObservableCollection<Client>();
        private ObservableCollection<Product> storage = new ObservableCollection<Product>();

        public event PropertyChangedEventHandler PropertyChanged;

        public double cashBox { get; set; }
        public Shop(ObservableCollection<Product> storage, double cashBox)
        {
            this.cashBox = cashBox;
            this.storage = storage;
        }
        public Shop() { }
        public ObservableCollection<Product> Storage
        {
            get
            {
                return storage;
            }
            set
            {
                storage = value;
                OnPropertyChanged(nameof(Storage));
            }

        }

        public ObservableCollection<Client> Clients
        {
            get
            {
                return clientsList;
            }
            set
            {
                clientsList = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
        protected void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void generateCliensList()
        {
            int clientCountRand = rand.Next(1, 10);
            for (int i = 0; i < clientCountRand; i++)
            {
                clientsList.Add(new Client(i));
                OnPropertyChanged(nameof(clientCountRand));
            }
        }

        public void generateStorage()
        {
            int productAmount = rand.Next(1, 5);
            for (int i = 0; i < productAmount; i++)
            {
                int productID = rand.Next(0, storage.Count);

                if (storage.ElementAt(productID).productInitialPrice < cashBox)
                {
                    Product product = storage.ElementAt(productID);
                    product.productCount++;
                    storage.RemoveAt(productID);
                    storage.Insert(productID, product);
                    cashBox -= storage.ElementAt(productID).productInitialPrice;
                }
            }
        }
        public void chooseClient()
        {
            for (int i = 0; i < clientsList.Count; i++)
            {
                int currentClientID = rand.Next(0, clientsList.Count);
                buyProduct();
                clientsList.RemoveAt(currentClientID);
            }
        }

        public void buyProduct()
        {
                int productAmount = rand.Next(1, 5);

                for (int i = 0; i <= productAmount; i++)
                {
                    int productID = rand.Next(0, storage.Count);

                    if (storage.ElementAt(productID).productCount > 0)
                    {
                        Product product = storage.ElementAt(productID);
                        product.productCount--;
                        storage.RemoveAt(productID);
                        storage.Insert(productID, product);
                        cashBox += storage.ElementAt(productID).productSellingPrice;

                    }

                }
        }
    }

}
