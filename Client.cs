using System;

namespace Lab6_team1
{
    [Serializable]
    public class Client
    {
        private int clientID { get; set; }

        public Client(int ID = 0) { 
            clientID = ID;
        }
        public Client() { }
    }
}
