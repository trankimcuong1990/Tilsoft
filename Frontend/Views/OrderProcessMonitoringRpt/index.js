//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.type = context.type;
    $scope.id = context.id;
    $scope.factoryOrderSelected = false;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getData(
                context.id,
                context.type,
                function (data) {
                    $scope.data = data.data;
                    $('#content').show();

                    angular.forEach($scope.data.saleOrderDTOs, function(item){
                        angular.forEach(item.factoryOrderDTOs, function(fItem){
                            if(fItem.isSelected) {
                                $scope.factoryOrderSelected = true;
                            }
                        });
                    });

                    $timeout(function(){
                        //
                        // JQUERY CODE
                        //
                        $('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
                        $('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').attr('title', 'Collapse this branch').on('click', function(e) {
	                        var children = $(this).parent('li.parent_li').find(' > ul > li');
	                        if (children.is(':visible')) {
		                        children.hide('fast');
		                        $(this).attr('title', 'Expand this branch').find(' > i').removeClass().addClass('fa fa-lg fa-plus-circle');
	                        } else {
		                        children.show('fast');
		                        $(this).attr('title', 'Collapse this branch').find(' > i').removeClass().addClass('fa fa-lg fa-minus-circle');
	                        }
	                        e.stopPropagation();
                        });
                    }, 0);
                },
                function (error) {
                    $scope.data = null;
                }
            );
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);