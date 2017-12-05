var operateCitiesVM = new function() {

    var cities = ko.observableArray([]);
    var cityName = ko.observable().extend({
        required: { params: true, message: "Необходимо указнать наименнование города"} // TODO
    });

    var getCities = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetCitiesUrl)
            .then(function (data) {
                cities(data);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var addCity = function() {
        var validationGroup = ko.validation.group([cityName]);
        if (validationGroup().length !== 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            Name: cityName()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiAddCityUrl, data)
            .then(function() {
                cityName(undefined);
                $('#close-button').click();
                validationGroup.showAllMessages(false);
                getCities();
                notificationHelper.success(window.resource.texts.success, "Новый город успешно добавлен"); //TODO
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteCity = function (city) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteCityUrl.replace("cityId", city.Id);
        ajaxHelper.getWithoutResult(url)
            .then(function() {
                getCities();
                notificationHelper.success(window.resource.texts.success, "Город успешно удален"); //TODO
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function() {
        getCities();
    };

    return {
        init: init,
        cities: cities,
        cityName: cityName,

        addCity: addCity,
        deleteCity: deleteCity
    };
};