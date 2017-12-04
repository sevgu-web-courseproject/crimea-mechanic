var carServiceApplicationCardVM = new function() {
    var model = {
        Id: ko.observable(),
        ContactName: ko.observable(),
        ContactPhone: ko.observable(),
        Car: {
            Mark: ko.observable(),
            Model: ko.observable(),
            Year: ko.observable(),
            EngineCapacity: ko.observable(),
            FuelTypeDescription: ko.observable()
        },
        Description: ko.observable(),
        StateDescription: ko.observable(),
        CityName: ko.observable(),
        Created: ko.observable(),
        State: ko.observable(),
        WorkClassDescription: ko.observable(),
        WorkTypeDescription: ko.observable()
    };

    var rejectApplication = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiRejectApplicationUrl.replace("applicationId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.applicationExecutionCancelled; 
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var completeApplication = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiCompleteApplicationUrl.replace("applicationId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.applicationExecutionCompleted;
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var getCard = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetInfoForCarServiceUrl.replace("applicationId", model.Id());
        ajaxHelper.get(url)
            .then(function(data) {
                ko.mapping.fromJS(data, {}, model);
                model.Created(timeHelper.toLocalTime(model.Created()));
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                localStorage.error = ajaxHelper.extractErrors($xhr);
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            });
    }

    var init = function() {
        getCard();
    };

    return {
        init: init,
        model: model,

        rejectApplication: rejectApplication,
        completeApplication: completeApplication
    };
};