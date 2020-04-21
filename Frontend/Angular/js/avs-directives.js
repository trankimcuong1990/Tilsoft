angular.module('avs-directives', []);

// scroller
angular.module('avs-directives').directive('avsTable', function ($timeout, $compile) {
    return {
        restrict: 'A',
        scope: {
            handler: '=gridHandler'
        },
        link: function (scope, elem, attrs) {
            var obj = angular.element(elem);
            if (scope.handler) {
                scope.handler.init = true;
                scope.handler.goTop = function () {
                    obj.find('.avs-table-body').each(function () {
                        angular.element(this).scrollTop(0);
                    });
                };
            }

            obj.find('.avs-table-header tr td').each(function () {                
                if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                    angular.element(this).on('click', function () {
                        var sortDirection = 'asc';
                        if (angular.element(this).hasClass('sorting_asc')) {
                            sortDirection = 'desc';
                        }
                        else {
                            sortDirection = 'asc';
                        }

                        angular.element(this).parent().find('td').each(function () {
                            if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                                angular.element(this).removeClass('sorting_desc');
                                angular.element(this).removeClass('sorting_asc');
                                angular.element(this).removeClass('sorting');
                                angular.element(this).addClass('sorting');
                            }
                        });

                        angular.element(this).removeClass('sorting');
                        if (sortDirection === 'asc') {
                            angular.element(this).addClass('sorting_asc');
                        }
                        else {
                            angular.element(this).addClass('sorting_desc');
                        }
                        scope.handler.sort(angular.element(this).data('colname'), sortDirection);
                    });
                }
            });

            obj.find('.avs-table-body').each(function () {
                this.scrollLeft = 0;
                $(this).parent().find('.avs-table-header')[0].scrollLeft = this.scrollLeft;

                // declare event on scroll for table body
                angular.element(this).on('scroll', function () {
                    var scrollLeft = this.scrollLeft;
                    $(this).parent().find('.avs-table-header')[0].scrollLeft = scrollLeft;

                    if (scope.handler.isTriggered) {
                        if (this.scrollTopMax) {
                            if (angular.element(this).scrollTop() >= this.scrollTopMax) { // reach the end of the scroll
                                scope.handler.loadMore();
                            }
                        }
                        else {
                            if (Math.round(angular.element(this).scrollTop()) >= this.scrollHeight - this.clientHeight) { // reach the end of the scroll
                                scope.handler.loadMore();
                            }
                        }
                    }

                    //
                    //tracking top, left position
                    //get scroll position so we can back to exactly position of row is openning
                    if (scope.handler.getScrollPosistion) {
                        var tableBodyObject = angular.element(this);
                        scope.handler.getScrollPosistion(tableBodyObject.scrollTop(), tableBodyObject.scrollLeft());      
                    }                                  
                });
            });

            // set column width
            $timeout(function () {
                var jHeader = obj.find('.avs-table-header table tr.header-row');                
                var colIndex = 0;
                var colWidth = [];
                var totalWidth = 0;
                $.each(jHeader[0].cells, function (i, item) {
                    if ($(item).inlineStyle('width')) { // && !$(item).hasClass('ng-hide')
                        var colSpan = parseInt(item.colSpan);
                        for (var j = 1; j <= colSpan; j++) {
                            var width = parseInt($(item).inlineStyle('width').replace('px', '')) / colSpan;
                            colWidth.push({
                                width: width,
                                condition: $(item).attr('ng-if')
                            });
                        }
                        totalWidth += parseInt($(item).inlineStyle('width').replace('px', ''));
                    }
                });
                if (obj.find('.avs-table-body table colgroup').length === 0) {                     
                    // using dummy row
                    var dummyRow = '<tr class="dummy-row">';
                    for (colIndex = 0; colIndex < colWidth.length; colIndex++) {
                        if (colWidth[colIndex].condition) {
                            dummyRow += '<td style="width: ' + colWidth[colIndex].width + 'px;" ng-if="' + colWidth[colIndex].condition + '"></td>';
                        }
                        else {
                            dummyRow += '<td style="width: ' + colWidth[colIndex].width + 'px;">&nbsp;</td>';
                        }
                    }
                    dummyRow += '</tr>';
                    obj.find('.avs-table-body table').append(dummyRow);
                    $compile(obj.find('.avs-table-body table .dummy-row'))(scope.$parent);
                }
                obj.find('.avs-table-body table').css('width', totalWidth + 'px');
                obj.find('.avs-table-header table').css('width', totalWidth + 'px');
                obj.find('.avs-table-header-container').css('width', (totalWidth + 50) + 'px');

                scope.handler.init = false;
            }, 0);
        }
    };
});

