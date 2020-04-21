'use strict';

(function ($) {
    $.fn.inlineStyle = function (prop) {
        return this.prop("style")[$.camelCase(prop)];
    };
}(jQuery));

var jsHelper = {
    showForm: function (id, callback) {
        $('.avs-form').hide();
        $('#' + id).show();

        if (callback) {
            callback();
        }
    },

    loadingSwitch: function (isShow) {
        if (isShow) {
            $('#loading-mask').show();
        }
        else {
            $('#loading-mask').hide();
        }
    },

    showMessage: function (type, message) {
        var displayString = '';
        var iconClass = '';
        switch (type) {
            case 'warning':
                displayString = 'Warning';
                iconClass = 'warning';
                if (message === null) {
                    message = 'Unknow error, operation failed';
                }
                break;

            case 'success':
                displayString = 'Success';
                iconClass = 'check';
                if (message === null) {
                    message = 'Update success';
                }
                break;

            case 'info':
                displayString = 'Information';
                iconClass = 'info';
                if (message === null) {
                    message = 'Unknow issue';
                }
                break;
        }

        var html = '';
        html += '<div class="alert alert-' + type + ' fade in">';
        html += '<button data-dismiss="alert" class="close"> x </button>';
        html += '<i class="fa-fw fa fa-' + iconClass + '"></i>';
        html += '<strong>' + displayString + '</strong> ' + message;
        html += '</div>';
        $('#notificationMessage article').html(html);
        $('#notificationMessage').show();
    },

    sum: function (data, colNames) {
        var total = parseFloat(0);
        for (var i = 0; i < data.length; i++) {
            for (var j = 0; j < colNames.length; j++) {
                if (data[i][colNames[j]] !== null) {
                    total += parseFloat(data[i][colNames[j]]);
                }
            }
        }
        return total;
    },

    round: function (number, decimal) {
        var result = number;
        if (decimal === 0) {
            result = Math.round(number);
        }
        else {
            var decimalNumber = 1;
            for (var i = 1; i <= decimal; i++) {
                decimalNumber = decimalNumber * 10;
            }
            result = Math.round(number * decimalNumber) / decimalNumber;
        }
        return result;
    }
}