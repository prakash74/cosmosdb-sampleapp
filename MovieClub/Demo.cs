using System;
using System.Threading;
using Microsoft.Azure.Documents.Client;
using NewRelic.Api.Agent;

namespace MovieDocumentsClub
{
    public class Demo
    {
        private CosmosRepository<Movie> _repo;
        private string endpoint = "https://preddycosmosdb.documents.azure.com:443/";
        private string key = "kWSMmIBdBYRzwWtY2EVwfAODN3pKBo8LGSx8u4Rr0ZmDymRz4wvxRV6sbl9SYa37SHE1R1uZHM5bwRJFJrJ8hw==";
        private string databaseName = "MoviesDb";
        private string collectionName = "Movie";

        public Demo()
        {
            _repo = new CosmosRepository<Movie>(endpoint, key);
        }

        [Transaction]
        public void RunDemo(int n)
        {
            Console.WriteLine("*************");
            Console.WriteLine("Create Database or Read existing one");
            var db = _repo.CreateDatabaseIfNotExistsAsync(databaseName);
            Console.WriteLine($"Database {db.Id} read. Its link is {db.SelfLink}");
            Console.WriteLine("*************\n\n");

            Console.WriteLine("*************");
            Console.WriteLine("Create DocumentCollection or read existing one");
            var collection = _repo.CreateorReadCollection(db.SelfLink, collectionName);
            Console.WriteLine($"Collection {collection.Id} read with link {collection.SelfLink}");
            Console.WriteLine("*************\n\n");

            Console.WriteLine("*************");
            Console.WriteLine("Putting new document using collection link");
            Movie movie = new Movie
            {
                MovieName = "LOTR" + n,
                Rating = "PG13",
                ReleaseDate = DateTime.Now
            };
            var doc = _repo.CreateDocument(collection.SelfLink, movie);
            Console.WriteLine($"Document {doc.Id} created with link {doc.SelfLink}");
            Console.WriteLine("*************\n\n");


            Console.WriteLine("*************");
            Console.WriteLine("Putting another document using collection URI");
            Movie movie2 = new Movie
            {
                MovieName = "Harry Potter " + n,
                Rating = "G",
                ReleaseDate = DateTime.Now
            };

            var doc2 = _repo.CreateDocumentFromUri(UriFactory.CreateDocumentCollectionUri(db.Id, collection.Id), movie2);
            Console.WriteLine($"Document {doc2.Id} created with link {doc2.SelfLink}");
            Console.WriteLine("*************\n\n");

            Console.WriteLine("***Sleeping...");
            Thread.Sleep(5000);
            Console.WriteLine("***Waking\n\n");

            Console.WriteLine("About to start deleting");

            _repo.DeleteDocument(doc.SelfLink);
            _repo.DeleteDocumentFromUri(UriFactory.CreateDocumentUri(db.Id, collection.Id, doc2.Id));
        }
    }
}
