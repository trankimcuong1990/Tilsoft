//
// APP START
//
var siteRoot = document.location.protocol + '//' + document.location.host + '/';
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'ngRoute', 'avs-directives']);
var uniqueId = (new Date()).getUTCMilliseconds();
// config the app instance to add routing
tilsoftApp.config(function($routeProvider){		
	$routeProvider.when('/', {
		cache: false,
		controller: 'orderController',
		templateUrl: siteRoot + '/Angular/app/sample3Mng/detail/order.html?' + uniqueId
	});
	$routeProvider.when('/product/:productId', {
		cache: false,
		controller: 'productController',
		templateUrl: siteRoot + '/Angular/app/sample3Mng/detail/product.html?' + uniqueId
	});
	$routeProvider.when('/product/requirement/:productId', {
		cache: false,
		controller: 'requirementController',
		templateUrl: siteRoot + '/Angular/app/sample3Mng/detail/requirement.html?' + uniqueId
	});
	$routeProvider.otherwise({
		redirectTo: '/'
	});
});
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;
}]);