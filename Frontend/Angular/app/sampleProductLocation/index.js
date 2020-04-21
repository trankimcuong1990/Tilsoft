$('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element($('body')).scope();
        scope.event.research();
        return false;
    }
});

(function () {
    'use strict';

    angular
        .module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive'])
        .controller('tilsoftController', sampleProductLocationController);
    sampleProductLocationController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function sampleProductLocationController($scope, $cookieStore, sampleProductLocationService, $timeout) {
        $scope.data = [];
        $scope.supportSeason = [];
        $scope.supportClient = [];
        $scope.supportFactory = [];
        $scope.supportLocation = [];
        $scope.supportStatus = [];

        $scope.sampleProductIDs = [];
        $scope.indexs = [];

        $scope.editData = [];
        $scope.editFactoryLocation = [];
        $scope.editOtherLocation = [];

        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.currentRows = 0;
        $scope.pageIndex = 1;

        $scope.isEnabled = true;
        $scope.checkAll = false;

        // add for UI new.
        $scope.dateLocation = null;
        $scope.factoryLocation = null;
        $scope.otherLocation = null;
        $scope.remarkLocation = null;

        $scope.filters = {
            Season: '',
            Code: '',
            ArticleDescription: '',
            Factory: '',
            Location: '',
            Client: '',
            Quantity: '',
            Status: ''
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieVal = $cookieStore.get(context.cookieID);
        if (cookieVal) {
            $scope.filters = cookieVal;
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    sampleProductLocationService.searchFilter.pageIndex = ++$scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                sampleProductLocationService.searchFilter.sortedDirection = sortedDirection;
                sampleProductLocationService.searchFilter.sortedBy = sortedBy;
                sampleProductLocationService.pageIndex = $scope.pageIndex = 1;
            },
            isTriggered: false
        };

        $scope.event = {
            search: search,
            research: research,
            clear: clear,
            all: all,
            item: item,
            edit: edit,
            close: close,
            save: save
        };

        $scope.event.search();

        function search(isLoadMore) {
            init();
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;
            sampleProductLocationService.searchFilter.filters = $scope.filters;
            sampleProductLocationService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.searchData);
                    $scope.supportSeason = data.data.seasons;
                    $scope.supportStatus = data.data.statuses;
                    $scope.supportFactory = data.data.factories;
                    $scope.supportClient = data.data.clients;
                    $scope.supportLocation = data.data.locations;

                    $scope.totalPage = Math.ceil(data.totalRows / sampleProductLocationService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    var subNo = $scope.totalRows - $scope.currentRows;
                    if (subNo > sampleProductLocationService.searchFilter.pageSize) {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(sampleProductLocationService.searchFilter.pageSize);
                    } else {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(subNo);
                    }

                    if ($scope.filters.Status !== null || $scope.filters.Status !== '') {
                        if ($scope.filters.Status === 4 || $scope.filters.Status === 6 || $scope.filters.Status === 7 || $scope.filters.Status === 10 || $scope.filters.Status === 11) {
                            $scope.isEnabled = false;
                        } else {
                            $scope.isEnabled = true;
                        }
                    }

                    if ($scope.checkAll === true) {
                        $scope.sampleProductIDs = [];
                        $scope.indexs = [];
                        $scope.event.all();
                    }

                    $('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function research() {
            $scope.data = [];
            $scope.pageIndex = 1;
            $scope.currentRows = 0;
            sampleProductLocationService.searchFilter.pageIndex = 1;
            sampleProductLocationService.searchFilter.sortedBy = 'Season';
            sampleProductLocationService.searchFilter.sortedDirection = 'DESC';
            $scope.event.search();
        }

        function clear() {
            $scope.filters = {
                Season: '',
                Code: '',
                ArticleDescription: '',
                Factory: '',
                Location: '',
                Client: '',
                Quantity: '',
                Status: ''
            };

            $scope.data = [];
            $scope.event.research();
        }

        function all() {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].isSelected = $scope.checkAll;
                if ($scope.data[i].isSelected) {
                    $scope.sampleProductIDs.push($scope.data[i].sampleProductItemID);
                    $scope.indexs.push(i);
                } else {
                    var position = $scope.sampleProductIDs.indexOf($scope.data[i]);
                    $scope.sampleProductIDs.splice(position, 1);
                    $scope.indexs.splice(i, 1);
                }
            }
        }

        function item(item, index) {
            var isAllOrNotAll = checkAllOrNotAll();
            if (isAllOrNotAll) {
                $scope.checkAll = true;
            } else {
                $scope.checkAll = false;
            }
            if (item.isSelected === false) {
                var position = $scope.sampleProductIDs.indexOf(item);
                $scope.sampleProductIDs.splice(position, 1);
                $scope.indexs.splice(position, 1);
            } else {
                $scope.sampleProductIDs.push(item.sampleProductItemID);
                $scope.indexs.push(index);
            }
        }

        function edit() {
            if ($scope.sampleProductIDs.length === 0) {
                return false;
            }
            var sampleProductIDs = joinString();
            sampleProductLocationService.getdata(
                {
                    filters: {
                        SampleProductIDs: sampleProductIDs
                    }
                },
                function (data) {
                    $scope.editData = data.data.sampleProducts; //console.log($scope.editData);
                    $scope.editFactoryLocation = data.data.factoryLocations;
                    $scope.editOtherLocation = data.data.otherLocations;

                    $scope.$apply();

                    $('#frmSelectLocation').modal();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function close() {
            $('#frmSelectLocation').modal('hide');
        }

        function save() {
            sampleProductLocationService.updatedata(
                {
                    filters: {
                        sampleProductIDs: joinString(),
                        factoryLocationIDs: joinFactory(),
                        otherLocationIDs: joinOther(),
                        locationDates: joinDate(),
                        remarks: joinRemark()
                    }
                },
                function (data) {
                    for (var i = 0; i < data.data.length; i++) {
                        var sampleProduct = data.data[i]; //console.log(sampleProduct);
                        for (var j = 0; j < scope.indexs.length; j++) {
                            var position = scope.indexs[j];
                            if (sampleProduct.sampleProductItemID === scope.data[position].sampleProductItemID) {
                                scope.data[position] = sampleProduct;
                                scope.data[position].isSelected = false;
                            }
                        }
                    }
                    $scope.sampleProductIDs = [];
                    $scope.dateLocation = null;
                    $scope.factoryLocation = null;
                    $scope.otherLocation = null;
                    $('#frmSelectLocation').modal('hide');
                    $scope.checkAll = false;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function checkAllOrNotAll() {
            for (var i = 0; i < $scope.data.length; i++) {
                if ($scope.data[i].isSelected === false) {
                    return false;
                }
            }
            return true;
        }

        function init() {
            sampleProductLocationService.searchFilter.pageSize = context.pageSize;
            sampleProductLocationService.serviceUrl = context.serviceUrl;
            sampleProductLocationService.token = context.token;
        }

        function joinString() {
            var result = '';
            if ($scope.sampleProductIDs.length === 0) {
                return result;
            }
            for (var i = 0; i < $scope.sampleProductIDs.length; i++) {
                if (i !== 0) {
                    result = result + ',';
                }
                result = result + $scope.sampleProductIDs[i].toString();
            }
            return result;
        }

        function joinFactory() {
            var result = '';
            for (var i = 0; i < $scope.editData.length; i++) {
                if (i !== 0) {
                    result = result + ',';
                }

                if ($scope.factoryLocation === null) {
                    result = result + "null";
                } else {
                    result = result + $scope.factoryLocation.toString();
                }
            }
            return result;
        }

        function joinOther() {
            var result = '';
            for (var i = 0; i < $scope.editData.length; i++) {
                if (i !== 0) {
                    result = result + ',';
                }

                if ($scope.otherLocation === null) {
                    result = result + "null";
                } else {
                    result = result + $scope.otherLocation.toString();
                }
            }

            return result;
        }

        function joinDate() {
            var result = '';
            for (var i = 0; i < $scope.editData.length; i++) {
                if (i !== 0) {
                    result = result + ',';
                }
                //if ($scope.editData[i].locationDate === null || $scope.editData[i].locationDate === '') {
                //    result = result + "null";
                //} else {
                //    result = result + $scope.editData[i].locationDate.toString();
                //}
                if ($scope.dateLocation === null || $scope.dateLocation === '') {
                    result = result + "null";
                } else {
                    result = result + $scope.dateLocation.toString();
                }
            }
            return result;
        }

        function joinRemark() {
            var result = '';
            for (var i = 0; i < $scope.editData.length; i++) {
                if (i !== 0) {
                    result = result + ',';
                }
                if ($scope.editData[i].remark === null) {
                    result = result + "null";
                } else {
                    result = result + $scope.editData[i].remark.toString();
                }
            }
            return result;
        }
    }
})();