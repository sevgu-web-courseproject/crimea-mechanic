var userCarCardVM = new function() {

    var model = {
        Id: ko.observable(),
        Name: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carNameIsRequired } 
        }),
        Mark: ko.observable(),
        Model: ko.observable(),
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
        FuelTypeDescription: ko.observable(),
        EngineCapacity: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carEngineTypeIsRequired } 
        }),
        Applications: ko.observableArray([]),
        IsDeleted: ko.observable()
    };

    var fuelTypes = [
        { Id: 10, Name: window.resource.texts.gasoline }, 
        { Id: 20, Name: window.resource.texts.diesel }, 
        { Id: 30, Name: window.resource.texts.other } 
    ];

    var getCard = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetCarUrl.replace("carId", model.Id());
        ajaxHelper.get(url)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                if (model.Applications() && model.Applications().length) {
                    model.Applications().forEach(function (item) {
                        item.Created(timeHelper.toLocalTime(item.Created()));
                    });
                }
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var editCar = function () {
        var validationGroup = ko.validation.group([
            model.Name,
            model.Vin,
            model.Year,
            model.FuelType,
            model.EngineCapacity
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }

        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiEditCarUrl;
        var data = JSON.stringify({
            Id: model.Id(),
            Name: model.Name(),
            Year: model.Year(),
            Vin: model.Vin(),
            FuelType: model.FuelType(),
            EngineCapacity: model.EngineCapacity()
        });

        ajaxHelper.postJsonWithoutResult(url, data)
            .then(function () {
                $('#close-button').click();
                getCard();
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteCar = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteCarUrl.replace("carId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.carSuccessfullyDeleted; 
                window.location.href = window.resource.urls.webUiGarageUrl;
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var restoreCar = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiRestoreCarUrl.replace("carId", model.Id());
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                localStorage.success = window.resource.texts.carSucessfullyRestored; 
                window.location.href = window.resource.urls.webUiGarageUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function () {
        $('#car-year').mask("9999");
        $('#car-engine-capacity').mask("9.9");
        getCard();
    };

    return {
        init: init,
        model: model,
        fuelTypes: fuelTypes,
        editCar: editCar,
        deleteCar: deleteCar,
        restoreCar: restoreCar
    };
};