( function () {
    "use strict";

    angular.module( "tilsoftApp" ).service( "dataService", showroomService );
    showroomService.$inject = ["$http", "jsonService"];

    function showroomService( $http, jsonService ) {
        angular.extend( this, jsonService );

        var self = this;
        self.searchFilter.sortedBy = "ProductionItemID";
        self.searchFilter.sortedDirection = "ASC";
        self.exportExcel = exportExcel;
        self.exportExcelWithoutImage = exportExcelWithoutImage;


        function exportExcel(factorywarehouseID, filters, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'exportexcel?factorywarehouseID=' + factorywarehouseID,
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

        function exportExcelWithoutImage(factorywarehouseID, filters, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'exportexcelwithoutimage?factorywarehouseID=' + factorywarehouseID,
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
        }

    };
} )();