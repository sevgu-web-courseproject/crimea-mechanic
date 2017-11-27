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
        IsApproveButtonVisible: ko.observable(true),
        IsRejectButtonVisible: ko.observable(true)
    };

    var approve = function () {
        $(document).trigger("showLoadingPanel");
        model.IsApproveButtonVisible(false);
        model.IsRejectButtonVisible(false);
        var url = window.resource.urls.webApiApproveCarServiceUrl.replace("carServiceId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = "Заявка принята";
                window.location.href = window.resource.urls.webUiRegistrationRequestsUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    var reject = function () {
        $(document).trigger("showLoadingPanel");
        model.IsApproveButtonVisible(false);
        model.IsRejectButtonVisible(false);
        var url = window.resource.urls.webApiRejectCarServiceUrl.replace("carServiceId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = "Заявка отклонена";
                window.location.href = window.resource.urls.webUiRegistrationRequestsUrl;
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
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
        reject: reject
    };
};
