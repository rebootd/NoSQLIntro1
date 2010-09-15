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
		private IDocumentSession _session = null;
		
		public TestBase()
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080/" };
            _documentStore.Initialize();

            //create indexes
            Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof(BlogRavenDB.Indexes.PostsByPublished).Assembly, _documentStore);
        }

        ~TestBase()
        {
			if (_session != null && _session.HasChanges)
				_session.SaveChanges();

			if (_documentStore != null)
				_documentStore.Dispose();
            _documentStore = null;
        }

        public IDocumentSession DocumentSession
        {
            get
            {
				if (_session == null)
					_session = _documentStore.OpenSession();
				return _session;
            }
        }
    }
}