angular.module('avs-directives').directive('avsScroll', function ($timeout) {
    return {
        restrict: 'A',
        scope: {
            handler: '=gridHandler'
        },
        link: function (scope, elem, attrs) {
            var obj = angular.element(elem);
            scope.$watch(function () { return angular.element('#content').is(':visible') }, function () {
                if (angular.element('#content').is(':visible')) {
                    scope.handler.refresh();
                }
            });

            scope.handler.goTop = function () {
                obj.find('.tilsoft-table').each(function () {
                    angular.element(this).scrollTop(0);
                });
            }

            scope.handler.refresh = function () {
                $('.tilsoft-table-wrapper[avs-scroll=""] .tilsoft-table').each(function () {
                    var jObject = $(this);
                    var jHeader = jObject.find('.tilsoft-table-header');
                    var jFilter = jObject.find('.tilsoft-table-filter');
                    var jTotalRow = jObject.find('.tilsoft-table-totalrow');
                    var jSubTotalRow = jObject.find('.tilsoft-table-subtotalrow');
                    var initHeight = 0;
                    jHeader.css('top', jObject.scrollTop());
                    initHeight = parseInt(jHeader.height()) || parseInt(jHeader.clientHeight);
                    if (jFilter.length > 0) {
                        jFilter.css('top', jObject.scrollTop() + initHeight);
                        initHeight += parseInt(jFilter.height()) || parseInt(jFilter.clientHeight);
                    }
                    if (jTotalRow.length > 0) {
                        jTotalRow.css('top', jObject.scrollTop() + initHeight);
                        initHeight += parseInt(jTotalRow.height()) || parseInt(jTotalRow.clientHeight);
                    }
                    if (jSubTotalRow.length > 0) {
                        jSubTotalRow.css('top', jObject.scrollTop() + initHeight);
                        initHeight += parseInt(jSubTotalRow.height()) || parseInt(jSubTotalRow.clientHeight);
                    }
                    jObject.find('.tilsoft-table-body').css('margin-top', initHeight + 'px');
                });
            }

            obj.find('.tilsoft-table-header div').each(function () {
                if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                    angular.element(this).on('click', function () {
                        var sortDirection = 'asc';
                        if (angular.element(this).hasClass('sorting_asc')) {
                            sortDirection = 'desc';
                        }
                        else {
                            sortDirection = 'asc';
                        }

                        angular.element(this).parent().find('div').each(function () {
                            if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                                angular.element(this).removeClass('sorting_desc');
                                angular.element(this).removeClass('sorting_asc');
                                angular.element(this).removeClass('sorting');
                                angular.element(this).addClass('sorting');
                            }
                        });

                        angular.element(this).removeClass('sorting');
                        if (sortDirection === 'asc') {
                            angular.element(this).addClass('sorting_asc');
                        }
                        else {
                            angular.element(this).addClass('sorting_desc');
                        }
                        scope.handler.sort(angular.element(this).data('colname'), sortDirection);
                    });
                }
            });

            obj.find('.tilsoft-table').each(function () {
                angular.element(this).on('scroll', function () {
                    var jObject = angular.element(this);
                    var jHeader = jObject.find('.tilsoft-table-header');
                    var jFilter = jObject.find('.tilsoft-table-filter');
                    var jTotalRow = jObject.find('.tilsoft-table-totalrow');
                    var jSubTotalRow = jObject.find('.tilsoft-table-subtotalrow');
                    var initHeight = 0;

                    jHeader.css('top', jObject.scrollTop());
                    initHeight = parseInt(jHeader.css('height'));

                    if (jFilter.length > 0) {
                        jFilter.css('top', jObject.scrollTop() + initHeight);
                        initHeight += parseInt(jFilter.css('height'))
                    }

                    if (jTotalRow.length > 0) {
                        jTotalRow.css('top', jObject.scrollTop() + initHeight);
                        initHeight += parseInt(jTotalRow.css('height'))
                    }

                    if (jSubTotalRow.length > 0) {
                        jSubTotalRow.css('top', jObject.scrollTop() + initHeight);
                    }

                    if (scope.handler.isTriggered) {
                        if (this.scrollTopMax) {
                            if (angular.element(this).scrollTop() >= this.scrollTopMax) { // reach the end of the scroll
                                scope.handler.loadMore();
                            }
                        }
                        else {
                            if (Math.round(angular.element(this).scrollTop()) >= this.scrollHeight - this.clientHeight) { // reach the end of the scroll
                                scope.handler.loadMore();
                            }
                        }
                    }
                });
            });
        }
    }
});

// local auto completed
angular.module('avs-directives').directive('avsLocalAutoComplete', function ($parse) {
    return {
        link: function (scope, element, attrs) {
            var dataItemValue = $parse(attrs.ngItemValue);
            var dataUiItems = $parse(attrs.ngUiItems)(scope);
            var functionSelected = $parse(attrs.ngItemSelected);

            scope.$watch(dataItemValue, function (value) {
                element.val('');
                angular.forEach(dataUiItems, function (item, key) {
                    if (item.id == value) {
                        element.val(item.label);
                    }
                }, null);
            });

            //
            // DOM manipulation
            //
            element.autocomplete({
                source: dataUiItems,
                minLength: 3,
                select: function (event, ui) {
                    dataItemValue.assign(scope, ui.item.id);
                    scope.$apply();
                    functionSelected(scope, null);
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        dataItemValue.assign(scope, null);
                        scope.$apply();
                        element.val('');
                        functionSelected(scope, null);
                    }
                },
            })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return jQuery("<li>")
                        .data('item.autocomplete', item)
                        .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                        .appendTo(ul);
                };

            //
            // UI Handler
            //
            if (attrs.ngUiHandler) {
                var handlerObj = $parse(attrs.ngUiHandler);
                handlerObj.assign(scope, {
                    setData: function () {
                        alert('set data from handler');
                        element.val('');
                        dataItemValue.assign(scope, null);
                    }
                });
            }
        }
    }
});

