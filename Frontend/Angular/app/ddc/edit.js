//
// SUPPORT
//
jQuery('#main-form').keypress(function(e){
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.grid', 'ui.grid.resizeColumns', 'ui.grid.pinning', 'ui.grid.edit', 'ui.grid.cellNav']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;

    //
    // grid config
    //    
    $scope.grdDDC = {
        enableSorting: false,
        enableColumnResizing: true,
        columnDefs: [
            { name: 'Client Code', field: 'clientUD', width: 100, enableHiding: false, enableCellEdit: false },
            { name: 'Client Name', field: 'clientNM', width: 100, enableHiding: false, enableCellEdit: false },
            { name: 'Min USD', field: 'minUSD', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Avg USD', field: 'avgUSD', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Max USD', field: 'maxUSD', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Min EUR', field: 'minEUR', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Avg EUR', field: 'avgEUR', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Max EUR', field: 'maxEUR', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Wicker Cont.Qnt', field: 'wickerContQty', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Wicker Promo Cont.Qnt', field: 'wickerPromoContQty', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Wood Acacia Cont.Qnt', field: 'woodAcaciaContQty', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Wood Teak Cont.Qnt', field: 'woodTeakContQty', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'China Cont.Qnt', field: 'chinaContQty', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Indo Cont.Qnt', field: 'indoContQty', width: 100, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Remark', field: 'remark', width: 100, enableHiding: false, enableCellEditOnFocus: true }
        ],
        data: null
    };

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                context.season,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.grdDDC.data = $scope.data.details;
                    $scope.$apply();
                    jQuery('#content').show();

                    console.log($scope.data);

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.grdDDC.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function($event){
            $event.preventDefault();

            if($scope.editForm.$valid)
            {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            $scope.method.refresh(data.data.ddcid);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function($event){
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id+'?s=';
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);