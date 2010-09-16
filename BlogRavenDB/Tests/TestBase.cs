using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Raven.Database;
using Raven.Client;
using Raven.Client.Document;
using Raven.Database.Indexing;
using BlogRavenDB.Models;

namespace BlogRavenDB.Tests
{
    public class TestBase
    {
        private static readonly string _connectionStringHost = ConfigurationManager.AppSettings["connectionStringHost"];
        private DocumentStore _documentStore;
		
		public TestBase()
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080/" };
            _documentStore.Initialize();

            //create indexes
            Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof(BlogRavenDB.Indexes.PostsByPublished).Assembly, _documentStore);
        }

        ~TestBase()
        {
			if (_documentStore != null)
				_documentStore.Dispose();
            _documentStore = null;
        }

        public IDocumentSession DocumentSession
        {
            get
            {
				return _documentStore.OpenSession();
            }
        }

        protected List<T> fetch_by_index<T>(string index)
        {
            return DocumentSession.LuceneQuery<T>(index)
                .ToList();
        }
    }
}