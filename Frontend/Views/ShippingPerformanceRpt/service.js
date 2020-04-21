(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
        angular.extend(this, jsonService);

        //
        // extend base function here
        //
        this.searchFilter.sortedBy = 'FactoryID';
        this.searchFilter.sortedDirection = 'ASC';

        this.exportExcel = function (filters, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'export-excel',
                data: JSON.stringify(filters),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type == 0) {
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

})();