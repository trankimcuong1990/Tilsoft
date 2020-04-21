angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
    angular.extend(this, jsonService);

    this.getHtmlReport = function (season, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'gethtmlreport?season=' + season,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
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
    this.getSeason = function (successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: 'POST',
            url: this.supportServiceUrl + 'getseasons',
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
}]);