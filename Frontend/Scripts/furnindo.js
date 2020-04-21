(function ($) {

    $.fn.furnindoTable = function () {
        var aTable = this.find('table');
        aTable.find('th').each(function () {
            if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                $(this).attr('ng-click', 'sort($event)');
                $(this).click(function () {
                    var sortDirection = 'asc';
                    if ($(this).hasClass('sorting_asc')) {
                        sortDirection = 'desc';
                    }
                    else {
                        sortDirection = 'asc';
                    }

                    aTable.find('th').each(function () {
                        if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                            $(this).removeClass('sorting_desc');
                            $(this).removeClass('sorting_asc');
                            $(this).removeClass('sorting');
                            $(this).addClass('sorting');
                        }
                    });

                    $(this).removeClass('sorting');
                    if (sortDirection == 'asc') {
                        $(this).addClass('sorting_asc');
                    }
                    else {
                        $(this).addClass('sorting_desc');
                    }
                });
            }
        });

        // pagination
        var pagerHtml = '<ul class="pagination pagination-sm custom-pagination">';
        pagerHtml += '<li><a href="#" ng-click="goToFirst($event)">First</a></li>';
        pagerHtml += '<li><a href="#" ng-click="goToPrev($event)">Prev</a></li>';
        pagerHtml += '<li><span>Page <input type="text" style="height:15px;padding:2px;width:30px; text-align: right;" ng-model="pageIndex" id="' + this.attr('id') + '_pageIndex' + '" />/{{totalPage}} <a href="#" ng-click="goToPage($event)">Go</a></span></li>';
        pagerHtml += '<li><a href="#" ng-click="goToNext($event)">Next</a></li>';
        pagerHtml += '<li><a href="#" ng-click="goToLast($event)">Last</a></li>';
        pagerHtml += '</ul>';
        aTable.after(pagerHtml);

        return this;
    };

    ///
    $.fn.furnindoTableSortAtClient = function () {
        var idTable = $(this).attr('id');
        var aTable = this.find('table');
        aTable.find('th').each(function () {
            if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                $(this).attr('ng-click', 'sortAtClient($event)');
                $(this).click(function () {
                    var sortDirection = 'asc';
                    if ($(this).hasClass('sorting_asc')) {
                        sortDirection = 'desc';
                    }
                    else {
                        sortDirection = 'asc';
                    }

                    aTable.find('th').each(function () {
                        if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                            $(this).removeClass('sorting_desc');
                            $(this).removeClass('sorting_asc');
                            $(this).removeClass('sorting');
                            $(this).addClass('sorting');
                        }
                    });

                    $(this).removeClass('sorting');
                    if (sortDirection == 'asc') {
                        $(this).addClass('sorting_asc');
                    }
                    else {
                        $(this).addClass('sorting_desc');
                    }

                    $(this).attr('data-tableid', idTable);
                });
            }
        });
        return this;
    };


    ///
    $.fn.scrollableTable = function (onPageChangeCallBack, onSortCallBack) {
        //
        // INIT
        //
        //this.append('<div class="scrollbar"><div class="scrollbar-content">&nbsp;</div></div>');

        this.find('.tilsoft-table').scroll(function () {
            $(this).parent().find('.tilsoft-table-header').css('top', $(this).scrollTop());
        });

        if (onPageChangeCallBack != null)
        {
            var pagerHtml = '<ul style="margin-top: 10px;" class="pagination pagination-sm custom-pagination">';
            pagerHtml += '<li><a class="move-first" href="#">First</a></li>';
            pagerHtml += '<li><a class="move-prev" href="#">Prev</a></li>';
            pagerHtml += '<li><span>Page <font class="page-index">1</font>/<font class="total-page">{{totalPage}}</font> <a class="move-page" href="#">Go</a></span></li>';
            pagerHtml += '<li><a class="move-next" href="#">Next</a></li>';
            pagerHtml += '<li><a class="move-last" href="#">Last</a></li>';
            pagerHtml += '</ul>';
            pagerHtml += '<span class="more-info">Found: {{totalRows}} item(s)</span>';
            this.append(pagerHtml);

            //
            // EVENTs
            //
            this.find('.move-first').click(function (event) {
                event.preventDefault();
                $(this).parent().parent().find('.page-index').html(1);
                onPageChangeCallBack(1);
            });

            this.find('.move-prev').click(function (event) {
                event.preventDefault();

                var currentPage = parseInt($(this).parent().parent().find('.page-index').html());
                if (currentPage <= 1) {
                    currentPage = 1;
                }
                else {
                    currentPage--;
                }
                $(this).parent().parent().find('.page-index').html(currentPage);

                onPageChangeCallBack(currentPage);
            });

            this.find('.move-next').click(function (event) {
                event.preventDefault();

                var currentPage = parseInt($(this).parent().parent().find('.page-index').html());
                var totalPage = parseInt($(this).parent().parent().find('.total-page').html());
                if (currentPage >= totalPage) {
                    currentPage = totalPage;
                }
                else {
                    currentPage++;
                }
                $(this).parent().parent().find('.page-index').html(currentPage);

                onPageChangeCallBack(currentPage);
            });

            this.find('.move-last').click(function (event) {
                event.preventDefault();
                var totalPage = parseInt($(this).parent().parent().find('.total-page').html());
                $(this).parent().parent().find('.page-index').html(totalPage);
                onPageChangeCallBack(totalPage);
            });

            this.find('.move-page').click(function (event) {
                event.preventDefault();
                var currentPage = parseInt($(this).parent().parent().find('.page-index').html());
                onPageChangeCallBack(currentPage);
            });
        }

        if (onSortCallBack != null)
        {
            this.find('.tilsoft-table-header div').each(function () {
                if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                    $(this).click(function (event) {
                        event.preventDefault();

                        var sortDirection = 'asc';
                        if ($(this).hasClass('sorting_asc')) {
                            sortDirection = 'desc';
                        }
                        else {
                            sortDirection = 'asc';
                        }

                        $(this).parent().find('div').each(function () {
                            if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                                $(this).removeClass('sorting_desc');
                                $(this).removeClass('sorting_asc');
                                $(this).removeClass('sorting');
                                $(this).addClass('sorting');
                            }
                        });

                        $(this).removeClass('sorting');
                        if (sortDirection == 'asc') {
                            $(this).addClass('sorting_asc');
                        }
                        else {
                            $(this).addClass('sorting_desc');
                        }

                        $(this).parent().parent().parent().find('.page-index').val(1);
                        onSortCallBack($(this).data('colname'), sortDirection);
                    });
                }
            });
        }
        //
        // METHODs
        //
        this.updateLayout = function () {
            this.find('.scrollbar-content').css('height', this.find('.tilsoft-table-body table').height() + 'px');
        };

        return this;
    };

    ///


    ///
    $.fn.scrollableTable2 = function (pageIndexRef, totalPageRef, onPageChangeCallBack, onSortCallBack) {
        //
        // INIT
        //
        //this.append('<div class="scrollbar"><div class="scrollbar-content">&nbsp;</div></div>');

        var pagerHtml = '<ul style="margin-top: 10px;" class="pagination pagination-sm custom-pagination">';
        pagerHtml += '<li><a class="move-first" href="#">First</a></li>';
        pagerHtml += '<li><a class="move-prev" href="#">Prev</a></li>';
        pagerHtml += '<li><span>Page <font class="page-index">{{' + pageIndexRef + '}}</font>/<font class="total-page">{{' + totalPageRef + '}}</font></li>';
        pagerHtml += '<li><a class="move-next" href="#">Next</a></li>';
        pagerHtml += '<li><a class="move-last" href="#">Last</a></li>';
        pagerHtml += '</ul>';
        this.append(pagerHtml);

        //
        // EVENTs
        //
        this.find('.move-first').click(function (event) {
            event.preventDefault();
            $(this).parent().parent().find('.page-index').html(1);
            onPageChangeCallBack(1);
        });

        this.find('.move-prev').click(function (event) {
            event.preventDefault();

            var currentPage = parseInt($(this).parent().parent().find('.page-index').html());
            if (currentPage <= 1) {
                currentPage = 1;
            }
            else {
                currentPage--;
            }
            $(this).parent().parent().find('.page-index').html(currentPage);

            onPageChangeCallBack(currentPage);
        });

        this.find('.move-next').click(function (event) {
            event.preventDefault();

            var currentPage = parseInt($(this).parent().parent().find('.page-index').html());
            var totalPage = parseInt($(this).parent().parent().find('.total-page').html());
            if (currentPage >= totalPage) {
                currentPage = totalPage;
            }
            else {
                currentPage++;
            }
            $(this).parent().parent().find('.page-index').html(currentPage);

            onPageChangeCallBack(currentPage);
        });

        this.find('.move-last').click(function (event) {
            event.preventDefault();
            var totalPage = parseInt($(this).parent().parent().find('.total-page').html());
            $(this).parent().parent().find('.page-index').html(totalPage);
            onPageChangeCallBack(totalPage);
        });

        this.find('.move-page').click(function (event) {
            event.preventDefault();
            var currentPage = parseInt($(this).parent().parent().find('.page-index').val());
            onPageChangeCallBack(currentPage);
        });

        this.find('.tilsoft-table').scroll(function () {
            $(this).parent().find('.tilsoft-table-header').css('top', $(this).scrollTop());
        });

        //this.find('.scrollbar').scroll(function () {
        //    var _realScroller = $(this).parent().find('.tilsoft-table-body');
        //    if (_realScroller.scrollTop() != $(this).scrollTop()) {
        //        _realScroller.scrollTop($(this).scrollTop());
        //    }
        //});

        //this.find('.tilsoft-table-body').scroll(function () {
        //    var _fakeScroller = $(this).parent().parent().find('.scrollbar');
        //    if (_fakeScroller.scrollTop() != $(this).scrollTop()) {
        //        _fakeScroller.scrollTop($(this).scrollTop());
        //    }
        //});

        this.find('.tilsoft-table-header div').each(function () {
            if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                $(this).click(function (event) {
                    event.preventDefault();

                    var sortDirection = 'asc';
                    if ($(this).hasClass('sorting_asc')) {
                        sortDirection = 'desc';
                    }
                    else {
                        sortDirection = 'asc';
                    }

                    $(this).parent().find('div').each(function () {
                        if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                            $(this).removeClass('sorting_desc');
                            $(this).removeClass('sorting_asc');
                            $(this).removeClass('sorting');
                            $(this).addClass('sorting');
                        }
                    });

                    $(this).removeClass('sorting');
                    if (sortDirection == 'asc') {
                        $(this).addClass('sorting_asc');
                    }
                    else {
                        $(this).addClass('sorting_desc');
                    }

                    $(this).parent().parent().parent().find('.page-index').val(1);
                    onSortCallBack($(this).data('colname'), sortDirection);
                });
            }
        });

        //
        // METHODs
        //
        this.updateLayout = function () {
            this.find('.scrollbar-content').css('height', this.find('.tilsoft-table-body table').height() + 'px');
        };

        return this;
    };

    $.fn.scrollableTable3 = function () {
        return this.each(function () {
            $(this).find('.scrollable-table2-grid').width($(this).find('.scrollable-table2-header').width() - 30);
            $(this).find('.t-body').scroll(function () {
                if ($(this).parent().find('.t-head').scrollLeft() != $(this).scrollLeft()) {
                    $(this).parent().find('.t-head').scrollLeft($(this).scrollLeft());
                }
            });
            $(this).find('.scrollable-table2-grid').append('<tr><td colspan="' + $(this).find('.scrollable-table2-grid-header').find('td').length + '" style="border-bottom: none;">&nbsp;</td></tr>');
        });
    };

    $.fn.sortableTable = function (onSortCallBack) {
        this.find('.tilsoft-table-header div').each(function () {
            if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                $(this).click(function (event) {
                    event.preventDefault();

                    var sortDirection = 'asc';
                    if ($(this).hasClass('sorting_asc')) {
                        sortDirection = 'desc';
                    }
                    else {
                        sortDirection = 'asc';
                    }

                    $(this).parent().find('div').each(function () {
                        if ($(this).hasClass('sorting') || $(this).hasClass('sorting_asc') || $(this).hasClass('sorting_desc')) {
                            $(this).removeClass('sorting_desc');
                            $(this).removeClass('sorting_asc');
                            $(this).removeClass('sorting');
                            $(this).addClass('sorting');
                        }
                    });

                    $(this).removeClass('sorting');
                    if (sortDirection == 'asc') {
                        $(this).addClass('sorting_asc');
                    }
                    else {
                        $(this).addClass('sorting_desc');
                    }

                    $(this).parent().parent().parent().find('.page-index').val(1);
                    onSortCallBack($(this).data('colname'), sortDirection);
                });
            }
        });

        return this;
    };

    ///

    /// QUICK SEARCH BOX
    $.fn.QuickSearchBox = function (config, selectedObj) {
        $(this).devbridgeAutocomplete({
            serviceUrl: config.url,
            dataType: 'json',
            minChars: config.minChars,
            ajaxSettings: {
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + config.token
                },
                type: "GET"
            },
            onSelect: function (suggestion) {
                selectedObj.id = suggestion.data.optionID;
                selectedObj.data = suggestion.data;
                scope.$apply();
            },
            transformResult: function (response) {
                return {
                    suggestions: jQuery.map(response, function (item) {
                        return { value: item.optionNM, data: item };
                    })
                };
            },
            onInvalidateSelection: function () {
                selectedObj.id = null;
                selectedObj.data = null;
                $(this).val('');
            },
            showNoSuggestionNotice: true,
            noCache: true,
            onSearchStart: function (query) {
                jsHelper.loadingSwitch(true);
            },
            onSearchComplete: function (query, suggestions) {
                jsHelper.loadingSwitch(false);
            }
        });

        return this;
    };
}($));