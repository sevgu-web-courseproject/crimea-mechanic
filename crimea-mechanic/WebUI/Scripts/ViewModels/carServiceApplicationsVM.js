var carServiceApplicationsVM = new function() {

    var applications = ko.observableArray([]);
    var filter = {
        currentPage: ko.observable(1),
        itemsPerPage: ko.observable(10),
        createdFrom: ko.observable(null),
        createdTo: ko.observable(null),
        state: ko.observable(null)
    };

    var itemsCount = ko.observable();

    var applicationStates = [
        { Id: 15, Name: window.resource.texts.onWork }, 
        { Id: 20, Name: window.resource.texts.declined }, 
        { Id: 25, Name: window.resource.texts.completed }
    ];

    var getApplications = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetInfosForServiceUrl;
        var data = JSON.stringify({
            CurrentPage: filter.currentPage(),
            ItemsPerPage: filter.itemsPerPage(),
            State: filter.state(),
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
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
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
        var data = JSON.stringify({
            currentPage: filter.currentPage(),
            itemsPerPage: filter.itemsPerPage(),
            createdFrom: filter.createdFrom(),
            createdTo: filter.createdTo(),
            state: filter.state()
        });
        localStorage.carServiceApplications = data;
        window.location.href = window.resource.urls.webUiApplicationCard + '/' + application.Id;
    };

    var init = function () {
        if (localStorage.carServiceApplications) {
            ko.mapping.fromJSON(localStorage.carServiceApplications, {}, filter);
            localStorage.removeItem("carServiceApplications");
        }

        getApplications();

        filter.createdFrom.subscribe(function() {
            filter.currentPage(1);
            getApplications();
        });

        filter.createdTo.subscribe(function() {
            filter.currentPage(1);
            getApplications();
        });

        filter.state.subscribe(function() {
            filter.currentPage(1);
            getApplications();
        });
    };
    
    return {
        init: init,
        applications: applications,
        itemsCount: itemsCount,
        applicationStates: applicationStates,

        currentPage: filter.currentPage,
        itemsPerPage: filter.itemsPerPage,
        createdFrom: filter.createdFrom,
        createdTo: filter.createdTo,
        state: filter.state,

        pages: pages,
        changePage: changePage,
        saveFilter: saveFilter,
    };
};