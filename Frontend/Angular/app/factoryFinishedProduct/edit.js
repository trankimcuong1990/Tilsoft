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
    $scope.factoryTeams = null;
    $scope.factorySteps = null;
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factoryTeams = data.data.factoryTeams;
                    $scope.factorySteps = data.data.factorySteps;
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
                            $scope.method.refresh(data.data.data.factoryFinishedProductID);
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

        //factory finished product price
        addComponentPrice: function ($event) {
            $event.preventDefault();
            $scope.data.factoryFinishedProductPriceDTOs.push({
                factoryFinishedProductPriceID: $scope.method.getNewID(),
            });
        },

        createComponentPrice: function ($event, factoryFinishedProductID, factoryFinishedProductPriceItem) {
            $event.preventDefault();
            jsonService.createfactoryfinishedproductprice(factoryFinishedProductID, factoryFinishedProductPriceItem
                , function (data) {
                    factoryFinishedProductPriceItem.factoryFinishedProductPriceID = data.data;
                    jsHelper.processMessage(data.message);
                }
                , function (error) {
                    jsHelper.showMessage('warning', error);
                }
                )
        },

        deleteCoponentPrice: function ($event, factoryFinishedProductPriceID) {
            $event.preventDefault();
            if (factoryFinishedProductPriceID > 0) {
                jsonService.deleteFactoryFinishedProductPrice(factoryFinishedProductPriceID
                , function (data) {
                    var j = -1;
                    for (var i = 0; i < $scope.data.factoryFinishedProductPriceDTOs.length; i++) {
                        if ($scope.data.factoryFinishedProductPriceDTOs[i].factoryFinishedProductPriceID == factoryFinishedProductPriceID) {
                            j = i;
                            break;
                        }
                    }
                    if (j >= 0) {
                        $scope.data.factoryFinishedProductPriceDTOs.splice(j, 1);
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
                for (var i = 0; i < $scope.data.factoryFinishedProductPriceDTOs.length; i++) {
                    if ($scope.data.factoryFinishedProductPriceDTOs[i].factoryFinishedProductPriceID == factoryFinishedProductPriceID) {
                        j = i;
                        break;
                    }
                }
                if (j >= 0) {
                    $scope.data.factoryFinishedProductPriceDTOs.splice(j, 1);
                }
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