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
        ServiceId: ko.observable(),
        ServiceName: ko.observable(),
        State: ko.observable(),
        StateDescription: ko.observable(),
        CityName: ko.observable(),
        CityId: ko.observable(),
        Offers: ko.observableArray([]),
        WorkClassDescription: ko.observable(),
        WorkTypeDescription: ko.observable()
    };

    var editApplication = {
        description: ko.observable().extend({
            required: { params: true, message: window.resource.errors.specifyWorksDescription },
            notEqual: { params: model.Description, message: "Дополнительная информацию совпадает с текущей"} //TODO
        })
    }

    var getCard = function () {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetApplicationCardForUserUrl.replace("applicationId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                model.Created(timeHelper.toLocalTime(model.Created()));
                editApplication.description(model.Description());
                var validationGroup = ko.validation.group([
                    editApplication.description
                ]);
                validationGroup.showAllMessages(false);
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
                localStorage.success = window.resource.texts.applicationSuccessfullyDeleted; 
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
                notificationHelper.success(window.resource.texts.success, window.resource.texts.offerAccepted); 
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
                localStorage.success = window.resource.texts.executionCancelled; 
                window.location.href = window.resource.urls.webUiApplicationsUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var sendEditApplication = function () {
        var validationGroup = ko.validation.group([
            editApplication.description
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            ApplicationId: model.Id(),
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
        sendEditApplication: sendEditApplication
    };
};