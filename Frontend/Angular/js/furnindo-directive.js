angular.module('furnindo-directive', []);

angular.module('furnindo-directive', ['ui.bootstrap']).directive('ngConfirmClick', ['$modal', function ($modal) {
    return {
        restrict: 'A',
        scope: {
            ngConfirmClick: "&",
            item: "=",
            selectedItem: "="
        },
        link: function (scope, element, attrs) {
            if (element.context.tagName === 'INPUT' || element.context.tagName === 'SELECT') {
                var popupCloseEvent = attrs.popupcloseevent;
                var controlID = attrs.id;
                element.change(function () {
                    var ModalInstanceCtrl = function ($scope, $modalInstance, confirmDialogFactory) {
                        $scope.ok = function () {
                            confirmDialogFactory.setSelected(controlID, scope.selectedItem);
                            $scope.$emit(popupCloseEvent, { result: true, selectedItem: confirmDialogFactory.getSelected(controlID) });
                            $modalInstance.close();
                        };
                        $scope.cancel = function () {
                            $scope.$emit(popupCloseEvent, { result: false, selectedItem: confirmDialogFactory.getSelected(controlID) });
                            $modalInstance.dismiss('cancel');
                        };
                    };
                    var message = attrs.ngConfirmMessage || "Are you sure you want to change?";
                    var modalHtml = '<div class="modal-body">' + message + '</div>';
                    modalHtml += '<div class="modal-footer"><button class="btn btn-success" ng-click="ok()">Yes</button><button class="btn btn-danger" ng-click="cancel()">No</button></div>';
                    var modalInstance = $modal.open({
                        template: modalHtml,
                        controller: ModalInstanceCtrl,
                        scope: scope
                    });
                    modalInstance.result.then(function () {
                        scope.ngConfirmClick({
                            item: scope.item
                        });
                    }, function () { });

                });
            }

        }
    }
}
]).factory('confirmDialogFactory', function () {
    this.data = [];
    var setSelected = function (controlID, newObj) {
        if (this.data === undefined) {
            this.data = [];
            this.data.push({ controlID: controlID, newObj: newObj });
        }
        else {
            x = this.data.filter(function (o) { return o.controlID === controlID });
            if (x.length === 0) {
                this.data.push({ controlID: controlID, newObj: newObj });
            }
            else {
                x[0].newObj = newObj;
            }
        }
    }
    var getSelected = function (controlID) {
        return this.data.filter(function (o) { return o.controlID === controlID })[0].newObj;
    }
    return {
        setSelected: setSelected,
        getSelected: getSelected
    };
});

// currency box
angular.module('furnindo-directive').directive('furnindoCurrency', function ($filter) {
    return {
        require: 'ngModel',
        scope: {
            symbol: '@'
        },
        link: function (scope, element, attrs, controller) {
            controller.$parsers.unshift(returnValue);
            controller.$formatters.unshift(formatValue);

            function formatValue(value) {
                if (isNaN(parseFloat(value)) || parseFloat(value) === 0) {
                    return '0,00';
                }
                return $filter('currency')(parseFloat(value), '', 2);
            }

            function returnValue(value) {
                value = value.replace(/['.']/g, '').replace(/,/g, '.');
                if (isNaN(parseFloat(value)) || parseFloat(value) === 0) {
                    return '0.00';
                }
                return value;
            }

            //
            // EVENT
            //
            jQuery(element).bind('keydown', function (e) {
                if (e.keyCode === 110 || e.keyCode === 190) { // catch if user press the . key
                    e.preventDefault();
                    if (jQuery(element).val().indexOf(',') < 0) {
                        jQuery(element).val(jQuery(element).val() + ','); // replace . with ,
                    }
                }
            });

            jQuery(element).bind('click', function (e) {
                jQuery(element).val(jQuery(element).val().replace(/['.']/g, ''));
                jQuery(element).select();
            });
            jQuery(element).bind('focus', function (e) {
                jQuery(element).val(jQuery(element).val().replace(/['.']/g, ''));
                jQuery(element).select();
            });

            jQuery(element).bind('blur', function (e) {
                jQuery(element).val(formatCurrency(jQuery(element).val()));
                jQuery(element).trigger('input');
            });

            //
            // PRIVATE Function
            //
            function formatCurrency(value) {
                value = value.replace(/['.']/g, '').replace(/,/g, '.');
                if (isNaN(parseInt(value)) || parseInt(value) === 0) {
                    value = 0;
                }
                value = $filter('currency')(parseFloat(value), '', 2);
                return value.trim();
            }
        }
    }
});

