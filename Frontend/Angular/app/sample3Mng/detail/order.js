angular.module('tilsoftApp').controller('orderController', ['$scope', '$http', 'dataService', function ($scope, $http, dataService) {
    //
    // property
    //
    $scope.data = {};

    //
    // event
    //
    $scope.event = {
        init: function () {
            $('#content').show();
            $('#ribbon .breadcrumb').html('').append('<li>Sample Order</li>');
            $('#page-title').html('').append('Sample Order');
            
            $scope.data = dataService.getLocalData(dataService.dataKey);
            if(!$scope.data){
                // get data
                jsHelper.loadingSwitch(false);
                $http({
                    method: 'GET',
                    url: 'http://localhost/sample-data.php',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
                        $scope.data = response.data.data.data;
                    }
                    else {
                        jsHelper.showMessage('warning', response.data.message.message);
                        console.log(response.data.message.message);
                    }
                }, function errorCallback(response) {
                    jsHelper.loadingSwitch(false);
                    jsHelper.showMessage('warning', response.data.message);
                }); 
            }
        }
    }
	$scope.$on('$destroy', function(){
		dataService.setLocalData(dataService.dataKey, $scope.data);
	});

    //
    // init
    //
    $scope.event.init();
}]);