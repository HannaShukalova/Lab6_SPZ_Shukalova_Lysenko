using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Data;

namespace Lab6_team1
{
    public partial class MainWindow : Window
    {
        private Shop shop = new Shop(new ObservableCollection<Product>() {
            new Product("Apple", 50, 5.5, 6.0),
            new Product("Cherry", 50, 10.5, 12.0),
            new Product("Orange", 50, 15.5, 17.5),
            new Product("Peach", 50, 12.0, 15.5),
            new Product("Tomato", 50, 7.3, 9.5),
            new Product("Potato", 50, 3.2, 4.5),
            new Product("Cucumber", 50, 4.1, 5.0)
        }, 1000);

        private string serialPath = ".\\shop.xml";

        Thread clientThread;
        Thread buyThread;
        Thread storageThread;

        Boolean firstMode = true;

        public MainWindow()
        {
            InitializeComponent();
            productList.ItemsSource = shop.Storage;
        }
        private void startNewShopButton_Click(object sender, RoutedEventArgs e)
        {
            clientThread = new Thread(generateClient);
            buyThread = new Thread(buyProduct);
            storageThread = new Thread(generateProduct);

            object lockitems = new object();
            BindingOperations.EnableCollectionSynchronization(shop.Storage, lockitems);

            clientThread.Start();
            buyThread.Start();
            storageThread.Start();
        }

        private void generateClient()
        {
            int i = 0;
            while (true)
            {
                Thread.Sleep(4000);
                if (firstMode)
                {
                    shop.generateCliensList();
                    Console.WriteLine(i + " client");
                    i++;
                }
            }
        }

        private void buyProduct()
        {
            int i = 0;
            while (true)
            {
                Thread.Sleep(2000);
                if (firstMode)
                {
                    shop.chooseClient();
                    Console.WriteLine(i + " product");
                    i++;
                }
            }
        }

        private void generateProduct()
        {
            int i = 0;
            while (true)
            {
                Thread.Sleep(2000);
                if (!firstMode)
                {
                    shop.generateStorage();
                    Console.WriteLine(i + " storage");
                    i++;
                }
            }
        }

        private void loadShopButton_Click(object sender, RoutedEventArgs e)
        {
            Shop deShop = deSerialXML();
            lblcashBox.Content = deShop.cashBox;
        }

        public void serialXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Shop));

            if (File.Exists(serialPath))
            {
                File.Delete(serialPath);
            }

            using (FileStream fs = new FileStream(serialPath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, shop);

                Console.WriteLine("...Serializer");
            }
        }

        public Shop deSerialXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Shop));
            Shop newShop;

            if (!File.Exists(serialPath))
            {
                throw new FileLoadException("File not exist");
            }

            using (FileStream fs = new FileStream(serialPath, FileMode.OpenOrCreate))
            {
                newShop = (Shop)formatter.Deserialize(fs);

                Console.WriteLine("...Deserializer");
            }

            return newShop;
        }

        public void stopButton_Click(object sender, RoutedEventArgs e)
        {
            clientThread.Abort();
            buyThread.Abort();
            storageThread.Abort();
        }

        public void changeMode_Click(object sender, RoutedEventArgs e)
        {
            if (firstMode)
            {
                firstMode = false;
                serialXML();
            }
            else
            {
                firstMode = true;
                serialXML();
            }
        }
    }
}
