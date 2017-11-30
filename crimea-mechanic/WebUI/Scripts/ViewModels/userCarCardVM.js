var userCarCardVM = new function() {

    var model = {
        Id: ko.observable(),
        Name: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать имя автомобиля' } //TODO перевести
        }),
        Mark: ko.observable(),
        Model: ko.observable(),
        Vin: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать vin-номер автомобиля' }, //TODO перевести
            minLength: { params: 17, message: 'Vin-номер должен содержать 17 символов' } //TODO перевести
        }),
        Year: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать год производства автомобиля' }, //TODO перевести
            max: { params: 2017, message: 'Недопустимое значение года' }, //TODO перевести
            min: { params: 1930, message: 'Недопустимое значение года' } //TODO перевести
        }),
        FuelType: ko.observable().extend({ 
            required: { params: true, message: 'Необходимо указать тип топлива автомобиля' } //TODO перевести
        }),
        FuelTypeDescription: ko.observable(),
        EngineCapacity: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать объем двигателя автомобиля' } //TODO перевести
        }),
        Applications: ko.observableArray([]),
        IsDeleted: ko.observable()
    };

    var fuelTypes = [
        { Id: 10, Name: "Бензин" }, //TODO перевести
        { Id: 20, Name: "Дизель" }, //TODO перевести
        { Id: 30, Name: "Другой" } //TODO перевести
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
                localStorage.success = "Машина успешно удалена"; //TODO перевести
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
                localStorage.success = "Машина успешно восстановлена"; //TODO перевести
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