function drag() {   
    updateIndex = function (e, ui) {
        $('td.index', ui.item.parent()).each(function (i) {
            $(this).find('input').val(i + 1);
            $(this).find('input').trigger('input')
        });
    };
    $("#table-sort tbody").sortable({
        stop: updateIndex
    });
}

function drop() {
    $("#table-sort tbody").sortable("destroy");
}

tilsoftApp.controller('_GeneralController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    
    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {         
            if (sortedBy === 'deltaPercent') {               
                angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                    item.deltaPercent = $scope.deltaPercent(item);
                }); 
            }
            if (sortedDirection === 'asc') {
                $scope.offerDataContainer.offerDTO.offerLineDTOs.sort(function (a, b) {                            
                    return a[sortedBy] > b[sortedBy] ? 1 : -1;
                });
            }
            else if (sortedDirection === 'desc') {
                $scope.offerDataContainer.offerDTO.offerLineDTOs.sort(function (a, b) {
                    return a[sortedBy] < b[sortedBy] ? 1 : -1;
                });
            }
            $scope.$apply();
            //alert(sortedBy);
            //alert(sortedDirection);
            //$scope.data = [];
            //dataService.searchFilter.sortedDirection = sortedDirection;
            //$scope.pageIndex = 1;
            //dataService.searchFilter.pageIndex = scope.pageIndex;
            //dataService.searchFilter.sortedBy = sortedBy;
            //$scope.event.search();
        },
        isTriggered: false
    };
    $scope.nullHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false
    };    

    $scope.isEdit = function () {       
        if (context.id == 0)
            return false;
        return true;
    };

    //
    //selected tab
    //
    $scope.selectedTab = function (tabName) {
        //remove all active class of tab
        $("#tab-content-detail > div").removeClass("active");
        //add active class to selected tab
        $("#" + tabName).addClass("active");
    };

    //
    //selected tab
    //
    $scope.selectedGeneralTab = function (tabName) {
        //remove all active class of tab
        $("#tab-content-general > div").removeClass("active");
        //add active class to selected tab
        $("#" + tabName).addClass("active");
    };

    $scope.removeOfferLine = function ($event, offerLineID) {
        $event.preventDefault();

        isExist = false;
        for (j = 0; j < $scope.offerDataContainer.offerDTO.offerLineDTOs.length; j++) {
            if ($scope.offerDataContainer.offerDTO.offerLineDTOs[j].offerLineID == offerLineID) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.offerDataContainer.offerDTO.offerLineDTOs.splice(j, 1);
        }

        //re-index row
        var lineData = $scope.offerDataContainer.offerDTO.offerLineDTOs.sort(function (a, b) { return a.rowIndex - b.rowIndex });
        $.each(lineData, function (i) {
            lineData[i].rowIndex = i + 1;
        });
    };

    $scope.removeOfferLineSparepart = function ($event, offerLineSparePartID) {
        $event.preventDefault();

        isExist = false;
        for (j = 0; j < $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.length; j++) {
            if ($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs[j].offerLineSparePartID == offerLineSparePartID) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.splice(j, 1);
        }

        //sort data
        var lineData = $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.sort(function (a, b) { return a.rowIndex - b.rowIndex });
        $.each(lineData, function (i) {
            lineData[i].rowIndex = i + 1;
        });
    };

    $scope.removeOfferLineSampleProduct = function ($event, offerLineSampleProductID) {
        $event.preventDefault();

        isExist = false;
        for (j = 0; j < $scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.length; j++) {
            if ($scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs[j].offerLineSampleProductID == offerLineSampleProductID) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.splice(j, 1);
        }

        //sort data
        var lineData = $scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.sort(function (a, b) { return a.rowIndex - b.rowIndex });
        $.each(lineData, function (i) {
            lineData[i].rowIndex = i + 1;
        });
    };


    //
    //cost of item
    //
    $scope.delta = function (offerLineItem) {
        var delta = $scope.salePriceInUSD(offerLineItem)
            - $scope.purchasingPriceInUSD(offerLineItem)
            - $scope.commissionInUSD(offerLineItem)
            - $scope.lcCostInUSD(offerLineItem)
            - $scope.interestInUSD(offerLineItem)
            - $scope.damagesCostInUSD(offerLineItem)
            - $scope.transportationInUSD(offerLineItem)
            - $scope.bonusInUSD(offerLineItem)

        return delta;
    };

    $scope.deltaPercent = function (offerLineItem) {       
        var deltaPercent = $scope.delta(offerLineItem) / $scope.purchasingPriceInUSD(offerLineItem) * 100;
        return deltaPercent;
    };


    $scope.salePriceInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;

        var currency = $scope.offerDataContainer.offerDTO.currency;
        var exRate = $scope.getExchangeRate();
        var unitPrice = parseFloat(offerLineItem.unitPrice === null || offerLineItem.unitPrice === '' ? 0 : offerLineItem.unitPrice);

        //get amount in USD
        var salePriceInUSD = unitPrice * (currency === 'EUR' ? exRate : 1);
        return salePriceInUSD;
    };

    $scope.purchasingPriceInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;
        var purchasingPriceInUSD = parseFloat(offerLineItem.planingPurchasingPrice === null ? 0 : offerLineItem.planingPurchasingPrice);

        //get purchasing amount
        return purchasingPriceInUSD;
    };

    $scope.bonusInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;

        var currency = $scope.offerDataContainer.offerDTO.currency;
        var exRate = $scope.getExchangeRate();
        var season = $scope.offerDataContainer.offerDTO.season;
        var unitPrice = parseFloat(offerLineItem.unitPrice === null || offerLineItem.unitPrice === '' ? 0 : offerLineItem.unitPrice);

        var bonusPercent = null;
        var estimatedAdditionalCost = $scope.offerDataContainer.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) bonusPercent = estimatedAdditionalCost[0].bonusPercent;

        //get commission
        var bonusPercentInUSD = 0
        bonusPercentInUSD = unitPrice * (currency === 'EUR' ? exRate : 1) * bonusPercent / 100;
        return bonusPercentInUSD;
    };

    $scope.commissionInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;

        var currency = $scope.offerDataContainer.offerDTO.currency;
        var exRate = $scope.getExchangeRate();
        var unitPrice = parseFloat(offerLineItem.unitPrice === null || offerLineItem.unitPrice === '' ? 0 : offerLineItem.unitPrice);    

        //get commission
        var commissionInUSD = 0
        commissionInUSD = unitPrice * (currency === 'EUR' ? exRate : 1) * offerLineItem.commissionPercent / 100;
        return commissionInUSD;
    };

    $scope.lcCostInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;

        var currency = $scope.offerDataContainer.offerDTO.currency;
        var exRate = $scope.getExchangeRate();
        var season = $scope.offerDataContainer.offerDTO.season;
        var unitPrice = parseFloat(offerLineItem.unitPrice === null || offerLineItem.unitPrice === '' ? 0 : offerLineItem.unitPrice);

        //get lc cost percent
        var lcCostPercent = null;
        var estimatedAdditionalCost = $scope.offerDataContainer.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) lcCostPercent = estimatedAdditionalCost[0].lcCostPercent;

        //get payment type
        var paymentTypeID = null;
        var paymentTerms = $scope.offerDataContainer.paymentTermDTOs.filter(o => o.paymentTermID === $scope.offerDataContainer.offerDTO.paymentTermID);
        if (paymentTerms.length > 0) {
            paymentTypeID = paymentTerms[0].paymentTypeID;
        }

        //get lc cost
        var lcCostInUSD = 0;
        if (lcCostPercent !== null) {
            lcCostInUSD = unitPrice * lcCostPercent / 100 * (currency === 'EUR' ? exRate : 1);
        }
        else if (paymentTypeID === 4) {
            lcCostInUSD = unitPrice * 1 / 100 * (currency === 'EUR' ? exRate : 1);
        }
        return lcCostInUSD;
    };

    $scope.interestInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;

        var currency = $scope.offerDataContainer.offerDTO.currency;
        var exRate = $scope.getExchangeRate();
        var season = $scope.offerDataContainer.offerDTO.season;
        var unitPrice = parseFloat(offerLineItem.unitPrice === null || offerLineItem.unitPrice === '' ? 0 : offerLineItem.unitPrice);

        //get interest cost percent
        var interestCostPercent = null;
        var estimatedAdditionalCost = $scope.offerDataContainer.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) interestCostPercent = estimatedAdditionalCost[0].interestCostPercent;

        //get payment day && payment method && down value
        var inDays = null;      
        var downValue = 0;
        var paymentTerms = $scope.offerDataContainer.paymentTermDTOs.filter(o => o.paymentTermID === $scope.offerDataContainer.offerDTO.paymentTermID);
        if (paymentTerms.length > 0) {
            inDays = parseFloat(paymentTerms[0].inDays == null ? 0 : paymentTerms[0].inDays);        
            downValue = parseFloat(paymentTerms[0].downValue == null ? 0 : paymentTerms[0].downValue);
        }      
        //get interest cost
        var interestInUSD = 0;
        if (interestCostPercent !== null) {
            interestInUSD = unitPrice * (currency === 'EUR' ? exRate : 1) * interestCostPercent / 100;          
        }
        else if (inDays !== null) {        
            var interestPercentDTOs = $scope.offerDataContainer.interestPercentDTOs.filter(o => o.season === $scope.offerDataContainer.offerDTO.season);          
            interestInUSD = $scope.salePriceInUSD(offerLineItem) * (1 - downValue / 100) * inDays * interestPercentDTOs[0].settingValue / 100 / 360;          
        }
       
        return interestInUSD;
    };

    $scope.damagesCostInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;
        var price = offerLineItem.planingPurchasingPrice ? offerLineItem.planingPurchasingPrice : offerLineItem.autoPlaningPurchasingPrice

        //get damage cost
        var damagesCostInUSD = parseFloat(price === null ? 0.001 : price) * 1.25 / 100;
        return damagesCostInUSD;
    };

    $scope.transportationInUSD = function (offerLineItem) {
        if ($scope.offerDataContainer === null || $scope.offerDataContainer.offerDTO === null) return 0;

        var currency = $scope.offerDataContainer.offerDTO.currency;
        var exRate = $scope.getExchangeRate();
        var qnt40HC = offerLineItem.qnt40HC === null || offerLineItem.qnt40HC == '' ? 0 : offerLineItem.qnt40HC;
        var transportationFee = $scope.offerDataContainer.offerDTO.transportationFee;
        transportationFee = parseFloat(transportationFee === null || transportationFee === '' ? 0 : transportationFee);

        //get transportation
        var transportationInUSD = 0;
        if (currency === 'EUR' && qnt40HC !== 0) {
            transportationInUSD = transportationFee / qnt40HC * exRate;
        }
        return transportationInUSD;
    };

    $scope.getCommissionPercent = function () {
        var commissionUSDProduct = 0;
        var saleAmountUSDProduct = 0;
        var commissionUSDSparepart = 0;
        var saleAmountUSDSparepart = 0;

        if ($scope.offerDataContainer && $scope.offerDataContainer.offerDTO && $scope.offerDataContainer.offerDTO.offerLineDTOs) {           
            angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                commissionUSDProduct += parseFloat($scope.commissionInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                saleAmountUSDProduct += parseFloat($scope.salePriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
            });        
        }

        if ($scope.offerDataContainer && $scope.offerDataContainer.offerDTO && $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs) {         
            angular.forEach($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs, function (item) {
                commissionUSDSparepart += parseFloat($scope.commissionInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                saleAmountUSDSparepart += parseFloat($scope.salePriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
            });
        }

        return (commissionUSDProduct + commissionUSDSparepart) / (saleAmountUSDProduct + saleAmountUSDSparepart) * 100;
    };

    $scope.getTotalRow = function (category) {
        if ($scope.offerDataContainer && $scope.offerDataContainer.offerDTO && $scope.offerDataContainer.offerDTO.offerLineDTOs) {
            switch (category) {
                case 'quantity':
                    return $scope.offerDataContainer.offerDTO.offerLineDTOs.reduce((output, item) => output + (item.quantity ? parseInt(item.quantity) : 0), 0);

                case 'quantity40HC':
                    return $scope.offerDataContainer.offerDTO.offerLineDTOs.reduce((output, item) => output + jsHelper.round(item.quantity / item.qnt40HC, 2), 0.00);

                case 'commissionPercent':
                    var sum = $scope.offerDataContainer.offerDTO.offerLineDTOs.reduce((output, item) => output + jsHelper.round((item.commissionPercent ? parseInt(item.commissionPercent) : 0), 2), 0.00);
                    var count = $scope.offerDataContainer.offerDTO.offerLineDTOs.length;
                    return sum / count;
                case 'commissionPercentSparepart':
                    var sum = $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.reduce((output, item) => output + jsHelper.round((item.commissionPercent ? parseInt(item.commissionPercent) : 0), 2), 0.00);
                    var count = $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.length;
                    return sum / count;  
                case 'deltaTotalPercentSingle':
                    var totalPercent = 0;
                    var totalItem = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        if (item.quantity > 0) {
                            if ($scope.purchasingPriceInUSD(item) > 0) {
                                totalPercent += parseFloat($scope.deltaPercent(item));
                                totalItem += 1;
                            }
                        }
                    });
                    totalPercent = totalPercent / totalItem;
                    return totalPercent;

                case 'deltaTotalPercent':
                    var salePriceInUSD = 0;
                    var purchasingPriceInUSD = 0
                    var commissionInUSD = 0
                    var bonusInUSD = 0
                    var lcCostInUSD = 0
                    var interestInUSD = 0
                    var damagesCostInUSD = 0
                    var transportationInUSD = 0

                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        if (parseFloat($scope.purchasingPriceInUSD(item) * (item.quantity === null ? 0 : item.quantity)) > 0)
                        {
                            salePriceInUSD += parseFloat($scope.salePriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            purchasingPriceInUSD += parseFloat($scope.purchasingPriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            commissionInUSD += parseFloat($scope.commissionInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            bonusInUSD += parseFloat($scope.bonusInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            lcCostInUSD += parseFloat($scope.lcCostInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            interestInUSD += parseFloat($scope.interestInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            damagesCostInUSD += parseFloat($scope.damagesCostInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                            transportationInUSD += parseFloat($scope.transportationInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                        }                        
                    });                 

                    return (salePriceInUSD - purchasingPriceInUSD - commissionInUSD - bonusInUSD - lcCostInUSD - interestInUSD - damagesCostInUSD - transportationInUSD) / purchasingPriceInUSD * 100;

                case 'deltaTotal':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.delta(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;
                case 'bonus':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += $scope.bonusInUSD(item) * item.quantity
                    });
                    return total;
                case 'commission':
                    var total = 0;                    
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += $scope.commissionInUSD(item) * item.quantity
                    });
                    return total;

                case 'lccost':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.lcCostInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'interest':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.interestInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'damages':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.damagesCostInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'transportation':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.transportationInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'saleAmount':
                    return $scope.offerDataContainer.offerDTO.offerLineDTOs.reduce((output, item) => output + item.unitPrice * item.quantity, 0.00);

                case 'vat':
                    return $scope.offerDataContainer.offerDTO.offerLineDTOs.reduce((output, item) => output + item.unitPrice * item.quantity, 0.00) * ($scope.offerDataContainer.offerDTO.vat ? $scope.offerDataContainer.offerDTO.vat / 100 : 0);

                case 'total':
                    return $scope.offerDataContainer.offerDTO.offerLineDTOs.reduce((output, item) => output + item.unitPrice * item.quantity, 0.00) * (1 + ($scope.offerDataContainer.offerDTO.vat ? $scope.offerDataContainer.offerDTO.vat / 100 : 0));

                case 'purchaseAmount':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.purchasingPriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'saleAmountInUSD':
                    var total = 0;
                    angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs, function (item) {
                        total += parseFloat($scope.salePriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;
            }
        }

        return null;
    };    
}]);
