angular.module('tilsoftApp').service('factoryAccessService', ['$rootScope', 'dataService', function ($rootScope, dataService,$scope) {
    this.data = null;
    this.parentScope = null;
    this.checkvaluetrue = true;
    this.checkvaluetruenotification = false;

    this.openForm = function () {
        this.data = angular.copy(this.parentScope.data.factoryAccesses);
        for (var i = 0; i < this.parentScope.data.factoryAccesses.length; i++) {
            if (this.parentScope.data.factoryAccesses[i].canAccess == false) {
                this.checkvaluetrue = false;
                break;
            }
        }
        jsHelper.showPopup('frmFactoryAccess', function () {
            var elements = jQuery('#frmFactoryAccess').find('.tilsoft-table-body');
            var elemObj = null;
            if (elements.length > 0) {
                elemObj = jQuery(elements[0]);
                elemObj.css('height', jQuery(window).height() - 51 - 50 - 42 - 25 - elemObj.offset().top);
                // 51: footer height
                // 50: margin
                // 42: form toolbar height at bottom
                // 25: safety
            }
        });
        this.parentScope.gridHandler.refresh();
    };

    this.closeForm = function () {
        jsHelper.hidePopup('frmFactoryAccess', function () {
            jsHelper.goToSection('factory-access-section');
        });
    };

    this.selectAll = function (item)
    {
        if (this.checkvaluetrue === true)
        {
            angular.forEach(this.data, function (sItem) {
                sItem.canAccess = true;
            });
        }
        if (this.checkvaluetrue === false) {
            angular.forEach(this.data, function (sItem) {
                sItem.canAccess = false;
            });
        }
    },
   this.selectnotification= function(item)
    {
       if(this.checkvaluetruenotification===true)
       {
           angular.forEach(this.data, function (checkitem) {
               checkitem.canReceiveNotification = true;
           });
       }
       else {
           angular.forEach(this.data, function (checkitem) {
               checkitem.canReceiveNotification = false;
           });
       }
    }

    this.update = function () {
        if (this.parentScope.frmFactoryAccess.$valid) {
            dataService.updateFactoryAccess(
                context.id,
                this.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        $rootScope.$broadcast('refresh');
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
    };
}]);