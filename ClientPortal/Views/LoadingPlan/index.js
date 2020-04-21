angular.module('cpApp').controller('cpController', ['$scope', '$rootScope', '$cookies', '$timeout', 'dataService', function ($scope, $rootScope, $cookies, $timeout, dataService) {
    dataService.serviceUrl = dataService.serviceUrl + 'api/cp-loadingplan/';
    dataService.searchFilter.sortedBy = 'ETA';
    dataService.searchFilter.sortedDirection = 'DESC';

    $scope.data = {
        support: {
            containerTypes: []
        },
        filters: {
            BLNo: '',
            ETDFrom: '',
            ETDTo: '',
            ETAFrom: '',
            ETATo: '',
            LoadingPlanUD: '',
            ContainerTypeID: '',
            ContainerNo: '',
            SealNo: '',
            ProformaInvoiceNo: '',
            ClientOrderNumber: ''
        },
        data: {
            loadingPlans: [],
            orders: []
        }
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getInit(
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data.support.containerTypes = data.data.containerTypeDTOs;
                        $rootScope.$emit('frmResult');
                    }
                    else {
                        console.log(data.message);
                    }
                },
                function (error) {
                    $scope.data.support.containerTypes = [];
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {};

    //
    // init
    //
    $timeout(function () {
        $scope.event.init();
    }, 0);
}]);