jsonService.searchFilter.sortedBy = 'UpdatedDate';
jsonService.searchFilter.sortedDirection = 'DESC';
//jsonService.serviceUrl = context.serviceUrl;
//jsonService.token = context.token;


jsonService.changeProductStatus = function (id, statusId, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",      
        url: this.serviceUrl + 'updateproductstatus?id=' + id + '&statusId=' + statusId,
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

jsonService.updateFinishStatus = function (id, file, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'updateproductstatus2?id=' + id + '&statusId=4&file=' + file,
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
//jsonService.getProgress = function (id, successCallBack, errorCallBack) {
//    jQuery.ajax({
//        cache: false,
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json',
//            'Authorization': 'Bearer ' + this.token
//        },
//        type: "POST",
//        dataType: "json",
//        url: this.serviceUrl + 'getprogress?id=' + id,
//        beforeSend: function () {
//            jsHelper.loadingSwitch(true);
//        },
//        success: function (data) {
//            jsHelper.loadingSwitch(false);
//            successCallBack(data);
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            jsHelper.loadingSwitch(false);
//            errorCallBack(xhr.responseJSON.exceptionMessage);
//        }
//    });
//}
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

// QA remark
jsonService.updateQARemark = function (id, data, successCallBack, errorCallBack) {
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
        url: this.serviceUrl + 'updateqaremark?id=' + id,
        beforeSend: function () {
            jsHelper.loadingSwitch(true);
        },
        success: function (data) {
            jsHelper.loadingSwitch(false);
            if (data.message.type == 0) {               
                successCallBack(data);
            }
            else {
                jsHelper.showMessage('warning', data.message.message);
                console.log(data.message.message);
              //  errorCallBack(response);
            }

            successCallBack(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            jsHelper.loadingSwitch(false);
            //jsHelper.showMessage('warning', response.data.message);
            errorCallBack(xhr.responseJSON.exceptionMessage);
        }
    });
}
jsonService.deleteQARemark = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: "json",
        url: this.serviceUrl + 'deleteqaremark?id=' + id,
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

//jsonService.updateQARemark = function (id, data, iSuccessCallback, iErrorCallback) {
//    jsHelper.loadingSwitch(true);
//    $http({
//        method: 'POST',
//        url: this.serviceUrl + 'updateqaremark?id=' + id,
//        data: JSON.stringify(data),
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json',
//            'Authorization': 'Bearer ' + this.token
//        }
//    }).then(function successCallback(response) {
//        jsHelper.loadingSwitch(false);

//        if (response.data.message.type == 0) {
//            //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
//            iSuccessCallback(response.data);
//        }
//        else {
//            jsHelper.showMessage('warning', response.data.message.message);
//            console.log(response.data.message.message);
//            iErrorCallback(response);
//        }
//    }, function errorCallback(response) {
//        jsHelper.loadingSwitch(false);
//        jsHelper.showMessage('warning', response.data.message);
//        iErrorCallback(response);
//    });
//};
//jsonService.deleteQARemark = function (id, iSuccessCallback, iErrorCallback) {
//    jsHelper.loadingSwitch(true);
//    $http({
//        method: 'POST',
//        url: this.serviceUrl + 'deleteqaremark?id=' + id,
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json',
//            'Authorization': 'Bearer ' + this.token
//        }
//    }).then(function successCallback(response) {
//        jsHelper.loadingSwitch(false);

//        if (response.data.message.type == 0) {
//            //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
//            iSuccessCallback(response.data);
//        }
//        else {
//            jsHelper.showMessage('warning', response.data.message.message);
//            console.log(response.data.message.message);
//            iErrorCallback(response);
//        }
//    }, function errorCallback(response) {
//        jsHelper.loadingSwitch(false);
//        jsHelper.showMessage('warning', response.data.message);
//        iErrorCallback(response);
//    });
//};

jsonService.quicksearchPiece = function (filters, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(filters),
        url: this.supportServiceUrl + 'quicksearchpiecemodel',
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

jsonService.quicksearchCushion = function (filters, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(filters),
        url: this.supportServiceUrl + 'quicksearchcushionoption',
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

jsonService.quicksearchSubMaterialOption = function (filters, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(filters),
        url: this.supportServiceUrl + 'quicksearchsubmaterialoption',
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

jsonService.loadTabQCIssued = function (id, successCallBack, errorCallBack) {

    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getDataQCIssued?id=' + id,
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

jsonService.initTracking = function (id, mdlId, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'inittracking?id=' + id + '&mdlId=' + mdlId,
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

jsonService.saveDetailTracking = function (id, idIssue, data, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(data),
        url: this.serviceUrl + 'updatedetailtracking?id=' + id + '&idIssue=' + idIssue,
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

jsonService.deleteDetailTracking = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'deletedetailtracking?id=' + id,
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

jsonService.restore = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'restore?id=' + id,
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

jsonService.getModelDefaultFactoryDetail = function (modelID, factoryID, successCallBack, errorCallBack) {

    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getModelDefaultFactory?modelID=' + modelID + '&factoryID=' + factoryID,
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



//================================================Prinf=============================================================//
jsonService.exportExcelModel = function (filters, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(filters),
        url: this.serviceUrl + 'exportexcelmodel',
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

//===========================================Prinf End========================================================//
//get sample product
jsonService.getSampleProductData = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getsampleproductdata?id=' + id,
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

jsonService.getProductData = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getproductdata?id=' + id,
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

jsonService.getInitDataCreateModel = function (successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getinitdatacreatemodel',
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

jsonService.getDataCreateModel = function (id, sampleProductID, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getdatacreatemodel?id=' + id + '&sampleProductID=' + sampleProductID,
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


jsonService.getProductBreakDownData = function (id, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getproductbreakdown?id=' + id,
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

jsonService.getModelDefaultOptionBySeason = function (id, season, successCallBack, errorCallBack) {

    jQuery.ajax({

        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getmodeldefaultoptionbyseason?id=' + id + '&season=' + season,
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

jsonService.getModelDefaultOptionByPreviousSeason = function (id, season, successCallBack, errorCallBack) {

    jQuery.ajax({

        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        url: this.serviceUrl + 'getmodeldefaultoptionbypreviousseason?id=' + id + '&season=' + season,
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

jsonService.approveData = function (id, data, successCallBack, errorCallBack) {

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
        url: this.serviceUrl + 'approveData?id=' + id,
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

jsonService.resetData = function (id, data, successCallBack, errorCallBack) {

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
        url: this.serviceUrl + 'resetData?id=' + id,
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
