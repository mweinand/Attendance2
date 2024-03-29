﻿$(document).ready(function () {
    var scanInput = $("#ScanInput");
    var statusMessage = $('#StatusMessage');
    var checking = false;
    var delay = 1500;

    var resetForm = function () {
        scanInput.css('background-color', '#FFF');
        scanInput.prop('disabled', false);
        scanInput.val('');
        scanInput.focus();
    };

    var showError = function (direction) {
        scanInput.css('background-color', '#F00');
        statusMessage.css('color', '#F00');
        statusMessage.text('Invalid User');
        setTimeout(resetForm, delay);
    };

    var showSuccess = function (direction, user) {
        scanInput.css('background-color', '#0F0');
        statusMessage.css('color', '#0F0');
        statusMessage.text(user);
        setTimeout(resetForm, delay);
    };

    var verifyQuickAction = function (direction) {
        if (confirm('You just logged in.  Are you sure you want to logout?')) {


        }
    };

    var punchClock = function (serial) {
        scanInput.css('background-color', '#FF0');
        scanInput.prop('disabled', true);
        $.ajax({
            url: '/Clock/Punch',
            dataType: 'json',
            type: 'POST',
            data: {
                Serial: serial
            },
            success: function (data) {
                switch (data.Result) {
                    case 0:
                        showError(data.Direction);
                        break;
                    case 1:
                        showSuccess(data.Direction, data.Name);
                        break;
                    case 2:
                        break;
                }
                reloadCurrentUsers();
            }
        });

    }

    var isReloading = false;
    var reloadCurrentUsers = function () {
        if (isReloading) {
            return;
        }
        $("#LoggedInUsers").load('/Clock/CurrentEmployees', function () {
            isReloading = false;
        });
        isReloading = true;
    };

    $('#LoggedInUsers').on('click', 'a.sign-out-link', function (e) {
        punchClock($(this).attr('data-id'));
        return false;
    });

    scanInput.focus(function () {
        statusMessage.css('color', '#000');
        statusMessage.text('Ready');
    });

    scanInput.blur(function () {
        statusMessage.css('color', '#000');
        statusMessage.text('Waiting...');
    });

    scanInput.keypress(function (e) {
        if (e.which == 13) {
            var input = scanInput.val();
            punchClock(input);
        }
    });

    resetForm();

    $.ajaxSetup({ cache: false });
});
