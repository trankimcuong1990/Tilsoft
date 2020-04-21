var grdModelSearchResult = jQuery('#grdModelSearchResult').scrollableTable(null, null);
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        modelID: '',
        modelNM: ''
    };

    //
    // event
    //
    $scope.event = {
        goNext: function ($event) {
            $event.preventDefault();

            if ($scope.data.modelID == '') {
                alert('Please select all option before proceed to the next step');
                return;
            }
            else {
                window.location = context.nextURL + $scope.data.modelID;
            }
        },
        goBack: function ($event) {
            $event.preventDefault();
            
            window.location = context.backURL;
        }
    };

    //
    // method
    //
    $scope.method = {
    };

    //
    // child component
    //
    searchModelTimer = null;
    $scope.quicksearchModel = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'ModelNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,
        event: {
            searchBoxKeyUp: function () {
                if (jQuery('#txtModelNM').val().length >= 3) {
                    clearTimeout(searchModelTimer);
                    searchModelTimer = setTimeout(
                        function () {
                            $scope.quicksearchModel.filters.filters.searchQuery = jQuery('#txtModelNM').val();
                            $scope.quicksearchModel.filters.pageIndex = 1;
                            jsonService.quicksearchModel(
                                $scope.quicksearchModel.filters,
                                function (data) {
                                    if (data.message.type == 0) {
                                        $scope.quicksearchModel.data = data.data;
                                        $scope.quicksearchModel.totalPage = Math.ceil(data.totalRows / $scope.quicksearchModel.filters.pageSize);
                                        grdModelSearchResult.updateLayout();
                                        $scope.$apply();
                                        jQuery('#model-popup').show();
                                    }
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                }
                            );
                        },
                        500);
                }
            },
            close: function ($event, clearSearchBox) {
                jQuery('#model-popup').hide();
                if (clearSearchBox) {
                    $scope.data.modelNM = '';
                }
                $scope.quicksearchModel.data = null;
                $event.preventDefault();
            },
            ok: function ($event, id) {
                $scope.data.modelID = id;
                jQuery.each($scope.quicksearchModel.data, function () {
                    if (this.modelID == id) {
                        $scope.data.modelNM = this.modelNM;
                    }
                });
                $scope.quicksearchModel.event.close($event, false);
            }
        }
    }

    //
    // init
    //
    jQuery('#content').show();
}]);