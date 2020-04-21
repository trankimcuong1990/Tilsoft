jsonService.searchFilter.sortedBy = 'UpdatedDate';
jsonService.searchFilter.sortedDirection = 'DESC';

jsonService.load = function (id, clientId, methodId, currency, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'get?id=' + id + '&clientId=' + clientId + '&m=' + methodId + '&c=' + currency,
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