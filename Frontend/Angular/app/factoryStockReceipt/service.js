﻿jsonService.quickSearchStockRemain = function (searchFilter, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(searchFilter),
        url: this.serviceUrl + 'quicksearchstockremain',
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