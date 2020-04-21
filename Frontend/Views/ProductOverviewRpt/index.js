//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.seasons = [];
    $scope.seasons.push({ seasonValue: '', seasonText: '' });
    Array.prototype.push.apply($scope.seasons, jsHelper.getSeasons());
    $scope.priceHistoryUI = {
        orderBy: '-season',
        filters: {
            season: '',
            clientUD:'',
            factoryUD: '',
            articleCode:'',
            description: ''
        }
    }

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getOverviewData(
                context.id, context.offerSeasonDetailID,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data;

                        $('#content').show();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        },
        priceHistory_sort: function (e) {
            $this = $(e.currentTarget);
            var newClass = '';
            if ($this.hasClass('sorting') || $this.hasClass('sorting_desc')) {
                newClass = 'sorting_asc';
            }
            else {
                newClass = 'sorting_desc';
            }
            angular.forEach($this.parent().find('th'), function (item) { 
                if($(item).hasClass('sorting_desc') || $(item).hasClass('sorting_asc')) {
                    $(item).removeClass('sorting_desc').removeClass('sorting_asc').addClass('sorting');
                }
            });
            $this.removeClass('sorting_desc').removeClass('sorting_asc').removeClass('sorting').addClass(newClass);
            if (newClass == 'sorting_desc') {
                $scope.priceHistoryUI.orderBy = '-' + $(e.currentTarget).data('column');
            }
            else {
                $scope.priceHistoryUI.orderBy = $(e.currentTarget).data('column');
            }
        }
    }

    //
    // method
    //
    $scope.method = {
    }


    //
    // init
    //
    $scope.event.init();
}]);