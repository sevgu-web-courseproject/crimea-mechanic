var carServiceCardVM = new function() {

    var model = {
        Id: ko.observable(null),
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
        Reviews: ko.observableArray([])
    };

    var init = function() {
        var url = window.resource.urls.webApiGetCarServiceCardUrl.replace("carServiceId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    var getPhotoUrl = window.resource.urls.webApiGetPhotoUrl;

    return {
        init: init,
        model: model,
        getPhotoUrl: getPhotoUrl
    };
};