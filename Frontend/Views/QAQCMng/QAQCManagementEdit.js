'use strict';

$('a.gallery').featherlightGallery({
    previousIcon: '&#9664;',     /* Code that is used as previous icon */
    nextIcon: '&#9654;',         /* Code that is used as next icon */
    galleryFadeIn: 100,          /* fadeIn speed when slide is loaded */
    galleryFadeOut: 300          /* fadeOut speed before slide is loaded */
});
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    //property
    //
    $scope.data = null;

    //
    //function
    //
    $scope.event = {

        load: function () {
            dataService.getDataReport(
                context.id,
                function (data) {
                    $scope.data = data.data;
                    //$scope.event.initMap($scope.data.latitudeC, $scope.data.longitudeC);
                    $scope.map.initMap01($scope.data.latitudeC, $scope.data.longitudeC);
                    $scope.map.initMap02($scope.data.latitudeF, $scope.data.longitudeF);
                    jQuery('#content').show();
                    //window.location = context.refreshUrl + id;
                },
                function (error) {
                    //Noting to do
                }
            );
        },

        setStatus: function (statusID, comment) {
            dataService.setStatusReport(
                context.id,
                statusID,
                comment,
                function (data) {
                    window.location = context.refreshUrl + context.id;
                },

                function (error) {
                }
            );
        },

        viewImage: function (item) {
            $scope.images = [];
            $scope.images = JSON.parse(JSON.stringify(item));
            jQuery("#imagePoup").modal();
        },

        
    };

    $scope.method = {
        caculatorCritical: function (defectName) {
            var result = 0;
            if ($scope.data !== null) {
                angular.forEach($scope.data.reportDefects, function (item) {
                    if (item.defectANM !== null && item.defectANM !== '') {
                        if (defectName.toUpperCase() === item.defectANM.toUpperCase()) {
                            result = result + item.defectAQty;
                        }
                    }
                    if (item.defectBNM !== null && item.defectBNM !== '') {
                        if (defectName.toUpperCase() === item.defectBNM.toUpperCase()) {
                            result = result + item.defectBQty;
                        }
                    }
                    if (item.defectCNM !== null && item.defectCNM !== '') {
                        if (defectName.toUpperCase() === item.defectCNM.toUpperCase()) {
                            result = result + item.defectCQty;
                        }
                    }
                });
            }
            return result;
        },
        caculatorMajor: function (defectName) {
            var result = 0;
            if ($scope.data !== null) {
                angular.forEach($scope.data.reportDefects, function (item) {
                    if (item.defectANM !== null && item.defectANM !== '') {
                        if (defectName.toUpperCase() === item.defectANM.toUpperCase()) {
                            result = result + item.defectAQty;
                        }
                    }
                    if (item.defectBNM !== null && item.defectBNM !== '') {
                        if (defectName.toUpperCase() === item.defectBNM.toUpperCase()) {
                            result = result + item.defectBQty;
                        }
                    }
                    if (item.defectCNM !== null && item.defectCNM !== '') {
                        if (defectName.toUpperCase() === item.defectCNM.toUpperCase()) {
                            result = result + item.defectCQty;
                        }
                    }
                });
            }
            return result;
        },
        caculatorMinor: function (defectName) {
            var result = 0;
            if ($scope.data !== null) {
                angular.forEach($scope.data.reportDefects, function (item) {
                    if (item.defectANM !== null && item.defectANM !== '') {
                        if (defectName.toUpperCase() === item.defectANM.toUpperCase()) {
                            result = result + item.defectAQty;
                        }
                    }
                    if (item.defectBNM !== null && item.defectBNM !== '') {
                        if (defectName.toUpperCase() === item.defectBNM.toUpperCase()) {
                            result = result + item.defectBQty;
                        }
                    }
                    if (item.defectCNM !== null && item.defectCNM !== '') {
                        if (defectName.toUpperCase() === item.defectCNM.toUpperCase()) {
                            result = result + item.defectCQty;
                        }
                    }
                });
            }
            return result;
        }
    };

    $scope.map = {
        initMap01: function (latitudeC, longitudeC) {
            var mapOptions = {
                zoom: 15,
                center: new google.maps.LatLng(latitudeC, longitudeC),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById('map01'), mapOptions);
            var marker = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(latitudeC, longitudeC)
            });
            var infowindow = new google.maps.InfoWindow({
                content: '<div>Your location created report</div>'
            });
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
            infowindow.open(map, marker);
        },

        initMap02: function (latitudeF, longitudeF) {
            var mapOptions = {
                zoom: 15,
                center: new google.maps.LatLng(latitudeF, longitudeF),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById('map02'), mapOptions);
            var marker = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(latitudeF, longitudeF)
            });
            var infowindow = new google.maps.InfoWindow({
                content: '<div>Your location finished report</div>'
            });
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
            infowindow.open(map, marker);
        }
    };
    //
    //init controller
    // 
    $scope.event.load();

}]);