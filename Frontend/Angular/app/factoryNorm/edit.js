var quickSearchResultGridProduct = jQuery('#quickSearchResultGrid-product').scrollableTable2(
    'quickSearchProduct.filters.pageIndex',
    'quickSearchProduct.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchProduct.filters.pageIndex = currentPage;
            scope.quickSearchProduct.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchProduct.filters.sortedDirection = sortedDirection;
            scope.quickSearchProduct.filters.pageIndex = 1;
            scope.quickSearchProduct.filters.sortedBy = sortedBy;
            scope.quickSearchProduct.method.search();
        });
    }
);

var quickSearchResultGridMaterial = jQuery('#quickSearchResultGrid-material').scrollableTable2(
    'quickSearchMaterial.filters.pageIndex',
    'quickSearchMaterial.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.pageIndex = currentPage;
            scope.quickSearchMaterial.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.sortedDirection = sortedDirection;
            scope.quickSearchMaterial.filters.pageIndex = 1;
            scope.quickSearchMaterial.filters.sortedBy = sortedBy;
            scope.quickSearchMaterial.method.search();
        });
    }
);


var quickSearchResultGridFinishedProduct = jQuery('#quickSearchResultGrid-finishedproduct').scrollableTable2(
    'quickSearchFinishedProduct.filters.pageIndex',
    'quickSearchFinishedProduct.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchFinishedProduct.filters.pageIndex = currentPage;
            scope.quickSearchFinishedProduct.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchFinishedProduct.filters.sortedDirection = sortedDirection;
            scope.quickSearchFinishedProduct.filters.pageIndex = 1;
            scope.quickSearchFinishedProduct.filters.sortedBy = sortedBy;
            scope.quickSearchFinishedProduct.method.search();
        });
    }
);

