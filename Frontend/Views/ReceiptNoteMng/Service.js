angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', '$rootScope', function ($http, jsonService, $rootScope) {
    angular.extend(this, jsonService);


    this.getPurchasingInvoice = function (searchFilter, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            //contentType: "application/json",
            url: context.serviceUrl + 'gets-purchasing',
            data: JSON.stringify(searchFilter),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + context.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type === 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    this.getFactorySaleInvoice = function (searchFilter, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            //contentType: "application/json",
            url: context.serviceUrl + 'gets-factory-sale-invoice',
            data: JSON.stringify(searchFilter),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + context.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type === 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    this.setStatus = function (receiptNoteID, statusID, receiptNoteTypeID, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            url: this.serviceUrl + 'set-status?receiptNoteID=' + receiptNoteID + '&statusID=' + statusID + '&receiptNoteTypeID=' + receiptNoteTypeID,
            //data: JSON.stringify(null),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type === 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    this.getPrintout = function (receiptNoteID, successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",
            dataType: 'json',
            url: this.serviceUrl + 'get-receiptnote-printout?receiptNoteID=' + receiptNoteID,
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

    this.getSupplier = function (successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",
            dataType: 'json',
            url: this.serviceUrl + 'get-supplier',
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


    /*
     * Change Data
     */
    this.pushDataInvoice = function (data) {
        $rootScope.$broadcast('dataService.pushDataInvoice', data);
        data = null;
    };
    this.pushDataSaleInvoice = function (data) {
        $rootScope.$broadcast('dataService.pushDataSaleInvoice', data);
        data = null;
    };
}]);
