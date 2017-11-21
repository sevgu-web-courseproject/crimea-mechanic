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
    }
};