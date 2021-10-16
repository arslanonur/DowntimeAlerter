function StartRecurringNotificationJob() {
    $.ajax({
        url: '/Job/StartRecurringNotificationJob")',
        type: 'GET',
        async: true,
        dataType: "json",
        success: function (response) {

        },
        error: function (ex) {

        }
    });
}