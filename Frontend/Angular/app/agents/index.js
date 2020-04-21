jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

var searchGrid = jQuery('#grdSearchResult').scrollableTable(null, function (sortedBy, sortedDirection) {
    var scope = angular.element(jQuery('body')).scope();
    datasource = scope.data;
    if (sortedDirection == 'asc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] > b[sortedBy] ? 1 : -1;
        });
    }
    else if (sortedDirection == 'desc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] < b[sortedBy] ? 1 : -1;
        });
    }
    scope.$apply();
});

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        agentNM: '',
    };
    $scope.data = null;
    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.event.search();
        },
        search: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                    jQuery('#content').show();
                    if (data.message.type == 2)
                    {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        delete: function (id, $event) {
            $event.preventDefault();
            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].agentID == data.data) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                            }
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },        
    }
    $scope.event.reload();
}]);