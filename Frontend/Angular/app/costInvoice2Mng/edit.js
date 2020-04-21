function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', costInvoice2Controller);
    costInvoice2Controller.$inject = ['$scope', 'dataService'];

    function costInvoice2Controller($scope, costInvoice2Service) {
        $scope.data = [];
        $scope.currencies = [];
        $scope.seasons = [];
        $scope.costInvoice2Creditors = [];
        $scope.costInvoice2Types = [];

        $scope.totalRecal = 0;

        // paid is.
        $scope.noPaid = null;
        $scope.listPaid = [
            { id: false, name: 'PENDING' },
            { id: true, name: 'PAID' }
        ];

        // auto-complete support client.
        $scope.ngInitClient = null;
        $scope.ngItemClient = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestClient = {
            url: context.supportServiceUrl + 'getClient2',
            token: context.token,
        };
        $scope.ngSelectedClient = {
            id: null,
            label: null,
            description: null
        };

        // auto-complete support factory.
        $scope.ngInitFactory = null;
        $scope.ngItemFactory = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestFactory = {
            url: context.supportServiceUrl + 'getFactory2',
            token: context.token,
        };
        $scope.ngSelectedFactory = {
            id: null,
            label: null,
            description: null
        };

        $scope.event = {
            calculateAmount: calculateAmount,
            init: init,
            load: load,

            //file
            changeFile: changeFile,
            removeFile: removeFile,
            downloadFile: downloadFile,

            remove: remove,
            update: update,
            addForClient: addForClient,
            removeForClient: removeForClient,
            addForFactory: addForFactory,
            removeForFactory: removeForFactory,
            approve: approve,
        }

        //AmountClient and Amount Factory
        function calculateAmount(isChargedToClient, isChargedToFactory) {
            var amount = 0.00;
            $scope.data.totalAmount = 0.00;
            if ($scope.data.totalAmount !== undefined && $scope.data.totalAmount !== null && $scope.data.totalAmount !== '') {
                amount = $scope.data.totalAmount;
            }
            if (isChargedToClient) {
                angular.forEach($scope.data.costInvoice2Clients, function (value, key) {

                    if (value.amountClient !== null && value.amountClient !== '') {
                        if (amount === null) {
                            amount = 0.00;
                        }
                        amount = amount + parseInt(value.amountClient);
                    }
                    else {
                        amount = amount + 0.00;
                    }
                });
            }
            if (isChargedToFactory) {
                angular.forEach($scope.data.costInvoice2Factories, function (value, key) {
                    if (value.amountFactory !== null && value.amountFactory !== '') {
                        if (amount === null) {
                            amount = 0.00;
                        }
                        amount = amount + parseInt(value.amountFactory);
                    }
                    else {
                        amount = amount + 0.00;
                    }
                });
            }
            $scope.data.totalAmount = amount;
        }

        function init() {
            costInvoice2Service.serviceUrl = context.serviceUrl;
            costInvoice2Service.token = context.token;

            $scope.event.load();
        }

        //file functions   
        function changeFile() {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.file_NewFile = fileUploader2.selectedFileName;
                    $scope.data.file_HasChange = true;
                });
            };
            fileUploader2.open();
        }

        function removeFile() {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.file_NewFile = '';
            $scope.data.file_HasChange = true;
        }

        function downloadFile() {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        }

        function load() {
            costInvoice2Service.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.costInvoice2;
                    $scope.noPaid = $scope.data.isPaid;

                    // support module.
                    $scope.currencies = data.data.currencies;
                    $scope.seasons = data.data.seasons;
                    $scope.costInvoice2Creditors = data.data.costInvoice2Creditors;
                    $scope.costInvoice2Types = data.data.costInvoice2Types;

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function remove() {
            costInvoice2Service.delete(
                context.id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }

        function update() {
            if ($scope.data.isChargedToClient && $scope.data.costInvoice2Clients.length === 0) {
                jsHelper.showMessage('warning', 'Please list Client not empty!');
                return false;
            }

            if ($scope.data.isChargedToFactory && $scope.data.costInvoice2Factories.length === 0) {
                jsHelper.showMessage('warning', 'Please list Factory not empty!');
                return false;
            }

            if ($scope.editForm.$valid) {
                $scope.data.isPaid = $scope.noPaid;

                costInvoice2Service.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.costInvoice2.costInvoice2ID;
                            $scope.data = data.data.costInvoice2;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            } else {
                jsHelper.showMessage("warning", context.msgValid);
            }
        }

        function addForClient() {
            if ($scope.ngItemClient !== null) {
                angular.forEach($scope.data.costInvoice2Clients, function (value, key) {
                    if (value.clientID === $scope.ngItemClient.id) {
                        $scope.ngInitClient = null;
                        $scope.ngItemClient = null;

                        return false;
                    }
                })

                var costInvoice2Client = {
                    costInvoice2ClientID: -1,
                    clientID: $scope.ngItemClient.id,
                    clientUD: $scope.ngItemClient.label,
                    amountClient: null
                }

                $scope.data.costInvoice2Clients.push(costInvoice2Client);

                $scope.ngInitClient = null;
                $scope.ngItemClient = null;
            }
        }

        function addForFactory() {
            if ($scope.ngItemFactory !== null) {
                angular.forEach($scope.data.costInvoice2Factories, function (value, key) {
                    if (value.factoryID === $scope.ngItemFactory.id) {
                        $scope.ngInitFactory = null;
                        $scope.ngItemFactory = null;

                        return false;
                    }
                })

                var costInvoice2Factory = {
                    costInvoice2FactoryID: -1,
                    factoryID: $scope.ngItemFactory.id,
                    factoryUD: $scope.ngItemFactory.label,
                    amountFactory: null
                }

                $scope.data.costInvoice2Factories.push(costInvoice2Factory);

                $scope.ngInitFactory = null;
                $scope.ngItemFactory = null;
            }
        }

        function removeForClient(item, isChargedToClient, isChargedToFactory) {
            var index = $scope.data.costInvoice2Clients.indexOf(item);
            $scope.data.costInvoice2Clients.splice(index, 1);
            $scope.event.calculateAmount(isChargedToClient, isChargedToFactory);
        }

        function removeForFactory(item, isChargedToClient, isChargedToFactory) {
            var index = $scope.data.costInvoice2Factories.indexOf(item);
            $scope.data.costInvoice2Factories.splice(index, 1);
            $scope.event.calculateAmount(isChargedToClient, isChargedToFactory);
        }

        function approve() {
            if ($scope.editForm.$valid) {
                costInvoice2Service.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.costInvoice2ID;
                            $scope.data = data.data.costInvoice2;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            } else {
                jsHelper.showMessage("warning", context.msgValid);
            }
        }

        $scope.event.init();
    };
})();