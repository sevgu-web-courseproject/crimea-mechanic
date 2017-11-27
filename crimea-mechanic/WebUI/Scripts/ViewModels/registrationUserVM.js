var registrationUserVM = new function() {
    var self = this;

    self.model = {
        Login: ko.observable().extend({
            required: { params: true, message: window.resource.errors.loginIsEmpty },
            email: {params: true, message: window.resource.errors.loginIsNotEmail }
        }),
        Password: ko.observable().extend({
            required: { params: true, message: window.resource.errors.passwordIsEmpty }
        }),
        PasswordСonfirmation: ko.observable().extend({
            required: { params: true, message: window.resource.errors.passwordIsEmpty }
        }),
        ContactName: ko.observable().extend({
            required: { params: true, message: window.resource.errors.contactNameIsEmpty }
        }),
        Phone: ko.observable().extend({
            required: { params: true, message: window.resource.errors.phoneNumberIsEmpty }
        })
    };

    var validationGroup = null;
    var submit = function () {
        validationGroup = ko.validation.group([
            self.model.Login,
            self.model.Password,
            self.model.PasswordСonfirmation,
            self.model.ContactName,
            self.model.Phone
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var objectToSend = ko.toJSON(self.model);
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiRegistrationUserUrl, objectToSend)
            .then(function () {
                localStorage.success = window.resource.texts.successUserRegistration;
                window.location.href = window.resource.urls.webUiSignInPageUrl;
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                hotificationHelper.error(window.resource.texts.error, text);
            });
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
        $('#phone').mask('+7(999) 999-99-99');
    }

    return {
        init: init,
        model: self.model,
        submit: submit,
        clearForm: clearForm
    }
};