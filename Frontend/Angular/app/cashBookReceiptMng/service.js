(function () {
    'use strict'

    angular
        .module('tilsoftApp')
        .service('dataService', cashBookReceiptService);
    cashBookReceiptService.$inject = ['$http', 'jsonService'];

    function cashBookReceiptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'BookDate';
        self.searchFilter.sortedDirection = 'ASC';

        self.updatePostCost = updatePostCost;
        self.deletePostCost = deletePostCost;
        self.updateCostItem = updateCostItem;
        self.deleteCostItem = deleteCostItem;
        self.updateCostItemDetail = updateCostItemDetail;
        self.deleteCostItemDetail = deleteCostItemDetail;
        self.exportCashBook = exportCashBook;

        function updatePostCost(data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: self.serviceUrl + 'updatepostcost',
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
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

        function deletePostCost(id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';

            if (title !== null) {
                confirmedMsg = 'Delete ' + context.title + ': ' + title + ' ?';
            }

            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deletepostcost?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallBack(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type === 0) {
                        iSuccessCallback(response.data);
                    }
                    else {
                        jsHelper.showMessage('warning', response.data.message.message);
                        console.log(response.data.message.message);
                        iErrorCallback(response);
                    }
                }, function errorCallBack(response) {
                    jsHelper.loadingSwitch(false);
                    jsHelper.showMessage('warning', response.data.message);
                    iErrorCallback(response);
                });
            }
        };

        function updateCostItem(data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: self.serviceUrl + 'updatecostitem',
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
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

        function deleteCostItem(id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';

            if (title !== null) {
                confirmedMsg = 'Delete ' + context.title + ': ' + title + ' ?';
            }

            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deletecostitem?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallBack(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type === 0) {
                        iSuccessCallback(response.data);
                    }
                    else {
                        jsHelper.showMessage('warning', response.data.message.message);
                        console.log(response.data.message.message);
                        iErrorCallback(response);
                    }
                }, function errorCallBack(response) {
                    jsHelper.loadingSwitch(false);
                    jsHelper.showMessage('warning', response.data.message);
                    iErrorCallback(response);
                });
            }
        };

        function updateCostItemDetail(data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: self.serviceUrl + 'updatecostitemdetail',
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
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

        function deleteCostItemDetail(id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';

            if (title !== null) {
                confirmedMsg = 'Delete ' + context.title + ': ' + title + ' ?';
            }

            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deletecostitemdetail?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallBack(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type === 0) {
                        iSuccessCallback(response.data);
                    }
                    else {
                        jsHelper.showMessage('warning', response.data.message.message);
                        console.log(response.data.message.message);
                        iErrorCallback(response);
                    }
                }, function errorCallBack(response) {
                    jsHelper.loadingSwitch(false);
                    jsHelper.showMessage('warning', response.data.message);
                    iErrorCallback(response);
                });
            }
        };

        function exportCashBook(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                },
                type: 'POST',
                url: self.serviceUrl + 'exportcashbook',
                data: JSON.stringify(filters),
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
    }
})();