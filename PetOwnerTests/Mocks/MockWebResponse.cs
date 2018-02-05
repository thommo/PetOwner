using System.IO;
using System.Net;

namespace PetOwnerTests.Mocks
{
    public class MockWebResponse : WebResponse
    {
        private Stream _responseStream;

        /// <summary>
        /// Create a new instance of MockWebResponse
        /// </summary>
        /// <param name="responseStream"></param>
        public MockWebResponse(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        /// <summary>
        /// Override WebResponse.GeResponseStream
        /// </summary>
        public override Stream GetResponseStream()
        {
            return _responseStream;
        }
    }
}
