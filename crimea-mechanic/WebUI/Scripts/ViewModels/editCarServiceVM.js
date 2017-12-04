var editCarServiceVM = new function() {

    var model = {
        Id: ko.observable(),
        Name: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carServiceNameIsEmpty }
        }),
        ManagerName: ko.observable().extend({
            required: { params: true, message: window.resource.errors.managerNameIsEmpty }
        }),
        Address: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carServiceAddressEmpty }
        }),
        Email: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carServiceEmailIsEmpty }
        }),
        Phones: ko.observableArray([]).extend({
            validation: {
                validator: function(val, someOtherVal) {
                    return val.length >= someOtherVal;
                },
                message: window.resource.errors.carServicePhonesIsEmpty,
                params: 1
            }
        }),
        Site: ko.observable(null),
        TimetableWorks: ko.observable(null),
        About: ko.observable(null),
        WorkTypes: ko.observableArray([]).extend({
            validation: {
                validator: function(val, someOtherVal) {
                    return val.length >= someOtherVal;
                },
                message: "Необходимо указать хотя бы один тип работы", //TODO
                params: 1
            }
        }),
        LogoId: ko.observable(),
        Photos: ko.observableArray([])
    };

    var workClasses = ko.observableArray([]);

    var checkWorkType = function(data, event) {
        if (!event.target.checked) {
            model.WorkTypes.remove(data.Id);
        } else {
            model.WorkTypes.push(data.Id);
        }
    };

    var checkWorkClass = function(data, event) {
        if (event.target.checked) {
            data.Types.forEach(function(item) {
                if (model.WorkTypes.indexOf(item.Id) < 0) {
                    model.WorkTypes.push(item.Id);
                }
            });
        } else {
            data.Types.forEach(function(item) {
                model.WorkTypes.remove(item.Id);
            });
        }
    };

    var phoneNumber = ko.observable();

    var addPhone = function() {
        if (phoneNumber()) {
            model.Phones.push(phoneNumber());
            phoneNumber(null);
        }
    };

    var removePhone = function(data) {
        model.Phones.remove(data);
    };

    var submit = function() {
        validationGroup = ko.validation.group([
            model.Name,
            model.ManagerName,
            model.CityId,
            model.Address,
            model.Email,
            model.Phones,
            model.WorkTypes
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }

        $(document).trigger("showLoadingPanel");
        var formData = new FormData();
        formData.append("Id", model.Id());
        formData.append("ManagerName", model.ManagerName());
        formData.append("Name", model.Name());
        formData.append("Address", model.Address());
        formData.append("Email", model.Email());
        formData.append("Site", model.Site());
        formData.append("TimetableWorks", model.TimetableWorks());
        formData.append("About", model.About());
        formData.append("Phones", ko.toJSON(model.Phones));
        formData.append("WorkTypes", ko.toJSON(model.WorkTypes));
        formData.append("Logo", $('#logo')[0].files[0]);
        var photos = $('#photos')[0].files;
        for (var i = 0; i < photos.length; i++) {
            formData.append("Photos", photos[i]);
        }

        ajaxHelper.postFormData(window.resource.urls.webApiEditCarServiceUrl, formData)
            .then(function() {
                localStorage.success = "Редактирование произведено успешно"; //TODO
                window.location.href = window.resource.urls.webUiMyProfileUrl;
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error("Error", text);
            });
    };

    var init = function() {
        $(document).trigger("showLoadingPanel");
        $('#phone-number').mask('+7(999) 999-99-99');

        var getWorkClasses = ajaxHelper.get(window.resource.urls.webApiGetWorkClassesUrl);
        var getCard = ajaxHelper.get(window.resource.urls.webApiGetCarServiceInfoForEditUrl);

        $(document).trigger("showLoadingPanel");
        Promise.all([getWorkClasses, getCard])
            .then(function(results) {
                workClasses(results[0]);
                ko.mapping.fromJS(results[1], {}, model);
                $(document).trigger("hideLoadingPanel");
            }, function(errors) {
                console.log(errors);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.errors.error, "Ошибка при получении данных");
            });
    };

    return {
        init: init,
        model: model,
        workClasses: workClasses,
        phoneNumber: phoneNumber,
        submit: submit,
        addPhone: addPhone,
        removePhone: removePhone,
        checkWorkType: checkWorkType,
        checkWorkClass: checkWorkClass
    };
};