using Newtonsoft.Json;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using RavenRBD.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RavenRBD
{
    public class Raven
    {

        public List<Order> Orders { get; set; }

        public void GetData()
        {
            using (StreamReader file = new StreamReader(@"C:\Users\kubry\Desktop\RBD\data.json"))
            {
                string json = file.ReadToEnd();
                Orders = JsonConvert.DeserializeObject<List<Order>>(json);
            }
        }
        
        public void AddDocument()
        {
            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]                        
                {                                   
                   "http://127.0.0.1:8080"          
                },
                Database = "ForwarderCompany",             
                Conventions = { }                   
            })
            {
                store.Initialize();                 

                using (IDocumentSession session = store.OpenSession())  
                {
                    for(int i = 0; i < Orders.Count(); i++)
                    {
                        Order order = this.Orders[i];
                        session.Store(order);
                    }
                                                                                                               
                    session.SaveChanges();                             
                                                                        
                }
            }

        }

        public List<FirstQuery> Query1()
        {
            List<FirstQuery> tmp = new List<FirstQuery>();

            using (IDocumentStore store = new DocumentStore
                {
                    Urls = new[]
                    {
                       "http://127.0.0.1:8080"
                    },
                    Database = "ForwarderCompany",
                    Conventions = { }
                }
            )
            {
                store.Initialize();

                using IDocumentSession session = store.OpenSession();

                var firstTime = Stopwatch.StartNew();

                tmp = session.Query<Order>()
                    .GroupBy(x => x.forwarder.ForwarderId)
                    .Select(x => new FirstQuery
                        {
                            ForwardeId = x.Key,
                            OrdersPrice = x.Sum(x => x.price)
                        }
                    )
                    .OrderByDescending(x => x.OrdersPrice)
                    .Take(10)
                    .ToList();

                firstTime.Stop();

                Console.WriteLine($"FirstQuery: {firstTime.ElapsedMilliseconds}");
            }
            
            return tmp;
        }

        public List<SecondQuery> Query2()
        {
            List<SecondQuery> tmp = new List<SecondQuery>();

            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]
                    {
                       "http://127.0.0.1:8080"
                    },
                Database = "ForwarderCompany",
                Conventions = { }
            }
            )
            {
                store.Initialize();

                using IDocumentSession session = store.OpenSession();

                var firstTime = Stopwatch.StartNew();

                tmp = session.Query<Order>().ToList()
                    .GroupBy(x => x.load.Type)
                    .Select(x => new SecondQuery
                    {
                        LoadType = x.Key,
                        Amount = x.Count()
                    }
                    )
                    .OrderByDescending(x => x.Amount)
                    .ToList();

                firstTime.Stop();

                Console.WriteLine($"SecondQuery: {firstTime.ElapsedMilliseconds}");
            }

            return tmp;
        }

        public List<ThirdQuery> Query3()
        {
            List<ThirdQuery> tmp = new List<ThirdQuery>();

            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]
                    {
                       "http://127.0.0.1:8080"
                    },
                Database = "ForwarderCompany",
                Conventions = { }
            }
            )
            {
                store.Initialize();

                using IDocumentSession session = store.OpenSession();

                var firstTime = Stopwatch.StartNew();

                tmp = session.Query<Order>()
                    .GroupBy(x => x.vehicle.transporter.TransporterId)
                    .Select(x => new ThirdQuery
                    {
                        TransporterId = x.Key,
                        Amount = x.Count()
                    }
                    )
                    .OrderByDescending(x => x.Amount)
                    .Take(10)
                    .ToList();

                firstTime.Stop();

                Console.WriteLine($"ThirdQuery: {firstTime.ElapsedMilliseconds}");
            }

            return tmp;
        }
    }
}