// remote auto complete
angular.module('avs-directives').directive('avsRemoteAutoComplete', function ($parse) {
    return {
        link: function (scope, element, attrs) {
            var dataItemValue = $parse(attrs.ngItemValue);
            var dataRequestParam = $parse(attrs.ngRequestParam)(scope);
            var functionSelected = $parse(attrs.ngItemSelected);

            var dataMethod = $(element).attr('data-method') || 'GET'
            var dataFormatFunction = $(element).attr('data-format-function');

            //
            // DOM manipulation
            //
            element.autocomplete({
                source: function (request, response) {
                    var getUrl = '';
                    var dataInitParam = $parse(attrs.ngInitParam)(scope);
                    getUrl = getUrl + dataRequestParam.url + '?param=' + dataInitParam;

                    var dataInitParam2 = $parse(attrs.ngInitParam2)(scope);
                    if (dataInitParam2 !== undefined && dataInitParam2 !== null) {
                        getUrl = getUrl + '&param2=' + dataInitParam2;
                    }

                    var dataInitParam3 = $parse(attrs.ngInitParam3)(scope);
                    if (dataInitParam3 !== undefined && dataInitParam3 !== null) {
                        getUrl = getUrl + '&param3=' + dataInitParam3;
                    }

                    /// param 4
                    var dataInitParam4 = $parse(attrs.ngInitParam4)(scope);
                    if (dataInitParam4 !== undefined && dataInitParam4 !== null) {
                        getUrl = getUrl + '&param4=' + dataInitParam4;
                    }

                    /// param 5
                    var dataInitParam5 = $parse(attrs.ngInitParam5)(scope);
                    if (dataInitParam5 !== undefined && dataInitParam5 !== null) {
                        getUrl = getUrl + '&param5=' + dataInitParam5;
                    }

                    jQuery.ajax({
                        url: getUrl,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            'Authorization': 'Bearer ' + dataRequestParam.token
                        },
                        type: dataMethod,
                        dataType: "json",
                        data: {
                            term: request.term
                        },
                        success: function (data) {
                            if (dataFormatFunction) {
                                response(window[dataFormatFunction](data));
                            }
                            else {
                                response(data);
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(jqXHR);
                            console.log(textStatus);
                            console.log(errorThrown);
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    dataItemValue.assign(scope, ui.item);
                    scope.$apply();
                    functionSelected(scope, null);
                },
                change: function (event, ui) {
                    //if (ui.item == null) {
                    //    return;
                    //}

                    if (!ui.item) {
                        dataItemValue.assign(scope, null);
                        scope.$apply();
                        element.val('');
                        functionSelected(scope, null);
                    }
                },
            })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return jQuery("<li>")
                        .data('item.autocomplete', item)
                        .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                        .appendTo(ul);
                };

            //
            // UI Handler
            //
            if (attrs.ngUiHandler) {
                var handlerObj = $parse(attrs.ngUiHandler);
                handlerObj.assign(scope, {
                    setData: function (id, label) {
                        dataItemValue.assign(scope, id);
                        element.val(label);
                    }
                });
            }
        }
    }
});

//
// CALENDAR BOX
//
angular.module('avs-directives').directive('avsCalendar', ['$locale', '$parse', function ($locale, $parse) {
    return {
        link: function (scope, elem, attrs) {
            var dataSource = $parse(attrs.ngDataSource);
            var initValue = $parse(attrs.ngDataSource)(scope);
            var functionOnChange = $parse(attrs.ngOnChange);

            //
            // WATCH CHANGES DYNAMICALLY
            //
            scope.$watch(attrs.ngDataSource, function (value) {
                jQuery(elem).val($parse(attrs.ngDataSource)(scope));
            });

            /*
			 * JQUERY UI DATE
			 * Dependency: js/libs/jquery-ui-1.10.3.min.js
			 */
            if (jQuery.fn.datepicker) {
                var localeDateFormat = 'dd/mm/yy';
                switch ($locale.id) {
                    case 'nl-nl':
                        localeDateFormat = 'dd/mm/yy';
                        break;

                    default:
                        localeDateFormat = 'mm/dd/yy';
                        break;
                }

                var $this = jQuery(elem),
                    dataDateFormat = $this.attr('data-dateformat') || localeDateFormat;

                $this.datepicker({
                    dateFormat: dataDateFormat,
                    prevText: '<i class="fa fa-chevron-left"></i>',
                    nextText: '<i class="fa fa-chevron-right"></i>',
                    onSelect: function (date) {
                        dataSource.assign(scope, date);
                        functionOnChange(scope, null);
                        scope.$apply();
                    }
                });
                $this.val(initValue);

                //clear memory reference
                $this = null;
            }
        }
    }
}]);

//
// CURRENCY BOX
// options: 
// -- ng-currency-symbol: the currency symbol, support html. Ex: &euro;. Default value will be used if not declare: default value = '$'
// -- ng-decimal: number of digit for decimal. Ex: ng-decimal=2 => xxx,xx. Default value will be used if not declare: default value = 2 
//
angular.module('avs-directives').directive('avsCurrency', ['$locale', '$parse', function ($locale, $parse) {
    return {
        link: function (scope, elem, attrs) {
            var dataSource = $parse(attrs.ngDataSource);
            var initValue = $parse(attrs.ngDataSource)(scope);
            var functionOnChange = $parse(attrs.ngOnChange);

            //
            // WATCH CHANGES DYNAMICALLY
            //
            scope.$watch(attrs.ngDataSource, function (value) {
                if (value !== undefined) {
                    jQuery(elem).autoNumeric('set', value);
                }
                else {
                    jQuery(elem).autoNumeric('set', null);
                }
            });

            /*
			 * AUTO NUMERIC
			 * Dependency: js/libs/autoNumeric-1.9.41.js
			 */
            var localeSetting = { dec: ',', sep: '.', sym: attrs.ngCurrencySymbol || '$', mdec: attrs.ngDecimal || 2 };
            switch ($locale.id) {
                case 'nl-nl':
                    localeSetting.dec = ',';
                    localeSetting.sep = '.';
                    break;

                default:
                    localeSetting.dec = '.';
                    localeSetting.sep = ',';
                    break;
            }
            jQuery(elem).autoNumeric('init', { aSign: localeSetting.sym + ' ', aDec: localeSetting.dec, aSep: localeSetting.sep, lZero: 'deny', mDec: localeSetting.mdec });
            if (initValue === undefined) {
                initValue = null;
            }
            jQuery(elem).autoNumeric('set', initValue);

            //
            // EVENT
            //
            jQuery(elem).bind('change', function (e) {
                dataSource.assign(scope, jQuery(elem).autoNumeric('get'));
                functionOnChange(scope, null);
                scope.$apply();
            });
        }
    }
}]);

//
// NUMBER BOX
// options: 
// -- ng-decimal: number of digit for decimal. Ex: ng-decimal=2 => xxx,xx. Default value will be used if not declare: default value = 0
//
angular.module('avs-directives').directive('avsNumber', ['$locale', '$parse', function ($locale, $parse) {
    return {
        link: function (scope, elem, attrs) {
            var dataSource = $parse(attrs.ngDataSource);
            var initValue = $parse(attrs.ngDataSource)(scope);
            var functionOnChange = $parse(attrs.ngOnChange);

            //
            // WATCH CHANGES DYNAMICALLY
            //
            scope.$watch(attrs.ngDataSource, function (value) {
                if (value !== undefined) {
                    jQuery(elem).autoNumeric('set', value);
                }
                else {
                    jQuery(elem).autoNumeric('set', null);
                }
            });

            /*
			 * AUTO NUMERIC
			 * Dependency: js/libs/autoNumeric-1.9.41.js
			 */
            var localeSetting = { dec: ',', sep: '.', sym: '', mdec: attrs.ngDecimal || 0 };
            switch ($locale.id) {
                case 'nl-nl':
                    localeSetting.dec = ',';
                    localeSetting.sep = '.';
                    break;

                default:
                    localeSetting.dec = '.';
                    localeSetting.sep = ',';
                    break;
            }
            jQuery(elem).autoNumeric('init', { aSign: localeSetting.sym + ' ', aDec: localeSetting.dec, aSep: localeSetting.sep, lZero: 'deny', mDec: localeSetting.mdec });
            if (initValue === undefined) {
                initValue = null;
            }
            jQuery(elem).autoNumeric('set', initValue);

            //
            // EVENT
            //
            jQuery(elem).bind('change', function (e) {
                dataSource.assign(scope, jQuery(elem).autoNumeric('get'));
                functionOnChange(scope, null);
                scope.$apply();
            });
        }
    }
}]);


//
// SELECT 2
// Description: add null option <option></option> if you want to show the clear selection button
// Options: 
//		ng-data-source: data source field
//		ng-item-senected: event for selected item (including clear selected item - select null value)
//
angular.module('avs-directives').directive('avsSelect', ['$locale', '$parse', function ($locale, $parse) {
    return {
        link: function (scope, elem, attrs) {
            var dataSource = null;
            var initValue = null;
            if (attrs.ngDataSource) {
                dataSource = $parse(attrs.ngDataSource);
                initValue = $parse(attrs.ngDataSource)(scope);
            }

            var dataURL;
            if (attrs.ngDataUrl) {
                dataURL = $parse(attrs.ngDataUrl);
            }

            var dataToken;
            if (attrs.ngDataToken) {
                dataToken = $parse(attrs.ngDataToken);
            }

            var dataMethod = 'GET';
            var functionSelected = $parse(attrs.ngItemSelected);
            var dataFormatFunction = null;

            //
            // WATCH CHANGES DYNAMICALLY
            //
            if (attrs.ngDataSource) {
                scope.$watch(attrs.ngDataSource, function (value) {
                    if (value !== undefined) {
                        jQuery(elem).val(value).trigger('change');
                    }
                    else {
                        jQuery(elem).val(null).trigger('change');
                    }
                });
            }

            /*
			 * SELECT2 PLUGIN
			 * Usage:
			 * Dependency: js/plugin/select2/
			 */
            if (jQuery.fn.select2) {
                var $this = jQuery(elem),
                    width = $this.attr('data-select-width') || '100%';

                dataURL = $this.attr('data-url') || dataURL;
                dataToken = $this.attr('data-token') || dataToken;
                dataMethod = $this.attr('data-method') || dataMethod;
                dataFormatFunction = $this.attr('data-format-function') || dataMethod;

                if (dataURL && dataToken) {
                    $this.select2({
                        allowClear: true,
                        width: width,
                        ajax: {
                            url: dataURL,
                            dataType: 'json',
                            delay: 250,
                            data: function (params) {
                                return {
                                    query: params
                                };
                            },
                            results: function (data, params) {
                                if (dataFormatFunction) {
                                    return window[dataFormatFunction](data);
                                }
                                else {
                                    return {
                                        results: $.map(data, function (e, i) {
                                            return {
                                                id: e.optionID,
                                                text: e.optionUD,
                                                data: e
                                            }
                                        })
                                    };
                                }
                            },
                            cache: false,
                            transport: function (params, success, failure) {
                                params.headers = {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + dataToken
                                };
                                params.type = dataMethod;
                                var $request = $.ajax(params);
                                $request.then(success);
                                $request.fail(failure);
                                return $request;
                            }
                        },
                        minimumInputLength: 3
                    });
                }
                else {
                    $this.select2({
                        allowClear: true,
                        width: width
                    });
                }


                //clear memory reference
                $this = null;
            }

            //
            // EVENT
            //
            jQuery(elem).on("select2-selected", function (e) {
                var selectItem = jQuery(elem).select2('data');
                if (selectItem) {
                    if (dataSource) {
                        dataSource.assign(scope, selectItem.id);
                        scope.$apply();
                    }
                    functionSelected(scope, { item: selectItem });
                }
            });
            jQuery(elem).on("select2-clearing", function (e) {
                if (dataSource) {
                    dataSource.assign(scope, null);
                    scope.$apply();
                }
                functionSelected(scope, null);
            });
        }
    }
}]);

//
// SELECT OR OTHER
// Description: select from a list of options or add new item as text
// Options: 
//		ng-data-source: data source field
//		ng-text-source: text source field
//		ng-ui-items: local data source from memory
//		data-format-function: global function to parse data return from server
//		data-url: get data from server
//		data-token: token authentication
//		data-method: post or get, get is the default method
//
angular.module('avs-directives').directive('avsSelectOrOther', ['$locale', '$parse', function ($locale, $parse) {
    return {
        templateUrl: '//' + window.location.host + '/Angular/js/avs-directive/select-or-other.html',
        link: function (scope, elem, attrs) {
            var valueSource = $parse(attrs.ngDataSource);
            var textSource = $parse(attrs.ngTextSource);
            var referenceSource = $parse(attrs.ngReferenceSource);
            var dataUiItems = $parse(attrs.ngUiItems)(scope);

            //
            // JQUERY HANDLER
            //
            var $this = $(elem);
            // search handler
            $this.find('.search-button-handler').click(function () {
                $(elem).find('.default-view').hide();
                $(elem).find('.select-editor').show();
            });
            $this.find('.select-editor').find('.ok-button-handler').click(function () {
                $(elem).find('.default-view').show();
                $(elem).find('.select-editor').hide();

                valueSource.assign(scope, $(elem).find('.select-editor').find('.hidden-value').val());
                if (attrs.ngReferenceSource) {
                    referenceSource.assign(scope, $(elem).find('.select-editor').find('.hidden-reference').val());
                }
                textSource.assign(scope, $(elem).find('.select-editor').find('.select-input').val());
                scope.$apply();

                $(elem).find('.select-editor').find('.hidden-value').val('');
                $(elem).find('.select-editor').find('.hidden-reference').val('');
                $(elem).find('.select-editor').find('.select-input').val('');
            });
            $this.find('.select-editor').find('.cancel-button-handler').click(function () {
                $(elem).find('.default-view').show();
                $(elem).find('.select-editor').hide();

                $(elem).find('.select-editor').find('.hidden-value').val('');
                $(elem).find('.select-editor').find('.hidden-reference').val('');
                $(elem).find('.select-editor').find('.select-input').val('');
            });
            // text input handler
            $this.find('.edit-button-handler').click(function () {
                $(elem).find('.default-view').hide();
                $(elem).find('.input-editor').show();
            });
            $this.find('.input-editor').find('.ok-button-handler').click(function () {
                $(elem).find('.default-view').show();
                $(elem).find('.input-editor').hide();

                valueSource.assign(scope, null);
                if (attrs.ngReferenceSource) {
                    referenceSource.assign(scope, null);
                }
                textSource.assign(scope, $(elem).find('.input-editor').find('input').val());
                scope.$apply();
            });
            $this.find('.input-editor').find('.cancel-button-handler').click(function () {
                $(elem).find('.default-view').show();
                $(elem).find('.input-editor').hide();
            });
            // clear handler
            $this.find('.clear-button-handler').click(function () {
                valueSource.assign(scope, null);
                if (attrs.ngReferenceSource) {
                    referenceSource.assign(scope, null);
                }
                textSource.assign(scope, null);
                scope.$apply();
            });

            if (attrs.ngUiItems) {
                $(elem).find('.select-editor').find('.select-input').autocomplete({
                    source: dataUiItems,
                    minLength: 3,
                    select: function (event, ui) {
                        $(elem).find('.select-editor').find('.hidden-value').val(ui.item.id);
                        $(elem).find('.select-editor').find('.hidden-reference').val(ui.item.description);
                    },
                    change: function (event, ui) {
                        if (!ui.item) {
                            $(elem).find('.select-editor').find('.hidden-value').val('');
                            $(elem).find('.select-editor').find('.hidden-reference').val('');
                        }
                    },
                })
                    .data("ui-autocomplete")._renderItem = function (ul, item) {
                        return jQuery("<li>")
                            .data('item.autocomplete', item)
                            .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                            .appendTo(ul);
                    };
            }

            var dataFormatFunction = $this.attr('data-format-function');
            var dataURL = $this.attr('data-url');
            var dataToken = $this.attr('data-token') || '';
            var dataMethod = $this.attr('data-method') || 'GET';
            if (dataURL) {
                $(elem).find('.select-editor').find('.select-input').autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: dataURL,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + dataToken
                            },
                            type: dataMethod,
                            dataType: "json",
                            data: {
                                query: request.term
                            },
                            success: function (data) {
                                if (dataFormatFunction) {
                                    response(window[dataFormatFunction](data));
                                }
                                else {
                                    response(data);
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.log(jqXHR);
                                console.log(textStatus);
                                console.log(errorThrown);
                            }
                        });
                    },
                    minLength: 3,
                    select: function (event, ui) {
                        $(elem).find('.select-editor').find('.hidden-value').val(ui.item.id);
                        $(elem).find('.select-editor').find('.hidden-reference').val(ui.item.description);
                    },
                    change: function (event, ui) {
                        if (!ui.item) {
                            $(elem).find('.select-editor').find('.hidden-value').val('');
                            $(elem).find('.select-editor').find('.hidden-reference').val('');
                        }
                    },
                })
                    .data("ui-autocomplete")._renderItem = function (ul, item) {
                        return jQuery("<li>")
                            .data('item.autocomplete', item)
                            .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                            .appendTo(ul);
                    };
            }

            //clear memory reference
            $this = null;

            //
            // WATCH CHANGES DYNAMICALLY
            //
            scope.$watch(attrs.ngDataSource, function (value) {
            });
            scope.$watch(attrs.ngTextSource, function (value) {
                if (value !== undefined) {
                    $(elem).find('.default-view').find('input').val(value);
                    $(elem).find('.input-editor').find('input').val(value);
                }
                else {
                    $(elem).find('.default-view').find('input').val('');
                    $(elem).find('.input-editor').find('input').val('');
                }
            });
        }
    }
}]);

