var registrationRequestsVM = new function () {

    var requests = ko.observableArray([]);
    var currentPage = ko.observable(1);
    var itemsPerPage = ko.observable(10);
    var itemsCount = ko.observable(0);

    var init = function () {
        getRequests();
    };

    var getRequests = function () {
        var url = window.resource.urls.webApiGetRegistrationRequestsUrl;
        var filter = {
            CurrentPage: currentPage(),
            ItemsPerPage: itemsPerPage(),
        }
        ajaxHelper.postJson(url, JSON.stringify(filter))
            .then(function (data) {
                requests(data.Items);
                itemsCount(data.ItemsCount);
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    return {
        init: init,
        requests: requests,
        currentPage: currentPage,
        itemsPerPage: itemsPerPage,
        itemsCount: itemsCount
    };
};