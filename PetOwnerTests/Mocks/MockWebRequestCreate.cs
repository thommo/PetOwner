using System;
using System.Net;

namespace PetOwnerTests
{
    internal class MockWebRequestCreate : IWebRequestCreate
    {
        static WebRequest _nextRequest;
        static object _lockObject = new object();

        static public WebRequest NextRequest
        {
            get { return _nextRequest; }
            set
            {
                lock (_lockObject)
                    _nextRequest = value;
            }
        }

        /// <summary>
        /// Create a WebRequest instance
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public WebRequest Create(Uri uri)
        {
            return _nextRequest;
        }
        public static MockWebRequest CreateTestRequest(string response)
        {
            MockWebRequest request = new MockWebRequest(response);
            NextRequest = request;
            return request;
        }
    }
}
