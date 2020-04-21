(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', bifaCompanyService);
    bifaCompanyService.$inject = ['$http', 'jsonService'];

    function bifaCompanyService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'BifaCompanyID';
        self.searchFilter.sortedDirection = 'ASC';

        self.updateBifaCity = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateBifaCity?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteBifaCity = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa city: ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteBifaCity?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updateBifaIndustry = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateBifaIndustry?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteBifaIndustry = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa industry: ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteBifaIndustry?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.getBifaAddress = function (id, param, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            if (param == null) {
                param = {};
            }
            $http({
                method: 'POST',
                url: this.serviceUrl + 'getBifaAddress?id=' + id,
                data: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type == 0) {
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
        self.updateBifaAddress = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateBifaAddress?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteBifaAddress = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa address: ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteBifaAddress?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updateBifaClub = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateBifaClub?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteBifaClub = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa club: ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteBifaClub?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updateBifaClubMember = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateBifaClubMember?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteBifaClubMember = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa club member in : ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteBifaClubMember?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updateBifaEmailAddress = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateBifaEmailAddress?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteBifaEmailAddress = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa email address : ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteBifaEmailAddress?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updateTelephone = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateTelephone?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteTelephone = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa telephone : ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteTelephone?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updatePerson = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updatePerson?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deletePerson = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete bifa person : ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deletePerson?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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

        self.updateEvent = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateEvent?id=' + id,
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);
                if (response.data.message.type == 0) {
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
        self.deleteEvent = function (id, title, iSuccessCallback, iErrorCallback) {
            var confirmedMsg = 'Delete the current data ?';
            if (title != null) {
                confirmedMsg = 'Do you want to delete event : ' + title + ' ?';
            }
            if (confirm(confirmedMsg)) {
                jsHelper.loadingSwitch(true);
                $http({
                    method: 'POST',
                    url: self.serviceUrl + 'deleteEvent?id=' + id,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + self.token
                    }
                }).then(function successCallback(response) {
                    jsHelper.loadingSwitch(false);

                    if (response.data.message.type == 0) {
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
        }
    };
})();