//
// Description: create a scrollable table which will trigger a function call when user scroll to the bottom of the table
// Author: Themy
//
angular.module('avs-directives').directive('avsScrollableTable', function () {
    return {
        link: function (scope, elem, attrs) {
            var obj = angular.element(elem);

            obj.find('.tilsoft-table-header div').each(function () {
                if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                    angular.element(this).on('click', function () {
                        var sortDirection = 'asc';
                        if (angular.element(this).hasClass('sorting_asc')) {
                            sortDirection = 'desc';
                        }
                        else {
                            sortDirection = 'asc';
                        }

                        angular.element(this).parent().find('div').each(function () {
                            if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                                angular.element(this).removeClass('sorting_desc');
                                angular.element(this).removeClass('sorting_asc');
                                angular.element(this).removeClass('sorting');
                                angular.element(this).addClass('sorting');
                            }
                        });

                        angular.element(this).removeClass('sorting');
                        if (sortDirection == 'asc') {
                            angular.element(this).addClass('sorting_asc');
                        }
                        else {
                            angular.element(this).addClass('sorting_desc');
                        }
                        scope.$apply(attrs.ngSortData + "('" + angular.element(this).data('colname') + "', '" + sortDirection + "')");
                    });
                }
            });

            obj.find('.tilsoft-table').each(function () {
                angular.element(this).scrollTop(0);
                angular.element(this).on('scroll', function () {
                    var jObject = angular.element(this);
                    var jHeader = jObject.find('.tilsoft-table-header');
                    var jFilter = jObject.find('.tilsoft-table-filter');
                    var jTotalRow = jObject.find('.tilsoft-table-totalrow');
                    var jSubTotalRow = jObject.find('.tilsoft-table-subtotalrow');

                    jHeader.css('top', jObject.scrollTop());
                    if (jFilter) {
                        jFilter.css('top', jObject.scrollTop());
                    }
                    if (jTotalRow) {
                        jTotalRow.css('top', jObject.scrollTop());
                    }
                    if (jSubTotalRow) {
                        jSubTotalRow.css('top', jObject.scrollTop());
                    }
                    if (attrs.ngLoadNextPage) {
                        if (this.scrollTopMax) {
                            if (angular.element(this).scrollTop() >= this.scrollTopMax) { // reach the end of the scroll
                                scope.$apply(attrs.ngLoadNextPage);
                            }
                        }
                        else {
                            if (Math.round(angular.element(this).scrollTop()) >= this.scrollHeight - this.clientHeight) { // reach the end of the scroll
                                scope.$apply(attrs.ngLoadNextPage);
                            }
                        }
                    }                    
                });
            });
        }
    };
});

