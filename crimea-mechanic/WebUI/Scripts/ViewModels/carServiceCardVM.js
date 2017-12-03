var carServiceCardVM = new function() {

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

    var availableMarks = [1, 2, 3, 4, 5];

    var changeCarServiceState = function (blocked) {
        $(document).trigger("showLoadingPanel");
        var url = blocked
            ? window.resource.urls.webApiBlockCarServiceUrl
            : window.resource.urls.WebApiUnBlockCarServiceUrl;
        url = url.replace("carServiceId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.stateOfCarServiceChanged;
                window.location.reload();
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var newReview = {
        mark: ko.observable().extend({
            required: {
                params: true, message: window.resource.errors.markRequired
            }
        }),
        review: ko.observable().extend({
            required: {
                params: true, message: window.resource.errors.reviewRequired
            }
        })
    };

    var addReview = function () {
        var validationGroup = ko.validation.group([
            newReview.mark,
            newReview.review
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            ServiceId: model.Id(),
            Mark: newReview.mark(),
            Review: newReview.review()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.WebApiAddReviewUrl, data)
            .then(function () {
                localStorage.success = window.resource.texts.reviewSuccessAdd;
                window.location.reload();
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteReview = function (reviewId) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.WebApiDeleteReviewUrl.replace("reviewId", reviewId);
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.reviewSuccessDelete;
                window.location.reload();
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function() {
        var url = window.resource.urls.webApiGetCarServiceCardUrl.replace("carServiceId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                model.Reviews().forEach(function (review) {
                    review.Created(timeHelper.toLocalTime(review.Created()));
                });
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    return {
        init: init,
        model: model,
        availableMarks: availableMarks,
        newReview: newReview,
        addReview: addReview,
        deleteReview: deleteReview,
        changeCarServiceState: changeCarServiceState
    };
};