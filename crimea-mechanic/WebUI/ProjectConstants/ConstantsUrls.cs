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

    }
}