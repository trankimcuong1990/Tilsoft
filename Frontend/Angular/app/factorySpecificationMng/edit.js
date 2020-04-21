//
// APP START
//

$("#pricingID").select2({
    closeOnSelect: true,
});

$("#qualityID").select2({
    closeOnSelect: true,
});


var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;
    $scope.factoryOrderDetails = null;
    $scope.newCommnet = {
        factorySpecificationCommentID: '',
        factorySpecificationID: '',
        comment: ''
    };

    $scope.factorySpecificationTypes = [
        { 'typeID': 1, 'typeNM': 'SAME AS CLIENT SPEC' },
        { 'typeID': 2, 'typeNM': 'DIFFERENT FROM CLIENT SPEC' }
    ];

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    console.log($scope.data);
                    $scope.event.getFactoryOrderDetail($scope.data.factoryID, $scope.data.modelID, $scope.data.articleCode);
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        },
        update: function () {
            if ($scope.data.clientSpecificationTypeNM === null || $scope.data.clientSpecificationTypeNM === '') {
                jsHelper.showMessage('warning', 'Please ask AVT to update Client Specification first');
                return false;
            }

            if ($scope.data.factorySpecificationTypeID === 2 && ($scope.data.factoryFileFriendlyName === null || $scope.data.factoryFileFriendlyName === '')) {
                return false;
            }

            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.factorySpecificationID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },
        deleteItem: function () {
            //$event.preventDefault();
            dataService.delete(
                context.id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        // Factory File
        changeFactoryFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        $scope.data.factoryFileFriendlyName = img.filename;
                        $scope.data.factoryFileLocation = img.fileURL;
                        $scope.data.newFile = img.filename;
                        $scope.data.factoryFileLocation_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFactoryFile: function () {
            $scope.data.factoryFileFriendlyName = '';
            $scope.data.factoryFileLocation = '';
            $scope.data.newFile = '';
            $scope.data.factoryFileLocation_HasChange = true;
        },

        // Comment
        addComment: function ($event) {
            $event.preventDefault();
            $scope.data.factorySpecificationComments.push({
                factorySpecificationCommentID: $scope.method.getNewID(),
                factorySpecificationID: context.id,
                comment: $scope.newCommnet.comment
            });
            console.log($scope.data.factorySpecificationComments);
            $scope.event.updateComment($scope.data.factorySpecificationComments);
        },
        deleteComment: function ($event, index, item) {
            $scope.data.factorySpecificationComments.splice(index, 1);
            $scope.event.updateComment($scope.data.factorySpecificationComments);
        },
        updateComment: function (item) {
            // Add new and delete
            dataService.updateComment(
                item,
                function (data) {
                    var result = data.data;
                    $scope.data.factorySpecificationComments = result;
                    $scope.newCommnet = {
                        factorySpecificationCommentID: '',
                        factorySpecificationID: '',
                        comment: ''
                    };
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        getFactoryOrderDetail: function (factoryID, modelID, articleCode) {
            dataService.getFactoryOrderDetail(
                factoryID,
                modelID,
                articleCode,
                function (data) {
                    var result = data.data;
                    $scope.factoryOrderDetails = result;
                    console.log(result);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
    };

    //
    // init
    //
    $scope.event.init();
}]);