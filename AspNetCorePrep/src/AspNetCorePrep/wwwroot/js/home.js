// Load the number of hours and put it on the home page.
$.get("/timeentries/totalhours")
    .done(function (data) {
        $("#total-hours").text(data);
    });