var grdFinishedProductNorm = jQuery('#grdFinishedProductNorm').scrollableTable(null,null);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.units = null;
    $scope.factoryGoodsProcedures = null;
    $scope.materialGroupTypes = null;
    $scope.newID = -1;

    $scope.isCreatingNewComponent = false; // mark as creating component
    $scope.component = {
        factoryFinishedProductUD: '',
        factoryFinishedProductNM : ''
    }
    $scope.parentComponent = null; // mark as creating sub component
    $scope.isEditMode = false;

    //watch the change value on list component norm
    $scope.$watch(function ($scope) {
        return $scope.data.factoryFinishedProductNorms.map(function (item) {
            return { 'factoryFinishedProductID': item.factoryFinishedProductID, 'normValue': item.normValue, 'factoryGoodsProcedureID': item.factoryGoodsProcedureID, 'materialGroupTypeID': item.materialGroupTypeID };
        });
    },
    function (newValArray, oldValArray) {
        for (i = 0; i < newValArray.length; i++) {
            if (newValArray[i].factoryFinishedProductID != oldValArray[i].factoryFinishedProductID
                || newValArray[i].normValue != oldValArray[i].normValue
                || newValArray[i].factoryGoodsProcedureID != oldValArray[i].factoryGoodsProcedureID
                || newValArray[i].materialGroupTypeID != oldValArray[i].materialGroupTypeID)
            {
                $scope.data.factoryFinishedProductNorms[i].isEditing = true;
            }
        }
    }, true);

    //watch the change value on list sub component norm
    $scope.$watch(function ($scope) {
        return $scope.data.factoryFinishedProductNorms.map(function (item) {
            if (item.factoryFinishedProductNorms) {
                return item.factoryFinishedProductNorms.map(function (sItem) {
                    return { 'factoryFinishedProductID': sItem.factoryFinishedProductID, 'normValue': sItem.normValue, 'factoryGoodsProcedureID': sItem.factoryGoodsProcedureID, 'materialGroupTypeID': sItem.materialGroupTypeID };
                });
            }
        });
    },
    function (newValArray, oldValArray) {
        for (i = 0; i < newValArray.length; i++)
        {
            if (newValArray[i] != 'undefined') {
                for (j = 0; j < newValArray[i].length; j++)
                {
                    if (newValArray[i][j].factoryFinishedProductID != oldValArray[i][j].factoryFinishedProductID
                        || newValArray[i][j].normValue != oldValArray[i][j].normValue
                        || newValArray[i][j].factoryGoodsProcedureID != oldValArray[i][j].factoryGoodsProcedureID
                        || newValArray[i][j].materialGroupTypeID != oldValArray[i][j].materialGroupTypeID)
                    {
                        $scope.data.factoryFinishedProductNorms[i].factoryFinishedProductNorms[j].isEditing = true;
                    }
                }
            }
        }
    }, true);

    //watch the change value on list material norm of component norm
    $scope.$watch(function ($scope) {
        return $scope.data.factoryFinishedProductNorms.map(function (item) {
            if (item.factoryMaterialNorms) {
                return item.factoryMaterialNorms.map(function (mItem) {
                    return { 'factoryMaterialID': mItem.factoryFinishedProductID, 'normValue': mItem.normValue };
                });
            }
        });
    },
    function (newValArray, oldValArray) {
        for (i = 0; i < newValArray.length; i++) {
            if (newValArray[i] != 'undefined') {
                for (j = 0; j < newValArray[i].length; j++) {
                    if (newValArray[i][j].factoryMaterialID != oldValArray[i][j].factoryMaterialID || newValArray[i][j].normValue != oldValArray[i][j].normValue) {
                        $scope.data.factoryFinishedProductNorms[i].factoryMaterialNorms[j].isEditing = true;
                    }
                }
            }
        }
    }, true);

    //watch the change value on list material norm  of sub component norm
    $scope.$watch(function ($scope) {
        return $scope.data.factoryFinishedProductNorms.map(function (item) {
            if (item.factoryFinishedProductNorms) {
                return item.factoryFinishedProductNorms.map(function (sItem) {
                    if (sItem.factoryMaterialNorms) {
                        return sItem.factoryMaterialNorms.map(function (mItem) {
                            return { 'factoryMaterialID': mItem.factoryFinishedProductID, 'normValue': mItem.normValue };
                        });
                    }
                });
            }
        });
    },
    function (newValArray, oldValArray) {
        for (i = 0; i < newValArray.length; i++) {
            if (newValArray[i] != 'undefined') {
                for (j = 0; j < newValArray[i].length; j++) {
                    if (newValArray[i][j] != 'undefined') {
                        for (k = 0; k < newValArray[i][j].length; k++) {
                            if (newValArray[i][j][k].factoryMaterialID != oldValArray[i][j][k].factoryMaterialID || newValArray[i][j][k].normValue != oldValArray[i][j][k].normValue) {
                                $scope.data.factoryFinishedProductNorms[i].factoryFinishedProductNorms[j].factoryMaterialNorms[k].isEditing = true;
                            }
                        }
                    }
                }
            }
        }
    }, true);


    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.units = data.data.units;
                    $scope.factoryGoodsProcedures = data.data.factoryGoodsProcedures;
                    $scope.materialGroupTypes = data.data.materialGroupTypes;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    //monitor change item on grid detail (this way only apply for colllection with 1 level)
                    //angular.forEach($scope.data.factoryFinishedProductOrderNorms, function (item, index) {
                    //    $scope.$watch('data.factoryFinishedProductOrderNorms[' + index + '].normValue', function (newVal, oldVal) {
                    //        if (newVal != oldVal)
                    //        {
                    //            item.isEditing = true;
                    //        }
                    //    });
                    //    $scope.$watch('data.factoryFinishedProductOrderNorms[' + index + '].factoryGoodsProcedureID', function (newVal, oldVal) {
                    //        if (newVal != oldVal) {
                    //            item.isEditing = true;
                    //        }
                    //    });
                    //    $scope.$watch('data.factoryFinishedProductOrderNorms[' + index + '].factoryFinishedProductID', function (newVal, oldVal) {
                    //        if (newVal != oldVal) {
                    //            item.isEditing = true;
                    //        }
                    //    });
                    //});
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
                            $scope.method.refresh(data.data.factoryNormID);
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

        //set norm grid in edit mode
        editMode: function ($event,mode) {
            $event.preventDefault();
            $scope.isEditMode = mode;
        },

        //new component
        newComponent: function ($event) {
            $event.preventDefault();
            $scope.isCreatingNewComponent = true;
        },

        cancelComponent: function ($event) {
            $event.preventDefault();
            $scope.isCreatingNewComponent = false;
        },

        saveComponent: function ($event) {
            $event.preventDefault();
            
            jsonService.createNewFactoryFinishedProduct(
                $scope.component.factoryFinishedProductUD, $scope.component.factoryFinishedProductNM,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {

                        //add to component norm
                        if ($scope.parentComponent != null) { //is creating sub component
                            if ($scope.parentComponent.factoryFinishedProductNorms == null) $scope.parentComponent.factoryFinishedProductNorms = [];
                            $scope.parentComponent.factoryFinishedProductNorms.push({
                                factoryFinishedProductNormID: $scope.method.getNewID(),
                                factoryFinishedProductID: data.data,
                                factoryFinishedProductUD: $scope.component.factoryFinishedProductUD,
                                factoryFinishedProductNM: $scope.component.factoryFinishedProductNM,
                                //normValue: 1,
                                factoryMaterialNorms: [],
                                isEditing: true,
                            });
                        }
                        else { // is creating component
                            if ($scope.data.factoryFinishedProductNorms == null) $scope.data.factoryFinishedProductNorms = [];
                            $scope.data.factoryFinishedProductNorms.push({
                                factoryFinishedProductNormID: $scope.method.getNewID(),
                                factoryFinishedProductID: data.data,
                                factoryFinishedProductUD: $scope.component.factoryFinishedProductUD,
                                factoryFinishedProductNM: $scope.component.factoryFinishedProductNM,
                                //normValue: 1,
                                factoryMaterialNorms: [],
                                isEditing: true,
                            });
                        }
                        
                        //reset value
                        $scope.component.factoryFinishedProductUD = '';
                        $scope.component.factoryFinishedProductNM = '';

                        //back to main screen
                        $scope.isCreatingNewComponent = false;
                        $scope.parentComponent = null;
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );            
        },

        //new sub component
        newSubComponent: function ($event,finishedProductNormItem) {
            $event.preventDefault();
            $scope.isCreatingNewComponent = true;
            $scope.parentComponent = finishedProductNormItem;
        },

        //component norm
        addComponentNorm: function ($event) {
            $event.preventDefault();
            $scope.data.factoryFinishedProductNorms.push({
                factoryFinishedProductNormID: $scope.method.getNewID(),
                factoryMaterialNorms : [],
                isEditing: true,
            });
            //setTimeout(function () { runAllForms() }, 500);
        },

        removeComponentNorm: function ($event, id) {
            $event.preventDefault();                       
            if (id > 0) {
                jsonService.deleteFinishedProductNorm(id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        //delete on client
                        angular.forEach($scope.data.factoryFinishedProductNorms, function (item, index) {
                            if (item.factoryFinishedProductNormID == id) {
                                $scope.data.factoryFinishedProductNorms.splice(index, 1);
                                return;
                            }
                        });
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
            }
            else
            {
                //delete on client
                angular.forEach($scope.data.factoryFinishedProductNorms, function (item, index) {
                    if (item.factoryFinishedProductNormID == id) {
                        $scope.data.factoryFinishedProductNorms.splice(index, 1);
                        return;
                    }
                });
            }
        },

        updateComponentNorm: function ($event, item) {
            $event.preventDefault();
            jsonService.createComponentNorm($scope.data.factoryNormID, item,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) { //success
                        item.factoryFinishedProductNormID = data.data;
                        item.isEditing = false;
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },

        //sub component norm
        addSubComponentNorm: function ($event, finishedProductNormItem) {
            $event.preventDefault();
            if (finishedProductNormItem.factoryFinishedProductNorms == null) finishedProductNormItem.factoryFinishedProductNorms = [];
            finishedProductNormItem.factoryFinishedProductNorms.push({
                factoryFinishedProductNormID: $scope.method.getNewID(),
                factoryMaterialNorms: [],
                isEditing: true,
            });
        },

        removeSubComponentNorm: function ($event, finishedProductNormItem, id) {
            $event.preventDefault();
            if (id > 0)
            {
                jsonService.deleteFinishedProductNorm(id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        //delete on client
                        angular.forEach(finishedProductNormItem.factoryFinishedProductNorms, function (item, index) {
                            if (item.factoryFinishedProductNormID == id) {
                                finishedProductNormItem.factoryFinishedProductNorms.splice(index, 1);
                                return;
                            }
                        });
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
            }
            else
            {
                angular.forEach(finishedProductNormItem.factoryFinishedProductNorms, function (item, index) {
                    if (item.factoryFinishedProductNormID == id) {
                        finishedProductNormItem.factoryFinishedProductNorms.splice(index, 1);
                        return;
                    }
                });
            }                   
        },

        updateSubComponentNorm: function ($event,parentItem,item) {
            $event.preventDefault();
            jsonService.createSubComponentNorm(parentItem.factoryFinishedProductNormID, item,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) { //success
                        item.factoryFinishedProductNormID = data.data;
                        item.isEditing = false;
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },


        //material norm
        addMaterialNorm: function ($event, subFinishedProductNormItem) {
            $event.preventDefault();
            
            if (subFinishedProductNormItem.factoryMaterialNorms == null) subFinishedProductNormItem.factoryMaterialNorms = [];
            subFinishedProductNormItem.factoryMaterialNorms.push({
                factoryMaterialNormID: $scope.method.getNewID(),
                isEditing : true,
            });
        },

        removeMaterialNorm: function ($event, subFinishedProductNormItem, id) {
            $event.preventDefault();
            if (id > 0) {
                jsonService.deleteMaterialNorm(id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        //remove on client
                        angular.forEach(subFinishedProductNormItem.factoryMaterialNorms, function (item, index) {
                            if (item.factoryMaterialNormID == id) {
                                subFinishedProductNormItem.factoryMaterialNorms.splice(index, 1);
                                return;
                            }
                        });
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
            }
            else
            {
                //remove on client
                angular.forEach(subFinishedProductNormItem.factoryMaterialNorms, function (item, index) {
                    if (item.factoryMaterialNormID == id) {
                        subFinishedProductNormItem.factoryMaterialNorms.splice(index, 1);
                        return;
                    }
                });
            }                     
        },

        updateMaterialNorm: function ($event,componentItem,item) {
            $event.preventDefault();
            jsonService.createFactoryMaterialNorm(componentItem.factoryFinishedProductNormID, item,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) { //success
                        item.factoryMaterialNormID = data.data;
                        item.isEditing = false;
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },

        //add goods procedure to component
        addGoodsProcedureToComponent: function ($event, finishedProductNormItem) {
            $event.preventDefault();
            if (finishedProductNormItem.factoryFinishedProductNormFactoryGoodsProcedures == null) finishedProductNormItem.factoryFinishedProductNormFactoryGoodsProcedures = [];
            finishedProductNormItem.factoryFinishedProductNormFactoryGoodsProcedures.push({
                factoryFinishedProductNormFactoryGoodsProcedureID: $scope.method.getNewID(),
                isDefault: false
            });
            finishedProductNormItem.isEditing = true;
        },

        //add goods procedure to component
        removeGoodsProcedureToComponent: function ($event, finishedProductNormItem, id) {
            $event.preventDefault();
            finishedProductNormItem.isEditing = true;

            angular.forEach(finishedProductNormItem.factoryFinishedProductNormFactoryGoodsProcedures, function (item, index) {
                if (item.factoryFinishedProductNormFactoryGoodsProcedureID == id) {
                    finishedProductNormItem.factoryFinishedProductNormFactoryGoodsProcedures.splice(index, 1);
                    return;
                }
            });
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
        },
        getFactoryGoodsProcedureNM: function (id) {
            if ($scope.factoryGoodsProcedures == null) return '';
            name = '';
            for (i = 0; i < $scope.factoryGoodsProcedures.length; i++) // nen dung for thay vi angular.forEach doi voi truong hop tim gia tri (tim thay la lap tuc exit vong for, angular.forEach ko exit duoc)
            {
                item = $scope.factoryGoodsProcedures[i];
                if (item.factoryGoodsProcedureID == id)
                {
                    name = item.factoryGoodsProcedureNM;
                    break;
                }
            }
            return name;
        },
        getMaterialGroupTypeName: function (id) {
            if ($scope.materialGroupTypes == null) return '';
            name = '';
            for (i = 0; i < $scope.materialGroupTypes.length; i++) // nen dung for thay vi angular.forEach doi voi truong hop tim gia tri (tim thay la lap tuc exit vong for, angular.forEach ko exit duoc)
            {
                item = $scope.materialGroupTypes[i];
                if (item.materialGroupTypeID == id) {
                    name = item.materialGroupTypeNM;
                    break;
                }
            }
            return name;
        }
    },

    //quick search product
    searchProductTimer = null;
    $scope.quickSearchProduct = {
        data: null,
        filters: {
            filters: {
                searchQuery: '',
                modelStatusID: -1,
            },
            sortedBy: 'ModelNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-product',
        gridSearchResultID: 'quickSearchResultGrid-product',
        searchQueryBoxID: 'quickSearchBox-product',

        method: {
            search: function () {
                supportService.quicksearchModelSeason(
                    $scope.quickSearchProduct.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchProduct.data = data.data;
                            $scope.quickSearchProduct.totalPage = Math.ceil(data.totalRows / $scope.quickSearchProduct.filters.pageSize);
                            quickSearchResultGridProduct.updateLayout();
                            $scope.$apply();
                            jQuery('#' + $scope.quickSearchProduct.popupformID).show();
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
                if ($event.keyCode == 13) {
                    $scope.quickSearchProduct.event.searchClick();
                }
                else if (jQuery('#' + $scope.quickSearchProduct.searchQueryBoxID).val().length >= 3) {
                    clearTimeout(searchProductTimer);
                    searchProductTimer = setTimeout(
                        function () {
                            $scope.quickSearchProduct.event.searchClick();
                        },
                        500);
                }
            },

            searchClick: function () {
                $scope.quickSearchProduct.filters.filters.searchQuery = jQuery('#' + $scope.quickSearchProduct.searchQueryBoxID).val();
                $scope.quickSearchProduct.filters.pageIndex = 1;
                $scope.quickSearchProduct.method.search();
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchProduct.popupformID).hide();
                jQuery('#' + $scope.quickSearchProduct.searchQueryBoxID).val('');
                $scope.quickSearchProduct.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event, item) {
                $scope.data.modelID = item.modelID;
                $scope.data.modelUD = item.modelUD;
                $scope.data.modelNM = item.modelNM;
                $scope.quickSearchProduct.event.close($event);
            }
        }
    },

    //quick search material
    searchMaterialTimer = null;
    $scope.quickSearchMaterial = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'FactoryMaterialNM',
            sortedDirection: 'ASC',
            pageSize: 7,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-material',
        gridSearchResultID: 'quickSearchResultGrid-material',
        //searchQueryBoxID: 'quickSearchBox-material',

        factoryMaterialNormEditingItem : null,

        method: {
            search: function () {
                supportService.quickSearchFactoryMaterial(
                    $scope.quickSearchMaterial.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchMaterial.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.quickSearchMaterial.totalPage = Math.ceil(totalRows / $scope.quickSearchMaterial.filters.pageSize);
                            quickSearchResultGridMaterial.updateLayout();
                            $scope.$apply();

                            //show the search result in ui
                            //jQuery("#factory-material-norm" + $scope.quickSearchMaterial.factoryMaterialNormEditingItem.factoryMaterialNormID).append(jQuery('#' + $scope.quickSearchMaterial.popupformID));
                            jQuery('#' + $scope.quickSearchMaterial.popupformID).show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            onSearch: function ($event, factoryMaterialNormItem) {
                $event.preventDefault();
                //mark the factory material norm item is editing
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem = factoryMaterialNormItem;
                //search data
                $scope.quickSearchMaterial.filters.filters.searchQuery = (factoryMaterialNormItem.factoryMaterialNM == null ? '' : factoryMaterialNormItem.factoryMaterialNM);
                $scope.quickSearchMaterial.filters.pageIndex = 1;
                $scope.quickSearchMaterial.method.search();
            },

            onKeyUp: function ($event, factoryMaterialNormItem) {
                if ($event.keyCode == 13) {                    
                    $scope.quickSearchMaterial.event.onSearch($event, factoryMaterialNormItem);
                }
                else if ($scope.quickSearchMaterial.factoryMaterialNormEditingItem.factoryMaterialNM.length >= 3) {
                    clearTimeout(searchMaterialTimer);
                    searchMaterialTimer = setTimeout(
                        function () {
                            $scope.quickSearchMaterial.event.onSearch($event, factoryMaterialNormItem);
                        },
                        500);
                }
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchMaterial.popupformID).hide();
                jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val('');
                $scope.quickSearchMaterial.data = null;
                $event.preventDefault();
            },
            onSelectedItem: function ($event,item) {
                $event.preventDefault();
                //edit editing row
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem.factoryMaterialID = item.factoryMaterialID
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem.factoryMaterialUD = item.factoryMaterialUD
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem.factoryMaterialNM = item.factoryMaterialNM
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem.unitID = item.unitID
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem.unitNM = item.unitNM
                $scope.quickSearchMaterial.factoryMaterialNormEditingItem.isEditing = true;
                $scope.quickSearchMaterial.event.close($event);
            }
        }
    },


    //quick search finished product
    searchFinishedProductTimer = null;
    $scope.quickSearchFinishedProduct = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'FactoryFinishedProductNM',
            sortedDirection: 'ASC',
            pageSize: 7,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-finishedproduct',
        gridSearchResultID: 'quickSearchResultGrid-finishedproduct',
        //searchQueryBoxID: 'quickSearchBox-finishedproduct',

        factoryFinishedProductNormEditingItem: null,

        method: {
            search: function () {
                supportService.quickSearchFactoryFinishedProduct(
                    $scope.quickSearchFinishedProduct.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchFinishedProduct.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.quickSearchFinishedProduct.totalPage = Math.ceil(totalRows / $scope.quickSearchFinishedProduct.filters.pageSize);
                            quickSearchResultGridFinishedProduct.updateLayout();
                            $scope.$apply();

                            //show the search result in ui
                            //jQuery("#factory-finishedproduct-norm" + $scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem.factoryFinishedProductNormID).append(jQuery('#' + $scope.quickSearchFinishedProduct.popupformID));
                            jQuery('#' + $scope.quickSearchFinishedProduct.popupformID).show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            onSearch: function ($event, factoryFinishedProductNormItem) {
                $event.preventDefault();
                //mark the factory finished product norm item is editing
                $scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem = factoryFinishedProductNormItem;
                //search data
                $scope.quickSearchFinishedProduct.filters.filters.searchQuery = (factoryFinishedProductNormItem.factoryFinishedProductNM == null ? '' : factoryFinishedProductNormItem.factoryFinishedProductNM);
                $scope.quickSearchFinishedProduct.filters.pageIndex = 1;
                $scope.quickSearchFinishedProduct.method.search();
            },

            onKeyUp: function ($event, factoryFinishedProductNormItem) {
                if ($event.keyCode == 13) {
                    $scope.quickSearchFinishedProduct.event.onSearch($event, factoryFinishedProductNormItem);
                }
                else if ($scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem.factoryFinishedProductNM.length >= 3) {
                    clearTimeout(searchFinishedProductTimer);
                    searchFinishedProductTimer = setTimeout(
                        function () {
                            $scope.quickSearchFinishedProduct.event.onSearch($event, factoryFinishedProductNormItem);
                        },
                        500);
                }
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchFinishedProduct.popupformID).hide();
                jQuery('#' + $scope.quickSearchFinishedProduct.searchQueryBoxID).val('');
                $scope.quickSearchFinishedProduct.data = null;
                $event.preventDefault();
            },
            onSelectedItem: function ($event, item) {
                $event.preventDefault();
                //edit editing row
                $scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem.factoryFinishedProductID = item.factoryFinishedProductID
                $scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem.factoryFinishedProductUD = item.factoryFinishedProductUD
                $scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem.factoryFinishedProductNM = item.factoryFinishedProductNM
                $scope.quickSearchFinishedProduct.factoryFinishedProductNormEditingItem.isEditing = true;
                $scope.quickSearchFinishedProduct.event.close($event);
            }
        }
    },

    //
    // init
    //
    $scope.event.load();
}]);