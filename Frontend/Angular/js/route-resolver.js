'use strict';

define([], function () {
    var routeResolver = function () {
        this.$get = function () {
            return this;
        };

        this.route = function () {
            var resolve = function (path, controllerInstanceName, fileName) {
                var routeDef = {};
                routeDef.templateUrl = path + '/views/' + fileName + '.html';
                routeDef.controller = controllerInstanceName;
                routeDef.resolve = {
                    load: ['$q', '$rootScope', function ($q, $rootScope) {
                        var dependencies = [path + '/controllers/' + fileName + '.js'];
                        return resolveDependencies($q, $rootScope, dependencies);
                    }]
                };
                return routeDef;
            },

            resolveDependencies = function ($q, $rootScope, dependencies) {
                var defer = $q.defer();
                require(dependencies, function () {
                    defer.resolve();
                    $rootScope.$apply();
                });

                return defer.promise;
            };

            return {
                resolve: resolve
            }
        }();
    };

    angular.module('routeResolverServices', []).provider('routeResolver', routeResolver);
});
