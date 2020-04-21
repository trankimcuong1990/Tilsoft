
$(document).ready(function () {
    $("#kl").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#nl tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});
//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = [];
    $scope.newID = -1;
    $scope.productionItemMaterialTypes = null;
    $scope.productionItemGroups = null;
    $scope.productionItemSpecs = null;
    $scope.factoryWarehouses = null;
    $scope.factoryRawMaterialSuppliers = null;
    $scope.types = null;
    $scope.autoGenerate = true;
    $scope.breakDownCategorys = null;
    $scope.outSourcingCostTypeDTOs = null;
    $scope.currentHistory = [];

    $scope.mrps = [
        { 'mrpID': 1, 'mrpNM': 'Yes' },
        { 'mrpID': 2, 'mrpNM': 'No' }
    ];

    $scope.procurementMethods = [
        { 'methodID': 1, 'methodNM': 'Buy' },
        { 'methodID': 2, 'methodNM': 'Make' }
    ];

    $scope.countries = [
        { 'countryID': 1, 'countryCode': 'Vietnam' },
        { 'countryID': 2, 'countryCode': 'Indonesia' },
        { 'countryID': 3, 'countryCode': 'Netherland' }
    ];

    $scope.valuationMethods = [
        { 'methodID': "1", 'methodNM': 'Moving Average' },
        { 'methodID': "2", 'methodNM': 'Standard' },
        { 'methodID': "3", 'methodNM': 'FIFO ' }
    ];

    $scope.status = [
        { overViewStatus: 'New', name: 'New' },
        { overViewStatus: 'Used', name: 'Used' }
    ];

    $scope.basedOns = [
        { 'basedOnID': 1, 'basedOnNM': 'Per Container (Load ability)' },
        { 'basedOnID': 2, 'basedOnNM': 'Per Weight of Frame (KG)' },
        { 'basedOnID': 3, 'basedOnNM': 'Per total value of material (%)' },
        { 'basedOnID': 4, 'basedOnNM': 'Per Item (PCS)' }
    ];

    $scope.depreciationTypes = null;
    $scope.assetClases = null;
    $scope.branches = null;
    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.productionItemMaterialTypes = data.data.productionItemMaterialTypes;
                    $scope.productionItemGroups = data.data.productionItemGroups;
                    $scope.productionItemSpecs = data.data.productionItemSpecs;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.factoryRawMaterialSuppliers = data.data.factoryRawMaterialSuppliers;
                    $scope.units = data.data.units;
                    $scope.productionItemTypes = data.data.productionItemTypes;
                    $scope.depreciationTypes = data.data.depreciationTypeDTOs;
                    $scope.assetClases = data.data.assetClassDTOs;
                    $scope.breakDownCategorys = data.data.breakDownCategoryDTOs;
                    $scope.branches = data.data.branches;
                    $scope.data.userGroupID = context.userGroupID;
                    $scope.outSourcingCostTypeDTOs = data.data.outSourcingCostTypeDTOs;
                    //console.log(data);

                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    if ($scope.data.startDate !== null && $scope.data.userfulLife !== null) {
                        $scope.event.addDay($scope.data.startDate, $scope.data.userfulLife);
                    }


                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.productionItemMaterialTypes = null;
                    $scope.productionItemGroups = null;
                    $scope.productionItemSpecs = null;
                    $scope.factoryWarehouses = null;
                    $scope.factoryRawMaterialSuppliers = null;
                    $scope.units = null;
                    $scope.productionItemTypes = null;
                    $scope.yesNoValues = null;
                }
            );
        },
        update: function () {
            //if ($scope.data.productionItemWarehouses.length < 1 && $scope.data.productionItemTypeID !== 7) {
            //    jsHelper.showMessage('warning', 'Please add at least one default factory warehouse!');
            //    return false;
            //}

            for (var i = 0; i < $scope.data.productionItemSubUnitDTOs.length; i++) {
                var item = $scope.data.productionItemSubUnitDTOs[i];
                if (item.conversionFactor === "0") {
                    jsHelper.showMessage('warning', 'Please input conversion factor of Sub Unit must be different number 0!');
                    return false;
                }
                if ($scope.data.unitID === item.unitID) {
                    for (var j = 0; i < $scope.units.length; j++) {
                        var subItem = $scope.units[j];
                        if (item.unitID === subItem.unitID) {
                            jsHelper.showMessage('warning', 'The Sub Unit ' + subItem.unitNM + ' duplicate with UoM. Please select a different Sub Unit.');
                            return false;
                        }
                    }
                }
                if (item.validFrom === null || item.validFrom === "") {
                    jsHelper.showMessage('warning', 'Please input valid From for Sub Unit !');
                    return false;
                }
            }
            if ($scope.editForm.$valid) {
                var count = 0;
                for (var m = 0; m < $scope.branches.length; m++) {
                    var branchDefault = $scope.branches[m];
                    var countDefault = 0;
                    var countAdded = 0;
                    for (var n = 0; n < $scope.data.productionItemWarehouses.length; n++) {
                        var WarehouseDefault = $scope.data.productionItemWarehouses[n];
                        if (branchDefault.branchID === WarehouseDefault.branchID && WarehouseDefault.isDefault) {
                            countDefault++;
                        }
                        else {
                            if (branchDefault.branchID === WarehouseDefault.branchID && !WarehouseDefault.isDefault) {
                                countAdded++;
                            }
                        }
                    }
                    if (countDefault === 0 && countAdded > 0) {
                        jsHelper.showMessage('warning', 'Branch ' + branchDefault.branchUD + ' must have default warehouse');
                        break;
                    }
                    if (countDefault > 1) {
                        jsHelper.showMessage('warning', 'Branch ' + branchDefault.branchUD + ' must have only one default warehouse');
                        break;
                    }
                    count++;
                }
                if (count === $scope.branches.length) {
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.productionItemID);
                            }
                        },
                        function (error) {
                            //jsHelper.showMessage('warning', error.data.message.message);
                        }
                    );
                }
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }

        },

        //
        // Logo
        //
        changeLogo: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation = img.fileURL;
                        scope.data.thumbnailLocation = img.fileURL;
                        scope.data.fileLocation_NewFile = img.filename;
                        scope.data.fileLocation_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeLogo: function ($event) {
            $scope.data.fileLocation = '';
            $scope.data.thumbnailLocation = '';
            $scope.data.fileLocation_NewFile = '';
            $scope.data.fileLocation_HasChange = true;
        },

        //Warehouse
        removeWarehouse: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }

            isExist = false;
            for (j = 0; j < $scope.data.productionItemWarehouses.length; j++) {
                if ($scope.data.productionItemWarehouses[j].productionItemWarehouseID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.productionItemWarehouses.splice(j, 1);
            }
        },
        addWarehouse: function ($event) {
            if ($event !== null) {
                $event.preventDefault();
            }
            $scope.data.productionItemWarehouses.push({
                productionItemWarehouseID: $scope.method.getNewID(),
                isDefault: 0
            });
        },

        //Vender
        removeVender: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }

            isExist = false;
            for (j = 0; j < $scope.data.productionItemVenders.length; j++) {
                if ($scope.data.productionItemVenders[j].productionItemVenderID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.productionItemVenders.splice(j, 1);
            }
        },
        addVender: function ($event) {
            if ($event !== null) {
                $event.preventDefault();
            }

            $scope.data.productionItemVenders.push({
                productionItemVenderID: $scope.method.getNewID(),
                isDefault: 0,
                remark: null
            });
        },

        //Unit
        removeSubUnit: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }

            isExist = false;
            for (j = 0; j < $scope.data.productionItemSubUnitDTOs.length; j++) {
                if ($scope.data.productionItemSubUnitDTOs[j].productionItemUnitID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.productionItemSubUnitDTOs.splice(j, 1);
            }
        },
        addSubUnit: function ($event) {
            if ($event !== null) {
                $event.preventDefault();
            }

            $scope.data.productionItemSubUnitDTOs.push({
                productionItemUnitID: $scope.method.getNewID(),
                remark: null,
                validFrom: null,
                conversionFactor: null,
                unitID: null
            });
        },

        getEndDate: function (startDateStr) {
            if ($scope.data.userfulLife != "" && $scope.data.startDate != "") {
                var dateParts = startDateStr.split("/");

                //Covert tring to Datetime.
                var startDate = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);

                var endDate = new Date(startDate.setMonth(startDate.getMonth() + 1 + parseInt($scope.data.userfulLife)));

                //Get time.
                var day = endDate.getDate();
                var month = endDate.getMonth();
                var year = endDate.getFullYear();

                //Inclue Zero Before .
                var includeZero = new $scope.method.Padder(2);
                var dayNew = includeZero.pad(day);
                var monthNew = includeZero.pad(month);

                //Reture Time.
                $scope.data.endDate = (dayNew.toString() + "/" + monthNew.toString() + "/" + year.toString());
            }
        },


        addDay: function (dd, numOfMonth) {

            //get day && month && year		
            var day = parseInt(dd.substr(0, 2));
            var mm = parseInt(dd.substr(3, 2));
            var yyyy = parseInt(dd.substr(6, 4));
            var dates = [];

            //Set Price
            var depreciationPerMonth = parseInt($scope.data.depreciationPerMonth);
            var depreciationNetValue = parseInt($scope.data.netValue);

            for (var i = 1; i <= numOfMonth; i++) {

                var _monthInput = parseInt(mm - 1) + i;

                //month
                var _month = _monthInput % 12;
                if (_month === 0) _month = 12;

                //year
                var _year = parseInt(parseInt(_monthInput - 1) / 12);
                _year = _year + parseInt(yyyy);

                //Get Month And Year 
                var dateAdded = /*day + '/' +*/ _month.toString().padStart(2, "0") + '/' + _year;

                //Set DEPRECIATION VALUE
                var depreciationValue = $scope.data.depreciationPerMonth;

                //Get TOTAL DEPRECIATED VALUE
                for (depreciationPerMonth; depreciationPerMonth <= depreciationNetValue;) {

                    var totalDepreciationValue;
                    if (i === 1) {
                        totalDepreciationValue = parseInt(depreciationPerMonth);
                    } else {
                        totalDepreciationValue = parseInt(totalDepreciationValue) + parseInt(depreciationPerMonth);
                    }
                    break;
                }

                //Get NET VALUE
                var netValue = parseInt($scope.data.netValue) - parseInt(totalDepreciationValue);

                if (i == (numOfMonth - 1)) {
                    var getdepreciationValue = netValue;
                    var getTotalDepreciationValue = totalDepreciationValue;
                }

                if (i == numOfMonth) {
                    depreciationValue = getdepreciationValue;
                    totalDepreciationValue = parseInt(depreciationValue) + parseInt(getTotalDepreciationValue);
                    netValue = parseInt($scope.data.netValue) - parseInt(totalDepreciationValue);
                }

                //Push 
                dates.push({ dateAdded, depreciationValue, totalDepreciationValue, netValue });
            }

            $scope.dataBTKH = dates;
        },

        exportExcel: function () {
            dataService.exportExcel(
                context.id,
                function (data) {
                    if (data.message.type == 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        //remove required when production item type is cost
        changeRequired: function (id) {
            if (id == 7) {
                $('.error-cus').addClass('ng-hide');
                $('.remove-required').removeAttr('required');
                $('.select-cus').removeClass('state-error');
            } else {
                $('.error-cus').removeClass('ng-hide');
                $('.remove-required').prop('required', true);
                $('.select-cus').addClass('state-error');
            }
        },

        //History Unit
        openHistoryUnit: function (item) {
            $scope.currentHistory = JSON.parse(JSON.stringify(item));
            jQuery("#unitHistory").modal('show');
        },
        closeHistoryUnit: function () {
            $scope.currentHistory = [];
            jQuery("#unitHistory").modal('hide');
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        getNetValue: function (data) {
            var result = 0;
            if ($scope.data != null) {
                if (data.initiateValue != null && data.increasedAdjustmentValue != null && data.accumulatedDepreciation != null && data.decreasedAdjustmentValue != null) {
                    result = parseInt(data.initiateValue) + parseInt(data.increasedAdjustmentValue) - parseInt(data.accumulatedDepreciation) - parseInt(data.decreasedAdjustmentValue);
                    $scope.data.netValue = result;
                }

            }
            return result;
        },

        getDepreciationPerMonth: function (data) {
            var result = 0;
            if ($scope.data != null) {
                if (data.netValue != null && data.userfulLife != null) {
                    result = parseInt(data.netValue) / parseInt(data.userfulLife);
                    $scope.data.depreciationPerMonth = result;
                }
            }
            return result;
        },

        Padder: function (len, pad) {
            if (len === undefined) {
                len = 1;
            } else if (pad === undefined) {
                pad = '0';
            }

            var pads = '';
            while (pads.length < len) {
                pads += pad;
            }

            this.pad = function (what) {
                var s = what.toString();
                return pads.substring(0, pads.length - s.length) + s;
            };
        },

        getEndDay: function (dd, numOfMonth) {
            if (dd === undefined || dd === null || dd === '') return null;
            //get day && month && year		
            var day = parseInt(dd.substr(0, 2));
            var mm = parseInt(dd.substr(3, 2));
            var yyyy = parseInt(dd.substr(6, 4));
            var dates = [];

            numOfMonth = parseInt(numOfMonth);

            for (var i = 1; i <= numOfMonth; i++) {
                var _monthInput = parseInt(mm - 1) + i;

                //month
                var _month = _monthInput % 12;
                if (_month === 0) _month = 12;

                //year
                var _year = parseInt(parseInt(_monthInput - 1) / 12);
                _year = _year + parseInt(yyyy);

                var dateAdded = day + '/' + _month.toString().padStart(2, "0") + '/' + _year;
                if (i === numOfMonth) {
                    return $scope.data.endDate = dateAdded;
                }
            }
        },

        checkDefault: function ($event, data) {
            for (var j = 0; j < $scope.data.productionItemWarehouses.length; j++) {
                if ($scope.data.productionItemWarehouses[j].productionItemWarehouseID != data.productionItemWarehouseID
                    && $scope.data.productionItemWarehouses[j].isDefault
                    && $scope.data.productionItemWarehouses[j].branchID == data.branchID) {
                    data.isDefault = false;
                }
            }
        }

    };

    //
    // init
    //
    $scope.event.init();
}]);