var garagePageVM = new function () {

    var cars = ko.observableArray([]);
    var currentPage = ko.observable(1);
    var itemsPerPage = ko.observable(10);
    var itemsCount = ko.observable(0);

    var marks = ko.observableArray([]);
    var mark = ko.observable();
    var models = ko.observableArray([]);

    var fuelTypes = [
        { Id: 10, Name: "Бензин" },
        { Id: 20, Name: "Дизель" },
        { Id: 30, Name: "Другой"}
    ];

    var newCar = {
        Name: ko.observable(),
        ModelId: ko.observable(),
        Vin: ko.observable(),
        Year: ko.observable(),
        FuelType: ko.observable(),
        EngineCapacity: ko.observable()
    };

    var getCars = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetUserCars;
        var filter = {
            CurrentPage: currentPage(),
            ItemsPerPage: itemsPerPage()
        }
        ajaxHelper.postJson(url, JSON.stringify(filter))
            .then(function (data) {
                cars(data.Items);
                itemsCount(data.ItemsCount);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error(window.resource.texts.error, text);
            });
    };

    var getMarks = function () {
        $('#car-year').mask("9999");
        $('#car-engine-capacity').mask("9.9");
        if (marks().length > 0) {
            return;
        }
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetMarksUrl;
        ajaxHelper.get(url)
            .then(function (data) {
                marks(data);
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error(window.resource.texts.error, text);
            });
    };

    var getModels = function(markId) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetModelsUrl.replace("markId", markId);
        ajaxHelper.get(url)
            .then(function (data) {
                models(data);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error(window.resource.texts.error, text);
            });
    }

    mark.subscribe(function (newValue) {
        if (newValue) {
            var match = ko.utils.arrayFirst(marks(), function (item) {
                return item.Name === newValue;
            });
            getModels(match.Id);
        }
    });

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

    var changePage = function (page) {
        currentPage(page);
        $(document).trigger("showLoadingPanel");
        getCars();
    };

    var init = function () {
        getCars();
    };

    return {
        init: init,
        cars: cars,
        itemsPerPage: itemsPerPage,
        itemsCount: itemsCount,
        currentPage: currentPage,
        pages: pages,
        changePage: changePage,
        getMarks: getMarks,
        marks: marks,
        mark: mark,
        models: models,
        newCar: newCar,
        fuelTypes: fuelTypes
    };
};