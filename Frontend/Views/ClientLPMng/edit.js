function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label,
            internalCompanyNM: item.internalCompanyNM,
            email1: item.email1,
            email2: item.email2
        }
    });
}
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', clientLPMngController);
    clientLPMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function clientLPMngController($scope, clientLPMngService, $timeout) {
        // variable
        $scope.data = [];
        $scope.employees = [];
        $scope.lpManagingTeams = [];

        $scope.listAutoUpdateSimilarLP = [
            { isAutoUpdateSimilarLP: true, name: 'Yes' },
            { isAutoUpdateSimilarLP: false, name: 'No' }
          
        ];

        // variables for event quick-search
        $scope.ngInitClientLP = null;
        $scope.ngItemClientLP = {
            id: null,
            label: null,
            description: null,
            email1: null,
            email2: null,
            internalCompanyNM: null,
        };
        $scope.ngRequestClientLP = {
            url: context.serviceUrl + 'getEmployee',
            token: context.token,
        };
        $scope.ngSelectedClientLP = {
            id: null,
            label: null,
            description: null
        };

        // event
        $scope.event = {
            get: get,
            update: update,
            deleted: deleted,
            remove: remove,
            addEmailUser: addEmailUser,
            showInforOfEmployee: showInforOfEmployee,
            hideInforOfEmployee: hideInforOfEmployee
        };     

        function get() {
            clientLPMngService.serviceUrl = context.serviceUrl;
            clientLPMngService.token = context.token;

            clientLPMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.employees = data.data.supportEmployeeDTOs;
                    $scope.lpManagingTeams = data.data.supportLPManagingDTOs;
                  
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                clientLPMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.clientLPID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function deleted(id) {
            clientLPMngService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {

                });
        };

        function remove($event, item) {
            //$event.preventDefault();
            var confirmedMsg = 'Delete ' + item.employeeUD + ' ?'
            if (confirm(confirmedMsg) === true) {
                var index = $scope.data.clientLPNotificationDTOs.indexOf(item);
                $scope.data.clientLPNotificationDTOs.splice(index, 1);
                //$scope.totalRowsScan = $scope.data.clientLPNotificationDTOs.length;
            }

        };

        function addEmailUser() {
            $scope.data.clientLPNotificationDTOs.push({
                clientLPNotificationID: clientLPMngService.getIncrementId(),
                userID: -1,
                employeeUD: '',
                email1: '',
                email2: '',
                internalCompanyNM: '',
            });
        };

        function showInforOfEmployee(ngItemClientLP, item) {
            $timeout(function () {
                item.userID = ngItemClientLP.id;
                item.email1 = ngItemClientLP.email1;
                item.email2 = ngItemClientLP.email2;
                item.internalCompanyNM = ngItemClientLP.internalCompanyNM;
            }, 0);
            
            //$scope.apply();
        };

        function hideInforOfEmployee(item) {           
            $timeout(function () {
                if (item.employeeNM === '') {
                    item.userID = -1;
                    item.email1 = '';
                    item.email2 = '';
                    item.internalCompanyNM = '';
                }
            }, 0);
        }
        
        // default event
        $scope.event.get();
    }
})();