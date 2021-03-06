﻿var garagePageVM = new function () {

    var cars = ko.observableArray([]);
    var currentPage = ko.observable(1);

    var itemsPerPage = ko.observable(10);
    var itemsCount = ko.observable(0);
    var showDeleted = ko.observable(false);

    var marks = ko.observableArray([]);
    var mark = ko.observable();
    var models = ko.observableArray([]);

    var fuelTypes = [
        { Id: 10, Name: window.resource.texts.gasoline }, 
        { Id: 20, Name: window.resource.texts.diesel }, 
        { Id: 30, Name: window.resource.texts.other }
    ];

    var newCar = {
        Name: ko.observable().extend({
            required: {
                params: true, message: window.resource.errors.carNameIsRequired } 
        }),
        ModelId: ko.observable().extend({
            required: { params: true, message: window.resource.errors.modelSpecify}
        }),
        Vin: ko.observable().extend({
            required: { params: true, message: window.resource.errors.vinSpecify }, 
            minLength: { params: 17, message: window.resource.errors.vinCharacters } 
        }),
        Year: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carYearRequired },
            max: { params: 2017, message: window.resource.errors.yearError }, 
            min: { params: 1930, message: window.resource.errors.yearError } 
        }),
        FuelType: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carFuelTypeIsRequired } 
        }),
        EngineCapacity: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carEngineTypeIsRequired } 
        })
    };

    var getCars = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetUserCarsUrl;
        var filter = {
            CurrentPage: currentPage(),
            ItemsPerPage: itemsPerPage(),
            IsDeleted: showDeleted()
        }
        ajaxHelper.postJson(url, JSON.stringify(filter))
            .then(function (data) {
                cars(data.Items);
                itemsCount(data.ItemsCount);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
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
                notificationHelper.error(window.resource.texts.error, text);
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
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    mark.subscribe(function (newValue) {
        if (newValue) {
            var match = ko.utils.arrayFirst(marks(),
                function(item) {
                    return item.Name === newValue;
                });
            getModels(match.Id);
        } else {
            models([]);
        }
    });

    var pages = ko.pureComputed(function () {
        var pagesCount = itemsCount() % itemsPerPage() !== 0
            ? (itemsCount()) / itemsPerPage() + 1
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

    var clearNewCarFiels = function () {
        mark(null);
        newCar.Name(null);
        newCar.ModelId(null);
        newCar.Vin(null);
        newCar.Year(null);
        newCar.FuelType(null);
        newCar.EngineCapacity(null);
    };

    var addCar = function() {
        var validationGroup = ko.validation.group([
            newCar.Name,
            newCar.ModelId,
            newCar.Vin,
            newCar.Year,
            newCar.FuelType,
            newCar.EngineCapacity
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }

        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiAddCarUrl;
        var objToSend = ko.mapping.toJSON(newCar);
        ajaxHelper.postJsonWithoutResult(url, objToSend)
            .then(function () {
                $('#close-button').click();
                clearNewCarFiels();
                validationGroup.showAllMessages(false);
                getCars();
                notificationHelper.success(window.resource.texts.success, window.resource.texts.carAddToGarageSuccess); 
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var saveFilter = function (car) {
        var filter = {
            itemsPerPage: itemsPerPage(),
            currentPage: currentPage(),
            showDeleted: showDeleted()
        };
        localStorage.garageFilter = JSON.stringify(filter);
        window.location.href = window.resource.urls.webUiUserCarCardUrl + '/' + car.Id;
    };

    var init = function () {
        if (localStorage.garageFilter) {
            var filter = JSON.parse(localStorage.garageFilter);
            itemsPerPage(filter.itemsPerPage);
            currentPage(filter.currentPage);
            showDeleted(filter.showDeleted);
            localStorage.removeItem("garageFilter");
        }

        getCars();

        showDeleted.subscribe(function () {
            getCars();
        });
    };

    return {
        init: init,
        cars: cars,
        itemsPerPage: itemsPerPage,
        itemsCount: itemsCount,
        currentPage: currentPage,
        pages: pages,
        showDeleted: showDeleted,
        changePage: changePage,
        getMarks: getMarks,
        marks: marks,
        mark: mark,
        models: models,
        newCar: newCar,
        fuelTypes: fuelTypes,
        addCar: addCar,
        saveFilter: saveFilter
    };
};