var userApplicationsVM = new function() {

    var applications = ko.observableArray([]);
    var itemsCount = ko.observable(0);

    var filter = {
        itemsPerPage: ko.observable(10),
        currentPage: ko.observable(1),
        state: ko.observable(null),
        createdFrom: ko.observable(null),
        createdTo: ko.observable(null)
    };

    var applicationStates = [
        {Id: 5, Name: "Удалена"},
        {Id: 10, Name: "В поиске автосервиса"},
        {Id: 15, Name: "На исполнении"},
        {Id: 20, Name: "Отклонена"},
        {Id: 25, Name: "Завершена"}
    ];

    var getApplications = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetUserApplicationsUrl;
        var data = JSON.stringify({
            CurrentPage: filter.currentPage(),
            ItemsPerPage: filter.itemsPerPage(),
            State: filter.state(),
            CreatedFrom: filter.createdFrom(),
            CreatedTo: filter.createdTo()
        });
        ajaxHelper.postJson(url, data)
            .then(function(data) {
                data.Items.forEach(function(item) {
                    item.Created = timeHelper.toLocalTime(item.Created);
                });
                itemsCount(data.ItemsCount);
                applications(data.Items);
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
            createdTo: filter.createdTo()
        });
        window.location.href = window.resource.urls.webUiApplicationCard + '/' + application.Id;
    };

    var init = function () {
        if (localStorage.userApplicationsFilter) {
            ko.mapping.fromJSON(localStorage.userApplicationsFilter, {}, filter);
            localStorage.removeItem("userApplicationsFilter");
        }
        getApplications();

        filter.state.subscribe(function() {
            getApplications();
        });
        filter.createdFrom.subscribe(function() {
            getApplications();
        });
        filter.createdTo.subscribe(function() {
            getApplications();
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
        pages: pages,
        changePage: changePage,
        saveFilter: saveFilter,
        applicationStates: applicationStates
    };
};