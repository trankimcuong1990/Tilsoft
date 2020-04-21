(function () {
    'use strict';
    var siteRoot = document.location.protocol + '//' + document.location.host + '/';
    var uniqueId = (new Date()).getUTCMilliseconds();

    angular.module('tilsoftApp', ['ngCookies', 'ngRoute', 'ngSanitize', 'avs-directives', 'avs-directives', 'furnindo-directive', 'ng-currency', 'ui.select2']);
    angular.module('tilsoftApp').config(function ($routeProvider) {
        $routeProvider.when('/', {
            cache: false,
            controller: 'calendarController',
            templateUrl: siteRoot + 'Views/AnnualLeaveCalendarMng/view/calendar.html?' + uniqueId
        });       
        $routeProvider.otherwise({
            redirectTo: '/'
        });
    });
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        $scope.viewmodel = {
            data: [],
            supportData: {
                locations: [],
                timeRange: [],
                users: [],
                appointmentAttachedFileTypes: [],
                employeeDepartmentDTOs: [],
                loadedStatus: [], // contain string: calendar, editEvent
            },
            calendarStatus: {
                currentMonth: new Date()
            },
            filters: {
                locations: [],
                month: 0,
                year: 0
            }
        };
        dataService.setLocalData(dataService.dataKey, $scope.viewmodel);
    }]);
})();
