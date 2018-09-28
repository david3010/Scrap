$(document).ready(function () {
    $('.noti-content').hide();
    updateNotification();
    // Click on notification icon for show notification
    $('span.noti').click(function (e) {
        e.stopPropagation();
        $('.noti-content').show();
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        updateNotification();
        $('span.count', this).html('&nbsp;');
    })
    // hide notifications
    $('html').click(function () {
        $('.noti-content').hide();
    })
    // update notification
    function updateNotification() {
        $('#notiContent').empty();
        $('#notiContent').append($('<li>Loading...</li>'));
        $.ajax({
            type: 'GET',
            url: '/scrap/Tickets/GetNotifications/',
            //url: '/Tickets/GetNotifications/',
            success: function (response) {
                $('#notiContent').empty();
                if (response == 0) {
                    $('#notiContent').append($('<li>No hay datos disponibles</li>'));
                }
                if (response > 0) {
                    $('#notiContent').append($('<li>Usted tiene: ' + response + ' tickets por autorizar</li> '));
                    $('span.count').html(response);
                }
                if (response > 20) {
                    window.alert('Usted tiene más de 20 tickets por autorizar...');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    };
});