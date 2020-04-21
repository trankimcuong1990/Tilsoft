(function () {
    'use strict';
    var siteRoot = document.location.protocol + '//' + document.location.host + '/';
    var uniqueId = (new Date()).getUTCMilliseconds();

    angular.module('tilsoftApp', ['ngCookies', 'ngRoute', 'ngSanitize', 'avs-directives', 'avs-directives', 'furnindo-directive', 'ng-currency', 'ui.select2']);
    angular.module('tilsoftApp').config(function ($routeProvider) {
        $routeProvider.when('/', {
            cache: false,
            controller: 'calendarController',
            templateUrl: siteRoot + 'Views/SCMAgendaMng/view/calendar.html?' + uniqueId
        });
        $routeProvider.when('/event/visit/:id', {
            cache: false,
            controller: 'eventVisitController',
            templateUrl: siteRoot + 'Views/SCMAgendaMng/view/eventVisit.html?' + uniqueId
        });
        $routeProvider.when('/event/edit/:id', {
            cache: false,
            controller: 'eventEditController',
            templateUrl: siteRoot + 'Views/SCMAgendaMng/view/eventEdit.html?' + uniqueId
        });
        $routeProvider.when('/event/view/:id', {
            cache: false,
            controller: 'eventViewController',
            templateUrl: siteRoot + 'Views/SCMAgendaMng/view/eventView.html?' + uniqueId
        });
        $routeProvider.otherwise({
            redirectTo: '/'
        });
    });
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.serviceUrlQCReport = context.serviceUrlQCReport;
        dataService.token = context.token;

        $scope.viewmodel = {
            data: [],
            id: 0,
            supportData: {
                locations: [],
                factories: [],
                timeRange: [],
                users: [],
                scmAppointmentAttachedFileTypes: [],
                employeeDepartmentDTOs: [],
                loadedStatus: [], // contain string: calendar, editEvent
            },
            calendarStatus: {
                currentMonth: new Date()
            },
            filters: {
                locations: [],
                factories: [],
                month: 0,
                year: 0
            }
        };
        dataService.setLocalData(dataService.dataKey, $scope.viewmodel);
    }]);
})();
