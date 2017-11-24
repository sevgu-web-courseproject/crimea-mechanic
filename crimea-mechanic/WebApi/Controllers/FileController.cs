using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using Common.Exceptions;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/File")]
    public class FileController : ApiControllerBase
    {

        #region Fields

        private readonly IFileManager _manager;

        #endregion

        #region Constructor

        public FileController(IFileManager manager)
        {
            _manager = manager;
        }

        #endregion

        [HttpGet]
        [Route("{carServiceId:long}/{fileId:long}")]
        public async Task<HttpResponseMessage> Get(long carServiceId, long fileId)
        {
            try
            {
                var file = _manager.GetCarServiceFile(carServiceId, fileId);
                using (var stream = new FileStream(file.Path, FileMode.Open))
                using (var streamContent = new StreamContent(stream))
                {
                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync())
                    };
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = file.Name
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    return result;
                }
            }
            catch (BusinessFaultException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (FileNotFoundException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
