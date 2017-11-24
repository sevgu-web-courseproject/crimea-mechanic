using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Review;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/CarServiceReview")]
    public class CarServiceReviewController : ApiControllerBase
    {
        #region Fields

        private readonly ICarServiceReviewManager _manager;

        #endregion

        #region Constructor

        public CarServiceReviewController(ICarServiceReviewManager manager)
        {
            _manager = manager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Добавить отзыв
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("Add")]
        public IHttpActionResult Add(AddReviewDto dto)
        {
            return CallBusinessLogicAction(() => _manager.Add(dto, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Редактировать отзыв
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("Edit")]
        public IHttpActionResult Edit(EditReviewDto dto)
        {
            return CallBusinessLogicAction(() => _manager.Edit(dto, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Удалить отзыв
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("Delete/{reviewId:long}")]
        public IHttpActionResult Delete(long reviewId)
        {
            return CallBusinessLogicAction(() => _manager.Delete(reviewId, User.Identity.GetUserId()));
        }

        #endregion
    }
}
