var userApplicationCardVM = new function() {

    var model = {
        Id: ko.observable(),
        Car: {
            Id: ko.observable(),
            Mark: ko.observable(),
            Model: ko.observable(),
            Year: ko.observable(),
            EngineCapacity: ko.observable(),
            FuelTypeDescription: ko.observable()
        },
        Description: ko.observable(),
        Created: ko.observable(),
        IsDeleted: ko.observable(),
        ServiceName: ko.observable(),
        State: ko.observable(),
        StateDescription: ko.observable(),
        CityName: ko.observable(),
        CityId: ko.observable(),
        Offers: ko.observableArray([])
    };

    var cities = ko.observableArray([]);

    var editApplication = {
        cityId: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать город' } //TODO перевести
        }),
        description: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать описание работ' } //TODO перевести
        })
    }

    var getCities = function() {
        if (cities().length) {
            return;
        }
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetCitiesUrl)
            .then(function (data) {
                cities(data);
                editApplication.cityId(model.CityId());
                editApplication.description(model.Description());
                $(document).trigger("hideLoadingPanel");
            })
            .catch(function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var getCard = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetApplicationCardForUserUrl.replace("applicationId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                model.Created(timeHelper.toLocalTime(model.Created()));
                if (model.Offers()) {
                    model.Offers().forEach(function (item) {
                        item.Created(timeHelper.toLocalTime(item.Created()));
                    });
                }
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                localStorage.error = ajaxHelper.extractErrors($xhr);
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            });
    };

    var deleteApplication = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteApplicationUrl.replace("applicationId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function() {
                localStorage.success = "Заявка успешно была удалена"; //TODO перевести
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var acceptOffer = function(offer) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiAcceptOfferUrl.replace("offerId", offer.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                getCard();
                notificationHelper.success(window.resource.texts.success, "Предложение успешно принято"); //TODO перевести
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var rejectApplication = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiRejectApplicationUrl.replace("applicationId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = "Выполнение заявки отменено"; //TODO перевести
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var sendEditApplication = function () {
        var validationGroup = ko.validation.group([
            editApplication.cityId,
            editApplication.description
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            ApplicationId: model.Id(),
            CityId: editApplication.cityId(),
            Description: editApplication.description()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiEditApplicationUrl, data)
            .then(function() {
                $('#close-button').click();
                getCard();
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function() {
        getCard();
    };
    
    return {
        init: init,
        model: model,
        deleteApplication: deleteApplication,
        acceptOffer: acceptOffer,
        rejectApplication: rejectApplication,
        editApplication: editApplication,
        getCities: getCities,
        cities: cities,
        sendEditApplication: sendEditApplication
    };
};