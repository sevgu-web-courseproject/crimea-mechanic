var registrationCarServiceVM = new function() {
    var model = {
        Login: ko.observable(),
        Password: ko.observable(),
        PasswordСonfirmation: ko.observable(),
        Name: ko.observable(),
        ManagerName: ko.observable(),
        CityId: ko.observable(),
        Cities: ko.observableArray([]),
        Address: ko.observable(),
        Email: ko.observable(),
        PhoneNumber: ko.observable(),
        Phones: ko.observableArray([]),
        Site: ko.observable(),
        TimetableWorks: ko.observable(),
        About: ko.observable()
    };

    var submit = function () {
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
        formData.append("WorkTags", "[]");
        formData.append("CarTags", "[]");
        formData.append("Logo", $('#logo')[0].files[0]);
        var photos = $('#photos')[0].files;
        for (var i = 0; i < photos.length; i++) {
            formData.append("Photos", photos[i]);
        }

        ajaxHelper.postFormData(window.resource.urls.webApiRegistrationCarServiceUrl, formData)
            .then(function() {
                window.location.href = window.resource.urls.webUiSignInPageUrl;
            }, function($xhr) {
                console.log($xhr);
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

    var init = function () {
        var getCities = ajaxHelper.get(window.resource.urls.webApiGetCitiesUrl);

        Promise.all([getCities])
            .then(function(results) {
                model.Cities(results[0]);
            }, function (errors) {
                console.log(errors);
            });
    };

    return {
        model: model,
        submit: submit,
        init: init,
        addPhone: addPhone,
        removePhone: removePhone
    };
};