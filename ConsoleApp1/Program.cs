using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static List<Socket> sockList = new List<Socket>();

        static List<Book> contactList = new List<Book>();
        static void Main(string[] args)
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(10);
            LoadData("data.txt");
            Thread thread = new Thread(Process);
            thread.Start();
            while (true)
            {
                Socket client = server.Accept();
                sockList.Add(client);
                Console.WriteLine("{0}进来了", client.RemoteEndPoint.ToString());
            }
        }
        static void LoadData(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] items = sr.ReadLine().Split("\t");
                        Book c = new Book { Id = items[0], Bookname = items[1], Booktype = items[2], Bookauthor = items[3], Bookprice = items[4] };
                        contactList.Add(c);
                    }
                }
            }
        }
        private static void Process()
        {

            while (true)
            {
                foreach(Socket client in sockList)
                {
                    int n = client.Available;
                    if (n > 0)
                    {
                        byte[] data = new byte[n];
                        client.Receive(data, SocketFlags.None);
                        string msg = Encoding.GetEncoding("utf-8").GetString(data);
                        try
                        {
                            Book c = contactList.Single(t => t.Id == msg);
                            string reponse = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", c.Id, c.Bookname, c.Booktype, c.Bookauthor, c.Bookprice);
                            client.Send(Encoding.GetEncoding("utf-8").GetBytes(reponse));

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("{0} is not exist!", msg);
                        }
                    }
                     
                }
                Thread.Sleep(300);
            }
        }
    }
    class Book
    {
        private string id;
        private string bookname;
        private string booktype;
        private string bookauthor;
        private string bookprice;

        public string Id { get => id; set => id = value; }
        public string Bookname { get => bookname; set => bookname = value; }
        public string Bookauthor { get => bookauthor; set => bookauthor = value; }
        public string Bookprice { get => bookprice; set => bookprice = value; }
        public string Booktype { get => booktype; set => booktype = value; }
    }
}
