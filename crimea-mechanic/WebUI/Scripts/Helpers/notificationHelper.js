var hotificationHelper = {
    error: function(title, message) {
        iziToast.error({
            title: title,
            message: message,
            position: 'bottomRight'
        });
    }
};