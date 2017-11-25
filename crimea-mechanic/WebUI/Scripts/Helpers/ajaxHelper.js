var ajaxHelper = {
    get: function(url) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                    url: url,
                    type: "GET",
                    dataType: "json"
                })
                .done(function (data) {
                    resolve(data);
                })
                .fail(function ($xhr) {
                    reject($xhr);
                });
        });
    },
    postJson: function(url, data) {
        return new Promise(function(resolve, reject) {
            $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: data,
                    contentType: "application/json"
                })
                .done(function (data) {
                    resolve(data);
                })
                .fail(function ($xhr) {
                    reject($xhr);
                });
        });
    },
    postJsonWithoutResult: function (url, data) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                    url: url,
                    type: "POST",
                    data: data,
                    contentType: "application/json"
                })
                .done(function () {
                    resolve();
                })
                .fail(function ($xhr) {
                    reject($xhr);
                });
        });
    },
    postFormData: function(url, data) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                    url: url,
                    type: "POST",
                    data: data,
                    contentType: false,
                    processData: false
                })
                .done(function () {
                    resolve();
                })
                .fail(function ($xhr) {
                    reject($xhr);
                });
        });
    },
    extractErrors: function ($xhr) {
        if ($xhr.readyState === 0) {
            return "Не удалось выполнить запрос. Пожалуйста, повторите попытку.";
        }
        if ($xhr.status === 403) {
            return "Недостаточно прав для выполнения операции";
        }
        var responceJson = $xhr.responseJSON;
        var err = '';
        if (responceJson) {
            if (responceJson.ModelState === null || responceJson.ModelState === undefined) {
                if (responceJson.ExceptionMessage) {
                    err = responceJson.ExceptionMessage;
                } else {
                    err = responceJson.Message;
                }
            } else {
                for (var key in responceJson.ModelState) {
                    if (responceJson.ModelState.hasOwnProperty(key)) {
                        if (key !== '') {
                            err += key + ' : ';
                        }
                        err += responceJson.ModelState[key] + '<br>';
                    }
                }
            }
        } else if ($xhr.message) {
            err = $xhr.message;
        } else {
            err = $xhr;
        }
        return err;
    }
};