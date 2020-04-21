(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', clientPermissionController);
    clientPermissionController.$inject = ['$scope', 'dataService'];

    function clientPermissionController($scope, clientPermissionController) {
        $scope.data = [];      
        $scope.employees = [];
        $scope.newID = 0;
        $scope.event =
            {
                activepage: activepage,
                updatedata: updatedata,
                deletedata: deletedata,
                removeLine: removeLine,
                addNewLineManual: addNewLineManual,
                getNewID: getNewID,
                assignAutoCompleteUserItem: assignAutoCompleteUserItem
            };

        $scope.event.activepage();

        function activepage() {
            createservices();           
            clientPermissionController.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.employees = data.data.employees;

                    console.log($scope.data);
                    console.log($scope.employees);
                    jQuery("#content").show();
                    $('#clientUD').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.supportoken
                                },
                                type: 'POST',
                                data: JSON.stringify({ filters: { searchQuery: request.term } }),
                                dataType: 'json',
                                url: context.supportUrl + 'getClient',
                                beforeSend: function () {
                                },
                                success: function (data) {
                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.data.clientUD = ui.item.label;
                            $scope.data.clientID = ui.item.clientID;
                            $scope.data.clientNM = ui.item.clientNM;
                            $scope.$apply();
                        }
                    });                   
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function updatedata($event) {
            
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                clientPermissionController.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.clientPermissionID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            } else {
                jsHelper.showMessage("warning", context.msgValid);
            }
        };

        function deletedata(id) {
            clientPermissionController.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };
        function removeLine (index) {
            $scope.data.clientPermissionDetailDTOs.splice(index, 1);
        };
        function addNewLineManual () {
            $scope.data.clientPermissionDetailDTOs.push({
                clientPermissionDetailID: $scope.event.getNewID()
            });
            $scope.event.assignAutoCompleteUserItem(); // add
        };

        function assignAutoCompleteUserItem() {
            angular.forEach($scope.data.clientPermissionDetailDTOs, function (item) {

                $('#clientPermissionDetailID' + item.clientPermissionDetailID).autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            cache: false,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + context.token
                            },
                            type: 'POST',
                            data: JSON.stringify({
                                filters: {
                                    searchQuery: request.term,                                   
                                },
                                sortedBy: 'employeeUD',
                                sortedDirection: 'ASC',
                                pageSize: 100,
                                pageIndex:1,

                            }),
                            dataType: 'json',
                            url: context.serviceUrl + 'QuickSearchEmployee',
                            beforeSend: function () {
                            },
                            success: function (data) {
                                response(data.data);
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                errorCallBack(xhr.responseJSON.exceptionMessage);
                            }
                        });
                    },
                    minLength: 3,
                    select: function (event,ui) {
                        item.employeeNM = ui.item.employeeNM;
                        item.userID = ui.item.id;
                    }
                });
                $('#moduleID' + item.clientPermissionDetailID).autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            cache: false,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + context.token
                            },
                            type: 'POST',
                            data: JSON.stringify({
                                filters: {
                                    searchQuery: request.term
                                },
                                sortedBy: 'ModuleUD',
                                sortedDirection: 'ASC',
                                pageSize: 100,
                                pageIndex: 1,

                            }),
                            dataType: 'json',
                            url: context.serviceUrl + 'QuickSearchModule',
                            beforeSend: function () {
                            },
                            success: function (data) {
                                response(data.data);
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                errorCallBack(xhr.responseJSON.exceptionMessage);
                            }
                        });
                    },
                    minLength: 3,
                    select: function (event,ui) {
                        item.displayText = ui.item.label;
                        item.moduleID = ui.item.id;
                        $scope.$apply();
                    }
                });
            })
        };
        function getNewID() {
            $scope.newID--;
            return $scope.newID;
        };
        function createservices() {
            clientPermissionController.serviceUrl = context.serviceUrl;
            clientPermissionController.token = context.token;           
        };
       
    };
})();
