var userCarCardVM = new function() {

    var model = {
        Id: ko.observable(),
        Name: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать имя автомобиля' }
        }),
        Mark: ko.observable(),
        Model: ko.observable(),
        Vin: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать vin-номер автомобиля' },
            minLength: { params: 17, message: 'Vin-номер должен содержать 17 символов' }
        }),
        Year: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать год производства автомобиля' },
            max: { params: 2017, message: 'Недопустимое значение года' },
            min: { params: 1930, message: 'Недопустимое значение года' }
        }),
        FuelType: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать тип топлива автомобиля' }
        }),
        FuelTypeDescription: ko.observable(),
        EngineCapacity: ko.observable().extend({
            required: { params: true, message: 'Необходимо указать объем двигателя автомобиля' }
        }),
        Applications: ko.observableArray([])
    };

    var fuelTypes = [
        //TODO Перевести
        { Id: 10, Name: "Бензин" },
        { Id: 20, Name: "Дизель" },
        { Id: 30, Name: "Другой" }
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
        deleteCar: deleteCar
    };
};