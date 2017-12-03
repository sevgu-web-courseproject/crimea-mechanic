var registrationCarServiceVM = new function() {
    var model = {
        Login: ko.observable().extend({
            required: { params: true, message: window.resource.errors.loginIsEmpty },
            email: { params: true, message: window.resource.errors.loginIsNotEmail }
        }),
        Password: ko.observable().extend({
            required: { params: true, message: window.resource.errors.passwordIsEmpty }
        }),
        PasswordСonfirmation: ko.observable().extend({
            required: { params: true, message: window.resource.errors.passwordIsEmpty }
        }),
        Name: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carServiceNameIsEmpty }
        }),
        ManagerName: ko.observable().extend({
            required: { params: true, message: window.resource.errors.managerNameIsEmpty }
        }),
        CityId: ko.observable().extend({
            required: { params: true, message: window.resource.errors.cityIsNotSelected }
        }),
        Cities: ko.observableArray([]),
        Address: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carServiceAddressEmpty }
        }),
        Email: ko.observable().extend({
            required: { params: true, message: window.resource.errors.carServiceEmailIsEmpty }
        }),
        PhoneNumber: ko.observable(),
        Phones: ko.observableArray([]).extend({
            validation: {
                validator: function (val, someOtherVal) {
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
                validator: function (val, someOtherVal) {
                    return val.length >= someOtherVal;
                },
                message: "Необходимо указать хотя бы один тип работы", //TODO
                params: 1
            }
        })
    };

    var workClasses = ko.observableArray([]);

    var checkWorkType = function (data, event) {
        if (!event.target.checked) {
            model.WorkTypes.remove(data.Id);
        } else {
            model.WorkTypes.push(data.Id);
        }
    };

    var checkWorkClass = function (data, event) {
        if (event.target.checked) {
            data.Types.forEach(function(item) {
                model.WorkTypes.push(item.Id);
            });
        } else {
            data.Types.forEach(function (item) {
                model.WorkTypes.remove(item.Id);
            });
        }
    };

    var validationGroup = null;

    var submit = function () {

        validationGroup = ko.validation.group([
            model.Login,
            model.Password,
            model.PasswordСonfirmation,
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
        formData.append("Login", model.Login());
        formData.append("Password", model.Password());
        formData.append("PasswordСonfirmation", model.PasswordСonfirmation());
        formData.append("ManagerName", model.ManagerName());
        formData.append("Name", model.Name());
        formData.append("CityId", model.CityId());
        formData.append("Address", model.Address());
        formData.append("Email", model.Email());
        formData.append("Site", model.Site());
        formData.append("TimetableWorks", model.TimetableWorks());
        formData.append("About", model.About());
        formData.append("Phones", ko.toJSON(model.Phones));
        formData.append("WorkTypes", ko.toJSON(model.WorkTypes));
        formData.append("CarTags", "[]");
        formData.append("Logo", $('#logo')[0].files[0]);
        var photos = $('#photos')[0].files;
        for (var i = 0; i < photos.length; i++) {
            formData.append("Photos", photos[i]);
        }

        ajaxHelper.postFormData(window.resource.urls.webApiRegistrationCarServiceUrl, formData)
            .then(function() {
                localStorage.success = window.resource.texts.successCarServiceRegistration;
                window.location.href = window.resource.urls.webUiSignInPageUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error("Error", text);
            });
    };

    var addPhone = function () {
        if (model.PhoneNumber()) {
            model.Phones.push(model.PhoneNumber());
            model.PhoneNumber(null);
        }
    };

    var removePhone = function (data) {
        model.Phones.remove(data);
    };

    var clearForm = function () {
        for (var key in self.model) {
            self.model[key](null);
        }
        if (validationGroup) {
            validationGroup.showAllMessages(false);
        }
    };

    var init = function () {
        $('#phone-number').mask('+7(999) 999-99-99');
        var getCities = ajaxHelper.get(window.resource.urls.webApiGetCitiesUrl);
        var getWorkClasses = ajaxHelper.get(window.resource.urls.webApiGetWorkClassesUrl);

        $(document).trigger("showLoadingPanel");
        Promise.all([getCities, getWorkClasses])
            .then(function(results) {
                model.Cities(results[0]);
                workClasses(results[1]);
                $(document).trigger("hideLoadingPanel");
            }, function (errors) {
                console.log(errors);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.errors.error, "Ошибка при получении данных");
            });
    };

    return {
        model: model,
        submit: submit,
        init: init,
        addPhone: addPhone,
        removePhone: removePhone,
        clearForm: clearForm,
        workClasses: workClasses,
        checkWorkType: checkWorkType,
        checkWorkClass: checkWorkClass
    };
};