angular.module('avs-directives').directive('avsScrollableTable2', function () {
    return {
        scope: {
            ngLoadNextPage: '&',
            ngSortData: '&',
            ngGetScrollTop: '&'
        },
        link: function (scope, elem, attrs) {
            var obj = angular.element(elem);

            obj.find('.tilsoft-table-header div').each(function () {
                if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                    angular.element(this).on('click', function () {
                        var sortDirection = 'asc';
                        if (angular.element(this).hasClass('sorting_asc')) {
                            sortDirection = 'desc';
                        }
                        else {
                            sortDirection = 'asc';
                        }

                        angular.element(this).parent().find('div').each(function () {
                            if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                                angular.element(this).removeClass('sorting_desc');
                                angular.element(this).removeClass('sorting_asc');
                                angular.element(this).removeClass('sorting');
                                angular.element(this).addClass('sorting');
                            }
                        });

                        angular.element(this).removeClass('sorting');
                        if (sortDirection == 'asc') {
                            angular.element(this).addClass('sorting_asc');
                        }
                        else {
                            angular.element(this).addClass('sorting_desc');
                        }                        
                        scope.$apply(scope.ngSortData({ sortedBy: angular.element(this).data('colname'), sortedDirection: sortDirection }));
                    });
                }
            });

            obj.find('.tilsoft-table').each(function () {
                angular.element(this).scrollTop(0);
                angular.element(this).on('scroll', function () {
                    var jObject = angular.element(this);
                    var jHeader = jObject.find('.tilsoft-table-header');
                    var jFilter = jObject.find('.tilsoft-table-filter');
                    var jTotalRow = jObject.find('.tilsoft-table-totalrow');
                    var jSubTotalRow = jObject.find('.tilsoft-table-subtotalrow');

                    jHeader.css('top', jObject.scrollTop());
                    if (jFilter) {
                        jFilter.css('top', jObject.scrollTop());
                    }
                    if (jTotalRow) {
                        jTotalRow.css('top', jObject.scrollTop());
                    }
                    if (jSubTotalRow) {
                        jSubTotalRow.css('top', jObject.scrollTop());
                    }
                    if (attrs.ngLoadNextPage) {
                        if (this.scrollTopMax) {
                            if (angular.element(this).scrollTop() >= this.scrollTopMax) { // reach the end of the scroll
                                scope.$apply(attrs.ngLoadNextPage);
                            }
                        }
                        else {
                            if (Math.round(angular.element(this).scrollTop()) >= this.scrollHeight - this.clientHeight) { // reach the end of the scroll
                                scope.$apply(scope.ngLoadNextPage());
                            }
                        }
                    }
                    //get scroll position so we can back to exactly position of row is openning 
                    scope.$apply(scope.ngGetScrollTop({
                        topValue: jObject.scrollTop(),
                        leftValue: jObject.scrollLeft()
                    }));
                });
            });
        }
    };
});

