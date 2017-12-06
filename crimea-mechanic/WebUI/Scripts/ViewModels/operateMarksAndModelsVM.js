var operateMarksAndModelsVM = new function() {

    var marks = ko.observableArray([]);
    var models = ko.observableArray([]);
    var mark = ko.observable();
    var markId = ko.observable();

    var markName = ko.observable().extend({
        required: { params: true, message: "Необходимо указать наименнование марки автомобиля" } //TODO
    });

    var modelName = ko.observable().extend({
        required: { params: true, message: "Необходимо указать наименнование модели автомобиля" } //TODO
    });

    var getMarks = function() {
        $(document).trigger("showLoadingPanel");
        ajaxHelper.get(window.resource.urls.webApiGetMarksUrl)
            .then(function(data) {
                marks(data);
                $(document).trigger("hideLoadingPanel");
            }, function($xhr) {
                var text = ajaxHelper.extractErrors($xhr);
                $(document).trigger("hideLoadingPanel");
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var getModels = function (markId) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiGetModelsUrl.replace("markId", markId);
        ajaxHelper.get(url)
            .then(function (data) {
                models(data);
                $(document).trigger("hideLoadingPanel");
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    }

    var addMark = function() {
        var validationGroup = ko.validation.group([markName]);
        if (validationGroup().length !== 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            Name: markName()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiAddCarMarkUrl, data)
            .then(function() {
                markName(undefined);
                validationGroup.showAllMessages(false);
                $('#close-button-mark').click();
                getMarks();
                notificationHelper.success(window.resource.texts.success, "Марка автомобиля успешно добавлена"); //TODO
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var addModel = function() {
        var validationGroup = ko.validation.group([modelName]);
        if (validationGroup().length !== 0) {
            validationGroup.showAllMessages();
            return;
        }
        $(document).trigger("showLoadingPanel");
        var data = JSON.stringify({
            Name: modelName(),
            MarkId: markId()
        });
        ajaxHelper.postJsonWithoutResult(window.resource.urls.webApiAddCarModelUrl, data)
            .then(function () {
                modelName(undefined);
                validationGroup.showAllMessages(false);
                $('#close-button-model').click();
                getModels(markId());
                notificationHelper.success(window.resource.texts.success, "Модель автомобиля успешно добавлена"); //TODO
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteMark = function() {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteMarkUrl.replace("markId", markId());
        ajaxHelper.getWithoutResult(url)
            .then(function() {
                getMarks();
                mark(undefined);
                markId(undefined);
                notificationHelper.success(window.resource.texts.success, "Марка автомобиля успешно удалена"); //TODO
            }, function($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var deleteModel = function(model) {
        $(document).trigger("showLoadingPanel");
        var url = window.resource.urls.webApiDeleteModelUrl.replace("modelId", model.Id);
        ajaxHelper.getWithoutResult(url)
            .then(function () {
                getModels(markId());
                notificationHelper.success(window.resource.texts.success, "Модель автомобиля успешно удалена"); //TODO
            }, function ($xhr) {
                $(document).trigger("hideLoadingPanel");
                var text = ajaxHelper.extractErrors($xhr);
                notificationHelper.error(window.resource.texts.error, text);
            });
    };

    var init = function () {
        getMarks();

        mark.subscribe(function (newValue) {
            if (newValue) {
                var match = ko.utils.arrayFirst(marks(),
                    function (item) {
                        return item.Name === newValue;
                    });
                markId(match.Id);
                getModels(match.Id);
            } else {
                markId(undefined);
                models([]);
            }
        });
    };

    return {
        init: init,
        marks: marks,
        models: models,
        mark: mark,
        markName: markName,
        modelName: modelName,
        addMark: addMark,
        addModel: addModel,
        deleteMark: deleteMark,
        deleteModel: deleteModel
    };
};