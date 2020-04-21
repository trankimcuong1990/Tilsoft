'use strict';

(function ($) {
    $.fn.inlineStyle = function (prop) {
        return this.prop("style")[$.camelCase(prop)];
    };
}(jQuery));

var jsHelper = {
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
    },

    convertToClientDate: function (serverDate) {
        return '14/10/1981';
    },

    convertToServerDate: function (clientDate) {
        return '1981-10-14';
    },

    stringToDate: function (dateString) { // dateString must be in the format dd/mm/yyyy
        var mObject = moment(dateString, 'DD/MM/YYYY HH:mm:ss');
        return mObject.toDate();
        //dateString = dateString.replace(/\//g, '-');
        //var dt = dateString.split(/\-|\s/);
        //return new Date(dt.slice(0, 3).reverse().join('-') + ' ' + dt[3]);
    },

    getTotalDateDiff: function (date1, date2) {
        var one_day = 1000 * 60 * 60 * 24;
        // Convert both dates to milliseconds
        var date1_ms = date1.getTime();
        var date2_ms = date2.getTime();

        // Calculate the difference in milliseconds
        var difference_ms = date2_ms - date1_ms;

        // Convert back to days and return
        return Math.round(difference_ms / one_day);
    },

    loadingSwitch: function (isShow) {
        if (isShow) {
            jQuery('#loading-mask').show();
        }
        else {
            jQuery('#loading-mask').hide();
        }
    },

    loadingSwitchExt: function (isShow, elemId) {
        if (isShow) {
            $('#' + elemId).hide();
            $('#' + elemId + '-loading').show();
        }
        else {
            $('#' + elemId).show();
            $('#' + elemId + '-loading').hide();
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
        jQuery('#notificationMessage article').html(html);
        jQuery('#notificationMessage').show();
    },

    processMessage: function (message) {
        switch (message.type) {
            case 0:
                this.showMessage('success', message.message);

                break;

            case 1:
                this.showMessage('warning', message.message);
                jQuery.each(message.detailMessage, function () {
                    jsHelper.showMessage('warning', this);
                });

                break;

            case 2:
                this.showMessage('warning', message.message);
                jQuery.each(message.detailMessage, function () {
                    jsHelper.showMessage('warning', this);
                });

                break;

        }
    },

    showPopup: function (popupId, openCallback) {
        // hide the main form
        jQuery('#main-form').hide();
        jQuery('#sparks').hide();

        // show the popup
        jQuery('#' + popupId).show();
        window.scrollTo(0, 0);

        // trigger callback
        openCallback();
    },

    hidePopup: function (popupId, closeCallback) {
        // hide the popup
        jQuery('#' + popupId).hide();

        // show the main form
        jQuery('#main-form').show();
        jQuery('#sparks').show();

        // trigger callback
        closeCallback();
    },

    processMessageEx: function (message, customMessage) {
        if (message.type === 0) {
            //message.message = '';
            if (customMessage !== null) {
                message.message = customMessage;
            }
        }

        console.log(message);

        switch (message.type) {
            case 0:
                jQuery.smallBox({
                    title: "Success",
                    content: message.message + '<br/><br/>This message will be gone 5 seconds, or you can click here to get rid of it',
                    color: "#5384AF",
                    timeout: 5000,
                    icon: "fa fa-check"
                });

                break;

            case 1:
                jQuery.smallBox({
                    title: "Warning",
                    content: message.message + '<br/><br/>This message will be gone 5 seconds, or you can click here to get rid of it',
                    color: "#C46A69",
                    timeout: 5000,
                    icon: "fa fa-warning shake animated"
                });

                break;

            case 2:
                jQuery.smallBox({
                    title: "Warning",
                    content: message.message + '<br/><br/>This message will be gone 5 seconds, or you can click here to get rid of it',
                    color: "#C46A69",
                    timeout: 5000,
                    icon: "fa fa-warning shake animated"
                });

                break;

        }
    },

    getCurrentSeason: function (isShort) {
        var today = new Date();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (mm >= 9) {
            if (isShort) {
                return (yyyy + '').substr(2, 2) + '/' + ((yyyy + 1) + '').substr(2, 2);
            }
            return yyyy + '/' + (yyyy + 1);
        }
        else {
            if (isShort) {
                return ((yyyy - 1) + '').substr(2, 2) + '/' + (yyyy + '').substr(2, 2);
            }
            return (yyyy - 1) + '/' + yyyy;
        }
    },

    getPrevSeason: function (season, isShort) {
        var data = season.split('/');
        if (isShort) {
            return ((data[0] - 1) + '').substr(2, 2) + '/' + (data[0] + '').substr(2, 2);
        }
        return (data[0] - 1) + '/' + data[0];
    },

    getNextSeason: function () {
        var today = new Date();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (mm >= 9) {
            return (yyyy + 1) + '/' + (yyyy + 2);
        }
        else {
            return yyyy + '/' + (yyyy + 1);
        }
    },

    getSeasons: function () {
        var seasons = [];
        var today = new Date();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear() + 1;
        for (var i = yyyy; i >= 2006; i--) {
            seasons.push({ seasonValue: i + '/' + (i + 1), seasonText: i + '/' + (i + 1) });
        }
        return seasons;
    },

    getYesNoValues: function () {
        return [
            { Value: true, Text: 'Yes' },
            { Value: false, Text: 'No' },
        ];
    },

    getArrayIndex: function (array, field, value) {
        var isExist = false;
        var index = 0;
        for (index = 0; index < array.length; index++) {
            if (array[index][field] === value) {
                isExist = true;
                break;
            }
        }

        if (!isExist) {
            return -1;
        }
        else {
            return index;
        }
    },

    getCurrentDateTime: function () {
        var currentdate = new Date();
        return currentdate.getDate() + "/"
            + (currentdate.getMonth() + 1) + "/"
            + currentdate.getFullYear() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
    },

    goToSection: function (id) {
        window.location.hash = id;
    },

    getRandomColor: function () {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    },

    refreshAvsScroll: function () {
        $('.tilsoft-table-wrapper[avs-scroll=""] .tilsoft-table').each(function () {
            var jObject = $(this);
            var jHeader = jObject.find('.tilsoft-table-header');
            var jFilter = jObject.find('.tilsoft-table-filter');
            var jTotalRow = jObject.find('.tilsoft-table-totalrow');
            var jSubTotalRow = jObject.find('.tilsoft-table-subtotalrow');
            var initHeight = 0;

            var colIndex = 0;
            var colWidth = [];
            var totalWidth = 15;
            $.each(jHeader.find("div"), function (item) {
                if ($(this).inlineStyle('width')) {
                    colWidth.push($(this).outerWidth());
                    totalWidth += $(this).outerWidth();
                }
            });
            jHeader.css('top', jObject.scrollTop());
            jHeader.css('width', totalWidth + 'px');

            initHeight = parseInt(jHeader.height()) || parseInt(jHeader.clientHeight);
            if (jFilter.length > 0) {
                jFilter.css('top', jObject.scrollTop() + initHeight);
                jFilter.css('width', totalWidth + 'px');
                initHeight += parseInt(jFilter.height()) || parseInt(jFilter.clientHeight);

                // change column width
                colIndex = 0;
                $.each(jFilter.find("div"), function (item) {
                    if (!$(this).inlineStyle('width') && colWidth[colIndex]) {
                        $(this).css('width', colWidth[colIndex] + 'px');
                    }
                    colIndex++;
                });
            }
            if (jTotalRow.length > 0) {
                jTotalRow.css('top', jObject.scrollTop() + initHeight);
                jTotalRow.css('width', totalWidth + 'px');
                initHeight += parseInt(jTotalRow.height()) || parseInt(jTotalRow.clientHeight);

                // change column width
                colIndex = 0;
                $.each(jTotalRow.find("div"), function (item) {
                    if (!$(this).inlineStyle('width') && colWidth[colIndex]) {
                        $(this).css('width', colWidth[colIndex] + 'px');
                    }
                    colIndex++;
                });
            }
            if (jSubTotalRow.length > 0) {
                jSubTotalRow.css('top', jObject.scrollTop() + initHeight);
                jSubTotalRow.css('width', totalWidth + 'px');
                initHeight += parseInt(jSubTotalRow.height()) || parseInt(jSubTotalRow.clientHeight);

                // change column width
                colIndex = 0;
                $.each(jSubTotalRow.find("div"), function (item) {
                    if (!$(this).inlineStyle('width') && colWidth[colIndex]) {
                        $(this).css('width', colWidth[colIndex] + 'px');
                    }
                    colIndex++;
                });
            }
            jObject.find('.tilsoft-table-body').css('margin-top', initHeight + 'px');
            colIndex = 0;
            //$.each(jObject.find('.tilsoft-table-body table tr:first-child td'), function (item) {
            //    // change column width
            //    if (!$(this).inlineStyle('width') && colWidth[colIndex]) {
            //        $(this).css('width', colWidth[colIndex] + 'px');
            //    }
            //    colIndex++;
            //});
            if (jObject.find('.tilsoft-table-body table .dummy-row').length === 0) {
                var dummyRow = '<tr class="dummy-row">';
                for (colIndex = 0; colIndex < colWidth.length; colIndex++) {
                    dummyRow += '<td style="width: ' + colWidth[colIndex] + 'px;">&nbsp;</td>';
                }
                dummyRow += '<td>&nbsp;</td></tr>';
                jObject.find('.tilsoft-table-body table').append(dummyRow);
            }
            jObject.find('.tilsoft-table-body').css('width', totalWidth + 'px');
        });
    },

    refreshAVSTable: function () {
        $('.avs-table-wrapper[avs-table=""]').each(function (i, elem) {
            var obj = $(elem);
            // set column width
            var jHeader = obj.find('.avs-table-header table tr.header-row');
            console.log(jHeader);
            var colIndex = 0;
            var colWidth = [];
            var totalWidth = 0;
            $.each(jHeader.find("td"), function (item) {
                if ($(this).inlineStyle('width')) {
                    colWidth.push($(this).outerWidth());
                    totalWidth += $(this).outerWidth();
                }
            });
            if (obj.find('.avs-table-body table .dummy-row').length === 0) {
                var dummyRow = '<colgroup>';
                for (colIndex = 0; colIndex < colWidth.length; colIndex++) {
                    dummyRow += '<col style="width: ' + colWidth[colIndex] + 'px;" />';
                }
                dummyRow += '</colgroup>';
                obj.find('.avs-table-body table').append(dummyRow);
                //obj.find('.avs-table-header table').append(dummyRow);
                console.log(dummyRow);
            }
            obj.find('.avs-table-body table').css('width', totalWidth + 'px');
            obj.find('.avs-table-header table').css('width', totalWidth + 'px');
            obj.find('.avs-table-header-container').css('width', (totalWidth + 50) + 'px');
        });
    },

    getPriceColor: function (sourceID) {
        switch (sourceID) {
            case 1, 2:
                return 'blue';

            case 3, 7:
                return 'orange';

            case 4, 5, 6:
                return 'green';
        }

        return '#000';
    },

    getShortSeason: function (season) {
        return season.substr(2, 2) + '/' + season.substr(7, 2);
    },

    getUniqueString: function () {
        var result = '';
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var charactersLength = characters.length;
        for (var i = 0; i < 10; i++) {
            result += characters.charAt(Math.floor(Math.random() * charactersLength));
        }
        return result;
    },

    autoMapper: function (fromOject, toObject) {
        for (var key in fromOject) {
            toObject[key] = fromOject[key];
        }
    },

    getDefaultColor: function (sourceDataCode) {
        switch (sourceDataCode) {
            case 'pi':
                return '#0ceb60';

            case 'piconfirmed':
                return '#ffc000';

            case 'ci':
                return '#f0390b';

            case 'expectation':
                return '#00b0f0';

            case '%delta':
                return '#f7a35c';

            case '%avgdelta':
                return '#8085e9';

            case 'delta':
                return '#b75288';            
        }
    }
}