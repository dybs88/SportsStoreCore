using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SportsStore.Tests.Base
{
    public class TestSession : ISession
    {
        public TestSession()
        {
            _sessionValues = new Dictionary<string, object>();
        }

        private IDictionary<string, object> _sessionValues;

        public bool IsAvailable => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();

        public IEnumerable<string> Keys => _sessionValues.Keys;

        public void Clear()
        {
            _sessionValues.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value)
        {
            _sessionValues.Add(key, value);
        }

        public void Remove(string key)
        {
            _sessionValues.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            if (_sessionValues[key] != null)
            {
                value = Encoding.ASCII.GetBytes(_sessionValues[key].ToString());
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
