//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {};
    $scope.data = [];
    $scope.nodata = false;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    $scope.data = data.data.data;
                    jQuery('#content').show();
                    $scope.method.refresh($scope.data);
                },
                function (error) {
                    $scope.data = [];
                }
            );
        },
        deleteAll: function (file, elem) {
            if (confirm('Delete all files?')) {
                dataService.deleteFiles(
                    function (data) {
                        // remove the images
                        jQuery('.superbox-show').hide();
                        jQuery('#superbox-content').html('<div class="superbox-float"></div>');
                    },
                    function (error) {
                        $scope.data = [];
                    }
                );
            }
        },
    }

    //
    // method
    //
    $scope.method = {
        getRefreshUrl: function (url) {
            var d = new Date();
            var n = d.getMilliseconds();
            if (url.indexOf('?') < 0) {
                return url + '?' + n;
            }
            else {
                return url + n;
            }
        },
        rotate: function (file, direction, elem) {
            dataService.rorateFile(
                file,
                direction,
                function (data) {
                    // refresh cache for the rotated image
                    jQuery('.superbox-img').each(function (index, item) {
                        if (jQuery(item).data('file') == file) {
                            jQuery(item).attr('src', $scope.method.getRefreshUrl(jQuery(item).attr('src')));
                            jQuery(item).data('img', $scope.method.getRefreshUrl(jQuery(item).data('img')));
                        }
                    });
                    jQuery('.superbox-current-img').attr('src', $scope.method.getRefreshUrl(jQuery('.superbox-current-img').attr('src')));
                },
                function (error) {
                    $scope.data = null;
                }
            );
        },
        delete: function (file) {            
            if (confirm('Delete file: ' + file + '?')) {
                dataService.deleteFile(
                    file,
                    function (data) {
                        // remove the image
                        jQuery('.superbox-show').hide();
                        jQuery('.superbox-list.active').remove();
                    },
                    function (error) {
                        $scope.data = null;
                    }
                );
            }
        },
        refresh: function (data) {
            if (data.length > 0) {
                $scope.nodata = false;
            }
            else {
                $scope.nodata = true;
            }
            console.log($scope.nodata);

            //
            // initialize superbox
            //
            jQuery('#superbox-content').html('');
            angular.forEach(data, function (item) {
                jQuery('#superbox-content').append('<div class="superbox-list"><img src="' + item.thumbnailPath + '" data-file="' + item.fileName + '" data-img="' + item.filePath + '" class="superbox-img"></div>');
            });
            jQuery('#superbox-content').append('<div class="superbox-float"></div>');
            jQuery('.superbox').SuperBox({
                buttons: [
                    {
                        label: '<i class="fa fa-rotate-left"></i> Rotate CCW',
                        class: 'primary',
                        callback: function (e) { scope.method.rotate(e, -1); }
                    },
                    {
                        label: '<i class="fa fa-rotate-right"></i> Rotate CW',
                        class: 'primary',
                        callback: function (e) { scope.method.rotate(e, 1); }
                    },
                    {
                        label: '<i class="fa fa-times"></i> Delete',
                        class: 'danger',
                        callback: function (e) { scope.method.delete(e); }
                    }
                ]
            });
        }
    }


    //
    // init
    //
    $scope.event.init();
}]);