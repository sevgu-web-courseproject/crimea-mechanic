﻿$(function () {
    $(document).on("showLoadingPanel", function () {
        if (!$(".light-box").length) {
            var loadingPanel = $("<div>").addClass("light-box");
            var spinner = $("<img>")
                .addClass("spinner")
                .attr({
                    src: "/Content/Images/spinner.gif"
                });
            loadingPanel.append(spinner);
            var body = $("body");
            body.css("overflow", "hidden");
            body.append(loadingPanel);
        }
    });

    $(document).on("hideLoadingPanel", function () {
        setTimeout(function() {
            $(".light-box").remove();
            $("body").css("overflow", "auto");
        }, 500);
    });
});