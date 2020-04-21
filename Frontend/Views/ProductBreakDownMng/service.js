(
    function () {
        'use strict';

        angular.module('tilsoftApp').service('dataService', productBreakDownService);
        productBreakDownService.$inject = ['$http', 'jsonService'];

        function productBreakDownService($http, jsonService) {
            angular.extend(this, jsonService);

            var self = this;
            self.searchFilter.sortedBy = 'ProductBreakDownID';
            self.searchFilter.sortedDirection = 'ASC';

            self.getDataWithModel = function (id, modelID, sampleProductID, iSuccessCallback, iErrorCallback) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'get?id=' + id + '&modelID=' + modelID + '&sampleProductID=' + sampleProductID,
                    data: JSON.stringify(this.searchFilter),
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
            }

            self.fillProductBreakDownCategories = function (iSuccessCallback, iErrorCallback) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'getdefaultcategories',
                    data: JSON.stringify(this.searchFilter),
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
            }

            self.approveProductBreakDown = function (id, iSuccessCallback, iErrorCallback) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'approveproductbreakdown?id=' + id,
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
            }

            self.fillProductBreakDownCategoriesAmount = function (iSuccessCallback, iErrorCallback) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'getdefaultcategoriesamount',
                    data: JSON.stringify(this.searchFilter),
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
            }

            self.fillProductBreakDownCategoriesPercent = function (iSuccessCallback, iErrorCallback) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'getdefaultcategoriespercent',
                    data: JSON.stringify(this.searchFilter),
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
            }

            self.autofillProductBreakDownCategory = function (typeCategoryID, iSuccessCallback, iErrorCallback) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'autofillProductBreakDownCategory?typeCategoryID=' + typeCategoryID,
                    data: JSON.stringify(this.searchFilter),
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
            }
        }
    }
)();