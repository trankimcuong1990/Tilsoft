//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.newID = -1;
    $scope.data = null;
    $scope.factorySteps = null;
    $scope.factoryMaterialGroups = null;

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factorySteps = data.data.factorySteps;
                    $scope.factoryMaterialGroups = data.data.factoryMaterialGroups;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.data.factoryTeamID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },

        //factory team step
        addTeamStep: function ($event) {
            $event.preventDefault();
            $scope.data.factoryTeamStepDTOs.push({
                factoryTeamStepID: $scope.method.getNewID(),
            });
        },

        createTeamStep: function ($event, factoryTeamID, factoryTeamStepItem) {
            $event.preventDefault();
            jsonService.createFactoryTeamStep(factoryTeamID, factoryTeamStepItem
                , function (data) {
                    factoryTeamStepItem.factoryTeamStepID = data.data;
                    jsHelper.processMessage(data.message);
                }
                , function (error) {
                    jsHelper.showMessage('warning', error);
                }
                )
        },

        deleteTeamStep: function ($event, factoryTeamStepID) {
            $event.preventDefault();

            if (factoryTeamStepID > 0) {
                jsonService.deleteTeamStep(factoryTeamStepID
                , function (data) {
                    var j = -1;
                    for (var i = 0; i < $scope.data.factoryTeamStepDTOs.length; i++) {
                        if ($scope.data.factoryTeamStepDTOs[i].factoryTeamStepID == factoryTeamStepID) {
                            j = i;
                            break;
                        }
                    }
                    if (j >= 0) {
                        $scope.data.factoryTeamStepDTOs.splice(j, 1);
                    }
                    $scope.$apply();
                }
                , function (error) {
                    jsHelper.showMessage('warning', error);
                }
                )
            }
            else {
                var j = -1;
                for (var i = 0; i < $scope.data.factoryTeamStepDTOs.length; i++) {
                    if ($scope.data.factoryTeamStepDTOs[i].factoryTeamStepID == factoryTeamStepID) {
                        j = i;
                        break;
                    }
                }
                if (j >= 0) {
                    $scope.data.factoryTeamStepDTOs.splice(j, 1);
                }
            }
        },

        //factory material group team
        addMaterialGroupTeam: function ($event) {
            $event.preventDefault();
            $scope.data.factoryMaterialGroupTeamDTOs.push({
                factoryMaterialGroupTeamID: $scope.method.getNewID(),
            });
        },

        createMaterialGroupTeam: function ($event, factoryTeamID, factoryMaterialGroupTeamItem) {
            $event.preventDefault();
            jsonService.createMaterialGroupTeam(factoryTeamID, factoryMaterialGroupTeamItem
                , function (data) {
                    factoryMaterialGroupTeamItem.factoryMaterialGroupTeamID = data.data;
                    jsHelper.processMessage(data.message);
                }
                , function (error) {
                    jsHelper.showMessage('warning', error);
                }
                )
        },

        deleteMaterialGroupTeam: function ($event, factoryMaterialGroupTeamID) {
            $event.preventDefault();

            if (factoryMaterialGroupTeamID > 0) {
                jsonService.deleteMaterialGroupTeam(factoryMaterialGroupTeamID
                , function (data) {
                    var j = -1;
                    for (var i = 0; i < $scope.data.factoryMaterialGroupTeamDTOs.length; i++) {
                        if ($scope.data.factoryMaterialGroupTeamDTOs[i].factoryMaterialGroupTeamID == factoryMaterialGroupTeamID) {
                            j = i;
                            break;
                        }
                    }
                    if (j >= 0) {
                        $scope.data.factoryMaterialGroupTeamDTOs.splice(j, 1);
                    }
                    $scope.$apply();
                }
                , function (error) {
                    jsHelper.showMessage('warning', error);
                }
                )
            }
            else {
                var j = -1;
                for (var i = 0; i < $scope.data.factoryMaterialGroupTeamDTOs.length; i++) {
                    if ($scope.data.factoryMaterialGroupTeamDTOs[i].factoryMaterialGroupTeamID == factoryMaterialGroupTeamID) {
                        j = i;
                        break;
                    }
                }
                if (j >= 0) {
                    $scope.data.factoryMaterialGroupTeamDTOs.splice(j, 1);
                }
                $scope.$apply();
            }
        },


    },

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    },
    
    //
    // init
    //
    $scope.event.load();
}]);