//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', 'editService', function ($scope, dataService, editService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        UserID: ''
    };

    //
    // get filter from cookies
    //
    $scope.data = [];
    $scope.users = [];
    $scope.selectedUserId = null;
    $scope.userId = context.userId;

    $scope.currentFile = null;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    $scope.editService = editService;
    $scope.editService.parentScope = $scope;

    //
    // event
    //
    $scope.event = {
        search: function (isLoadMore) {
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    $scope.data = data.data.data;
                    $scope.users = data.data.users;
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = [];
                    $scope.users = [];
                }
            );
        },
        init : function(){
            $scope.event.search();
        },
        addFile: function(){
            userFileMng.callback = function () {
                var newFiles = [];
                scope.$apply(function () {                    
                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        newFiles.push({
                            onlineFileID: dataService.getIncrementId(),
                            fileUD: '',
                            description: '',
                            updatedBy: '',
                            updatedDate: '',
                            friendlyName: value.fileName,
                            fileLocation: value.filePath,
                            hasChanged: true,
                            newFile: value.fileName
                        });
                    }, null);
                });

                dataService.uploadFiles(
                    newFiles,
                    function (data) {
                        $scope.event.search();
                    },
                    function (error) {
                        console.log(error);
                    }
                );
            };
            userFileMng.open();
        },
        removeFile: function(item){
            dataService.delete(
                item.onlineFileID,
                item.friendlyName,
                function (data) {
                    $scope.event.search();
                },
                function (error) {
                    console.log(error);
                }
            );
        },
        
    }

    //
    // trigger
    //
    $scope.$on('refresh', function (event, data) {
        jsHelper.hidePopup('frmEdit', function () { });
        $scope.event.search();
    })


    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);