//
// ADVANCE TABLE
// Description: create a scrollable table which will trigger a function call when user scroll to the bottom of the table + hide and show columns
// Directive: avs-advance-table
// Options: 
// -- ng-sort-data: trigger function when user click column header to sort. Ex: ng-sort-data="event.nameOfSortFunction", the function should accept 2 param as string: columnName and sortDirection.
// -- ng-load-next-page: on scroll to bottom function. Ex: ng-load-next-page="event.nameOfLoadNextPageFunction"
//
angular.module('avs-directives').directive('avsAdvanceTable', function () {
    return {
        link: function (scope, elem, attrs) {
            var obj = angular.element(elem);

            var colCount = obj.find('.tilsoft-table-header div').length - 1;
            var index = 1;
            var colOptions = [];
            obj.find('.tilsoft-table-header div').each(function () {
                if (index <= colCount && index > 1) {
                    colOptions.push({
                        colIndex: index,
                        colTitle: angular.element(this).html(),
                        isVisible: true
                    });
                }
                index++;

                if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                    angular.element(this).on('click', function () {
                        var sortDirection = 'asc';
                        if (angular.element(this).hasClass('sorting_asc')) {
                            sortDirection = 'desc';
                        }
                        else {
                            sortDirection = 'asc';
                        }

                        angular.element(this).parent().find('div').each(function () {
                            if (angular.element(this).hasClass('sorting') || angular.element(this).hasClass('sorting_asc') || angular.element(this).hasClass('sorting_desc')) {
                                angular.element(this).removeClass('sorting_desc');
                                angular.element(this).removeClass('sorting_asc');
                                angular.element(this).removeClass('sorting');
                                angular.element(this).addClass('sorting');
                            }
                        });

                        angular.element(this).removeClass('sorting');
                        if (sortDirection == 'asc') {
                            angular.element(this).addClass('sorting_asc');
                        }
                        else {
                            angular.element(this).addClass('sorting_desc');
                        }
                        scope.$apply(attrs.ngSortData + "('" + angular.element(this).data('colname') + "', '" + sortDirection + "')");
                    });
                }
            });

            obj.find('.dt-toolbar .col-setting ul').html('');
            angular.forEach(colOptions, function (item) {
                obj.find('.dt-toolbar .col-setting ul').append('<li><label><input type="checkbox" data-colindex="' + item.colIndex + '" checked /> ' + item.colTitle + '</label></li>');
            });
            obj.find('.dt-toolbar .col-setting ul li input[type=checkbox]').click(function () {
                var colIndex = parseInt($(this).data('colindex'));
                var isVisible = this.checked;
                if (isVisible) {
                    obj.find('.tilsoft-table-header > div:nth-child(' + colIndex + ')').show();
                    obj.find('.tilsoft-table-filter > div:nth-child(' + colIndex + ')').show();
                    obj.find('.tilsoft-table-body table > tbody > tr > td:nth-child(' + colIndex + ')').show();
                }
                else {
                    obj.find('.tilsoft-table-header > div:nth-child(' + colIndex + ')').hide();
                    obj.find('.tilsoft-table-filter > div:nth-child(' + colIndex + ')').hide();
                    obj.find('.tilsoft-table-body table > tbody > tr > td:nth-child(' + colIndex + ')').hide();
                }

                angular.forEach(colOptions, function (item) {
                    if (item.colIndex === colIndex) {
                        item.isVisible = isVisible;
                    }
                });
            });

            obj.find('.tilsoft-table').each(function () {
                angular.element(this).scrollTop(0);
                angular.element(this).on('scroll', function () {
                    var jObject = angular.element(this);
                    var jHeader = jObject.find('.tilsoft-table-header');
                    var jFilter = jObject.find('.tilsoft-table-filter');
                    var jTotalRow = jObject.find('.tilsoft-table-totalrow');
                    var jSubTotalRow = jObject.find('.tilsoft-table-subtotalrow');

                    jHeader.css('top', jObject.scrollTop());
                    if (jFilter) {
                        jFilter.css('top', jObject.scrollTop());
                    }
                    if (jTotalRow) {
                        jTotalRow.css('top', jObject.scrollTop());
                    }
                    if (jSubTotalRow) {
                        jSubTotalRow.css('top', jObject.scrollTop());
                    }
                    if (attrs.ngLoadNextPage) {
                        if (this.scrollTopMax) {
                            if (angular.element(this).scrollTop() >= this.scrollTopMax - 20) { // reach the end of the scroll
                                scope.$apply(attrs.ngLoadNextPage);
                            }
                        }
                        else {
                            if (Math.round(angular.element(this).scrollTop()) >= this.scrollHeight - this.clientHeight - 20) { // reach the end of the scroll
                                scope.$apply(attrs.ngLoadNextPage);
                            }
                        }
                    }
                });
            });
        }
    }
});

