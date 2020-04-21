jsonService.getInitData = function (successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getinitdata',
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

jsonService.getReport = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getreport?season=' + season,
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