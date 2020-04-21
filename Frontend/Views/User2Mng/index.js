//
// SUPPORT
//
jQuery('.search-filter').keypress(function(e){
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        UserUD: '',
        EmployeeNM: '',
        EmployeeFirstNM: '',
        UserName: '',
        Telephone: '',
        Email: '',
        SkypeID: '',
        UserGroupID: '',
        OfficeID: '',
        IsActivated: 'true',
        CompanyID: '',
        FactoryID: '',
        DepartmentID: '',
        JobTitle: '',
        IsSuperUser: 'any',
        JobLevelID: '',
        LocationID: ''
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];
    $scope.userGroups = null;
    $scope.offices = null;
    $scope.internalCompanies = null;
    $scope.factories = null;
    $scope.internalDepartments = null;
    $scope.jobLevels = null;
    $scope.locations = null;

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                UserUD: '',
                EmployeeNM: '',
                EmployeeFirstNM: '',
                UserName: '',
                Telephone: '',
                Email: '',
                SkypeID: '',
                UserGroupID: '',
                OfficeID: '',
                IsActivated: 'true',
                CompanyID: '',
                FactoryID: '',
                DepartmentID: '',
                JobTitle: '',
                IsSuperUser: 'any',
                JobLevelID: '',
                LocationID: ''
            };
            $scope.event.reload();
        },
        init: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.devRequestStatuses = data.data.devRequestStatuses;

                    $scope.userGroups = data.data.userGroups;
                    $scope.offices = data.data.offices;
                    $scope.internalCompanies = data.data.internalCompanies;
                    $scope.factories = data.data.factories;
                    $scope.internalDepartments = data.data.internalDepartments;
                    $scope.jobLevels = data.data.jobLevels;
                    $scope.locations = data.data.locations;
                    $scope.event.search();
                },
                function (error) {
                    $scope.userGroups = null;
                    $scope.offices = null;
                    $scope.internalCompanies = null;
                    $scope.factories = null;
                    $scope.internalDepartments = null;
                    $scope.jobLevels = null;
                    $scope.locations = null;
                }
            );
        },
        delete: function (item) {
                dataService.deactive(
                    item.userId,
                    item.employeeNM,
                    function (data) {
                        //
                        if (data.message.type == 0) {
                            $scope.data.splice($scope.data.indexOf(item), 1);
                            $scope.totalRows--;
                        }
                    },
                    function (error) {
                    }
                );
        },

        restore: function (id) {
            if (confirm('Active account ?')) {
                dataService.restore(
                   id,
               function (data) {
                   jsHelper.processMessage(data.message);
                   if (data.message.type === 0) {
                       var j = -1;
                       for (var i = 0; i < $scope.data.length; i++) {
                           if ($scope.data[i].userId === data.data) {
                               j = i;
                               break;
                           }
                       }

                       if (j >= 0) {
                           $scope.data.splice(j, 1);
                       }

                       $scope.$apply();
                   }
               },
               function (error) {
                   jsHelper.showMessage('warning', error);
               }
           );
            }
        }
    },
    //
    // init
    //
    $scope.event.init();
}]);