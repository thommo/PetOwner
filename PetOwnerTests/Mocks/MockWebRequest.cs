using PetOwnerTests.Mocks;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetOwnerTests
{
    public class MockWebRequest : WebRequest
    {
        private MemoryStream _requestStream { get; set; }
        private MemoryStream _responseStream;

        public override string ContentType { get; set; }
        public override long ContentLength { get; set; }

        /// <summary>Initializes a new instance of <see cref="TestWebRequest"/> 
        /// with the response to return.</summary>
        public MockWebRequest(string response)
        {
            _responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
        }

        /// <summary>Returns the request contents as a string.</summary>
        public string ContentAsString()
        {
            return System.Text.Encoding.UTF8.GetString(_requestStream.ToArray());
        }

        /// <summary>See <see cref="WebRequest.GetRequestStream"/>.</summary>
        public override Stream GetRequestStream()
        {
            return _requestStream;
        }

        /// <summary>See <see cref="WebRequest.GetResponse"/>.</summary>
        public override WebResponse GetResponse()
        {
            return new MockWebResponse(_responseStream);
        }

        public override async Task<WebResponse> GetResponseAsync()
        {
            return new MockWebResponse(_responseStream);
        }

        public override string Method { get; set; }
    }
}
