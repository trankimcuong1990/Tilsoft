angular
    .module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies'])
    .directive('format', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) {
                    return;
                }

                ctrl.$formatters.unshift(function () {
                    return $filter('number')(ctrl.$modelValue);
                });

                ctrl.$parsers.unshift(function (viewValue) {
                    var plainNumber = viewValue.replace(/[\,\.]/g, ''),
                        b = $filter('number')(plainNumber);

                    elem.val(b);

                    return plainNumber;
                });
            }
        };
    })
    .controller('tilsoftController', priceConfigurationController);

priceConfigurationController.$inject = ['$scope', 'dataService'];

function priceConfigurationController($scope, priceConfigurationService) {
    $scope.data = [];
    $scope.productElements = [];
    $scope.seasons = [];
    $scope.fscTypes = [];
    $scope.packagingMethods = [];
    $scope.frameMaterials = [];
    $scope.materialColors = [];
    $scope.cushionColors = [];
    $scope.seasonConfigurations = [];

    $scope.newID = -1;
    $scope.afterChanged = false;

    $scope.getFSCID = -1;
    $scope.getPackagingMethodID = -1;
    $scope.getFrameMaterialID = -1;
    $scope.getMaterialColorID = -1;
    $scope.getCushionColorID = -1;

    $scope.event = {
        actived: actived,
        updated: updated,
        deleted: deleted,
        changed: changed,
        addDetailFSC: addDetailFSC,
        removeDetailFSC: removeDetailFSC,
        addDetailPackagingMethod: addDetailPackagingMethod,
        removeDetailPackagingMethod: removeDetailPackagingMethod,
        addDetailFrameMaterial: addDetailFrameMaterial,
        removeDetailFrameMaterial: removeDetailFrameMaterial,
        addDetailMaterialColor: addDetailMaterialColor,
        removeDetailMaterialColor: removeDetailMaterialColor,
        addDetailCushionColor: addDetailCushionColor,
        removeDetailCushionColor: removeDetailCushionColor,
    };

    $scope.method = {
        init: init,
        refresh: refresh,
        getNewId: getNewId,
        getBodyScope: getBodyScope,
        getFSCName: getFSCName,
        getPackagingMethodName: getPackagingMethodName,
        getFrameMaterialName: getFrameMaterialName,
        getMaterialColorName: getMaterialColorName,
        getCushionColorName: getCushionColorName,
        checkSeasonExist: checkSeasonExist
    };

    $scope.event.actived();

    function actived() {
        $scope.method.init();

        priceConfigurationService.load(
            context.id,
            context.season,
            function (data) {
                $scope.data = data.data; console.log($scope.data.data); console.log(data.data);
                Array.prototype.push.apply($scope.productElements, data.data.supportProductElement);
                Array.prototype.push.apply($scope.seasons, data.data.supportSeason);
                Array.prototype.push.apply($scope.fscTypes, data.data.supportFSCType);
                Array.prototype.push.apply($scope.packagingMethods, data.data.supportPackagingMethod);
                Array.prototype.push.apply($scope.frameMaterials, data.data.supportFrameMaterial);
                Array.prototype.push.apply($scope.materialColors, data.data.supportMaterialColor);
                Array.prototype.push.apply($scope.cushionColors, data.data.supportCushionColor);
                Array.prototype.push.apply($scope.seasonConfigurations, data.data.seasonOfPriceConfiguration);

                $scope.$apply();

                jQuery('#content').show();
            },
            function (error) {
            }
        );
    };

    function updated($event) {
        $event.preventDefault();
        
        if (context.id == 0) {
            if (!checkSeasonExist($scope.data.data.season, $scope.seasonConfigurations)) {
                jsHelper.showMessage('warning', 'Season ' + $scope.data.data.season + ' already exist in DB.');
                return false;
            }
        }

        if ($scope.editForm.$valid) {
            priceConfigurationService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        $scope.method.refresh(1);
                        $scope.data = data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        } else {
            jsHelper.showMessage('warning', 'Invalid data.');
        }
    };

    function deleted() {

    };

    function changed($event) {

    };

    function init() {
        priceConfigurationService.serviceUrl = context.serviceUrl;
        priceConfigurationService.token = context.token;
    };

    function refresh(id) {
        jsHelper.loadingSwitch(true);
        window.location = context.refreshUrl + id +'?s=' + $scope.data.data.season;
    };

    function getNewId() {
        return $scope.newID--;
    };

    function getBodyScope() {
        return angular.element(jQuery('body')).scope();
    };

    function addDetailFSC($event) {
        $event.preventDefault();
        
        if ($scope.getFSCID == -1) {
            return false;
        }

        for (var i = 0; i < $scope.data.data.priceConfigurationDetailOfFCS.length; i++) {
            if ($scope.data.data.priceConfigurationDetailOfFCS[i].optionID == $scope.getFSCID) {
                return false;
            }
        }

        $scope.data.data.priceConfigurationDetailOfFCS.push({
            priceConfigurationDetailID: $scope.method.getNewId(),
            optionID: $scope.getFSCID,
            optionNM: $scope.method.getFSCName($scope.getFSCID),
            percentValue: 0,
            usdAmount: 0,
            eurAmount: 0
        });
    };

    function removeDetailFSC($event, id) {
        $event.preventDefault();

        $scope.data.data.priceConfigurationDetailOfFCS.splice(id, 1);
    };

    function addDetailPackagingMethod($event) {
        $event.preventDefault();

        if ($scope.getPackagingMethodID == -1) {
            return false;
        }

        for (var i = 0; i < $scope.data.data.priceConfigurationDetailOfPackingMethod.length; i++) {
            if ($scope.data.data.priceConfigurationDetailOfPackingMethod[i].optionID == $scope.getPackagingMethodID) {
                return false;
            }
        }

        $scope.data.data.priceConfigurationDetailOfPackingMethod.push({
            priceConfigurationDetailID: $scope.method.getNewId(),
            optionID: $scope.getPackagingMethodID,
            optionNM: $scope.method.getPackagingMethodName($scope.getPackagingMethodID),
            percentValue: 0,
            usdAmount: 0,
            eurAmount: 0
        });
    };

    function removeDetailPackagingMethod($event, id) {
        $event.preventDefault();

        $scope.data.data.priceConfigurationDetailOfPackingMethod.splice(id, 1);
    };

    function addDetailFrameMaterial($event) {
        $event.preventDefault();

        if ($scope.getFrameMaterialID == -1) {
            return false;
        }

        for (var i = 0; i < $scope.data.data.priceConfigurationDetailOfFrameMaterial.length; i++) {
            if ($scope.data.data.priceConfigurationDetailOfFrameMaterial[i].optionID == $scope.getFrameMaterialID) {
                return false;
            }
        }

        $scope.data.data.priceConfigurationDetailOfFrameMaterial.push({
            priceConfigurationDetailID: $scope.method.getNewId(),
            optionID: $scope.getFrameMaterialID,
            optionNM: $scope.method.getFrameMaterialName($scope.getFrameMaterialID),
            percentValue: 0,
            usdAmount: 0,
            eurAmount: 0
        });
    };

    function removeDetailFrameMaterial($event, id) {
        $event.preventDefault();

        $scope.data.data.priceConfigurationDetailOfFrameMaterial.splice(id, 1);
    };

    function addDetailMaterialColor($event) {
        $event.preventDefault();

        if ($scope.getMaterialColorID == -1) {
            return false;
        }

        for (var i = 0; i < $scope.data.data.priceConfigurationDetailOfMaterialColor.length; i++) {
            if ($scope.data.data.priceConfigurationDetailOfMaterialColor[i].optionID == $scope.getMaterialColorID) {
                return false;
            }
        }

        $scope.data.data.priceConfigurationDetailOfMaterialColor.push({
            priceConfigurationDetailID: $scope.method.getNewId(),
            optionID: $scope.getMaterialColorID,
            optionNM: $scope.method.getMaterialColorName($scope.getMaterialColorID),
            percentValue: 0,
            usdAmount: 0,
            eurAmount: 0
        });
    };

    function removeDetailMaterialColor($event, id) {
        $event.preventDefault();

        $scope.data.data.priceConfigurationDetailOfMaterialColor.splice(id, 1);
    };

    function addDetailCushionColor($event) {
        $event.preventDefault();

        if ($scope.getCushionColorID == -1) {
            return false;
        }

        for (var i = 0; i < $scope.data.data.priceConfigurationDetailOfCushionColor.length; i++) {
            if ($scope.data.data.priceConfigurationDetailOfCushionColor[i].optionID == $scope.getCushionColorID) {
                return false;
            }
        }

        $scope.data.data.priceConfigurationDetailOfCushionColor.push({
            priceConfigurationDetailID: $scope.method.getNewId(),
            optionID: $scope.getCushionColorID,
            optionNM: $scope.method.getCushionColorName($scope.getCushionColorID),
            percentValue: 0,
            usdAmount: 0,
            eurAmount: 0
        });
    };

    function removeDetailCushionColor($event, id) {
        $event.preventDefault();

        $scope.data.data.priceConfigurationDetailOfCushionColor.splice(id, 1);
    };

    function getFSCName(id) {
        for (var i = 0; i < $scope.fscTypes.length; i++) {
            if ($scope.fscTypes[i].fscTypeID === id) {
                return $scope.fscTypes[i].fscTypeNM;
            }
        }
        return '';
    };

    function getPackagingMethodName(id) {
        for (var i = 0; i < $scope.packagingMethods.length; i++) {
            if ($scope.packagingMethods[i].packagingMethodID === id) {
                return $scope.packagingMethods[i].packagingMethodNM;
            }
        }
        return '';
    };

    function getFrameMaterialName(id) {
        for (var i = 0; i < $scope.frameMaterials.length; i++) {
            if ($scope.frameMaterials[i].frameMaterialID === id) {
                return $scope.frameMaterials[i].frameMaterialNM;
            }
        }
        return '';
    };

    function getMaterialColorName(id) {
        for (var i = 0; i < $scope.materialColors.length; i++) {
            if ($scope.materialColors[i].materialColorID === id) {
                return $scope.materialColors[i].materialColorNM;
            }
        }
        return '';
    };

    function getCushionColorName(id) {
        for (var i = 0; i < $scope.cushionColors.length; i++) {
            if ($scope.cushionColors[i].cushionColorID === id) {
                return $scope.cushionColors[i].cushionColorNM;
            }
        }
        return '';
    };

    function checkSeasonExist(season, listSeason) {
        for (var i = 0; i < listSeason.length; i++) {
            if (listSeason[i] === season) {
                return false;
            }
        }
        return true;
    };
}