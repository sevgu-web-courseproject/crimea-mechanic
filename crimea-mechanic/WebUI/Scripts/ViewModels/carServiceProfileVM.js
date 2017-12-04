var carServiceProfileVM = new function() {

    var model = {
        Id: ko.observable(null),
        LogoPhotoId: ko.observable(null),
        AverageMark: ko.observable(),
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
        Reviews: ko.observableArray([]),
        ReviewId: ko.observable(),
        State: ko.observable(),
        WorkClasses: ko.observableArray([])
    };

    var init = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetCarServiceProfileUrl)
            .then(function(data) {
                ko.mapping.fromJS(data, {}, model);
                model.Reviews().forEach(function(review) {
                    review.Created(timeHelper.toLocalTime(review.Created()));
                });
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                localStorage.error = text;
            });
    };

    return {
        init: init,
        model: model
    };
};