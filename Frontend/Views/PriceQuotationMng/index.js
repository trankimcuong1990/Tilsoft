jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
        return false; // fixbug load twice after searching data
    }
});

(function () {
    'use strict';

    angular
        .module('tilsoftApp', ['ngCookies', 'avs-directives'])
        .controller('tilsoftController', priceQuotationController);

    priceQuotationController.$inject = ['$scope', '$cookieStore', '$timeout', 'dataService'];

    function priceQuotationController($scope, $cookieStore, $timeout, priceQuotationService) {
        $scope.data = [];
        $scope.editData = [];
        $scope.getData = [];
        $scope.historyPriceQuotations = [];

        $scope.totalRows = 0;
        $scope.totalPage = 0;
        $scope.currentRows = 0;
        $scope.pageIndex = 1;

        $scope.supportSeasons = [];
        $scope.supportPriceDifferences = [];
        $scope.supportQuotationStatus = [];

        $scope.filters = {
            Season: jsHelper.getCurrentSeason(),
            ClientUD: '',
            FactoryUD: '',
            ArticleCode: '',
            Description: '',
            PriceDifferenceCode: '',
            QuotationStatusID: '',
            OrderNumber: '',
        };

        $scope.quotationDetailIDs = [];
        $scope.indexs = [];

        $scope.factoryQuotations = [];
        //$scope.factoryQuotationIds = '';
        //$scope.factoryQuotationDetailIds = '';
        //$scope.factoryQuotationPrices = '';

        $scope.titlePriceSeason1 = '';
        $scope.titlePriceSeason2 = '';
        $scope.titlePriceSeason3 = '';
        $scope.titleDiffCode = '';
        $scope.titleDiffCode2 = null;
        $scope.applyDiffID = null;

        $scope.isEdit = false;
        $scope.isShowDiffCode = false;
        $scope.isCompany = null;

        $scope.isDisablePriceSeason1 = false;
        $scope.isDisablePriceSeason2 = false;
        $scope.isDisablePriceSeason3 = false;

        $scope.isSelectedAll = false;

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var quotationDetialID = -1;
        var editIndex = -1;

        var cookieValue = $cookieStore.get(context.cookieId);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    priceQuotationService.searchFilter.pageIndex = ++$scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (orderBy, orderDirection) {
                $scope.data = [];

                priceQuotationService.searchFilter.sortedBy = orderBy;
                priceQuotationService.searchFilter.sortedDirection = orderDirection;
                priceQuotationService.searchFilter.pageIndex = $scope.pageIndex = 1;

                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.method = {
            getTitleSeason: getTitleSeason
        };

        function getTitleSeason(currentSeason) {
            var arraySeason = currentSeason.split('/');

            var firstYear = arraySeason[0];
            var get2LastCharactersInFirstYear = firstYear.substr(2, 3);

            var titleSeason3 = (parseInt(firstYear) - 1).toString() + '-' + get2LastCharactersInFirstYear;
            var titleSeason2 = (parseInt(firstYear) - 2).toString() + '-' + pad((parseInt(get2LastCharactersInFirstYear) - 1).toString(), 2);
            var titleSeason1 = (parseInt(firstYear) - 3).toString() + '-' + pad((parseInt(get2LastCharactersInFirstYear) - 2).toString(), 2);

            $scope.titlePriceSeason1 = titleSeason1;
            $scope.titlePriceSeason2 = titleSeason2;
            $scope.titlePriceSeason3 = titleSeason3;
        };

        $scope.event = {
            search: search,
            reload: reload,
            filter: filter,
            clear: clear,
            edit: edit,
            save: save,
            cancel: cancel,
            change: change,
            apply: apply,
            get: get,
            all: all,
            select: select,
            updateDiffCode: updateDiffCode,
            closeApplyAllCodeForm: closeApplyAllCodeForm,
            updateOfferPrice: updateOfferPrice,
            closeOfferPrice: closeOfferPrice,
            updatePricePreviousSeason: updatePricePreviousSeason,
            approve: approve,
            accept: accept,
            init: init,
            save2: save2,
            getHistory: getHistory
        };

        $scope.event.init();

        // ui handling
        var colOptions = [];
        $timeout(function () {
            var colCount = $('#grdSearchResult').find('.tilsoft-table-header div').length - 1;
            var index = 1;           

            $('#grdSearchResult').find('.tilsoft-table-header div').each(function () {
                if (index <= colCount && index > 1) {
                    colOptions.push({
                        colIndex: index,
                        colTitle: angular.element(this).html().replace("<br>", " ").replace("<br/>", " "),
                        isVisible: true
                    });
                }
                index++;
            });

            $('#grdSearchResult').find('.dt-toolbar .col-setting ul').html('');
            angular.forEach(colOptions, function (item) {
                $('#grdSearchResult').find('.dt-toolbar .col-setting ul').append('<li><label><input type="checkbox" data-colindex="' + item.colIndex + '" checked /> ' + item.colTitle + '</label></li>');
            });
            $('#grdSearchResult').find('.dt-toolbar .col-setting ul li input[type=checkbox]').click(function () {
                var colIndex = parseInt($(this).data('colindex'));
                var isVisible = this.checked;
                if (isVisible) {
                    $('#grdSearchResult').find('.tilsoft-table-header > div:nth-child(' + colIndex + ')').show();
                    $('#grdSearchResult').find('.tilsoft-table-filter > div:nth-child(' + colIndex + ')').show();
                    $('#grdSearchResult').find('.tilsoft-table-body table > tbody > tr > td:nth-child(' + colIndex + ')').show();
                }
                else {
                    $('#grdSearchResult').find('.tilsoft-table-header > div:nth-child(' + colIndex + ')').hide();
                    $('#grdSearchResult').find('.tilsoft-table-filter > div:nth-child(' + colIndex + ')').hide();
                    $('#grdSearchResult').find('.tilsoft-table-body table > tbody > tr > td:nth-child(' + colIndex + ')').hide();
                }

                angular.forEach(colOptions, function (item) {
                    if (item.colIndex === colIndex) {
                        item.isVisible = isVisible;
                    }
                });
            });
        }, 2000);

        function init() {
            priceQuotationService.token = context.token;
            priceQuotationService.serviceUrl = context.serviceUrl;
            priceQuotationService.searchFilter.pageSize = context.pageSize;

            $cookieStore.put(context.cookieId, $scope.filters);

            $scope.isDefaultFilter = false;
            $scope.gridHandler.isTriggered = false;

            $scope.editData = {
                oldPrice1: null,
                oldPrice2: null,
                oldPrice3: null,
                price: null,
                salePrice: null
            }

            priceQuotationService.init(
                {
                    filters: {
                        Season: $scope.filters.Season
                    }
                },
                function (data) {
                    $scope.supportSeasons = data.data.season;
                    $scope.supportPriceDifferences = data.data.priceDifference;
                    $scope.supportQuotationStatus = data.data.quotationStatus;

                    $('#searchClient').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: 'POST',
                                data: JSON.stringify({ filters: { searchQuery: request.term } }),
                                dataType: 'json',
                                url: context.supportServiceUrl + 'getClient2',
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
                            $scope.filters.ClientUD = ui.item.label;
                            $scope.$apply();
                        }
                    });

                    $('#searchFactory').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: 'POST',
                                data: JSON.stringify({ filters: { searchQuery: request.term } }),
                                dataType: 'json',
                                url: context.supportServiceUrl + 'getFactory2',
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
                            $scope.filters.FactoryUD = ui.item.label;
                            $scope.$apply();
                        }
                    });

                    $scope.event.search();
                },
                function (error) {
                });
        };

        function search(loadMore) {
            $cookieStore.put(context.cookieId, $scope.filters);

            $scope.isDefaultFilter = false;
            $scope.gridHandler.isTriggered = false;

            priceQuotationService.searchFilter.filters = $scope.filters;
            priceQuotationService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data); //console.log(data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / priceQuotationService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    var subNo = $scope.totalRows - $scope.currentRows;
                    if (subNo > priceQuotationService.searchFilter.pageSize) {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(priceQuotationService.searchFilter.pageSize);
                    } else {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(subNo);
                    }

                    $scope.isCompany = (data.data.isCompany === null);
                    $scope.titleDiffCode = (data.data.isCompany === null) ? 'Diff.' : 'Qua';
                    $scope.titleDiffCode2 = (data.data.isCompany === null) ? 'Code' : 'lity';

                    $scope.method.getTitleSeason($scope.filters.Season);

                    if ($scope.isSelectedAll) {
                        $scope.quotationDetailIDs = [];
                        $scope.indexs = [];

                        setSelectedIsTrue(true);
                        pushAllSelectedItems();
                    }

                    $('#content').show();

                    if (!loadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;


                    // ui function
                    $timeout(function () {
                        angular.forEach(colOptions, function (item) {
                            console.log(item);
                            if (item.isVisible) {
                                $('#grdSearchResult').find('.tilsoft-table-header > div:nth-child(' + item.colIndex + ')').show();
                                $('#grdSearchResult').find('.tilsoft-table-filter > div:nth-child(' + item.colIndex + ')').show();
                                $('#grdSearchResult').find('.tilsoft-table-body table > tbody > tr > td:nth-child(' + item.colIndex + ')').show();
                            }
                            else {
                                $('#grdSearchResult').find('.tilsoft-table-header > div:nth-child(' + item.colIndex + ')').hide();
                                $('#grdSearchResult').find('.tilsoft-table-filter > div:nth-child(' + item.colIndex + ')').hide();
                                $('#grdSearchResult').find('.tilsoft-table-body table > tbody > tr > td:nth-child(' + item.colIndex + ')').hide();
                            }
                        });
                    }, 0);                    
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                });
        };

        function reload() {
            $scope.data = [];
            $scope.currentRows = 0;
            priceQuotationService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

            $scope.isSelectedAll = false;
            setSelectedIsTrue(false);
        };

        function filter() {
        };

        function clear() {
            $scope.filters = {
                Season: jsHelper.getCurrentSeason(),
                ClientUD: '',
                FactoryUD: '',
                ArticleCode: '',
                Description: '',
                PriceDifferenceCode: '',
                QuotationStatusID: ''
            };

            $scope.priceDifferenceID = null;
            $scope.applyDiffID = null;
            $scope.clientID = null;
            $scope.factoryID = null;

            $scope.event.reload();
        };

        function edit(item, index) {
            //if (item.isEdit === false) {
            //    item.isEdit = true;
            //} else {
            //    item.isEdit = false;
            //}
            quotationDetialID = item.quotationDetailID;
            editIndex = index;

            priceQuotationService.getDataForPopupWithFilters(
                {
                    filters: {
                        QuotationID: item.quotationID,
                        QuotationDetailID: item.quotationDetailID
                    }
                },
                function (data) {
                    $scope.editData = data.data.data;
                    $scope.historyPriceQuotations = data.data.historyQuotationPrices;

                    $scope.isDisablePriceSeason1 = ($scope.editData.oldPrice1 === null);
                    $scope.isDisablePriceSeason2 = ($scope.editData.oldPrice2 === null);
                    $scope.isDisablePriceSeason3 = ($scope.editData.oldPrice3 === null);

                    $scope.editData.priceDifferenceID = getPriceDifferenceId($scope.editData.priceDifferenceCode);
                    $scope.editData.isSetPrice = item.isSetPrice;
                    $scope.editData.quotationID = item.quotationID;
                    $scope.editData.quotationDetailID = item.quotationDetailID;

                    console.log($scope.editData);
                    $scope.editData.price = null;

                    $('#price').val(null);

                    if ($scope.editData.oldPrice1 === null) {
                        $('#oldPrice1').val(null);
                    }
                    if ($scope.editData.oldPrice2 === null) {
                        $('#oldPrice2').val(null);
                    }
                    if ($scope.editData.oldPrice3 === null) {
                        $('#oldPrice3').val(null);
                    }
                    if ($scope.editData.salePrice === null) {
                        $('#salePrice').val(null);
                    }

                    $scope.$apply();

                    $('#warningMsgEdit').html('');
                    $('#warningMsgEdit').css('display', 'none');

                    $('#setOfferPriceQuotationForm').modal();
                },
                function (error) {
                });
        };

        function save() {
            $('#warningMsgEdit').html('');
            $('#warningMsgEdit').css('display', 'none');

            if ($scope.editForm.$valid) {
                $scope.editData.priceDifferenceCode = getPriceDifferenceCode($scope.editData.priceDifferenceID);
                $scope.editData.priceDifferenceRate = getPriceDifferenceRate($scope.editData.priceDifferenceID);

                priceQuotationService.update(
                    quotationDetialID,
                    $scope.editData,
                    function (data) {
                        $('#setOfferPriceQuotationForm').modal('hide');

                        $scope.data[editIndex] = data.data;
                    },
                    function (error) {
                        $('#notificationMessage').hide();

                        $('#warningMsgEdit').html(error.data.message.message);
                        $('#warningMsgEdit').css('display', 'block');
                    });
            }
        };

        function cancel() {
            $('#priceQuotationForm').modal('hide');
        };

        function change() {
            $scope.event.reload();
        };

        function apply() {
            if (!confirm('Do you want to apply difference code for all data ?')) {
                return;
            } else {
                if ($scope.quotationDetailIDs.length === 0) {
                    alert('You ever selected item to apply difference code. Please check data or check all data to execute.');
                } else {
                    $scope.applyDiffID = null;

                    $('#warning-msg').html('');
                    $('#warning-msg').css('display', 'none');

                    $('#applyAllCodeForm').modal();
                }
            }
        };

        function get() {
            priceQuotationService.getPriceSeason({
                filters: {
                    ModelID: $scope.editData.modelID,
                    FrameMaterialID: $scope.editData.frameMaterialID,
                    MaterialID: $scope.editData.materialID,
                    MaterialTypeID: $scope.editData.materialTypeID,
                    FactoryOrderDetailID: $scope.editData.factoryOrderDetailID,
                    FactoryOrderSparepartDetailID: $scope.editData.factoryOrderSparepartDetailID,
                    Season: $scope.editData.season
                }
            },
            function (data) {
                if ($scope.editData.oldPrice1 === null && data.data.priceOfSeason_3 !== undefined && data.data.priceOfSeason_3 !== null)
                    $scope.editData.oldPrice1 = data.data.priceOfSeason_3;
                if ($scope.editData.oldPrice2 === null && data.data.priceOfSeason_2 !== undefined && data.data.priceOfSeason_2 !== null)
                    $scope.editData.oldPrice2 = data.data.priceOfSeason_2;
                if ($scope.editData.oldPrice3 === null && data.data.priceOfSeason_1 !== undefined && data.data.priceOfSeason_1 !== null)
                    scope.editData.oldPrice3 = data.data.priceOfSeason_1;

                if ($scope.editData.oldPrice1 !== null) {
                    $scope.isNullPriceSeason1 = true;
                } else {
                    $scope.isDisablePriceSeason1 = true;
                }

                if ($scope.editData.oldPrice2 !== null) {
                    $scope.isNullPriceSeason2 = true;
                } else {
                    $scope.isDisablePriceSeason2 = true;
                }

                if ($scope.editData.oldPrice3 !== null) {
                    $scope.isNullPriceSeason3 = true;
                } else {
                    $scope.isDisablePriceSeason3 = true;
                }

                $scope.$apply();
            },
            function (error) {
            });
        }

        function all() {
            if (!$scope.isSelectedAll) {
                $scope.isSelectedAll = false;
                setSelectedIsTrue(false);
                spliceAllSelectItems();
            } else {
                $scope.quotationDetailIDs = [];
                $scope.indexs = [];
                $scope.isSelectedAll = true;
                setSelectedIsTrue(true);
                pushAllSelectedItems();
            }
        };

        function select(item, index) {
            if (index > -1) {
                if (!item.isSelected) {
                    item.isSelected = false;

                    if ($scope.isSelectedAll) {
                        $scope.isSelectedAll = false;
                    }

                    var postion = $scope.quotationDetailIDs.indexOf(item);

                    $scope.quotationDetailIDs.splice(postion, 1);
                    $scope.indexs.splice(postion, 1);
                } else {
                    item.isSelected = true;

                    if (checkSelectedAllOrNotAll()) {
                        $scope.isSelectedAll = true;
                    }

                    $scope.quotationDetailIDs.push(item.quotationDetailID);
                    $scope.indexs.push(index);
                }
            }
        };

        function updateDiffCode(id) {
            if (id === null) {
                $('#warning-msg').html('Please selected a value of Difference Code.');
                $('#warning-msg').css('display', 'block');

                $('#notificationMessage').hide();
            } else {
                priceQuotationService.applyAllCode(
                    id,
                    {
                        filters: {
                            priceDifferenceID: id,
                            quotationDetailIDs: listQuotationDetail(),
                            season: $scope.filters.Season,
                        }
                    },
                    function (data) {
                        for (var i = 0; i < scope.indexs.length; i++) {
                            var position = scope.indexs[i];

                            if (scope.data[position] === undefined) {
                                break;
                            }

                            scope.data[position].priceDifferenceCode = getPriceDifferenceCode(id);
                            scope.data[position].isSelected = false;
                        }

                        scope.quotationDetailIDs = [];
                        scope.indexs = [];

                        if ($scope.isSelectedAll) {
                            $scope.isSelectedAll = false;
                        }

                        $('#applyAllCodeForm').modal('hide');

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper('warning', error);
                    });
            }
        };

        function closeApplyAllCodeForm() {
            $('#applyAllCodeForm').modal('hide');
        };

        function updateOfferPrice() {

        };

        function closeOfferPrice() {
            $('#setOfferPriceQuotationForm').modal('hide');
        };

        function updatePricePreviousSeason() {
            if ($scope.quotationDetailIDs.length > 0) {
                priceQuotationService.getPricePreviousSeason(
                    {
                        filters: {
                            Season: $scope.filters.Season,
                            QuotationDetailIDs: listQuotationDetail()
                        }
                    },
                    function (data) {
                        for (var i = 0; i < data.data.length; i++) {
                            var quotationDetail = data.data[i];

                            for (var j = 0; j < scope.indexs.length; j++) {
                                var position = scope.indexs[j];

                                if (quotationDetail.quotationDetailID === scope.data[position].quotationDetailID) {
                                    scope.data[position].oldPrice1 = data.data[i].oldPrice1;
                                    scope.data[position].oldPrice2 = data.data[i].oldPrice2;
                                    scope.data[position].oldPrice3 = data.data[i].oldPrice3;

                                    scope.data[position].isSelected = false;
                                }
                            }
                        }

                        if ($scope.isSelectedAll) {
                            $scope.isSelectedAll = false;
                        }

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper('warning', error);
                    });
            } else {
                jsHelper('warning', 'Please select least one value to update.');
            }
        };

        function save2() {
            getFactoryQuotations();

            priceQuotationService.update2(
                $scope.factoryQuotations,
                function (data) {
                    for (var i = 0; i < data.data.length; i++) {
                        for (var j = 0; j < scope.data.length; j++) {
                            if (data.data[i].quotationDetailID === scope.data[j].quotationDetailID) {
                                scope.data[j] = data.data[i];
                            }
                        }
                    }

                    $scope.factoryQuotations = [];
                },
                function (error) {
                    $scope.factoryQuotations = [];
                    jsHelper.showMessage('warning', error);
                });
        }

        function approve(item, index) {
            if (item.quotationDetailID < 1) {
                return false;
            }

            priceQuotationService.approve(
                item.quotationDetailID,
                item,
                function (data) {
                    $scope.data[index] = data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper('warning', error);
                });
        };

        function accept(item, index) {
            if (index < 0) {
                return false;
            }

            priceQuotationService.approve(
                0,
                item,
                function (data) {
                    //debugger;
                    $scope.data[index] = data.data[0];
                    //$scope.$apply();
                },
                function (error) {
                    jsHelper('warning', error);
                });
        };

        function setValueSelectItem() {
        };

        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        };

        function getPriceDifferenceCode(id) {
            if (id === null)
                return '';
            // loop
            for (var i = 0; i < $scope.supportPriceDifferences.length; i++) {
                if ($scope.supportPriceDifferences[i].priceDifferenceID === id) {
                    return $scope.supportPriceDifferences[i].priceDifferenceUD;
                }
            }
        };

        function getPriceDifferenceRate(id) {
            if (id === null)
                return '';
            // loop
            for (var i = 0; i < $scope.supportPriceDifferences.length; i++) {
                if ($scope.supportPriceDifferences[i].priceDifferenceID === id) {
                    return $scope.supportPriceDifferences[i].rate;
                }
            }
        };

        function getPriceDifferenceId(code) {
            if (code === null) {
                return null;
            } else {
                for (var i = 0; i < $scope.supportPriceDifferences.length; i++) {
                    if ($scope.supportPriceDifferences[i].priceDifferenceUD === code) {
                        return $scope.supportPriceDifferences[i].priceDifferenceID;
                    }
                }
            }
        };

        function getDifferencePriceToEdit(id) {
            if (id === null)
                return false;

            for (var i = 0; i < $scope.supportPriceDifferences.length; i++) {
                if ($scope.supportPriceDifferences[i].priceDifferenceID === id) {
                    $scope.editData.priceDifferenceUD = $scope.supportPriceDifferences[i].priceDifferenceUD;
                    $scope.editData.rate = $scope.supportPriceDifferences[i].rate;
                }
            }
        }

        function listQuotationDetail() {
            var result = '';

            if ($scope.quotationDetailIDs.length === 1) {
                result = $scope.quotationDetailIDs[0].toString();
                return result;
            }

            for (var i = 0; i < $scope.quotationDetailIDs.length; i++) {
                if (i !== 0) {
                    result = result + ',';
                }
                result = result + $scope.quotationDetailIDs[i].toString();
            }

            return result;
        }

        function setSelectedIsTrue(isTrueOrFalse) {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].isSelected = isTrueOrFalse;
            }
        };

        function checkSelectedAllOrNotAll() {
            for (var i = 0; i < $scope.data.length; i++) {
                if (!$scope.data[i].isSelected) {
                    return false;
                }
            }
            return true;
        };

        function pushAllSelectedItems() {
            for (var i = 0; i < $scope.data.length; i++) {
                if ($scope.data[i].isSelected === true && $scope.data[i].quotationStatusID !== 3) {
                    $scope.quotationDetailIDs.push($scope.data[i].quotationDetailID);
                    $scope.indexs.push(i);
                }
            }
        };

        function spliceAllSelectItems() {
            $scope.quotationDetailIDs = [];
            $scope.indexs = [];
        };

        function getClientUD(clientID) {
            for (var i = 0; i < $scope.supportClients.length; i++) {
                if ($scope.supportClients[i].clientID === clientID) {
                    return $scope.supportClients[i].clientUD;
                }
            }

            return '';
        };

        function getFactoryUD(factoryID) {
            for (var i = 0; i < $scope.supportFactories.length; i++) {
                if ($scope.supportFactories[i].factoryID === factoryID) {
                    return $scope.supportFactories[i].factoryUD;
                }
            }
            return '';
        };

        function getFactoryQuotations() {
            //debugger;
            for (var i = 0; i < $scope.data.length; i++) {
                if ($scope.data[i].price === null || $scope.data[i].price === '') {
                    continue;
                }

                $scope.factoryQuotations.push($scope.data[i]);
                //if (i > 0) {
                //    $scope.factoryQuotationIds = $scope.factoryQuotationIds + ',';
                //    $scope.factoryQuotationDetailIds = $scope.factoryQuotationDetailIds + ',';
                //    $scope.factoryQuotationPrices = $scope.factoryQuotationPrices + ',';
                //}

                //$scope.factoryQuotationIds = $scope.factoryQuotationIds + $scope.data[i].quotationID.toString();
                //$scope.factoryQuotationDetailIds = $scope.factoryQuotationDetailIds + $scope.data[i].quotationDetailID.toString();
                //$scope.factoryQuotationPrices = $scope.factoryQuotationPrices + $scope.data[i].price.toString();
            }

            return $scope.factoryQuotationIds;
        };


        function getHistory(item, index) {
            quotationDetialID = item.quotationDetailID;
            editIndex = index;

            priceQuotationService.getDataForPopupWithFilters(
                {
                    filters: {
                        QuotationID: item.quotationID,
                        QuotationDetailID: item.quotationDetailID
                    }
                },
                function (data) {
                    $scope.historyPriceQuotations = data.data.historyQuotationPrices;
                    $scope.$apply();

                    $('#frmHistory').modal();
                },
                function (error) {
                });
        };
    };
})();