angular.module('furnindo-directive').directive('furnindoCurrencyNullable', function ($filter) {
    return {
        require: 'ngModel',
        scope: {
            symbol: '@'
        },
        link: function (scope, element, attrs, controller) {
            controller.$parsers.unshift(returnValue);
            controller.$formatters.unshift(formatValue);

            function formatValue(value) {
                return $filter('currency')(parseFloat(value), '', 2);
            }

            function returnValue(value) {
                value = value.replace(/['.']/g, '').replace(/,/g, '.');
                return value;
            }

            //
            // EVENT
            //
            jQuery(element).bind('keydown', function (e) {
                if (e.keyCode === 110 || e.keyCode === 190) { // catch if user press the . key
                    e.preventDefault();
                    if (jQuery(element).val().indexOf(',') < 0) {
                        jQuery(element).val(jQuery(element).val() + ','); // replace . with ,
                    }
                }
            });

            jQuery(element).bind('click', function (e) {
                jQuery(element).val(jQuery(element).val().replace(/['.']/g, ''));
                jQuery(element).select();
            });
            jQuery(element).bind('focus', function (e) {
                jQuery(element).val(jQuery(element).val().replace(/['.']/g, ''));
                jQuery(element).select();
            });
            jQuery(element).bind('blur', function (e) {
                jQuery(element).val(formatCurrency(jQuery(element).val()));
                jQuery(element).trigger('input');
            });

            //
            // PRIVATE Function
            //
            function formatCurrency(value) {
                value = value.replace(/['.']/g, '').replace(/,/g, '.');
                value = $filter('currency')(parseFloat(value), '', 2);
                return value.trim();
            }
        }
    }
});

// image & video box
angular.module('furnindo-directive').directive('furnindoImagebox', function () {
    return {
        scope: {
            donchange: '&',
            donremove: '&',
            dthumbnailurl: '=',
            dfileurl: '=',
            dheight: '@'
        },
        template: '<div class="img" style="height: {{dheight}}; background-image: url({{dthumbnailurl}})"></div><button title="Change" ng-click="donchange()"><i class="fa fa-folder-open-o"></i> Change</button><button title="Remove" ng-click="donremove()"><i class="fa fa-folder-open-o"></i> Remove</button><a ng-if="dfileurl" class="btn btn-primary" target="_blank" href="{{dfileurl}}" style="padding: 6px 15px 6px 15px; margin-top: 11px; vertical-align: top;"><i class="fa fa-search"></i> View</a>'
    }
});


angular.module('furnindo-directive').directive('furnindoMultiSelect', function () {
    return {
        restrict: "E",
        scope: {
            source: "=",
            itemSelected: "="
        },
        template: function (element, attrs) {
            //get attribute from control
            directiveID = "_" + attrs.id;
            itemID = attrs.itemid;
            itemText = attrs.itemtext;

            //building template
            var t = "<select multiple='multiple' ng-model='itemSelected' id='" + directiveID + "'><option ng-repeat='item in source' ng-value='item." + itemID + "'>{{item." + itemText + "}}</option></select>";
            return t;
        },
        link: function (scope, elem, attrs, controllers) {

            scope.$watch('itemSelected', function (value) {
                var directiveID = "_" + attrs.id;
                $("#" + directiveID).select2({
                    closeOnSelect: true,
                });
            })
        }
    }
});


