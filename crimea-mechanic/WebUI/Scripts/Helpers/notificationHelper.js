var notificationHelper = {
    error: function(title, message) {
        iziToast.error({
            title: title,
            message: message,
            position: 'bottomRight'
        });
    },
    success: function(title, message) {
        iziToast.success({
            title: title,
            message: message,
            position: 'bottomRight'
        });
    }
};