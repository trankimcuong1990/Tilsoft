var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive', 'ngRoute', 'ngCookies', 'customFilters', 'ui']);

tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', '$routeParams', 'dataService', function ($scope, $timeout, $routeParams, dataService) {
    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;  
    dataService.token = context.token;

    //
    //property
    //
    $scope.data = null;
    $scope.newID = -1;

    //
    //event
    //
    $scope.event = {
        changeFileUD: changeFileUD,
        removeFileUD: removeFileUD,
        autoDate: autoDate
    }

    $scope.selectedTab = function (tabName) {
        //remove all active class of tab
        $("#tab-content-detail > div").removeClass("active");
        //add active class to selected tab
        $("#" + tabName).addClass("active");
    };
    //
    //load data
    //
    $scope.load = function () {       
        dataService.load(
            context.id,
            null,
            function (data) {
                $scope.data = data.data; 

                //
                //show content
                //
                jQuery('#content').show();
            },
            function (error) {
                $scope.data = null;
            }
        );
    };

    $scope.update = function ($event) {
        $event.preventDefault();

        //validate
        if (!$scope.data.complianceProcessDTO.complianceCertificateTypeID) {
            jsHelper.showMessage("warning", "Certificate Type is required");
            return;
        }

        if ($scope.editForm.$valid) {
            dataService.update(
                context.id,
                $scope.data.complianceProcessDTO,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.refreshUrl + data.data.complianceProcessID;                    
                    }
                },            
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        } else {
            jsHelper.showMessage("warning", context.msgValid);
        }
    };

    function changeFileUD ($event, controlID, typeID) {
        masterUploader.multiSelect = true;
        masterUploader.imageOnly = false;
        masterUploader.callback = function () {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                angular.forEach(masterUploader.selectedFiles, function (img) {
                    switch (typeID) {
                        case 1:
                            var arr = scope.data.complianceProcessDTO.complianceAttachedFileDTOs.filter(function (o) { return o.complianceAttachedFileID == controlID });
                            break;
                        case 2:
                            var arr = scope.data.complianceProcessDTO.compliancePICDTOs.filter(function (o) { return o.compliancePICID == controlID });
                            break;
                    }
                    $(arr).each(function () {
                        this.fileUDUrl = img.fileURL;
                        this.fileUDFriendlyName = img.filename;
                        this.fileUDHasChange = true;
                        this.newfileUD = img.filename;
                    });
                }, null);
            });
        };
        masterUploader.open();
    }

    function removeFileUD ($event, controlID, typeID) {
        switch (typeID) {
            case 1:
                var arr = scope.data.complianceProcessDTO.complianceAttachedFileDTOs.filter(function (o) { return o.complianceAttachedFileID == controlID });
                break;  
            case 2:
                var arr = scope.data.complianceProcessDTO.compliancePICDTOs.filter(function (o) { return o.compliancePICID == controlID });
                break;  
        }
        $(arr).each(function () {
            this.fileUDUrl = '';
            this.fileUDFriendlyName = '';        
            this.fileUDHasChange = true;
            this.newfileUD = '';
            this.fileUD = '';
        });
    }

    function autoDate(expiredDate) { 

        var ngay = expiredDate.substring(0, 2);
        var thang = expiredDate.substring(3, 5);
        var nam = expiredDate.substring(6, 10);
        var tempDate = new Date(nam + '/' + thang + '/' + ngay);
      
        //auto bind status if expiredDate <= now 
        if (tempDate <= new Date())          
            scope.data.complianceProcessDTO.auditStatusID = 4; //expired

        //gen renewDate and fictiveDate
        if (scope.data.complianceProcessDTO.renewDate == null && scope.data.complianceProcessDTO.fictiveDate == null) {

            tempDate.setMonth(tempDate.getMonth() - 2);
            var formattedNewDate = tempDate.toISOString().slice(0, 10);

            var newDay = formattedNewDate.substring(8, 10);
            var newMonth = formattedNewDate.substring(5, 7);
            var newYear = formattedNewDate.substring(0, 4);
            var newDate = newDay + '/' + newMonth + '/' + newYear;

            scope.data.complianceProcessDTO.renewDate = newDate;
            scope.data.complianceProcessDTO.fictiveDate = newDate;
        }   
    }

    $scope.removeAttachedFile = function removeAttachedFile($event, id) {
        if ($event !== null) {
            $event.preventDefault();
        }
        var isExist = false;
        for (var j = 0; j < $scope.data.complianceProcessDTO.complianceAttachedFileDTOs.length; j++) {
            if ($scope.data.complianceProcessDTO.complianceAttachedFileDTOs[j].complianceAttachedFileID === id) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.data.complianceProcessDTO.complianceAttachedFileDTOs.splice(j, 1);
        }
    };

    $scope.addAttachedFile = function addAttachedFile($event) {
        if ($event !== null) {
            $event.preventDefault();
        }
        $scope.data.complianceProcessDTO.complianceAttachedFileDTOs.push({
            complianceAttachedFileID: $scope.method.getNewID(),
            isCertificate: 0,
            fileUD:''
        });
    };

    $scope.removePIC = function removePIC($event, id) {
        if ($event !== null) {
            $event.preventDefault();
        }
        debugger;
        var isExist = false;
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = dd + '/' + mm + '/' + yyyy;
        for (var j = 0; j < $scope.data.complianceProcessDTO.compliancePICDTOs.length; j++) {
            var item = $scope.data.complianceProcessDTO.compliancePICDTOs[j];
            if (item.preparationTimeFrom >= today) {
                if (item.compliancePICID === id) {
                    isExist = true;
                    break;
                }
            }            
        }
        if (isExist) {
            $scope.data.complianceProcessDTO.compliancePICDTOs.splice(j, 1);
        }
    };

    $scope.addPIC = function addPIC($event) {
        if ($event !== null) {
            $event.preventDefault();
        }
        $scope.data.complianceProcessDTO.compliancePICDTOs.push({
            compliancePICID: $scope.method.getNewID(),
            employeeID: null,
            preparationTimeFrom: null,
            preparationTimeTo: null,
            remark: '',
        });
    };

    $scope.method = {
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        checkCertificate: function ($event, data) {
            for (var j = 0; j < $scope.data.complianceProcessDTO.complianceAttachedFileDTOs.length; j++) {
                if ($scope.data.complianceProcessDTO.complianceAttachedFileDTOs[j].complianceAttachedFileID !== data.complianceAttachedFileID
                    && $scope.data.complianceProcessDTO.complianceAttachedFileDTOs[j].isCertificate) {
                    data.isCertificate = 0;
                }
            }
        }
    };

    //
    //init controller
    // 
    $scope.load();

}]);
