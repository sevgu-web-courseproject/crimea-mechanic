var poolVM = new function() {

    var applications = ko.observableArray([]);
    var filter = {
        currentPage: ko.observable(1),
        itemsPerPage: ko.observable(10),
        createdFrom: ko.observable(null),
        createdTo: ko.observable(null),
        mark: ko.observable(null)
    };
    var itemsCount = ko.observable(0);
    var marks = ko.observableArray([]);

    var newOffer = {
        applicationId: ko.observable(),
        price: ko.observable(),
        content: ko.observable()
    }

    var getApplications = function (callback) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetPoolUrl;
        var data = JSON.stringify({
            CurrentPage: filter.currentPage(),
            ItemsPerPage: filter.itemsPerPage(),
            MarkId: filter.mark(),
            CreatedFrom: filter.createdFrom(),
            CreatedTo: filter.createdTo()
        });
        ajaxHelper.postJson(url, data)
            .then(function (data) {
                data.Items.forEach(function (item) {
                    item.Created = timeHelper.toLocalTime(item.Created);
                });
                itemsCount(data.ItemsCount);
                applications(data.Items);
                if (callback) {
                    callback();
                } else {
                    $(document).trigger("hideLoadingPanel");
                }
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
                $(document).trigger("hideLoadingPanel");
            });
    };

    var getMarks = function() {
        var url = window.resource.urls.webApiGetMarksFromPoolUrl;
        ajaxHelper.get(url)
            .then(function (data) {
                marks(data);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
                $(document).trigger("hideLoadingPanel");
            });
    };

    var pages = ko.pureComputed(function () {
        var pagesCount = itemsCount() % filter.itemsPerPage() !== 0
            ? (itemsCount()) / filter.itemsPerPage() + 1
            : (itemsCount()) / filter.itemsPerPage();
        if (pagesCount === 1) {
            return [];
        }
        var result;
        var prevPage = filter.currentPage() - 1;
        var nextPage = filter.currentPage() + 1;
        if (prevPage <= 0) {
            result = [];
            for (var i = 0; i < 3; i++) {
                var page = filter.currentPage() + i;
                if (page <= pagesCount && page > 0) {
                    result.push(filter.currentPage() + i);
                }
            }
            return result;
        }
        if (nextPage > pagesCount) {
            result = [];
            for (var i = 0; i < 3; i++) {
                var page = filter.currentPage() - i;
                if (page <= pagesCount && page > 0) {
                    result.unshift(filter.currentPage() - i);
                }
            }
            return result;
        }
        return [prevPage, filter.currentPage(), nextPage];
    });

    var changePage = function (page) {
        filter.currentPage(page);
        $(document).trigger("showLoadingPanel");
        getApplications();
    };

    var sendOffer = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiAddOfferUrl;
        var data = JSON.stringify({
            ApplicationId: newOffer.applicationId(),
            Price: newOffer.price(),
            Content: newOffer.content()
        });
        ajaxHelper.postJsonWithoutResult(url, data)
            .then(function () {
                $('#close-button').click();
                $(document).trigger("hideLoadingPanel");
                notificationHelper.success(window.resource.texts.success, window.resource.text.offerWasSended); 
                getApplications();
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
                $(document).trigger("hideLoadingPanel");
            });
    };

    var saveApplicationId = function(application) {
        newOffer.applicationId(application.Id);
    };

    var deleteOffer = function(application) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteOfferUrl.replace("offerId", application.OfferId);
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                $(document).trigger("hideLoadingPanel");
                notificationHelper.success(window.resource.texts.success, window.resource.texts.offerDeleted); 
                getApplications();
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
                $(document).trigger("hideLoadingPanel");
            });
    }

    var init = function () {
        getApplications(function() {
            getMarks();
        });

        filter.createdFrom.subscribe(function () {
            getApplications();
        });

        filter.createdTo.subscribe(function() {
            getApplications();
        });

        filter.mark.subscribe(function() {
            getApplications();
        });
    };

    return {
        init: init,
        applications: applications,
        currentPage: filter.currentPage,
        itemsPerPage: filter.itemsPerPage,
        createdFrom: filter.createdFrom,
        createdTo: filter.createdTo,
        mark: filter.mark,
        itemsCount: itemsCount,
        pages: pages,
        changePage: changePage,
        marks: marks,
        sendOffer: sendOffer,
        deleteOffer: deleteOffer,
        saveApplicationId: saveApplicationId,
        newOffer: newOffer
    };
};