var homePageVM = new function() {

    var bestServices = ko.observableArray([]);

    var init = function () {
        var url = window.resource.urls.webApiGetCarServicesUrl;
        var objToSend = JSON.stringify({
            CurrentPage: 1,
            ItemsPerPage: 3
        });
        ajaxHelper.postJson(url, objToSend)
            .then(function (data) {
                bestServices(data.Items);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.errors.error, text);
            });
    };

    return {
        init: init,
        bestServices: bestServices
    };
};