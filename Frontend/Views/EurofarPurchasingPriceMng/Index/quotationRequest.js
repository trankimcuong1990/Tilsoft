(function () {
    'use strict';

    angular.module('tilsoftApp').service('frmQuotationRequestJs', ['$http', function ($http) {
        var self = this;

        self.searchFilter = {
            filters: {
                Season: '',
                ClientNM: '',
                Description: context.autoArtCode ? context.autoArtCode : '',
                FactoryUD: '',
                ItemTypeID: null,
                StatusID: null,
                QuotationOfferDirectionID: null,
                ProformaInvoiceNo: '',
                OrderTypeID: '',
                BusinessDataStatusID: null,
                ShippedStatus: null,
                LDSFrom: null,
                LDSTo: null,
                CataloguePageNo: null,
                DeltaRate: null
            },
            sortedBy: 'OfferSeasonQuotationRequestID',
            sortedDirection: 'DESC',
            pageSize: 20,
            pageIndex: 1
        };
        self.serviceUrl = '';
        self.supportServiceUrl = '';
        self.token = '';
        self.data = [];
        self.factoryQuotationSearchResultDTOs = [];
        self.preferedFactoryDTOs = [];
        self.supportData = {
            seasons: jsHelper.getSeasons(),
            factories: [],
            yesNoValues: jsHelper.getYesNoValues(),
            deltaRateValues: [
                {value: 0, text: ' delta > 10% '},
                { value: 1, text: ' 5% <= delta < 10% ' },
                { value: 2, text: ' delta < 5% ' }
            ]
        };
        self.pageIndex = 1;
        self.totalRow = 0;
        self.totalPage = 0;

        self.gridHandler = {
            loadMore: function () {
                self.event.loadMore();
            },
            sort: function (sortedBy, sortedDirection) {
                self.data = [];
                self.searchFilter.sortedDirection = sortedDirection;
                self.pageIndex = 1;
                self.searchFilter.pageIndex = scope.pageIndex;
                self.searchFilter.sortedBy = sortedBy;
                self.event.search();
            },
            isTriggered: true
        };
        self.nullHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {

            },
            isTriggered: false
        };

        self.event = {
            init: function () {
                self.getInit(
                    function (data) {
                        if (data.message.type === 0) {
                            self.supportData.factories = data.data.factoryDTOs;
                            self.event.reload();
                        }
                        else {
                            console.log(data);
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
            },
            loadMore: function () {
                if (self.pageIndex < self.totalPage) {
                    self.pageIndex++;
                    self.searchFilter.pageIndex = self.pageIndex;
                    self.event.search();
                }
            },
            reload: function () {
                self.data = [];
                self.factoryQuotationSearchResultDTOs = [];
                self.pageIndex = 1;
                self.searchFilter.pageIndex = self.pageIndex;
                self.event.search();
            },
            search: function () {
                self.gridHandler.isTriggered = false;
                self.search(
                    function (data) {
                        Array.prototype.push.apply(self.data, data.data.data);
                        Array.prototype.push.apply(self.factoryQuotationSearchResultDTOs, data.data.factoryQuotationSearchResultDTOs);
                        Array.prototype.push.apply(self.preferedFactoryDTOs, data.data.preferedFactoryDTOs);
                        self.totalRow = data.totalRows;
                        self.totalPage = Math.ceil(data.totalRows / self.searchFilter.pageSize);                        
                        self.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        console.log(error);
                    }
                );
            },
            frmAssignFactory_show: function (item) {
                self.nullHandler.factoryUD = '';
                self.nullHandler.offerSeasonQuotationRequestID = item.offerSeasonQuotationRequestID;
                var dbFactories = self.factoryQuotationSearchResultDTOs.filter(o => o.offerSeasonQuotationRequestID === item.offerSeasonQuotationRequestID);
                angular.forEach(self.supportData.factories, function (factory) {
                    factory.isSelected = false;
                    if (dbFactories.length > 0 && dbFactories.filter(o => o.factoryID === factory.factoryID).length > 0) {
                        factory.isHidden = true;
                    }
                    else {
                        factory.isHidden = false;
                    }

                    if (self.preferedFactoryDTOs && self.preferedFactoryDTOs.filter(o => o.offerSeasonQuotationRequestID === item.offerSeasonQuotationRequestID && o.factoryID === factory.factoryID).length > 0) {
                        factory.isPrefered = 1;
                    }
                    else {
                        factory.isPrefered = 0;
                    }
                });
                $('#frmAssignFactory').modal('show');
            },
            frmAssignFactory_ok: function () {
                var dbFactories = self.supportData.factories.filter(o => o.isSelected === true);
                if (dbFactories.length > 0 && self.nullHandler.offerSeasonQuotationRequestID) {
                    var factoryIDs = dbFactories.map(a => a.factoryID);
                    self.update(
                        self.nullHandler.offerSeasonQuotationRequestID,
                        factoryIDs,
                        function (data) {
                            if (data.message.type === 0) {
                                angular.forEach(factoryIDs, function (id) {
                                    self.factoryQuotationSearchResultDTOs.push({
                                        offerSeasonQuotationRequestDetailID: null,
                                        offerSeasonQuotationRequestID: self.nullHandler.offerSeasonQuotationRequestID,
                                        factoryID: id,
                                        factoryUD: (self.supportData.factories.filter(o => o.factoryID === id).length > 0) ? self.supportData.factories.filter(o => o.factoryID === id)[0].factoryUD : ''
                                    });
                                });
                            }
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
                }
                $('#frmAssignFactory').modal('hide');
            }           
        };

        // get init data
        self.getInit = function (iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: 'POST',
                url: self.serviceUrl + 'getinitdata',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // get search data
        self.search = function (iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: 'POST',
                //contentType: "application/json",
                url: self.serviceUrl + 'gets',
                data: JSON.stringify(self.searchFilter),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // get data
        self.load = function (id, param, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            if (param === null) {
                param = {};
            }

            $http({
                method: 'POST',
                url: self.serviceUrl + 'get?id=' + id,
                data: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                console.log(response);
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // update data
        self.update = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'update?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // delete
        self.delete = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title !== null) {
                confirmedMsg = 'Delete ' + context.title + ': ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);

                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'delete?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type === 0) {
                        //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                        iSuccessCallback(response.data);
                    }
                    else {
                        jsHelper.showMessage('warning', response.data.message.message);
                        console.log(response.data.message.message);
                        iErrorCallback(response);
                    }
                }, function errorCallback(response) {
                    jsHelper.loadingSwitch(false);
                    jsHelper.showMessage('warning', response.data.message);
                    iErrorCallback(response);
                });
            }
        };

    }]);

})();