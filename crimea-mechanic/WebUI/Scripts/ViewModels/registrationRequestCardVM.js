var registrationRequestCardVM = new function () {

    var model = {
        Id: ko.observable(),
        LogoPhotoId: ko.observable(null),
        Name: ko.observable(),
        CityName: ko.observable(),
        Address: ko.observable(),
        ManagerName: ko.observable(),
        Email: ko.observable(),
        Site: ko.observable(),
        Phones: ko.observableArray([]),
        TimetableWorks: ko.observable(),
        PhotosId: ko.observableArray([]),
        About: ko.observable(),
        WorkClasses: ko.observableArray([])
    };

    var reason = ko.observable().extend({
        required: { params: true, message: "Необходимо указать причину отказа" } //TODO
    });

    var approve = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiApproveCarServiceUrl.replace("carServiceId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.applicationAccepted;
                window.location.href = window.resource.urls.webUiRegistrationRequestsUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var reject = function () {
        var validationGroup = ko.validation.group([reason]);
        if (validationGroup().length !== 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            CarServiceId: model.Id(),
            Reason: reason()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiRejectCarServiceUrl, data)
            .then(function () {
                localStorage.success = window.resource.texts.applicationDeclined;
                window.location.href = window.resource.urls.webUiRegistrationRequestsUrl;
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function () {
        var url = window.resource.urls.webApiGetRegistrationCardUrl.replace("carServiceId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                localStorage.error = text;
                window.location.href = window.resource.urls.webUiRegistrationRequestsUrl;
            });
    };

    return {
        init: init,
        model: model,
        approve: approve,
        reject: reject,
        reason: reason
    };
};
