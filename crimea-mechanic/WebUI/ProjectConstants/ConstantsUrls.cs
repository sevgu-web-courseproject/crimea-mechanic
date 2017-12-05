namespace WebUI.ProjectConstants
{
    public static class ConstantsUrls
    {
        public const string WebApiUri = "http://localhost:56000";

        public static string WebApiRegistrationUserUrl => Flurl.Url.Combine(WebApiUri, "api/Account/RegistrationUser");

        public static string WebApiRegistrationCarServiceUrl => Flurl.Url.Combine(WebApiUri, "api/Account/RegistrationCarService");

        public static string WebApiGetCitiesUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/GetCities");

        public static string WebApiGetCarServicesUrl => Flurl.Url.Combine(WebApiUri, "api/CarService/GetCarServices");

        public static string WebApiGetPhotoUrl => Flurl.Url.Combine(WebApiUri, "api/File");

        public static string WebApiGetGetCarServiceCard(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/GetCarServiceCard", carServiceId);

        public static string WebApiGetRegistrationRequestsUrl => Flurl.Url.Combine(WebApiUri, "api/CarService/GetRegistrationRequests");

        public static string WebApiGetRegistrationCardUrl(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/GetRegistrationCard", carServiceId);

        public static string WebApiApproveCarServiceUrl(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/ApproveCarService", carServiceId);

        public static string WebApiRejectCarServiceUrl => Flurl.Url.Combine(WebApiUri, "api/CarService/RejectCarService");

        public static string WebApiBlockCarServiceUrl(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/BlockCarService", carServiceId);

        public static string WebApiGetUserCarsUrl => Flurl.Url.Combine(WebApiUri, "api/Car/GetCars");

        public static string WebApiGetMarksUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/GetMarks");

        public static string WebApiGetModelsUrl(string markId) => Flurl.Url.Combine(WebApiUri, "api/Storage/GetModels", markId);

        public static string WebApiAddCarUrl => Flurl.Url.Combine(WebApiUri, "api/Car/AddCar");

        public static string WebApiGetCarUrl(string carId) => Flurl.Url.Combine(WebApiUri, "api/Car/GetCar", carId);

        public static string WebApiEditCarUrl => Flurl.Url.Combine(WebApiUri, "api/Car/EditCar");

        public static string WebApiDeleteCarUrl(string carId) => Flurl.Url.Combine(WebApiUri, "api/Car/DeleteCar", carId);

        public static string WebApiRestoreCarUrl(string carId) => Flurl.Url.Combine(WebApiUri, "api/Car/RestoreCar", carId);

        public static string WebApiGetUserApplicationsUrl => Flurl.Url.Combine(WebApiUri, "api/Application/GetInfosForUser");

        public static string WebApiGetApplicationCardForUserUrl(string applicationId) => Flurl.Url.Combine(WebApiUri, "api/Application/GetInfoForUser", applicationId);

        public static string WebApiDeleteApplicationUrl(string applicationId) => Flurl.Url.Combine(WebApiUri, "api/Application/Delete", applicationId);

        public static string WebApiAcceptOfferUrl(string offerId) => Flurl.Url.Combine(WebApiUri, "api/Application/AcceptOffer", offerId);

        public static string WebApiRejectApplicationUrl(string applicationId) => Flurl.Url.Combine(WebApiUri, "api/Application/RejectApplication", applicationId);

        public static string WebApiCompleteApplicationUrl(string applicationId) => Flurl.Url.Combine(WebApiUri, "api/Application/CompleteApplication", applicationId);

        public static string WebApiGetPoolUrl => Flurl.Url.Combine(WebApiUri, "api/Application/GetPool");

        public static string WebApiGetMarksFromPoolUrl => Flurl.Url.Combine(WebApiUri, "api/Application/GetMarksFromPool");

        public static string WebApiAddOfferUrl => Flurl.Url.Combine(WebApiUri, "api/Application/AddOffer");

        public static string WebApiDeleteOfferUrl(string offerId) => Flurl.Url.Combine(WebApiUri, "api/Application/DeleteOffer", offerId);

        public static string WebApiGetActiveCarsUrl => Flurl.Url.Combine(WebApiUri, "api/Car/GetActiveCars");

        public static string WebApiCreateApplicationUrl => Flurl.Url.Combine(WebApiUri, "api/Application/Create");

        public static string WebApiEditApplicationUrl => Flurl.Url.Combine(WebApiUri, "api/Application/Edit");

        public static string WebApiGetInfosForServiceUrl => Flurl.Url.Combine(WebApiUri, "api/Application/GetInfosForService");

        public static string WebApiGetInfoForCarServiceUrl(string applicationId) => Flurl.Url.Combine(WebApiUri, "api/Application/GetInfoForCarService", applicationId);

        public static string WebApiGetInfosForAdministratorUrl => Flurl.Url.Combine(WebApiUri, "api/Application/GetInfosForAdministrator");

        public static string WebApiGetInfoForAdminUrl(string applicationId) => Flurl.Url.Combine(WebApiUri, "api/Application/GetInfoForAdmin", applicationId);

        public static string WebApiUnBlockCarServiceUrl(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/UnBlockCarService", carServiceId);

        public static string WebApiAddReviewUrl => Flurl.Url.Combine(WebApiUri, "api/CarServiceReview/Add");

        public static string WebApiDeleteReviewUrl(string reviewId) => Flurl.Url.Combine(WebApiUri, "api/CarServiceReview/Delete", reviewId);

        public static string WebApiGetWorkClassesUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/GetWorkClasses");

        public static string WebApiGetWorkTypesFromPool => Flurl.Url.Combine(WebApiUri, "api/Application/GetWorkTypesFromPool");

        public static string WebApiGetCarServiceProfileUrl => Flurl.Url.Combine(WebApiUri, "api/CarService/GetProfile");

        public static string WebApiGetCarServiceInfoForEditUrl => Flurl.Url.Combine(WebApiUri, "api/CarService/GetInfoForEdit");

        public static string WebApiEditCarServiceUrl => Flurl.Url.Combine(WebApiUri, "api/CarService/Edit");

        public static string WebApiGetClientProfileUrl => Flurl.Url.Combine(WebApiUri, "api/Account/GetProfile");

        public static string WebApiEditClientProfileUrl => Flurl.Url.Combine(WebApiUri, "api/Account/EditProfile");

        public static string WebApiAddCityUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/AddCity");

        public static string WebApiAddCarMarkUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/AddCarMark");

        public static string WebApiAddCarModelUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/AddCarModel");

        public static string WebApiAddWorkClassUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/AddWorkClass");

        public static string WebApiAddWorkTypeUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/AddWorkType");

        public static string WebApiDeleteCityUrl(string cityId) => Flurl.Url.Combine(WebApiUri, "api/Storage/DeleteCity", cityId);

        public static string WebApiDeleteMarkUrl(string markId) => Flurl.Url.Combine(WebApiUri, "api/Storage/DeleteMark", markId);

        public static string WebApiDeleteModelUrl(string modelId) => Flurl.Url.Combine(WebApiUri, "api/Storage/DeleteModel", modelId);

        public static string WebApiDeleteWorkClassUrl(string workClassId) => Flurl.Url.Combine(WebApiUri, "api/Storage/DeleteWorkClass", workClassId);

        public static string WebApiDeleteWorkTypeUrl(string workTypeId) => Flurl.Url.Combine(WebApiUri, "api/Storage/DeleteWorkType", workTypeId);
    }
}