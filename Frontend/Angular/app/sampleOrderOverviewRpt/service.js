jsonService.getExcelReport = function (season, clientId, vnFactoryId, nlFactoryId, sampleProductStatusID, sampleOrderStatusID, sampleOrderID, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getexcelreport?season=' + season + '&c=' + clientId + '&vnf=' + vnFactoryId + '&nlf=' + nlFactoryId + '&s=' + sampleProductStatusID + '&so=' + sampleOrderStatusID + '&ud=' + sampleOrderID,
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
};

jsonService.getExcelSampleProcess = function (season, clientId, vnFactoryId, nlFactoryId, sampleProductStatusID, sampleOrderStatusID, sampleOrderID, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getexcelreportdatasampleprocess?season=' + season + '&c=' + clientId + '&vnf=' + vnFactoryId + '&nlf=' + nlFactoryId + '&s=' + sampleProductStatusID + '&so=' + sampleOrderStatusID + '&ud=' + sampleOrderID,
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