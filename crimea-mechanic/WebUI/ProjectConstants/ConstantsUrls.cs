﻿namespace WebUI.ProjectConstants
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

        public static string WebApiRejectCarServiceUrl(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/RejectCarService", carServiceId);

        public static string WebApiBlockCarServiceUrl(string carServiceId) => Flurl.Url.Combine(WebApiUri, "api/CarService/BlockCarService", carServiceId);

        public static string WebApiGetUserCarsUrl => Flurl.Url.Combine(WebApiUri, "api/Car/GetCars");

        public static string WebApiGetMarksUrl => Flurl.Url.Combine(WebApiUri, "api/Storage/GetMarks");

        public static string WebApiGetModelsUrl(string markId) => Flurl.Url.Combine(WebApiUri, "api/Storage/GetModels", markId);

        public static string WebApiAddCarUrl => Flurl.Url.Combine(WebApiUri, "api/Car/AddCar");

        public static string WebApiGetCarUrl(string carId) => Flurl.Url.Combine(WebApiUri, "api/Car/GetCar", carId);

        public static string WebApiEditCarUrl => Flurl.Url.Combine(WebApiUri, "api/Car/EditCar");

        public static string WebApiDeleteCarUrl(string carId) => Flurl.Url.Combine(WebApiUri, "api/Car/DeleteCar", carId);

        public static string WebApiRestoreCarUrl(string carId) => Flurl.Url.Combine(WebApiUri, "api/Car/RestoreCar", carId);
    }
}