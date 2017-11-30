var registrationRequestsVM = new function () {

    var requests = ko.observableArray([]);
    var currentPage = ko.observable(1);
    var itemsPerPage = ko.observable(10);
    var itemsCount = ko.observable(0);

    var pages = ko.pureComputed(function () {
        var pagesCount = itemsCount() % itemsPerPage() !== 0
            ? Math.round((itemsCount()) / itemsPerPage()) + 1
            : (itemsCount()) / itemsPerPage();
        if (pagesCount === 1) {
            return [];
        }
        var result;
        var prevPage = currentPage() - 1;
        var nextPage = currentPage() + 1;
        if (prevPage <= 0) {
            result = [];
            for (var i = 0; i < 3; i++) {
                var page = currentPage() + i;
                if (page <= pagesCount && page > 0) {
                    result.push(currentPage() + i);
                }
            }
            return result;
        }
        if (nextPage > pagesCount) {
            result = [];
            for (var i = 0; i < 3; i++) {
                var page = currentPage() - i;
                if (page <= pagesCount && page > 0) {
                    result.unshift(currentPage() - i);
                }
            }
            return result;
        }
        return [prevPage, currentPage(), nextPage];
    });

    var getRequests = function () {
        var url = window.resource.urls.webApiGetRegistrationRequestsUrl;
        var filter = {
            CurrentPage: currentPage(),
            ItemsPerPage: itemsPerPage()
        }
        ajaxHelper.postJson(url, JSON.stringify(filter))
            .then(function (data) {
                data.Items.forEach(function (item) {
                    item.Created = timeHelper.toLocalTime(item.Created);
                });
                requests(data.Items);
                itemsCount(data.ItemsCount);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error("Ошибка", text);
            });
    };

    var changePage = function(page) {
        currentPage(page);
        $(document).trigger("showLoadingPanel");
        getRequests();
    };

    var init = function () {
        getRequests();
    };

    return {
        init: init,
        requests: requests,
        currentPage: currentPage,
        itemsPerPage: itemsPerPage,
        itemsCount: itemsCount,
        pages: pages,
        changePage: changePage
    };
};