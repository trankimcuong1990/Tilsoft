﻿var reportPlcProductsService = {
    serviceUrl: '',
    supportServiceUrl: '',
    token: '',
    getFilterData: function (successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",
            dataType: 'json',
            url: this.serviceUrl + 'getfilters',
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
    },
    getReport: function (factoryID, successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",
            url: this.serviceUrl + 'getreport?factoryid='+ factoryID,
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
}