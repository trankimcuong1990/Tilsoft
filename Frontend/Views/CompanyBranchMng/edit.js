function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label,

            streetAddress: item.streetAddress,
            phone: item.phone,
            fax: item.fax,
            email: item.email,
            factoryID: item.factoryID,
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', companyBranchMngController);
    companyBranchMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function companyBranchMngController($scope, companybranchMngService, $timeout) {
        $scope.data = null;
        $scope.timeRanges = [];
        $scope.locations = null;
        $scope.factories = null;
        $scope.branchID = -1;

        $scope.autoBranch = {
            keyAPI: {
                url: context.serviceUrl + 'quick-search-branch',
                token: context.token,
            },
            initBranch: null,
            selectedBranch: {
                id: null,
                label: null,
                description: null,

                streetAddress: null,
                phone: null,
                fax: null,
                email: null,
                factoryID: null,
            }
        };

        $scope.option = "true";

        $scope.filter = {
            productionItemUD: '',
            productionItemNM: ''
        };

        $scope.newBranch = {
            branchID: null,
            branchUD: null,
            branchNM: null,
            companyID: null,
            streetAddress: null,
            phone: null,
            fax: null,
            email: null,
            factoryID: null,
            factoryUD: null,
            isDefault: null,
            remark: null,
        };

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            load: load,
            get: get,
            update: update,
            del: del,
            remove: remove,

            //
            // Logo
            //
            changeLogo: function () {
                fileUploader2.callback = function () {
                    scope.$apply(function () {
                        scope.data.thumbnailLocation = fileUploader2.selectedFileUrl;
                        scope.data.newFile = fileUploader2.selectedFileName;
                        scope.data.hasChanged = true;
                    });
                };
                fileUploader2.open();
            },
            removeLogo: function () {
                $scope.data.thumbnailLocation = '';
                $scope.data.newFile = '';
                $scope.data.hasChanged = true;
            },

            addFactory: function () {
                $scope.method.resetNewBranch();
                jQuery("#factoryForm").modal();
            },
            selectItemFactory: function () {
                var addingItem = [];
                var factoryItem = [];
                if ($scope.data.branches === null || $scope.data.branches.length === 0) {
                    angular.forEach($scope.factories, function (item) {
                        if (item.checkVal) {
                            addingItem = {
                                //tobe saved info
                                branchID: companybranchMngService.getIncrementId(),
                                factoryID: item.factoryID,
                                factoryUD: item.factoryUD,
                                factoryName: item.factoryName
                            };
                            $scope.data.branches.push(addingItem);
                        }
                    });
                } else {
                    for (var j = 0; j < $scope.factories.length; j++) {
                        var item = $scope.factories[j];
                        if (item.checkVal) {
                            if ($scope.method.checkFactory(item)) {
                                continue;
                            }
                            var addItem = {
                                factoryID: item.factoryID,
                                factoryUD: item.factoryUD,
                                factoryName: item.factoryName
                            };
                            factoryItem.push(addItem);
                        }
                    }
                    angular.forEach(factoryItem, function (item) {
                        addingItem = {
                            //tobe saved info
                            branchID: companybranchMngService.getIncrementId(),
                            factoryID: item.factoryID,
                            factoryUD: item.factoryUD,
                            factoryName: item.factoryName
                        };
                        $scope.data.branches.push(addingItem);
                    });
                }
            },
            checkDefault: function (item) {
                for (var i = 0; i < $scope.data.branches.length; i++) {
                    var sItem = $scope.data.branches[i];
                    if (sItem.branchID === item.branchID) {
                        sItem.isDefault = true;
                    }
                    else {
                        sItem.isDefault = false;
                    }
                }
            },

            getBranchQuichSearch: function (selectedBranch, companyID) {
                $scope.newBranch.branchID = $scope.method.getNewBranchID();
                $scope.newBranch.branchUD = selectedBranch.value;
                $scope.newBranch.branchNM = selectedBranch.description;
                $scope.newBranch.companyID = companyID;
                $scope.newBranch.streetAddress = selectedBranch.streetAddress;
                $scope.newBranch.phone = selectedBranch.phone;
                $scope.newBranch.fax = selectedBranch.fax;
                $scope.newBranch.email = selectedBranch.email;
                $scope.newBranch.factoryID = selectedBranch.factoryID;
                $scope.newBranch.factoryUD = selectedBranch.value;
                $scope.newBranch.isDefault = false;
                $scope.newBranch.remark = null;

                $timeout(function () {
                    return $scope.newBranch;
                }, 0);
            },
            pushBranches: function (newBranch, company) {
                if (newBranch !== null) {
                    if (!$scope.method.checkBranchUD(newBranch)) {
                        company.branches.push(newBranch);
                    }
                }

                $timeout(function () {
                    return company;
                }, 0);
            },
        };

        $scope.method = {
            checkFactory: function (item) {
                for (var i = 0; i < $scope.data.branches.length; i++) {
                    var sItem = $scope.data.branches[i];
                    if (sItem.factoryID === item.factoryID) {
                        return true;
                    }
                }
                return false;
            },
            checkBranchUD: function (item) {
                for (var i = 0; i < $scope.data.branches.length; i++) {
                    var sItem = $scope.data.branches[i];
                    if (sItem.branchUD === item.branchUD && sItem.branchID !== item.branchID) {
                        return true;
                    }
                }
                return false;
            },
            getNewBranchID: function () {
                return $scope.branchID = $scope.branchID - 1;
            },
            resetNewBranch: function () {
                $scope.autoBranch.initBranch = null;

                // Reset value new Branch
                $scope.newBranch = {
                    branchID: null,
                    branchUD: null,
                    branchNM: null,
                    companyID: null,
                    streetAddress: null,
                    phone: null,
                    fax: null,
                    email: null,
                    factoryID: null,
                    factoryUD: null,
                    isDefault: null,
                    remark: null,
                };
            },
        };

        //
        //implement function for event
        //
        function init() {
            companybranchMngService.serviceUrl = context.serviceUrl;
            companybranchMngService.supportServiceUrl = context.supportServiceUrl;
            companybranchMngService.token = context.token;
            companybranchMngService.getInit(
            function (data) {
                $scope.locations = data.data.locations;
                $scope.factories = data.data.factories;

                $scope.event.load();
                //$scope.locations = data.data.locations;
                //var addsItem = [];
                //for (var i = 0; i < data.data.factories.length; i++) {
                //    var sItem = data.data.factories[i];
                //    if (!$scope.method.checkFactory(sItem)) {
                //        addsItem.push(sItem);
                //    }
                //}
                //$scope.factories = addsItem;
                //$timeout(function () {
                //    $scope.factories = addsItem;
                //}, 0);
                //jQuery('#content').show();
            },
            function (error) {
                // do nothing
            });
        };

        function load() {
            companybranchMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.timeRanges = data.data.timeRanges;
                    jQuery('#content').show();
                },
                function (error) {
                    // do nothing
                });
        };

        function get() {
        };

        function del(id) {
            companybranchMngService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if ($scope.data.companyUD === null || $scope.data.companyUD === '') {
                jsHelper.showMessage('warning', 'Please input CompanyUD.');
                return false;
            }

            if ($scope.data.companyNM === null || $scope.data.companyNM === '') {
                jsHelper.showMessage('warning', 'Please input CompanyNM.');
                return false;
            }

            if ($scope.data.shortName === null || $scope.data.shortName === '') {
                jsHelper.showMessage('warning', 'Please input Short Name.');
                return false;
            }

            if ($scope.data.officialName === null || $scope.data.officialName === '') {
                jsHelper.showMessage('warning', 'Please input Official Name.');
                return false;
            }

            for (var i = 0; i < $scope.data.branches.length; i++) {
                var sItem = $scope.data.branches[i];
                if (sItem.branchID <= 0) {
                    if ($scope.method.checkBranchUD(sItem)) {
                        jsHelper.showMessage('warning', 'Branch Code: ' + sItem.branchUD + ' already exist.');
                        return false;
                    }
                }
            }

            if ($scope.editForm.$valid) {
                companybranchMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.companyID;
                            $scope.data = data.data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.message.message);
                    });
            } else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        };

        function remove($event, item) {
            //$event.preventDefault();
            var confirmedMsg = 'Delete ' + item.branchUD + ' ?'
            if (confirm(confirmedMsg) === true) {
                var index = $scope.data.branches.indexOf(item);
                $scope.data.branches.splice(index, 1);
                $scope.totalRowsScan = $scope.data.branches.length;
            }

        };
        //function approve() {
        //    if (confirm('Are you sure ?')) {
        //        companybranchMngService.approve(
        //            context.id,
        //            $scope.data,
        //            function (data) {
        //                window.location = context.refreshUrl + data.data.purchaseOrderID;
        //            },
        //            function (error) {
        //                jsHelper.showMessage('warning', error);
        //            });
        //    }
        //}


        //
        //init form
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
        
        //$scope.event.load();
    };
})();