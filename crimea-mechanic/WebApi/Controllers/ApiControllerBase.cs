using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected virtual IHttpActionResult CallBusinessLogicActionWithResult<T>(Func<string> bllAction)
        {
            try
            {
                var result = bllAction();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected virtual async Task<IHttpActionResult> CallBusinessLogicActionAsyncWithResult<T>(Func<Task<string>> bllAction)
        {
            try
            {
                var result = await bllAction();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected virtual IHttpActionResult CallBusinessLogicAction(Action bllAction)
        {
            try
            {
                bllAction();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected virtual async Task<IHttpActionResult> CallBusinessLogicActionAsync(Func<Task> bllAction)
        {
            try
            {
                await bllAction();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}