var carServicesVM = new function() {

    var carServices = ko.observableArray([]);
    var filter = {
        currentPage: ko.observable(1),
        itemsPerPage: ko.observable(10),
        name: ko.observable(null),
        cityId: ko.observable(null),
        stars: ko.observable(null),
        showBlocked: ko.observable(null)
    };

    var itemsCount = ko.observable(0);
    var cities = ko.observableArray([]);
    var starsCount = [
        1, 2, 3 ,4 ,5
    ];

    var getCarServices = function() {
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            CurrentPage: filter.currentPage(),
            ItemsPerPage: filter.itemsPerPage(),
            Name: filter.name(),
            CityId: filter.cityId(),
            Stars: filter.stars(),
            ShowBlocked: filter.showBlocked()
        });
        ajaxHelper.postJson(window.resource.urls.webApiGetCarServicesUrl, data)
            .then(function (data) {
                data.Items.forEach(function (item) {
                    item.Created = timeHelper.toLocalTime(item.Created);
                });
                itemsCount(data.ItemsCount);
                carServices(data.Items);
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
                $(document).trigger("hideLoadingPanel");
            });
    };

    var getCities = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetCitiesUrl)
            .then(function (data) {
                cities(data);
                $(document).trigger("hideLoadingPanel");
            })
            .catch(function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
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
        getCarServices();
    };

    var saveFilter = function(carService) {
        localStorage.carServiceFilter = JSON.stringify({
            currentPage: filter.currentPage(),
            itemsPerPage: filter.itemsPerPage(),
            name: filter.name(),
            cityId: filter.cityId(),
            stars: filter.stars(),
            showBlocked: filter.showBlocked()
        });
        window.location.href = window.resource.urls.webUiCarServiceCardUrl + '/' + carService.Id;
    };

    var init = function () {
        if (localStorage.carServiceFilter) {
            ko.mapping.fromJSON(localStorage.carServiceFilter, {}, filter);
            localStorage.removeItem("carServiceFilter");
        }

        getCarServices();
        getCities();

        filter.name.subscribe(function() {
            filter.currentPage(1);
            getCarServices();
        });

        filter.cityId.subscribe(function () {
            filter.currentPage(1);
            getCarServices();
        });

        filter.stars.subscribe(function () {
            filter.currentPage(1);
            getCarServices();
        });

        filter.showBlocked.subscribe(function () {
            filter.currentPage(1);
            getCarServices();
        });
    };

    return {
        init: init,
        carServices: carServices,
        itemsCount: itemsCount,
        pages: pages,
        changePage: changePage,
        cities: cities,
        starsCount: starsCount,

        currentPage: filter.currentPage,
        itemsPerPage: filter.itemsPerPage,
        name: filter.name,
        cityId: filter.cityId,
        stars: filter.stars,
        showBlocked: filter.showBlocked,
        saveFilter: saveFilter
    };
};