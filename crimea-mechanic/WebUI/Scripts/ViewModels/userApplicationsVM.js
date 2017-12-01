var userApplicationsVM = new function() {

    var applications = ko.observableArray([]);
    var itemsCount = ko.observable(0);

    var filter = {
        itemsPerPage: ko.observable(10),
        currentPage: ko.observable(1),
        state: ko.observable(null),
        createdFrom: ko.observable(null),
        createdTo: ko.observable(null),
        carId: ko.observable(null)
    };

    var cities = ko.observableArray([]);
    var cars = ko.observableArray([]);

    var newApplication = {
        carId: ko.observable().extend({
            required: { params: true, message: window.resource.errors.specifyCar } 
        }),
        cityId: ko.observable().extend({
            required: { params: true, message: window.resource.errors.specifyCity } 
        }),
        description: ko.observable().extend({
            required: { params: true, message: window.resource.errors.specifyWorksDescription } 
        })
    }

    var applicationStates = [
        { Id: 5, Name: window.resource.texts.deleted },  
        { Id: 10, Name: window.resource.texts.inSearch }, 
        { Id: 15, Name: window.resource.texts.onWork }, 
        { Id: 20, Name: window.resource.texts.declined }, 
        { Id: 25, Name: window.resource.texts.completed } 
    ];

    var getApplications = function(callback) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetUserApplicationsUrl;
        var data = JSON.stringify({
            CurrentPage: filter.currentPage(),
            ItemsPerPage: filter.itemsPerPage(),
            State: filter.state(),
            CreatedFrom: filter.createdFrom(),
            CreatedTo: filter.createdTo(),
            CarId: filter.carId()
        });
        ajaxHelper.postJson(url, data)
            .then(function(data) {
                data.Items.forEach(function(item) {
                    item.Created = timeHelper.toLocalTime(item.Created);
                });
                itemsCount(data.ItemsCount);
                applications(data.Items);
                if (callback) {
                    callback();
                }
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
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
        getApplications();
    };

    var saveFilter = function (application) {
        localStorage.userApplicationsFilter = JSON.stringify({
            currentPage: filter.currentPage(),
            itemsPerPage: filter.itemsPerPage(),
            state: filter.state(),
            createdFrom: filter.createdFrom(),
            createdTo: filter.createdTo(),
            carId: filter.carId()
        });
        window.location.href = window.resource.urls.webUiApplicationCard + '/' + application.Id;
    };

    var getActiveCars = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetActiveCarsUrl)
            .then(function(data) {
                cars(data);
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var createNewApplication = function () {
        if (cities().length) {
            return;
        }
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetCitiesUrl)
            .then(function(data) {
                cities(data);
                $(document).trigger("hideLoadingPanel");
            })
            .catch(function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var clearForm = function() {
        newApplication.carId(null);
        newApplication.cityId(null);
        newApplication.description(null);
    };

    var sendNewApplication = function () {
        var validationGroup = ko.validation.group([
            newApplication.carId,
            newApplication.cityId,
            newApplication.description
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            CarId: newApplication.carId(),
            CityId: newApplication.cityId(),
            Description: newApplication.description()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiCreateApplicationUrl, data)
            .then(function() {
                $('#close-button').click();
                clearForm();
                validationGroup.showAllMessages(false);
                getApplications();
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var clearFilter = function() {
        filter.currentPage(1);
        filter.itemsPerPage(10);
        filter.createdFrom(null);
        filter.createdTo(null);
        filter.state(null);
        filter.carId(null);
        getApplications();
    };

    var find = function () {
        filter.currentPage(1);
        getApplications();
    };

    var init = function () {
        if (localStorage.userApplicationsFilter) {
            ko.mapping.fromJSON(localStorage.userApplicationsFilter, {}, filter);
            localStorage.removeItem("userApplicationsFilter");
        }
        getApplications(function() {
            getActiveCars();
        });
    };

    return {
        init: init,
        applications: applications,
        currentPage: filter.currentPage,
        itemsPerPage: filter.itemsPerPage,
        itemsCount: itemsCount,
        state: filter.state,
        createdFrom: filter.createdFrom,
        createdTo: filter.createdTo,
        carId: filter.carId,
        pages: pages,
        changePage: changePage,
        saveFilter: saveFilter,
        applicationStates: applicationStates,
        cars: cars,
        cities: cities,
        createNewApplication: createNewApplication,
        newApplication: newApplication,
        sendNewApplication: sendNewApplication,
        clearFilter: clearFilter,
        find: find
    };
};