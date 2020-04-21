//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', 'commentService', 'priorityService', 'statusService', 'assignService', 'esthourspendService', 'unAssignService', function ($scope, dataService, commentService, priorityService, statusService, assignService, esthourspendService, unAssignService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.devRequestTypes = null;
    $scope.devRequestProjects = null;
    $scope.devRequestPriorities = null;
    $scope.devRequestPersons = null;
    $scope.devRequestStatuses = null;

    //
    // exposing service
    //
    $scope.commentService = commentService;
    $scope.priorityService = priorityService;
    $scope.statusService = statusService;
    $scope.assignService = assignService;
    $scope.unAssignService = unAssignService;

    $scope.esthourspendService = esthourspendService;
    $scope.esthourspendService.parentScope = $scope;

    $scope.dataService = dataService;

    //
    // event
    //
    $scope.event = {
        init:function(){
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.devRequestTypes = data.data.devRequestTypes;
                    $scope.devRequestProjects = data.data.devRequestProjects;
                    $scope.devRequestPriorities = data.data.devRequestPriorities;
                    $scope.devRequestStatuses = data.data.devRequestStatuses;
                    $scope.devRequestPersons = data.data.devRequestPersons;
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.devRequestTypes = null;
                    $scope.devRequestProjects = null;
                    $scope.devRequestPriorities = null;
                    $scope.devRequestStatuses = null;
                    $scope.devRequestPersons = null;
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        },
        getClass: function (id) {
            var result = '';
            switch (id) {
                case 1:
                    result = 'bg-color-blueDark';
                    break;

                case 3:
                    result = 'comment-icon';
                    break;

                case 4:
                    result = 'status-icon';
                    break;

                case 5:
                    result = 'priority-icon';
                    break;

                case 6:
                    result = 'bg-color-pinkDark';
                    break;

                case 6:
                    result = 'bg-color-pinkDark';
                    break;

                case 7:
                    result = 'bg-color-orange';
                    break;
            }
            return result;
        },
        getIcon: function (id) {
            var result = '';
            switch (id) {
                case 1: // CREATE
                    result = 'fa-file-text';
                    break;

                case 2: // ASSIGN
                    result = 'fa-group';
                    break;

                case 3: // COMMENT
                    result = 'fa-comments-o';
                    break;

                case 4: // STATUS
                    result = 'fa-info-circle';
                    break;

                case 5: // PRIORITY
                    result = 'fa-exclamation';
                    break;

                case 6: // EST END DATE
                    result = 'fa-calendar';
                    break;

                case 7: // UN ASSIGN
                    result = 'fa-chain-broken';
                    break;
            }
            return result;
        }
    };

    //
    // trigger
    //
    $scope.$on('refresh', function (event, data) {
        $scope.method.refresh(context.id);
    })

    //
    // init
    //
    $scope.event.init();
}]);