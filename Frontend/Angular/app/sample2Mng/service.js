jsonService.searchFilter.sortedBy = 'SampleOrderStatusID';
jsonService.searchFilter.sortedDirection = 'ASC';

//
// sample order
//
jsonService.getOrder = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getorder?id=' + id,
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
jsonService.updateOrder = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updateorder?id='+id,
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
jsonService.changeOrderStatus = function (id, statusId, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'changeorderstatus?id=' + id + '&statusId=' + statusId,
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
jsonService.deleteOrder = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deleteorder?id='+id,
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


//
// sample product
//
jsonService.getProduct = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getproduct?id=' + id,
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
jsonService.updateProduct = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updateproduct?id=' + id,
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
jsonService.deleteProduct = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deleteproduct?id=' + id,
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
jsonService.changeProductStatus = function (id, statusId, data, successCallBack, errorCallBack) {
    console.log(statusId);
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'changeproductstatus?id=' + id + '&statusId=' + statusId,
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

//
// sample reference image
//
jsonService.getReferenceImage = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getreferenceimage?id=' + id,
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
jsonService.updateReferenceImage = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updatereferenceimage?id=' + id,
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
jsonService.deleteReferenceImage = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deletereferenceimage?id=' + id,
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

//
// sample progress
//
jsonService.getProgress = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getprogress?id=' + id,
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
jsonService.updateProgress = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updateprogress?id=' + id,
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
jsonService.deleteProgress = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deleteprogress?id=' + id,
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

//
// sample tech drawing
//
jsonService.getDrawing = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getdrawing?id=' + id,
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
jsonService.updateDrawing = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updatedrawing?id=' + id,
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
jsonService.deleteDrawing = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deletedrawing?id=' + id,
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

//
// sample remark
//
jsonService.getRemark = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getremark?id=' + id,
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
jsonService.updateRemark = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updateremark?id=' + id,
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
jsonService.deleteRemark = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deleteremark?id=' + id,
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

//
// sample minute
//
jsonService.getMinute = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'getminute?id=' + id,
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
jsonService.updateMinute = function (id, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updateminute?id=' + id,
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
jsonService.deleteMinute = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deleteminute?id=' + id,
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

//
// report
//
jsonService.getMinuteReport = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getminutereport?id=' + id,
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

jsonService.getSampleReport = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getsamplereport?id=' + id,
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

