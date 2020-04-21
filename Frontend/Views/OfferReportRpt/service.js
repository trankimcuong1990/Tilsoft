﻿jsonService.getUserFOBItemOnly = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getuserfobitemonly?season=' + season,
        beforeSend: function () {
            //jsHelper.loadingSwitch(true);
        },
        success: function (data) {
            //jsHelper.loadingSwitch(false);
            successCallBack(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //jsHelper.loadingSwitch(false);
            errorCallBack(xhr.responseJSON.exceptionMessage);
        }
    });
}

jsonService.excelPOBItemOnly = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'excel-fobitemonly?season=' + season,
        beforeSend: function () {
            jsHelper.loadingSwitch(true);
        },
        success: function (data) {
            jsHelper.loadingSwitch(false);
            successCallBack(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            jsHelper.loadingSwitch(false);
            errorCallBack(xhr.responseJSON.exceptionMessage);
        }
    });
}