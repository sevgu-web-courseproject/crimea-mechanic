using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BusinessLogic.Objects;
using Common.Exceptions;
using Newtonsoft.Json;

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
            catch (BusinessFaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
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
            catch (BusinessFaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
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
            catch (BusinessFaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
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
            catch (BusinessFaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Получить строковое значение из запроса
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        protected string GetStringFromRequest(string key)
        {
            return HttpContext.Current.Request.Params[key];
        }

        /// <summary>
        /// Получить список из запроса
        /// </summary>
        /// <typeparam name="T">Тип данных в списке</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        protected List<T> GetList<T>(string key)
        {
            var json = HttpContext.Current.Request.Params[key];
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        /// <summary>
        /// Сохранить одиночный файл
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="directory">Папка внутри директории Files</param>
        /// <returns></returns>
        protected FileDto SaveFile(string key, string directory)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var dirInfo = new DirectoryInfo(Path.Combine(HttpContext.Current.Server.MapPath("~/Files"), directory));
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                var file = HttpContext.Current.Request.Files.Get(key);

                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files"), directory, Guid.NewGuid().ToString("N")) + extension;

                    file.SaveAs(path);

                    return new FileDto
                    {
                        Name = file.FileName,
                        Path = path
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Сохранить множество файлов
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="directory">Папка внутри директории Files</param>
        /// <returns></returns>
        protected List<FileDto> SaveFiles(string key, string directory)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var result = new List<FileDto>();

                var dirInfo = new DirectoryInfo(Path.Combine(HttpContext.Current.Server.MapPath("~/Files"), directory));
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                var files = HttpContext.Current.Request.Files.GetMultiple(key);

                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files"), directory, Guid.NewGuid().ToString("N")) + extension;

                    file.SaveAs(path);

                    result.Add(new FileDto
                    {
                        Name = file.FileName,
                        Path = path
                    });
                }
                return result;
            }
            return null;
        }
    }
}