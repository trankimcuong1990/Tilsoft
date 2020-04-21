(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', systemSetting2Controller);
    systemSetting2Controller.$inject = ['$scope', '$cookieStore', 'dataService'];

    function systemSetting2Controller($scope, $cookieStore, systemSetting2Service) {
        // support.
        $scope.support = {
            season: null,
            settingKey: [
                { 'settingKeyValue': 'ExRate-EUR-USD', 'settingKeyText': 'ExRate-EUR-USD' },
                { 'settingKeyValue': 'EstUSDContValue', 'settingKeyText': 'EstUSDContValue' },
                { 'settingKeyValue': 'EstEURContValue', 'settingKeyText': 'EstEURContValue' },
            ],
        };

        // data.
        $scope.data = {
            totalRows: 0,
            totalPage: 0,
            pageIndex: 1,
            systemSetting: [],
        };

        // filter.
        $scope.filters = {
            season: jsHelper.getCurrentSeason(),
        };

        // condition.
        $scope.conditions = {
            defaultFilter: angular.copy($scope.filters),
            isDefaultFilter: true,
        };

        // cookie browser.
        $scope.cookie = {
            value: $cookieStore.get(context.cookieID),
        };

        if ($scope.cookie.value) {
            $scope.filters = $scope.cookie.value;
        };

        // event.
        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            filter: filter,
            edit: edit,
            cancel: cancel,
            add: add,
            update: update,
            additemseassion: additemseassion,
            remove: remove,

        };

        // grid handler.
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.data.pageIndex < $scope.data.totalPage) {
                    systemSetting2Service.searchFilter.pageIndex = $scope.data.pageIndex + 1;

                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data.systemSetting = [];

                systemSetting2Service.searchFilter.sortedBy = orderBy;
                systemSetting2Service.searchFilter.sortedDirection = orderDirection;
                systemSetting2Service.searchFilter.pageIndex = $scope.data.pageIndex = 1;

                $scope.event.search();
            },
            isTriggered: false
        };

        // define event.
        function init() {
            systemSetting2Service.searchFilter.pageSize = context.pageSize;
            systemSetting2Service.serviceUrl = context.serviceUrl;
            systemSetting2Service.token = context.token;

            systemSetting2Service.getInit(
                function (success) {
                    $scope.support.season = success.data.season;

                    jQuery('#content').show();

                    $scope.event.search();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    jQuery('#content').show();
                });
        };

        function search(loadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.conditions.isDefaultFilter = angular.equals($scope.filters, $scope.conditions.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            systemSetting2Service.searchFilter.filters = $scope.filters;
            systemSetting2Service.search(
                function (success) {
                    Array.prototype.push.apply($scope.data.systemSetting, success.data.systemSetting);
                    console.log($scope.data.systemSetting);
                    $scope.data.totalPage = Math.ceil(success.totalRows / systemSetting2Service.searchFilter.pageSize);
                    $scope.data.totalRows = success.totalRows;

                    jQuery('#content').show();

                    if (!loadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    jQuery('#content').show();
                });
        };

        function refresh() {
            $scope.data.systemSetting = [];

            systemSetting2Service.searchFilter.pageIndex = 1;

            $scope.event.search();
        };

        function clear() {
            $scope.filters = {
                season: jsHelper.getCurrentSeason(),
            };

            $scope.event.refresh();
        };

        function filter() {
            $scope.event.refresh();
        };

        function edit(item) {
            item.isEditItem = true;
        };

        function cancel(item) {
            if (item.systemSettingID > 0) {
                item.isEditItem = false;
            } else {
                var index = $scope.data.systemSetting.indexOf(item);
                $scope.data.systemSetting.splice(index, 1);
            }
        };

        function add() {
            var item = {
                systemSettingID: -1,
                settingKey: null,
                settingValue: null,
                description: null,
                isEditItem: true,
                settingKeyInput: null,
                settingKeySelect: null,
                season: null,
            };

            $scope.data.systemSetting.splice(0, 0, item);
        };

        function update(item) {
            if (item.systemSettingID < 0) {
                if (item.settingKeyInput !== null && item.settingKeyInput !== '') {
                    item.settingKey = item.settingKeyInput;
                } else if (item.settingKeySelect !== null && item.settingKeySelect !== '') {
                    item.settingKey = item.settingKeySelect;
                }
            }

            // Check setting key have value.
            if (item.settingKey === null || item.settingKey === '') {
                jsHelper.showMessage('warning', 'Please input Setting Key!');
                return false;
            }

            // Check season have value.
            item.season = $scope.filters.season;
            if (item.season === null || item.season === '') {
                jsHelper.showMessage('warning', 'Please select Season!');
                return false;
            }

            systemSetting2Service.update(
                item.systemSettingID,
                item,
                function (success) {
                    jsHelper.processMessage(success.message);

                    if (success.message.type === 0) {
                        var index = $scope.data.systemSetting.indexOf(item);

                        if (item.systemSettingID < 0) {
                            $scope.data.totalRows = $scope.data.totalRows + 1;
                        }

                        $scope.data.systemSetting[index] = success.data;
                        $scope.data.systemSetting[index].isEditItem = false;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };
        function remove(item) {
            systemSetting2Service.delete(
                item.systemSettingID,
                null,
                function (success) {
                    jsHelper.processMessage(success.message);

                    var index = $scope.data.systemSetting.indexOf(item);
                    $scope.data.systemSetting.splice(index, 1);

                    $scope.data.totalRows = $scope.data.totalRows - 1;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };
        function additemseassion() {
            systemSetting2Service.coppyitemfromseasion(
                $scope.filters.season,
                    function (success) {
                        jsHelper.processMessage(success.message);
                        $scope.event.refresh();
                    },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }
        // default event
        $scope.event.init();
    };
})();
