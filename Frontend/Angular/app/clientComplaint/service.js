(function () {
    'use strict';

    angular
        .module('tilsoftApp')
        .service('dataService', ['$http', 'jsonService', ClientComplaintService]);

    function ClientComplaintService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        //#region public members
        self.searchFilter.sortedBy = 'UpdatedDate';
        self.searchFilter.sortedDirection = 'DESC';

        // overrided load method
        self.load = load;

        self.quickSearchFactoryOrderDetails = quickSearchFactoryOrderDetails;

        self.quickSearchPIs = quickSearchPIs;

        self.quickSearchClient = quickSearchClient;
        
        self.getShipmentStatusOfPISolution = getShipmentStatusOfPISolution;

        self.exportExcelClientComplaint = exportExcelClientComplaint;

        self.exportExcelClientComplaintItem = exportExcelClientComplaintItem;

        //#endregion public members

        //#region private methods
        function exportExcelClientComplaint(successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                url: self.serviceUrl + 'exportexcelclientcomplaint',
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

        function exportExcelClientComplaintItem(clientComplaintItemID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                url: self.serviceUrl + 'exportexcelclientcomplaintitem',
                data: JSON.stringify({
                    filters: {
                        clientComplaintItemID: clientComplaintItemID
                    }
                }),
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

        

        function getShipmentStatusOfPISolution(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                },
                type: "POST",
                url: self.serviceUrl + 'getshipmentstatusofpisolution',
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
        }

        function quickSearchFactoryOrderDetails(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                },
                type: "POST",
                url: self.serviceUrl + 'quicksearchfactoryorderdetails',
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
        }

        function quickSearchPIs(filters, successCallBack, errorCallBack) {
            
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
                url: self.serviceUrl + 'quicksearchpis',
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

        function quickSearchClient(filters, successCallBack, errorCallBack) {
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
                url: self.supportServiceUrl + 'quicksearchclient',
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

        function load(id, clientId, season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                },
                type: "POST",
                url: self.serviceUrl + 'get?id=' + id + '&clientId=' + clientId + '&season=' + season,
                beforeSend: function() {
                    jsHelper.loadingSwitch(true);
                },
                success: function(data) {
                    jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        }
        //#endregion private methods
    }
})();
