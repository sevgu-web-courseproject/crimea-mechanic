var registrationUserVM = new function() {

    var model = {
        Login: ko.observable(),
        Password: ko.observable(),
        PasswordСonfirmation: ko.observable(),
        ContactName: ko.observable(),
        Phone: ko.observable()
    };

    var submit = function () {
        var objectToSend = ko.toJSON(model);
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiRegistrationUserUrl, objectToSend)
            .then(function() {
                window.location.href = window.resource.urls.webUiSignInPageUrl;
            }, function($xhr) {
                console.log($xhr);
            });
    };

    var clearForm = function() {
        for (var key in model) {
            model[key](null);
        }
    };

    return {
        model: model,
        submit: submit,
        clearForm: clearForm
    }
};