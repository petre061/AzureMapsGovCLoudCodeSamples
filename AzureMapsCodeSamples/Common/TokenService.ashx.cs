using Microsoft.Azure.Services.AppAuthentication;
using System.Threading.Tasks;
using System.Web;

namespace AzureMapsGovCloudCodeSamples.Common
{
    /// <summary>
    /// Summary description for TokenService
    /// </summary>
    public class TokenService : HttpTaskAsyncHandler
    {

        /// <summary>
        /// This token provider simplifies access tokens for Azure Resources. It uses the Managed Identity of the deployed resource.
        /// For instance if this application was deployed to Azure App Service or Azure Virtual Machine, you can assign an Azure AD
        /// identity and this library will use that identity when deployed to production.
        /// </summary>
        /// <remarks>
        /// For the Web SDK to authorize correctly, you still must assign Azure role based access control for the managed identity
        /// as explained here https://github.com/Azure-Samples/Azure-Maps-AzureAD-Samples/tree/master/src/ClientGrant/AzureMapsWebApiToken. 
        /// </remarks>
        private static readonly AzureServiceTokenProvider tokenProvider = new AzureServiceTokenProvider();

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            string accessToken = await tokenProvider.GetAccessTokenAsync("https://atlas.microsoft.com/");

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/plain";
            context.Response.Write(accessToken);
        }
    }
}