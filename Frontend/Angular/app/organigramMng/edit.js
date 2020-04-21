//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {

    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.newID = -1;
    $scope.data = null;
    $scope.suppliers = null;
    $scope.locations = null;
    $scope.organizationChart = null;

    $scope.director = null;
    
    $scope.filters = {
            departmentID: ''
        };

    //
    $scope.departments = [];
    $scope.reportTos = [];

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
                    $scope.locations = data.data.locations;
                    $scope.organizationChart = data.data.organizationChart;
                    jQuery('#content').show();

                    //jQuery('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
                    //jQuery('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').attr('title', 'Close').on('click', function(e) {
                    //alert('aaaaa');
	                //var children = jQuery(this).parent('li.parent_li').find(' > ul > li');
	                //if (children.is(':visible')) {
		            //    children.hide('fast');
		            //    jQuery(this).attr('title', 'Open').find(' > i').removeClass().addClass('fa fa-lg fa-plus-circle');
	                //} else {
		            //    children.show('fast');
		            //    jQuery(this).attr('title', 'Close').find(' > i').removeClass().addClass('fa fa-lg fa-minus-circle');
	                //}
	                //e.stopPropagation();
	                //});
                    
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    // Get Director info
                    //var director = map.find(function (data) { return data.jobLevelNM === 'DIRECTOR'}); 
                    //console.log(director);
                    // Get group of Department
                    //var groups = scope.data.reduce(function (obj, item) {
                    //    obj[item.internalDepartmentNM] = obj[item.internalDepartmentNM] || [];
                    //    return obj;
                    //}, {});
                    //var myArray = Object.keys(groups).map(function (key) {
                    //    return {
                    //        internalDepartmentNM: key
                    //    };
                    //});
                    //$scope.departmentGr = myArray;

                    //get distinct department
                    var indexs = [];
                    angular.forEach($scope.data, function (value, key) {
                        keyID = value.departmentID.toString();
                        if (indexs.indexOf(keyID) < 0) {
                            indexs.push(keyID);
                            $scope.departments.push({ departmentID: value.departmentID, internalDepartmentNM: value.internalDepartmentNM });
                        }
                    }, null);

                    //get distinct reportto and department
                    //var indexs = [];
                    //angular.forEach($scope.data, function (value, key) {
                    //    keyID = value.departmentID.toString() + value.reportToID.toString();
                    //    if (indexs.indexOf(keyID) < 0) {
                    //        indexs.push(keyID);
                    //        $scope.reportTos.push({ 
                    //            departmentID: value.departmentID, 
                    //            internalDepartmentNM: value.internalDepartmentNM, 
                    //            reportToID: value.reportToID, 
                    //            reportToNM: value.employeeNM,   
                    //            reportToSkypeID: value.skypeID, 
                    //            reportToEmail: value.email1,
                    //            reportToTelephone: value.telephone1,
                    //            reportToJobTitle: value.jobTitle,
                    //            reportToJobNM: value.jobLevelNM
                    //        });
                    //    }
                    //}, null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.suppliers = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.update(
                    $scope.organizationChart.organigramID,
                    $scope.organizationChart,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(context.id);
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
        addImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.organizationChart.companyID = context.id;
                        scope.organizationChart.organigramID = $scope.method.getNewID();
                        scope.organizationChart.fileLocation = img.fileURL;
                        scope.organizationChart.thumbnailLocation = img.fileURL;
                        scope.organizationChart.imageFile_NewFile = img.filename;
                        scope.organizationChart.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
            console.log($scope.organizationChart);
        },
        changeImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.organizationChart.fileLocation = img.fileURL;
                        scope.organizationChart.thumbnailLocation = img.fileURL;
                        scope.organizationChart.imageFile_NewFile = img.filename;
                        scope.organizationChart.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        addWorkSpaceImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.organizationChart.companyID = context.id;
                        scope.organizationChart.organigramID = $scope.method.getNewID();
                        scope.organizationChart.workSpaceFileLocation = img.fileURL;
                        scope.organizationChart.workSpaceFileThumbnail = img.fileURL;
                        scope.organizationChart.workSpaceFile_NewFile = img.filename;
                        scope.organizationChart.workSpaceFile_HasChanged = true;
                    }, null);
                });
            };
            masterUploader.open();
            console.log($scope.organizationChart);
        },
        changeWorkSpaceImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.organizationChart.workSpaceFileLocation = img.fileURL;
                        scope.organizationChart.workSpaceFileThumbnail = img.fileURL;
                        scope.organizationChart.workSpaceFile_NewFile = img.filename;
                        scope.organizationChart.workSpaceFile_HasChanged = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeImage: function ($event) {
            $scope.organizationChart.fileLocation = '';
            $scope.organizationChart.thumbnailLocation = '';
            $scope.organizationChart.imageFile_HasChange = true;
        },
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
        getChildCollection: function (departmentID,reportToID) {
            var arr = scope.data.filter(function (o) { return o.reportToID == id });
            //$(arr).each(function () {
               
            //});
            return arr;
        },
        skypeChat: function(value){
            window.location = 'skype:' + value + '?chat';
        },
        openCall: function (value) {
            window.location = 'tel:' + value;
        },
        getDepartmentDetail: function(id){
            $scope.filters.departmentID = id;
            $scope.data.apply();    
            //console.log(id);
            //console.log($scope.filters.departmentID);
        },
    };

    //
    // init
    //
    $scope.event.init();
}]);


