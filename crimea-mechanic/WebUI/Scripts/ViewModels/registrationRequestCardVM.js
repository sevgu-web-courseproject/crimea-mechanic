var registrationRequestCardVM = new function () {

    var model = {
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
        var url = window.resource.urls.webApiApproveCarServiceUrl.replace("carServiceId", model.Id());
        ajaxHelper.get(url)
            .then(function () {
                model.IsApproveButtonVisible(false);
                model.IsRejectButtonVisible(false);
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    var reject = function () {
        var url = window.resource.urls.webApiRejectCarServiceUrl.replace("carServiceId", model.Id());
        ajaxHelper.get(url)
            .then(function () {
                model.IsApproveButtonVisible(false);
                model.IsRejectButtonVisible(false);
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    var init = function () {
        debugger;
        var url = window.resource.urls.webApiGetRegistrationRequestCardUrl.replace("carServiceId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    return {
        init: init,
        model: model,
        approve: approve,
        reject: reject
    };
};
