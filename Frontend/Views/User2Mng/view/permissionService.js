angular.module('tilsoftApp').service('permissionService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;
    this.parentScope = null;

    this.openForm = function () {
        this.data = angular.copy(this.parentScope.data.permissions);
        jsHelper.showPopup('frmPermission', function () {
            var elements = jQuery('#frmPermission').find('.tilsoft-table-body');
            var elemObj = null;
            if (elements.length > 0) {
                elemObj = jQuery(elements[0]);
                elemObj.css('height', jQuery(window).height() - 51 - 50 - 42 + 25 - elemObj.offset().top);
                // 51: footer height
                // 50: margin
                // 42: form toolbar height at bottom
                // 25: safety
            }
        });
        this.parentScope.gridHandler.refresh();
    };

    this.closeForm = function () {
        jsHelper.hidePopup('frmPermission', function () {
            jsHelper.goToSection('permission-section');
        });
    };

    this.update = function () {
        if (this.parentScope.frmPermission.$valid) {
            dataService.updatePermission(
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