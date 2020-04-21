jsonService.getReportData = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getreportdata?season=' + season,
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
};

jsonService.getMIReportData = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceMISaleUrl + 'getreport?season=' + season,
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
};

jsonService.getMISaleConclusion = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceMISaleConclusionUrl + 'getreport?season=' + season + '&saleID=-1',
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
};

jsonService.getDataForUserDashboard = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getDataForUserDashboard?season=' + season,
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
};

jsonService.getQuotationById = function (filters, successCallBack, errorCallBack) {
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
        url: this.supportServiceUrl + 'getDataForPopupWithFilters',
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
};

jsonService.updateQuotationById = function (id, data, successCallBack, errorCallBack) {
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
        url: this.supportServiceUrl + 'update?id=' + id,
        beforeSend: function () {
            //jsHelper.loadingSwitch(true);
        },
        success: function (data) {
            jsHelper.loadingSwitch(false);
            //successCallBack(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //jsHelper.loadingSwitch(false);
            errorCallBack(xhr.responseJSON.exceptionMessage);
        }
    });
};

jsonService.GetPendingPriceData = function (successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getpendingpricedata',
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
};

jsonService.GetProductionData = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getproductiondata?season=' + season,
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
};

jsonService.getProductionOverviewDetail = function (factoryOrderID, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getdataforproductionoverviewdetail?factoryOrderID=' + factoryOrderID,
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
};

jsonService.getFinanceOverviewByFactoryID = function (season, factoryID, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getFinanceOverviewByFactoryID?season=' + season + '&factoryID=' + factoryID,
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
};

jsonService.getWeeklyProductionData = function (season, factoryID, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceUrl + 'getweeklyproductiondata?season=' + season + '&factoryId=' + factoryID,
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
};

jsonService.getMISaleByClient = function (season, successCallBack, errorCallBack) {
    jQuery.ajax({
        cache: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
        },
        type: "POST",
        dataType: 'json',
        url: this.serviceMISaleByClient + 'getreport-dashboard?season=' + season,
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
};