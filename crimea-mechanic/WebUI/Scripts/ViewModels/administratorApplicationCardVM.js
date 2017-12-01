var administratorApplicationCardVM = new function() {

    var model = {
        Id: ko.observable(),
        UserContactName: ko.observable(),
        CarServiceId: ko.observable(),
        CarServiceName: ko.observable(),
        Car: {
            Mark: ko.observable(),
            Model: ko.observable(),
            Year: ko.observable(),
            EngineCapacity: ko.observable(),
            FuelTypeDescription: ko.observable()
        },
        Description: ko.observable(),
        State: ko.observable(),
        StateDescription: ko.observable(),
        CityName: ko.observable(),
        Created: ko.observable(),
        Offers: ko.observableArray([])
    };

    var getCard = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetInfoForAdminUrl.replace("applicationId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                model.Created(timeHelper.toLocalTime(model.Created()));
                if (model.Offers()) {
                    model.Offers().forEach(function (item) {
                        item.Created(timeHelper.toLocalTime(item.Created()));
                    });
                }
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                localStorage.error = ajaxHelper.extractErrors($xhr);
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            });
    };

    var init = function() {
        getCard();
    };

    return {
        init: init,
        model: model
    };
};