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
                data.Items.forEach(function (item) {
                    item.photoUrl = getPhotoUrl(item.Id, item.LogoPhotoId);
                });
                bestServices(data.Items);
            }, function($xhr) {
                console.log($xhr);
            });
    };

    var getPhotoUrl = function (serviceId, fileId) {
        return window.resource.urls.webApiGetPhotoUrl
            .replace("serviceId", serviceId)
            .replace("fileId", fileId);
    };

    return {
        init: init,
        bestServices: bestServices
    };
};