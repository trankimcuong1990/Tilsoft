jsonService.searchFilter.sortedBy = 'UpdatedDate';
jsonService.searchFilter.sortedDirection = 'DESC';

jsonService.load = function (id, bookingId, factoryId, parentId, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'get?id=' + id + '&b=' + bookingId + '&f=' + factoryId + '&p=' + parentId,
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