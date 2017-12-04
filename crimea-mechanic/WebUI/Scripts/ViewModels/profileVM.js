var profileVM = new function() {

    var model = {
        Id: ko.observable(),
        Email: ko.observable(),
        Name: ko.observable(),
        Phone: ko.observable(),
        Created: ko.observable()
    };

    var editProfile = {
        Name: ko.observable().extend({
            required: { params: true, message: "Необходимо указать контакное имя" } //TODO
        }),
        Phone: ko.observable().extend({
            required: { params: true, message: "Необходимо указать контактный номер" } //TODO
        })
    };

    var getProfile = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetClientProfileUrl)
            .then(function (data) {
                ko.mapping.fromJS(data, {}, model);
                model.Created(timeHelper.toLocalTime(model.Created()));
                editProfile.Name(model.Name());
                editProfile.Phone(model.Phone());
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                localStorage.error = text;
                window.location.href = window.resource.urls.webUiMyProfileUrl;
            });
    };

    var sendEditProfile = function () {
        var validationGroup = ko.validation.group([
            editProfile.Name,
            editProfile.Phone
        ]);
        if (validationGroup().length != 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            Name: editProfile.Name(),
            Phone: editProfile.Phone()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiEditClientProfileUrl, data)
            .then(function () {
                $('#close-button').click();
                getProfile();
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function () {
        $('#edit-phone').mask("+7(999) 999-99-99");
        getProfile();
    };

    return {
        init: init,
        model: model,
        editProfile: editProfile,
        sendEditProfile: sendEditProfile
    };
};