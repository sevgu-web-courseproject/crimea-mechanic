using System;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Review;
using BusinessLogic.Resources;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class CarServiceReviewManager : BaseManager, ICarServiceReviewManager
    {

        #region Fields

        private readonly IValidationManager _validationManager;

        #endregion

        #region Constructor

        public CarServiceReviewManager(IUnitOfWork unitOfWork, IUserInternalManager userManager, IValidationManager validationManager) : base(unitOfWork, userManager)
        {
            _validationManager = validationManager;
        }

        #endregion

        #region Implemetation of ICarServiceReviewManager

        public void Add(AddReviewDto dto, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);
            var user = UserManager.CheckAndGet(currentUserId);

            var carService = UnitOfWork.Repository<ICarServiceRepository>().Get(dto.ServiceId);
            if (carService == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceNotFound);
            }

            if (carService.Reviews.Any(rev => !rev.IsDeleted && rev.User.ApplicationUser.Id == currentUserId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ReviewAlreadyContains);
            }

            var validationResult = _validationManager.ValidateOperateReviewDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var review = Mapper.Map<CarServiceReview>(dto);
            review.User = user.UserProfile;
            review.Service = carService;
            carService.Points += review.Mark;

            review.Service.Reviews.Add(review);
            UnitOfWork.SaveChanges();
        }

        public void Edit(EditReviewDto dto, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var validationResult = _validationManager.ValidateOperateReviewDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var review = UnitOfWork.Repository<ICarServiceReviewRepository>().Get(dto.ReviewId);
            if (review == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ReviewNotFound);
            }

            if (review.User.ApplicationUser.Id != currentUserId)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ReviewDoesNotBelongToUser);
            }

            // Убираем очки от старого отзыва и добавляем новые
            review.Service.Points -= review.Mark;
            review.Service.Points += dto.Mark;

            review.Mark = dto.Mark;
            review.Review = dto.Review;
            review.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public void Delete(long reviewId, string currentUserId)
        {
            var isRegular = UserManager.IsUserInRole(Common.Constants.CommonRoles.Regular, currentUserId);
            if (!isRegular &&
                !UserManager.IsUserInRole(Common.Constants.CommonRoles.Administrator, currentUserId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserHasDifferentRole);
            }

            var review = UnitOfWork.Repository<ICarServiceReviewRepository>().Get(reviewId);
            if (review == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ReviewNotFound);
            }

            if (isRegular && review.User.ApplicationUser.Id != currentUserId)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ReviewDoesNotBelongToUser);
            }

            review.Service.Points -= review.Mark;

            review.IsDeleted = true;
            review.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        #endregion
    }
}
