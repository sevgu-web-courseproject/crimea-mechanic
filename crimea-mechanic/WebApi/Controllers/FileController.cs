using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/File")]
    public class FileController : ApiControllerBase
    {
        [HttpGet]
        [Route("{carServiceId:long}/{fileId:long}")]
        public async Task<HttpResponseMessage> Get(long carServiceId, long fileId)
        {
            var path = HttpContext.Current.Server.MapPath("~/Files/test.png");
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                using (var streamContent = new StreamContent(stream))
                {
                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync())
                    };
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "test.png"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    return result;
                }
            }
            catch (FileNotFoundException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
