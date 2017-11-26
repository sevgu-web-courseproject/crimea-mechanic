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
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error("Ошибка", text);
            });
    };

    return {
        init: init,
        bestServices: bestServices
    };
};