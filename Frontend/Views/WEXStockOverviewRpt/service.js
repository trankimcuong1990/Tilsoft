angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
    angular.extend(this, jsonService);

    this.searchFilter.sortedBy = 'StockQnt';
    this.searchFilter.sortedDirection = 'DESC';

    this.exportExcel = function (filters, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            url: this.serviceUrl + 'exportexcel',
            data: JSON.stringify(filters),
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

    this.updateSellingPrice = function (data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            url: this.serviceUrl + 'updatesellingprice',
            data: JSON.stringify(data),  //filters
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

    this.updatePurchasingPrice = function (data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            url: this.serviceUrl + 'updatepurchasingprice',
            data: JSON.stringify(data),  //filters
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

    this.updateObsolete = function (data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateobsolete',
            data: JSON.stringify(data),  //filters
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

    this.updateValueObsolescence = function (data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updatevalueobsolescence',
            data: JSON.stringify(data),  //filters
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

    this.updateProductPrice = function (data, successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: 'POST',
            dataType: "json",
            data: JSON.stringify(data),
            url: this.serviceUrl + 'updateProductPrice',
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

    this.updateEnableStatus = function (data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            url: this.serviceUrl + 'update-enable-status',
            data: JSON.stringify(data),
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

}]);