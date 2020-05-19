using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Linq.Expressions;
using NewRelic.Api.Agent;
using Microsoft.Azure.Documents;

namespace MovieDocumentsClub
{
    public class CosmosRepository<T>
    {
        private DocumentClient _client;

        private bool _disposed = false;

        public CosmosRepository(string endpoint, string key)
        {
            _client = new DocumentClient(new Uri(endpoint), key);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose any managed objects
                    // ...
                }

                // Now disposed of any unmanaged objects
                _client.Dispose();

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        [Trace]
        public Database CreateDatabase(string databaseId)
        {
            var db = _client.CreateDatabaseAsync(new Database { Id = databaseId }).Result;
            return db;
        }

        [Trace]
        public Database CreateDatabaseIfNotExistsAsync(string databaseId)
        {
            var db = _client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId }).Result;
            return db;
        }

        [Trace]
        public Document CreateDocument(string collectionLink, T newItem)
        {
            var resp = _client.CreateDocumentAsync(collectionLink, newItem).Result;
            return resp.Resource;
        }

        [Trace]
        public Document CreateDocumentFromUri(Uri collectionUri, T newItem)
        {
            var resp = _client.CreateDocumentAsync(collectionUri, newItem).Result;
            return resp.Resource;
        }

        [Trace]
        public DocumentCollection CreateDocumentCollection(string dbLink, string collectionId)
        {
            var resp = _client.CreateDocumentCollectionAsync(dbLink, new DocumentCollection { Id = collectionId }).Result;
            return resp.Resource;
        }

        [Trace]
        public DocumentCollection CreateDocumentCollectionFromUri(Uri dbUri, string collectionId)
        {
            var resp = _client.CreateDocumentCollectionAsync(dbUri, new DocumentCollection { Id = collectionId }).Result;
            return resp.Resource;
        }

        [Trace]
        public DocumentCollection CreateDocumentCollectionIfNotExists(string dbLink, string collectionId)
        {
            var resp = _client.CreateDocumentCollectionIfNotExistsAsync(dbLink, new DocumentCollection { Id = collectionId }).Result;
            return resp.Resource;
        }

        [Trace]
        public DocumentCollection CreateDocumentCollectionIfNotExistsFromUri(Uri dbUri, string collectionId)
        {
            var resp = _client.CreateDocumentCollectionIfNotExistsAsync(dbUri, new DocumentCollection { Id = collectionId }).Result;
            return resp.Resource;
        }

        [Trace]
        public Database ReadDatabase(string databaseId)
        {
            //URI is of the form "/dbs/{db identifier}"
            var response = _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).Result;
            var db = response.Resource;
            return db;
        }

        [Trace]
        public DocumentCollection CreateorReadCollection(string dbLink, string collectionId)
        {
            var c = _client.CreateDocumentCollectionIfNotExistsAsync(dbLink, new DocumentCollection { Id = collectionId }).Result;
            return c.Resource;
        }

        [Trace]
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        //TODO: CreatePermissionAsync
        //TODO: CreateStoredProcedureAsync
        //TODO: CreateTriggerAsync
        //TODO: CreateUserAsync
        //TODO: CreateUserDefinedFunctionAsync

        //DeleteDatabaseAsync
        [Trace]
        public void DeleteDocument(string documentLink)
        {
            var resp = _client.DeleteDocumentAsync(documentLink).Result;
        }

        [Trace]
        public void DeleteDocumentFromUri(Uri documentUri)
        {
            var resp = _client.DeleteDocumentAsync(documentUri).Result;
        }

        //DeleteDocumentCollectionAsync
        //DeletePermissionAsync
        //DeleteStoredProcedureAsync
        //DeleteTriggerAsync
        //DeleteUserAsync
        //DeleteUserDefinedFunctionAsync

        //ExecuteStoredProcedureAsync // THIS SIGNATURE IS DIFFER

        //ReadDocumentAsync
        //ReadDocumentCollectionAsync
        //ReadDocumentCollectionFeedAsync
        //ReadDocumentFeedAsync

        //Replace methods

        //Update and Upsert methods
    }
}
