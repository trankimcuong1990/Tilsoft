//
// Support
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// AP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngCookies', 'avs-directives', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    //
    // property
    //
    $scope.filters = {
        EmployeeNM: '',
        EmployeeFirstNM: '',
        JobTitle: '',
        JobLevelID: '',
        DepartmentID: '',
        CompanyID: '',
        BranchID: '',
        FactoryIDs: [],
        IsSuperUser: '',
        LocationID: '',
        TilsoftUsageID: '',
        HasLeft: 'false'
    };

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];
    $scope.jobLevels = null;
    $scope.departments = null;
    $scope.companies = null;
    $scope.branches = null;
    $scope.factories = null;
    $scope.yesNoValues = null;
    $scope.locations = null;

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.totalAccount = 0;
    $scope.selectedItem = null;

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                jsonService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            jsonService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
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
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function (isLoadMore) {

            if ($scope.filters.EmployeeNM == null) {
                $scope.filters.EmployeeNM = '';
            }
            if ($scope.filters.JobTitle == null) {
                $scope.filters.JobTitle = 0;
            }
            if ($scope.filters.JobLevelID == null) {
                $scope.filters.JobLevelID = '';
            }
            if ($scope.filters.DepartmentID == null) {
                $scope.filters.DepartmentID = '';
            }
            if ($scope.filters.CompanyID == null) {
                $scope.filters.CompanyID = '';
            }
            if ($scope.filters.BranchID == null) {
                $scope.filters.BranchID = '';
            }
            if ($scope.filters.FactoryIDs == null) {
                $scope.filters.FactoryIDs = '';
            }
            if ($scope.filters.IsSuperUser == null) {
                $scope.filters.IsSuperUser = '';
            }
            if ($scope.filters.LocationID == null) {
                $scope.filters.LocationID = '';
            }
            if ($scope.filters.TilsoftUsageID == null) {
                $scope.filters.TilsoftUsageID = '';
            }
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);

            $scope.gridHandler.isTriggered = false;
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.totalAccount = data.data.totalAccount;
                    $scope.$apply();
                    console.log($scope.filters);

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                    //$scope.$apply();
                    //searchResultGrid.updateLayout();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.totalAccount = 0;
                    $scope.$apply();
                }
            );
        },
        
        download: function (url) {
            window.open(url);
        },
        //Restore Account
        restore: function (id) {
            if (confirm('Active account ?')) {
                jsonService.restore(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].employeeID === data.data) {
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
        },

        init: function () {
            jsonService.getsearchfilter(
                function (data) {
                    $scope.jobLevels = data.data.jobLevels;
                    $scope.departments = data.data.departments;
                    $scope.companies = data.data.companies;
                    $scope.branches = data.data.branches;
                    $scope.factories = data.data.factories;
                    $scope.yesNoValues = data.data.yesNoValues;
                    $scope.locations = data.data.locations;
                    $scope.$apply();
                    $scope.event.search();
                },
                function (error) {
                    $scope.jobLevels = null;
                    $scope.departments = null;
                    $scope.companies = null;
                    $scope.branches = null;
                    $scope.yesNoValues = null;
                    $scope.locations = null;
                    $scope.$apply();
                }
            );
        },
        skypeChat: function (value) {
            window.location = 'skype:' + value + '?chat';
        },
        openCall: function (value) {
            window.location = 'tel:' + value;
        },
        clearFilter: function () {
            $scope.filters = {
                EmployeeNM: '',
                JobTitle: '',
                JobLevelID: '',
                DepartmentID: '',
                CompanyID: '',
                BranchID: '',
                FactoryIDs: [],
                IsSuperUser: '',
                LocationID: '',
                TilsoftUsageID: '',
                HasLeft: 'false'
            };
            $scope.event.reload();
        },

        // Age
        getYear: function (value) {
            if (value) {
                var date = value.split('/');
                date = new Date(date[2] + '-' + date[1] + '-' + date[0]);
                var now = new Date();
                var nowYear = now.getFullYear();
                var dataYear = date.getFullYear();
                var age = nowYear - dataYear;
                return age;
            }
        },

        //Get year, month, day
        getDiffDate: function (dateString) {
            if (dateString) {
                var now = new Date();
                var today = new Date(now.getYear(), now.getMonth(), now.getDate());

                var yearNow = now.getYear();
                var monthNow = now.getMonth();
                var dateNow = now.getDate();

                var date = dateString.split('/');
                date = new Date(date[2] + '-' + date[1] + '-' + date[0]);

                var yearOfDate = date.getYear();
                var monthOfDate = date.getMonth();
                var dateOfDate = date.getDate();
                var result = {};

                year = yearNow - yearOfDate;

                if (monthNow >= monthOfDate)
                    var month = monthNow - monthOfDate;
                else {
                    year--;
                    var month = 12 + monthNow - monthOfDate;
                }

                if (dateNow >= dateOfDate)
                    var date = dateNow - dateOfDate;
                else {
                    month--;
                    var date = 31 + dateNow - dateOfDate;

                    if (month < 0) {
                        month = 11;
                        year--;
                    }
                }

                result = {
                    years: year,
                    months: month,
                    days: date
                };

                var yearString = " years";
                var monthString = " months";
                var dayString = " days";

                if (result.years == 1)
                    yearString = " year";

                if (result.months == 1)
                    monthString = " month";

                if (result.days == 1)
                    dayString = " day";

                var resultString = "";

                if (result.years > 0 && result.months > 0 && result.days > 0)
                    resultString = result.years + yearString + ', ' + result.months + monthString + ', ' + result.days + dayString;
                else if (result.months > 0 && result.days > 0)
                    resultString = result.months + monthString + ', ' + result.days + dayString;
                else if (result.years > 0 && result.months > 0)
                    resultString = result.years + yearString + ', ' + result.months + monthString;
                else if (result.years > 0)
                    resultString = result.years + yearString;
                else if (result.months > 0)
                    resultString = result.months + monthString;
                else if (result.days > 0)
                    resultString = result.days + dayString;

                return resultString;
            }
        },

        deactiveEmployee: function (model) {
            $scope.selectedItem = model;
            $('#deactiveForm').modal('show');
        },

        delete: function (id) {            
            jsonService.deactiveAccount(id,
                $scope.data.leftDate,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        var j = -1;
                        for (var i = 0; i < $scope.data.length; i++) {
                            if ($scope.data[i].employeeID == data.data) {
                                j = i;
                                break;
                            }
                        }

                        if (j >= 0) {
                            $scope.data.splice(j, 1);
                        }

                        $scope.$apply();
                        $('#deactiveForm').modal('hide');
                    }                                       
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );

        },
        exportExcelEmployee: function ($event) {
            $event.preventDefault();

            jsonService.exportExcelEmployee({
                filters: {
                    JobLevelID: $scope.filters.JobLevelID,
                    DepartmentID: $scope.filters.DepartmentID,
                    CompanyID: $scope.filters.CompanyID,
                    LocationID: $scope.filters.LocationID,
                    HasLeft: $scope.filters.HasLeft,                  
                }
            },
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                });
        },

    }

    //
    // method
    //

    //
    // init
    //
    $scope.event.init();
}]);