//
// CURRENCY BOX 2
// Dependency: autoNumeric-1.9.41.js, angular-locale_xx-xx.js
// Directive: avs-currency-2
// Options: 
// -- ng-currency-symbol: the currency symbol, support html. Ex: &euro;. Default value will be used if not declare: default value = '$'
// -- ng-decimal: number of digit for decimal. Ex: ng-decimal=2 => xxx,xx. Default value will be used if not declare: default value = 2 
//
angular.module('avs-directives').directive('avsCurrency2', ['$locale', function ($locale) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, controller) {
            //
            // jQuery bootstrap
            //
            var localeSetting = { dec: ',', sep: '.', sym: attrs.ngCurrencySymbol || '$', mdec: attrs.ngDecimal || 2 };
            switch ($locale.id) {
                case 'nl-nl':
                    localeSetting.dec = ',';
                    localeSetting.sep = '.';
                    break;

                case 'vi-vn':
                    localeSetting.dec = ',';
                    localeSetting.sep = '.';
                    break;

                default:
                    localeSetting.dec = '.';
                    localeSetting.sep = ',';
                    break;
            }
            angular.element(elem).autoNumeric('init', { aSign: localeSetting.sym + ' ', aDec: localeSetting.dec, aSep: localeSetting.sep, lZero: 'deny', mDec: localeSetting.mdec });

            //
            // angular bootstrap
            //			
            // controller.$formatters.push(function(mValue){
            // return mValue;
            // });
            controller.$render = function () {
                if (controller.$viewValue !== undefined && controller.$viewValue !== null) {
                    angular.element(elem).autoNumeric('set', controller.$viewValue);
                }
                else {
                    angular.element(elem).autoNumeric('set', '');
                }
            };
            controller.$parsers.push(function (vValue) {
                return angular.element(elem).autoNumeric('get');
            });
        }
    }
}]);

