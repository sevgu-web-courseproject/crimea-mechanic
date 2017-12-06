var operateWorkClassesAndTypesVM = new function() {

    var workClasses = ko.observableArray([]);
    var workTypes = ko.observableArray([]);
    var workClass = ko.observable();

    var className = ko.observable().extend({
        required: { params: true, message: "Необходимо указать наименнование класса работ" } //TODO
    });
    var typeName = ko.observable().extend({
        required: { params: true, message: "Необходимо указать наименнование типа работы" } //TODO
    });

    var getWorkClasses = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetWorkClassesUrl)
            .then(function (data) {
                workClasses(data);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var addClass = function() {
        var validationGroup = ko.validation.group([className]);
        if (validationGroup().length !== 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            Name: className()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiAddWorkClassUrl, data)
            .then(function() {
                $('#close-button-class').click();
                className(null);
                validationGroup.showAllMessages(false);
                getWorkClasses();
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteClass = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteWorkClassUrl.replace("workClassId", workClass().Id);
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                getWorkClasses();
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var addType = function() {
        var validationGroup = ko.validation.group([typeName]);
        if (validationGroup().length !== 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            Name: typeName(),
            WorkClassId: workClass().Id
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiAddWorkTypeUrl, data)
            .then(function () {
                $('#close-button-type').click();
                typeName(null);
                validationGroup.showAllMessages(false);
                getWorkClasses();
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteType = function(type) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteWorkTypeUrl.replace("workTypeId", type.Id);
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                getWorkClasses();
            }, function ($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function () {
        getWorkClasses();

        workClass.subscribe(function (newValue) {
            if (newValue) {
                workTypes(newValue.Types);
            } else {
                workTypes([]);
            }
        });
    };

    return {
        init: init,

        workClasses: workClasses,
        workTypes: workTypes,
        workClass: workClass,
        className: className,
        typeName: typeName,

        addClass: addClass,
        deleteClass: deleteClass,
        addType: addType,
        deleteType: deleteType
    };
};