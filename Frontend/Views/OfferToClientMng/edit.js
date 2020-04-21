//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive', 'ngRoute', 'ngCookies', 'customFilters', 'ui'/*'customFilters', 'furnindo-directive', 'avs-directives', 'ui', 'ngRoute', 'ngCookies'*/]);
tilsoftApp.config(function ($routeProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/OfferToClientMng/_General?' + jsHelper.getUniqueString(),
            controller: '_GeneralController'
        });
    $routeProvider.when('/general',
        {
            templateUrl: '/OfferToClientMng/_General?' + jsHelper.getUniqueString(),
            controller: '_GeneralController'
        });
    $routeProvider.when('/offer-season-detail/0',
        {
            templateUrl: '/OfferToClientMng/_OfferSeasonDetailView?' + jsHelper.getUniqueString(),
            controller: '_OfferSeasonDetailController'
        });
    $routeProvider.when('/offer-season-detail/1',
        {
            templateUrl: '/OfferToClientMng/_OfferSeasonDetailView?' + jsHelper.getUniqueString(),
            controller: '_OfferSeasonDetailController'
        });
    $routeProvider.when('/offer-season-detail/2',
        {
            templateUrl: '/OfferToClientMng/_OfferSeasonDetailView?' + jsHelper.getUniqueString(),
            controller: '_OfferSeasonDetailController'
        });
});

tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.printOptionValue = 1;
    $scope.printOptionImageValue = 1;
    $scope.offerDataContainer = null;

    $scope.getExchangeRate = function () {
        if ($scope.offerDataContainer && $scope.offerDataContainer.offerDTO && $scope.offerDataContainer.offerDTO.season) {
            if ($scope.offerDataContainer.exchangeRateDTOs.filter(o => o.season === $scope.offerDataContainer.offerDTO.season).length > 0) {
                return $scope.offerDataContainer.exchangeRateDTOs.filter(o => o.season === $scope.offerDataContainer.offerDTO.season)[0].exRate;
            }
        }
        return null;
    };

    $scope.update = function ($event) {
        $event.preventDefault();
        //validate quantity
        var hasError = false;
        var messageError = '';
        if ($scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.length > 0) {
            angular.forEach($scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs, function (item) {
                if (item.offerSeasonDetailQuantity != null && item.quantity > item.offerSeasonDetailQuantity) {     
                    messageError = 'Sample product quantity in OTC can not be greater than OfferSeason'
                    hasError = true;
                }
            });
        }
        //if ($scope.offerDataContainer.offerDTO.offerLineDTOs.length > 0) {
        //    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
        //        if (item.offerSeasonDetailCommissionPercent != null && item.commissionPercent > item.offerSeasonDetailCommissionPercent) {
        //            messageError = 'Product commission percent in OTC can not be greater than OfferSeason'
        //            hasError = true;
        //        }
        //        if (item.offerSeasonDetailQuantity != null && item.quantity > item.offerSeasonDetailQuantity) {
        //            messageError = 'Product quantity in OTC can not be greater than OfferSeason'
        //            hasError = true;
        //        }
        //    });
        //}
        //if ($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.length > 0) {
        //    angular.forEach($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs, function (item) {
        //        if (item.offerSeasonDetailCommissionPercent != null && item.commissionPercent > item.offerSeasonDetailCommissionPercent) {
        //            messageError = 'Sparepart commission percent in OTC can not be greater than OfferSeason'
        //            hasError = true;
        //        }
        //        if (item.offerSeasonDetailQuantity != null && item.quantity > item.offerSeasonDetailQuantity) {
        //            messageError = 'Sparepart quantity in OTC can not be greater than OfferSeason'
        //            hasError = true;
        //        }
        //    });
        //}

        if (hasError) {
            jsHelper.showMessage('warning', messageError);
            return;
        }     

        if ($scope.editForm.$valid) {           
            dataService.update(context.id, 
                $scope.offerDataContainer.offerDTO,
                function (data) {

                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        context.id = data.data.offerID;
                        console.log(data.data);                       
                        window.location = context.refreshUrl + context.id + '?clientID=' + context.clientID + '&season=' + context.season + '&currency=' + context.currency;
                    }
                },
                function (error) {

                    jsHelper.showMessage('warning', error);
                }
            );
        }
        else {
            jsHelper.showMessage('warning', '@Frontend.Properties.Resources.INPUT_VALIDATION_FAILED');
        }
    };   

    $scope.updateClientInfo = function ($event) {     
        if ($scope.editForm.$valid) {
            dataService.updateClientInfo(context.id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {                      
                        console.log(data.data);
                        window.location = context.refreshUrl + context.id + '?clientID=' + context.clientID + '&season=' + context.season + '&currency=' + context.currency;
                    }
                },
                function (error) {

                    jsHelper.showMessage('warning', error);
                }
            );
        }
        else {
            jsHelper.showMessage('warning', '@Frontend.Properties.Resources.INPUT_VALIDATION_FAILED');
        }
    };   
    //
    // event
    //
    $scope.event = {
        load: function () {
            if (context.id > 0) {
                dataService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.offerDataContainer = data.data;
                        
                        //  $scope.$apply();

                        $('#content').show();
                        $timeout(function () {
                            jsHelper.refreshAvsScroll();
                        }, 100);
                        // monitor changes
                        $scope.$watch('offer', function () {
                            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                        });
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);

                        $scope.offerDataContainer = null;
                        // $scope.$apply();
                    }
                );
            }
            else {
                dataService.getOfferSeason(
                    context.clientID,
                    context.season,
                    function (data) {
                        $scope.offerDataContainer = data.data;                      
                        $scope.offerDataContainer.offerDTO.currency = context.currency;                    
                        //  $scope.$apply();

                        $('#content').show();
                        //$timeout(function () {
                        //    jsHelper.refreshAvsScroll();
                        //}, 100);
                        //// monitor changes
                        $scope.$watch('offer', function () {
                            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                        });
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);

                        $scope.offerDataContainer = null;
                    }
                );
            }

        },
    }

    $scope.refresh = function () {
        window.location = context.refreshUrl + context.id + '?clientID=' + context.clientID + '&season=' + context.season + '&currency=' + context.currency;
    };
   
    $scope.copy = function ($event) {
        $event.preventDefault();
       
        angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
            item.offerID = 0;
            item.offerLineID = -1;
        });
        angular.forEach($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs, function (item) {
            item.offerID = 0;
            item.offerLineSparePartID = -1;
        });

        if ($scope.editForm.$valid) {
            dataService.update(0,
                $scope.offerDataContainer.offerDTO,
                function (data) {

                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        console.log(data.data);
                        window.location = context.refreshUrl + data.data.offerID + '?clientID=' + context.clientID + '&season=' + context.season + '&currency=' + context.currency;
                    }
                },
                function (error) {

                    jsHelper.showMessage('warning', error);
                }
            );
        }
        else {
            jsHelper.showMessage('warning', '@Frontend.Properties.Resources.INPUT_VALIDATION_FAILED');
        }
    };

    $scope.delete = function ($event) {
        $event.preventDefault();

        dataService.delete(
            context.id,
            null,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type == 0) {
                    window.location = context.backUrl;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error.data.message.message);
            }
        );
    };

    $scope.showPrintOptionForm = function () {
        $scope.printOptions = [];
        $scope.printOptions.push({
            printOptionValue: 1,
            printOptionText: "Print with small image"
        });
        $scope.printOptions.push({
            printOptionValue: 2,
            printOptionText: "Print with small image & send email"
        });
        $scope.printOptions.push({
            printOptionValue: 3,
            printOptionText: "Print with large image"
        });
        $scope.printOptions.push({
            printOptionValue: 4,
            printOptionText: "Print with large image & send email"
        });
        $scope.printOptions.push({
            printOptionValue: 5,
            printOptionText: "Print as Powerpoint 2013"
        });

        $scope.printOptionsImage = [];
        $scope.printOptionsImage.push({
            printOptionImageValue: 1,
            printOptionImageText: "Pre-Loading"
        });
        $scope.printOptionsImage.push({
            printOptionImageValue: 2,
            printOptionImageText: "Model"
        });

        $('#frmPrintOption').modal('show');
    };
    $scope.printSelectedOffer = function () {
        var x = $scope.printOptionValue;
        var y = $scope.printOptionImageValue;
        switch (x) {
            case 1:
                $scope.printOffer5(y);
                break;
            case 2:
                $scope.printOffer5AndSendEmail(y);
                break;
            case 3:
                $scope.printOffer5_FullSizeImage(y);
                break;
            case 4:
                $scope.printOffer5AndSendEmail_FullSizeImage(y);
                break;
            case 5:
                $scope.printOffer5PP2013(y);
                break;
        }
    };

    $scope.printOffer5 = function (imageOption) {
        dataService.getPrintDataOffer5(context.id, false, false, imageOption,
            function (data) {
                if (data.message.type != 2) {
                    window.location = context.serviceReport + data.data;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printOffer5AndSendEmail = function (imageOption) {
        dataService.getPrintDataOffer5(context.id, true, false, imageOption,
            function (data) {
                window.location = context.serviceReport + data.data;
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printOffer5_FullSizeImage = function (imageOption) {
        dataService.getPrintDataOffer5(context.id, false, true, imageOption,
            function (data) {
                if (data.message.type != 2) {
                    window.location = context.serviceReport + data.data;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printOffer5AndSendEmail_FullSizeImage = function (imageOption) {
        dataService.getPrintDataOffer5(context.id, true, true, imageOption,
            function (data) {
                window.location = context.serviceReport + data.data;
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printOffer5PP2013 = function () {
        dataService.getPrintDataOfferPP2013(
            context.id,
            function (data) {
                if (data.message.type != 2) {
                    window.location = context.serviceReport + data.data;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.exportExcelNewVersion = function () {
        dataService.exportNewVersion(
             context.id,
            function (data) {
                window.location = context.serviceReport + data.data;
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printExcelFOBItemOnly = function (id) {
        dataService.excelPOBItemOnly(
            context.id,
            function (data) {
                //jsHelper.processMessage(data.message);
                if (data.message.type !== 2) {
                    window.location = context.serviceReport + data.data;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.event.load();
}]);