//
// NUMBER BOX
// Dependency: autoNumeric-1.9.41.js, angular-locale_xx-xx.js
// Directive: avs-number-2
// Options:
// -- ng-decimal: number of digit for decimal. Ex: ng-decimal=2 => xxx,xx. Default value will be used if not declare: default value = 0
// -- ng-on-change: on change function. Ex: ng-on-change="event.nameOfOnChangeFunction"
//
angular.module('avs-directives').directive('avsNumber2', ['$locale', function ($locale) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, controller) {
            //
            // jQuery bootstrap
            //
            var localeSetting = { dec: ',', sep: '.', sym: '', mdec: attrs.ngDecimal || 0 };
            switch ($locale.id) {
                case 'nl-nl':
                    localeSetting.dec = ',';
                    localeSetting.sep = '.';
                    break;

                case 'vi-vn':
                    localeSetting.dec = ',';
                    localeSetting.sep = '.';
                    break;

                default:
                    localeSetting.dec = '.';
                    localeSetting.sep = ',';
                    break;
            }
            angular.element(elem).autoNumeric('init', { aSign: localeSetting.sym + ' ', aDec: localeSetting.dec, aSep: localeSetting.sep, lZero: 'deny', mDec: localeSetting.mdec });

            //
            // angular bootstrap
            //			
            // controller.$formatters.push(function(mValue){
            // return mValue;
            // });
            controller.$render = function () {
                if (controller.$viewValue !== undefined && controller.$viewValue !== null) {
                    angular.element(elem).autoNumeric('set', controller.$viewValue);
                }
                else {
                    angular.element(elem).autoNumeric('set', '');
                }
            };
            controller.$parsers.push(function (vValue) {
                return angular.element(elem).autoNumeric('get');
            });
        }
    }
}]);

angular.module('avs-directives').directive('avsDateTime', ['$locale', '$parse', '$timeout', function ($locale, $parse, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            bindedMModel: "=ngModel"
        },
        link: function (scope, element, attrs, controller) {
            $(element).datetimepicker({
                sideBySide: false,
                format: "DD/MM/YYYY hh:mm A"
            });

            element.on("dp.change", function (e) {
                $timeout(function () {
                    scope.bindedMModel = e.target.value;
                });
            });

            //element.on('blur', function () {
            //    $timeout(function () {
            //        scope.bindedMModel = jQuery(element).val();
            //    });              
            //});                 
        }
    }
}]);

angular.module('avs-directives').directive('avsDate', ['$locale', '$parse', '$timeout', function ($locale, $parse, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            bindedMModel: "=ngModel"
        },
        link: function (scope, element, attrs, controller) {
            $(element).datetimepicker({
                sideBySide: false,
                format: "DD/MM/YYYY"
            });

            element.on("dp.change", function (e) {
                $timeout(function () {
                    scope.bindedMModel = e.target.value;
                });
            });

            //element.on('blur', function () {
            //    $timeout(function () {
            //        scope.bindedMModel = jQuery(element).val();
            //    });              
            //});                 
        }
    }
}]);