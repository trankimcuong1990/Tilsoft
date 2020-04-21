//
// SUPPORT
//
jQuery('#main-form').keypress(function(e){
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.plcImageTypes = null;
    $scope.newid = 0;

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                context.bookingid,
                context.factoryid,
                context.offerlineid,
                context.offerlinesparepartid,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.plcImageTypes = data.data.plcImageTypes;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.plcImageTypes = null;
                    $scope.$apply();
                }
            );
        },
        update: function($event){
            $event.preventDefault();

            if($scope.editForm.$valid)
            {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            $scope.method.refresh(data.data.plcid);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', 'Input data is invalid, please check');
            }
        },
        delete: function($event){
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        approve: function ($event) {
            $event.preventDefault();
            if (confirm('Approve the current plc ?')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
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
        },
        reset: function ($event) {
            $event.preventDefault();
            if (confirm('Reset the current plc to pending status?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
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
        },
        changeImage: function (id) {
            var imageObject = null;
            jQuery.each($scope.data.plcImages, function () {
                if (this.plcImageID == id) {
                    imageObject = this;
                }
            });

            if (imageObject && imageObject != '' && typeof imageObject != undefined) {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            imageObject.imageFile_DisplayUrl = img.fileURL;
                            imageObject.imageFile_NewFile = img.filename;
                            imageObject.imageFile_HasChange = true;
                        }, null);
                    });
                };
                masterUploader.open();
            }
        },
        removeImage: function (id) {
            isExist = false;
            for (j = 0; j < $scope.data.plcImages.length; j++) {
                if ($scope.data.plcImages[j].plcImageID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.plcImages.splice(j, 1);
            }
        },
        addImage: function () {
            var newId = $scope.method.getNewID();
            $scope.data.plcImages.push({
                plcImageID: newId,
                imageTypeID: null,
                imageFile: null,
                imageFile_HasChange: false,
                imageFile_NewFile: '',
                imageFile_DisplayUrl: '',
                imageFile_FullSizeUrl: ''
            });
            $scope.event.changeImage(newId);
        },
        onRating: function () {
            $scope.data.isRated = true;
        },
        print: function () {
            jsonService.getReport(
                context.id,
                function (data) {
                    window.location = context.backendReportUrl + data.data + ".xlsm";

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
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        },
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);