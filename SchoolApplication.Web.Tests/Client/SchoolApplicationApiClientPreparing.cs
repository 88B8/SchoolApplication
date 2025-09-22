using System.Text;

namespace SchoolApplication.Web.Tests.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SchoolApplicationApiClient    
    {
        /// <summary>
        /// 
        /// </summary>
        Task PrepareRequestAsync(HttpClient client_, HttpRequestMessage request_, StringBuilder urlBuilder_, CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        Task PrepareRequestAsync(HttpClient client_, HttpRequestMessage request_, string url_, CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        Task ProcessResponseAsync(HttpClient client_, HttpResponseMessage response_, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
