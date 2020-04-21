var reportDeliverySheduleService = {
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
    getReport: function (season,   clientNM,
                    eta,
                    containerNo,
                    saleID,
                   
                    successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",

            //season=2015/2016&clientNM=NOACH OUTDOOR&eta=2015-12-29&containerNo=PONU7915400&saleID=55&sale2ID=32119
            //+ '&sale2ID=' + sale2ID


            url: this.serviceUrl +  'getreport?season=' + season + '&clientNM='+ clientNM +  '&eta=' + eta +'&containerNo=' + containerNo +  '&saleID=' + saleID    ,
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



//var reportDeliverySheduleService = {
//    serviceUrl: '',
//    supportServiceUrl: '',
//    token: '',
//    getFilterData: function (successCallBack, errorCallBack) {
//        jQuery.ajax({
//            cache: false,
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json',
//                'Authorization': 'Bearer ' + this.token
//            },
//            type: "POST",
//            dataType: 'json',
//            url: this.serviceUrl + 'getfilters',
//            beforeSend: function () {
//                jsHelper.loadingSwitch(true);
//            },
//            success: function (data) {
//                jsHelper.loadingSwitch(false);
//                successCallBack(data);
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                jsHelper.loadingSwitch(false);
//                errorCallBack(xhr.responseJSON.exceptionMessage);
//            }
//        });
//    },
//    getReport: function (clientNM, eta, containerNo, season, saleID, sale2ID, successCallBack, errorCallBack) {
//        jQuery.ajax({
//            cache: false,
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json',
//                'Authorization': 'Bearer ' + this.token
//            },
//            type: "POST",

//            //'getreport?season=' + season + '&factoryID=' + factoryID,

//            url: this.serviceUrl + 'getreport?clientnm='+ clientNM +  '&eta=' + eta +'&containerno=' + containerNo + '&season=' + season + '&saleID=' + saleID + '&sale2ID=' + sale2ID   ,
//            beforeSend: function () {
//                jsHelper.loadingSwitch(true);
//            },
//            success: function (data) {
//                jsHelper.loadingSwitch(false);
//                successCallBack(data);
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                jsHelper.loadingSwitch(false);
//                errorCallBack(xhr.responseJSON.exceptionMessage);
//            }
//        });
//    }
//}