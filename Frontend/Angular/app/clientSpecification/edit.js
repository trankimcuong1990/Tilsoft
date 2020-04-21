(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', clientSpecificationController);
    clientSpecificationController.$inject = ['$scope', 'dataService', '$window'];

    function clientSpecificationController($scope, clientSpecificationService, $window) {
        $scope.data = [];
        $scope.clientSpecificationTypes = [];
        $scope.factorySpecComment = [];
        $scope.factorySpecComments = [];

        $scope.event = {
            load: load,
            change: change,
            download: download,
            remove: remove,
            update: update,
            openComment: openComment,
            postComment: postComment,
            delComment: delComment,
            back: back,
            changeType: changeType
        };

        load();

        function load() {
            clientSpecificationService.serviceUrl = context.serviceUrl;
            clientSpecificationService.token = context.token;

            clientSpecificationService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.resultData;
                    console.log($scope.data);
                    $scope.clientSpecificationTypes = data.data.clientSpecificationTypes;

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function change() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation = img.fileURL;
                        scope.data.friendlyName = img.filename;
                        scope.data.newFile = img.filename;
                        scope.data.hasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        };

        function download() {
            if ($scope.data.fileLocation) {
                console.log($scope.data.fileLocation);
                $window.open($scope.data.fileLocation);
            }
        };

        function remove() {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.newFile = '';
            $scope.data.hasChange = false;
        };

        function update() {
            if ($scope.data.clientSpecificationTypeID === 2 && ($scope.data.friendlyName === null || $scope.data.friendlyName === '')) {
                if (confirm('Please ask AVT to update Client Specification first')) {
                    window.location = context.clientContract + $scope.data.clientID;
                } else {
                    return false;
                }
            }

            if ($scope.data.clientSpecificationTypeID === 3 && ($scope.data.friendlyName === null || $scope.data.friendlyName === '')) {
                return false;
            }

            if ($scope.editForm.$valid) {
                clientSpecificationService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.clientSpecificationID + '?code=' + data.data.clientSpecificationUD;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            } else {
                jsHelper.showMessage('warning', context.errorMsg);
            }
        };

        function openComment(factorySpecificationID) {
            $('#' + factorySpecificationID).toggle();
        };

        function postComment(factorySpecID, factorySpec) {
            var comment = jQuery('#comment-' + factorySpecID).val();

            if (comment === undefined || comment === null || comment === '') {
                jsHelper.showMessage('warning', 'Comment is not empty. Please input value comment before click Post Comment');
            } else {
                $scope.dataComment = {
                    comment: comment,
                    factorySpecificationID: factorySpecID
                };

                clientSpecificationService.postComment(
                    $scope.dataComment,
                    function (data) {
                        var index = $scope.data.factorySpecifications.indexOf(factorySpec);

                        if ($scope.data.factorySpecifications[index].factorySpecificationComments === null) {
                            $scope.data.factorySpecifications[index].factorySpecificationComments = [];
                        }

                        $scope.data.factorySpecifications[index].factorySpecificationComments.splice(0, 0, data.data);
                        $scope.data.factorySpecifications[index].commentNo = $scope.data.factorySpecifications[index].factorySpecificationComments.length;

                        $scope.$apply();

                        jQuery('#comment-' + factorySpecID).val('');
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function delComment(factorySpecificationComment, factorySpecification) {
            if (confirm('Do you want to delete this comment?')) {
                clientSpecificationService.delComment(
                    factorySpecificationComment.factorySpecificationCommentID,
                    function (data) {
                        if (data.data === 99 || data.data === 98) {
                            jsHelper.showMessage('warning', data.message.message);
                        } else {
                            var index1 = $scope.data.factorySpecifications.indexOf(factorySpecification);
                            var index2 = $scope.data.factorySpecifications[index1].factorySpecificationComments.indexOf(factorySpecificationComment);

                            $scope.data.factorySpecifications[index1].factorySpecificationComments.splice(index2, 1);

                            $scope.data.factorySpecifications[index1].commentNo = $scope.data.factorySpecifications[index1].factorySpecificationComments.length;

                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function back() {
            window.location = context.backUrl;
        };

        function changeType(typeID, clientID) {
            renewFileSpecification();
            
            if (typeID === undefined || typeID === null || typeID === 3) {
                return false;
            }

            clientSpecificationService.getFileSpecification(
                typeID,
                clientID,
                function (data) {
                    if (typeID === 2 && data.data === null) {
                        if (confirm('Please ask AVT to update Client Specification first')) {
                            window.location = context.clientContract + clientID;
                        } else {
                            return false;
                        }
                    }

                    setFileSpecification(data.data);

                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function renewFileSpecification() {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.newFile = '';
            $scope.data.hasChange = false;
        };

        function setFileSpecification(dataFileSpecification) {
            $scope.data.friendlyName = dataFileSpecification.friendlyName;
            $scope.data.fileLocation = dataFileSpecification.fileLocation;
            $scope.data.newFile = dataFileSpecification.friendlyName;
            $scope.data.hasChange = true;
            $scope.data.clientSpecificationFileUD = '';
        };
    };
})();