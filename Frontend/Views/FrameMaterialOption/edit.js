//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

var grdMaterialSearchResult = jQuery('#grdMaterialSearchResult').scrollableTable2(
    'quicksearchMaterial.filters.pageIndex',
    'quicksearchMaterial.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quicksearchMaterial.filters.pageIndex = currentPage;
            scope.quicksearchMaterial.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quicksearchMaterial.filters.sortedDirection = sortedDirection;
            scope.quicksearchMaterial.filters.pageIndex = 1;
            scope.quicksearchMaterial.filters.sortedBy = sortedBy;
            scope.quicksearchMaterial.method.search();
        });
    }
);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.frameMaterials = null;
    $scope.frameMaterialColors = null;
    $scope.seasons = null;
    $scope.startListener = false;
    $scope.newID = 0;

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.frameMaterials = data.data.frameMaterials;
                    $scope.frameMaterialColors = data.data.frameMaterialColors;
                    $scope.seasons = data.data.seasons;
                    $scope.startListener = true;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.frameMaterials = null;
                    $scope.frameMaterialColors = null;
                    $scope.seasons = null;
                    $scope.startListener = false;
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
                            $scope.method.refresh(data.data.frameMaterialOptionID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', context.errMsg);
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
                            window.location = context.backURL;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        onOptionChange: function(){
            if(!$scope.startListener){
                return;
            }

            var optionCode = '';
            var optionName = '';

            // material
            if($scope.data.frameMaterialID > 0){
                for(j=0; j< $scope.frameMaterials.length; j++) {
                    if($scope.frameMaterials[j].frameMaterialID == $scope.data.frameMaterialID) {
                        optionCode += $scope.frameMaterials[j].frameMaterialUD;
                        optionName += $scope.frameMaterials[j].frameMaterialNM;
                        break;
                    }
                }
            }

            // type
            if($scope.data.frameMaterialColorID > 0){
                for(j=0; j< $scope.frameMaterialColors.length; j++) {
                    if($scope.frameMaterialColors[j].frameMaterialColorID == $scope.data.frameMaterialColorID) {
                        optionCode += $scope.frameMaterialColors[j].frameMaterialColorUD;
                        optionName += ' ' + $scope.frameMaterialColors[j].frameMaterialColorNM;

                        break;
                    }
                }
            }

            $scope.data.frameMaterialOptionUD = optionCode;
            $scope.data.frameMaterialOptionNM = optionName;
        },
        changeImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.imageFile_DisplayUrl = img.fileURL;
                        scope.data.imageFile_NewFile = img.filename;
                        scope.data.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeImage: function ($event) {
            $scope.data.imageFile_DisplayUrl = '';
            $scope.data.imageFile_NewFile = '';
            $scope.data.imageFile_HasChange = true;
        },
        removeMaterial: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }

            isExist = false;
            for (j = 0; j < $scope.data.materialOptions.length; j++) {
                if ($scope.data.materialOptions[j].frameMaterialOptionMaterialOptionID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.materialOptions.splice(j, 1);
            }
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshURL + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    // search material controller
    //
    var searchMaterialTimer = null;
    $scope.quicksearchMaterial = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'MaterialOptionNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,
        method: {
            search: function () {
                $scope.quicksearchMaterial.filters.filters.searchQuery = jQuery('#quicksearchBoxMaterial').val();
                jsonService.quicksearchMaterial(
                    $scope.quicksearchMaterial.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quicksearchMaterial.data = data.data;
                            $scope.quicksearchMaterial.totalPage = Math.ceil(data.totalRows / $scope.quicksearchMaterial.filters.pageSize);
                            grdMaterialSearchResult.updateLayout();
                            $scope.$apply();
                            jQuery('#material-popup').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            searchBoxKeyUp: function ($event) {
                if (jQuery('#quicksearchBoxMaterial').val().length >= 3) {
                    clearTimeout(searchMaterialTimer);
                    searchMaterialTimer = setTimeout(
                        function () {
                            $scope.quicksearchMaterial.filters.filters.searchQuery = jQuery('#quicksearchBoxMaterial').val();
                            $scope.quicksearchMaterial.filters.pageIndex = 1;
                            jsonService.quicksearchMaterial(
                                $scope.quicksearchMaterial.filters,
                                function (data) {
                                    if (data.message.type == 0) {
                                        $scope.quicksearchMaterial.data = data.data;
                                        $scope.quicksearchMaterial.totalPage = Math.ceil(data.totalRows / $scope.quicksearchMaterial.filters.pageSize);
                                        grdMaterialSearchResult.updateLayout();
                                        $scope.$apply();
                                        jQuery('#material-popup').show();
                                    }
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                }
                            );
                        },
                        500);
                }

                $event.preventDefault();
            },
            close: function ($event) {
                jQuery('#material-popup').hide();
                jQuery('#quicksearchBoxMaterial').val('');
                $scope.quicksearchMaterial.data = null;
                $event.preventDefault();
            },
            ok: function ($event, id) {
                jQuery.each($scope.quicksearchMaterial.data, function () {
                    if (this.materialOptionID == id) {
                        $scope.data.materialOptions.push({
                            frameMaterialOptionMaterialOptionID: $scope.method.getNewID(),
                            materialOptionID: this.materialOptionID,
                            materialOptionUD: this.materialOptionUD,
                            materialOptionNM: this.materialOptionNM,
                            imageFile_DisplayUrl: this.imageFile_DisplayUrl
                        });
                    }
                });
                grdMaterialSearchResult.updateLayout();
                $scope.quicksearchMaterial.event.close($event);
            }
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);