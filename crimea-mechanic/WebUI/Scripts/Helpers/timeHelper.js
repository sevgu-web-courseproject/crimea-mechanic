var timeHelper = {
    toLocalTime: function(utc) {
        return moment.utc(utc).local().format("DD.MM.YYYY HH:mm:ss");
    }
}