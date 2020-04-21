(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', bifaCompanyController);
    bifaCompanyController.$inject = ['$scope', '$timeout', 'dataService'];

    function bifaCompanyController($scope, $timeout, bifaCompanyService) {
        $scope.bifaCompany = null;
        $scope.bifaAddress = null;
        $scope.isOptionType = null;
        $scope.newID = -1;

        $scope.support = {
            bifaCities: [],
            bifaIndustries: [],
            bifaClubs: [],
            bifaPhoneTypes: [],
            positionGroup: [],
        };

        $scope.bifaCity = {
            bifaCityID: null,
            bifaCityNM: null,
        };

        $scope.bifaIndustry = {
            bifaIndustryID: null,
            bifaIndustryNM: null,
        };

        $scope.bifaClub = {
            bifaClubID: 0,
            bifaClubUD: null,
            bifaClubNM: null,
            description: null,
            updatedBy: null,
            updatedDate: null,
            employeeNM: null
        };

        $scope.bifaClubMember = {
            bifaClubMemberID: 0,
            bifaClubID: null,
            bifaClubUD: null,
            bifaClubNM: null,
            bifaCompanyID: null,
            joinedDate: null,
            remark: null,
            updatedBy: null,
            updatedDate: null,
            employeeNM: null,
        };

        $scope.emailAddress = null;
        $scope.listEmailAddress = [];

        $scope.telephone = null;

        $scope.person = null;

        $scope.eventData = null;

        $scope.event = {
            initBifaCompany: initBifaCompany,
            loadBifaCompany: loadBifaCompany,
            updateBifaCompany: updateBifaCompany,
            changeLogoBifaCompany: changeLogoBifaCompany,
            removeLogoBifaCompany: removeLogoBifaCompany,
            returnBifaCompany: returnBifaCompany,

            openBifaCity: openBifaCity,
            clearBifaCity: clearBifaCity,
            closeBifaCity: closeBifaCity,
            editBifaCity: editBifaCity,
            deleteBifaCity: deleteBifaCity,
            selectBifaCity: selectBifaCity,
            updateBifaCity: updateBifaCity,

            openBifaIndustry: openBifaIndustry,
            clearBifaIndustry: clearBifaIndustry,
            closeBifaIndustry: closeBifaIndustry,
            editBifaIndustry: editBifaIndustry,
            deleteBifaIndustry: deleteBifaIndustry,
            selectBifaIndustry: selectBifaIndustry,
            updateBifaIndustry: updateBifaIndustry,

            openBifaAddress: openBifaAddress,
            clearBifaAddress: clearBifaAddress,
            closeBifaAddress: closeBifaAddress,
            updateBifaAddress: updateBifaAddress,
            deleteBifaAddress: deleteBifaAddress,

            openBifaClubMember: openBifaClubMember,
            clearBifaClubMember: clearBifaClubMember,
            closeBifaClubMember: closeBifaClubMember,
            updateBifaClubMember: updateBifaClubMember,
            deleteBifaClubMember: deleteBifaClubMember,

            openBifaClub: openBifaClub,
            clearBifaClub: clearBifaClub,
            closeBifaClub: closeBifaClub,
            editBifaClub: editBifaClub,
            deleteBifaClub: deleteBifaClub,
            selectBifaClub: selectBifaClub,
            updateBifaClub: updateBifaClub,

            openBifaEmailAddress: openBifaEmailAddress,
            clearBifaEmailAddress: clearBifaEmailAddress,
            closeBifaEmailAddress: closeBifaEmailAddress,
            updateBifaEmailAddress: updateBifaEmailAddress,
            deleteBifaEmailAddress: deleteBifaEmailAddress,
            addBifaEmailAddress: addBifaEmailAddress,

            openTelephone: openTelephone,
            clearTelephone: clearTelephone,
            closeTelephone: closeTelephone,
            updateTelephone: updateTelephone,
            deleteTelephone: deleteTelephone,

            openPerson: openPerson,
            clearPerson: clearPerson,
            closePerson: closePerson,
            updatePerson: updatePerson,
            deletePerson: deletePerson,

            openEvent: openEvent,
            clearEvent: clearEvent,
            closeEvent: closeEvent,
            updateEvent: updateEvent,
            deleteEvent: deleteEvent,
            addAttachmentFile: addAttachmentFile,
            removeAttachmentFile: removeAttachmentFile,
        };

        $scope.method = {
            getNewID: getNewID,
            getElementExist: getElementExist,
            getPhoneTypeNM: getPhoneTypeNM,
            getPersonInCompany: getPersonInCompany,
            getEventCompany: getEventCompany,
        };

        $timeout(function () {
            $scope.event.initBifaCompany();
        }, 0);

        function initBifaCompany() {
            bifaCompanyService.serviceUrl = context.serviceUrl;
            bifaCompanyService.token = context.token;

            bifaCompanyService.getInit(
                function (data) {
                    if (data.message.type == 0) {
                        $scope.support.bifaCities = data.data.bifaCities;
                        $scope.support.bifaIndustries = data.data.bifaIndustries;
                        $scope.support.bifaClubs = data.data.bifaClubs;
                        $scope.support.bifaPhoneTypes = data.data.bifaPhoneTypes;
                        $scope.support.positionGroup = data.data.bifaPositionGroups;

                        $scope.event.loadBifaCompany();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function loadBifaCompany() {
            bifaCompanyService.load(
                context.id,
                null,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.bifaCompany = data.data.bifaCompany;

                        jQuery('#content').show();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function updateBifaCompany() {
            if ($scope.bifaCompany.bifaCompanyUD == null || $scope.bifaCompany.bifaCompanyUD == '') {
                jsHelper.showMessage('warning', 'Please input Bifa company code!');
            } else {
                if ($scope.bifaCompany.bifaCompanyNM == null || $scope.bifaCompany.bifaCompanyNM == '') {
                    jsHelper.showMessage('warning', 'Please input Bifa company name!');
                } else {
                    bifaCompanyService.update(
                        context.id,
                        $scope.bifaCompany,
                        function (data) {
                            window.location = context.refreshUrl + data.data.bifaCompany.bifaCompanyID;
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.data.exceptionMessage);
                        });
                }
            }
        };
        function changeLogoBifaCompany() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.bifaCompany.logo_DisplayUrl = img.fileURL;
                        scope.bifaCompany.logo_NewFile = img.filename;
                        scope.bifaCompany.logo_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        };
        function removeLogoBifaCompany() {
            $scope.bifaCompany.logo_DisplayUrl = '';
            $scope.bifaCompany.logo_NewFile = '';
            $scope.bifaCompany.logo_HasChange = true;
        };
        function returnBifaCompany() {
            window.location = context.backUrl;
        };

        function openBifaCity(isOptionType) {
            $scope.bifaCity.bifaCityID = 0;
            $scope.bifaCity.bifaCityNM = null;

            $scope.isOptionType = isOptionType;

            if (isOptionType == 1) {
                jQuery('#frmBifaAddress').modal('hide');
            }

            jQuery('#frmBifaCity').modal();
        };
        function clearBifaCity() {
            $scope.bifaCity.bifaCityID = 0;
            $scope.bifaCity.bifaCityNM = null;
        };
        function closeBifaCity() {
            jQuery('#frmBifaCity').modal('hide');

            if ($scope.isOptionType == 1) {
                jQuery('#frmBifaAddress').modal();
            }
        };
        function editBifaCity(bifaCity) {
            if (bifaCity != null) {
                $scope.bifaCity.bifaCityID = bifaCity.bifaCityID;
                $scope.bifaCity.bifaCityNM = bifaCity.bifaCityNM;
            }
        };
        function deleteBifaCity($event, bifaCity) {
            $event.preventDefault();

            bifaCompanyService.deleteBifaCity(
                bifaCity.bifaCityID,
                bifaCity.bifaCityNM,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.support.bifaCities.indexOf(bifaCity);
                        $scope.support.bifaCities.splice(index, 1);

                        $scope.bifaCity.bifaCityID = 0;
                        $scope.bifaCity.bifaCityNM = null;

                        if ($scope.bifaCompany.bifaCityID == bifaCity.bifaCityID) {
                            $scope.bifaCompany.bifaCityID = null;
                            $scope.bifaCompany.bifaCityNM = null;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function selectBifaCity(bifaCity) {
            if (bifaCity != null) {
                jQuery('#frmBifaCity').modal('hide');

                if ($scope.isOptionType == null) {
                    $scope.bifaCompany.bifaCityID = bifaCity.bifaCityID;
                    $scope.bifaCompany.bifaCityNM = bifaCity.bifaCityNM;
                }

                if ($scope.isOptionType == 1) {
                    $scope.bifaAddress.bifaCityID = bifaCity.bifaCityID;
                    $scope.bifaAddress.bifaCityNM = bifaCity.bifaCityNM;

                    jQuery('#frmBifaAddress').modal();
                }
            }
        };
        function updateBifaCity($event) {
            $event.preventDefault();

            if ($scope.bifaCity.bifaCityNM != null && $scope.bifaCity.bifaCityNM != '') {
                bifaCompanyService.updateBifaCity(
                    $scope.bifaCity.bifaCityID,
                    $scope.bifaCity,
                    function (data) {
                        if (data.message.type == 0) {
                            if ($scope.bifaCity.bifaCityID == 0) {
                                $scope.support.bifaCities.push(data.data);
                            } else {

                                angular.forEach($scope.support.bifaCities, function (item) {
                                    if (item.bifaCityID == data.data.bifaCityID) {
                                        item.bifaCityNM = data.data.bifaCityNM;
                                    }
                                });
                            }

                            $scope.bifaCity.bifaCityID = 0;
                            $scope.bifaCity.bifaCityNM = null;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            }
        };

        function openBifaIndustry() {
            $scope.bifaIndustry.bifaIndustryID = 0;
            $scope.bifaIndustry.bifaIndustryNM = null;

            jQuery('#frmBifaIndustry').modal();
        };
        function clearBifaIndustry() {
            $scope.bifaIndustry.bifaIndustryID = 0;
            $scope.bifaIndustry.bifaIndustryNM = null;
        };
        function closeBifaIndustry() {
            jQuery('#frmBifaIndustry').modal('hide');
        };
        function editBifaIndustry(bifaIndustry) {
            if (bifaIndustry != null) {
                $scope.bifaIndustry.bifaIndustryID = bifaIndustry.bifaIndustryID;
                $scope.bifaIndustry.bifaIndustryNM = bifaIndustry.bifaIndustryNM;
            }
        };
        function deleteBifaIndustry($event, bifaIndustry) {
            $event.preventDefault();

            bifaCompanyService.deleteBifaIndustry(
                bifaIndustry.bifaIndustryID,
                bifaIndustry.bifaIndustryNM,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.support.bifaIndustries.indexOf(bifaIndustry);
                        $scope.support.bifaIndustries.splice(index, 1);

                        $scope.bifaIndustry.bifaIndustryID = 0;
                        $scope.bifaIndustry.bifaIndustryNM = null;

                        if ($scope.bifaCompany.bifaIndustryID == bifaIndustry.bifaIndustryID) {
                            $scope.bifaCompany.bifaIndustryID = null;
                            $scope.bifaCompany.bifaIndustryNM = null;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function selectBifaIndustry(bifaIndustry) {
            if (bifaIndustry != null) {
                $scope.bifaCompany.bifaIndustryID = bifaIndustry.bifaIndustryID;
                $scope.bifaCompany.bifaIndustryNM = bifaIndustry.bifaIndustryNM;

                jQuery('#frmBifaIndustry').modal('hide');
            }
        };
        function updateBifaIndustry($event) {
            $event.preventDefault();

            if ($scope.bifaIndustry.bifaIndustryNM != null && $scope.bifaIndustry.bifaIndustryNM != '') {
                bifaCompanyService.updateBifaIndustry(
                    $scope.bifaIndustry.bifaIndustryID,
                    $scope.bifaIndustry,
                    function (data) {
                        if (data.message.type == 0) {
                            if ($scope.bifaIndustry.bifaIndustryID == 0) {
                                $scope.support.bifaIndustries.push(data.data);
                            } else {

                                angular.forEach($scope.support.bifaIndustries, function (item) {
                                    if (item.bifaIndustryID == data.data.bifaIndustryID) {
                                        item.bifaIndustryNM = data.data.bifaIndustryNM;
                                    }
                                });
                            }

                            $scope.bifaIndustry.bifaIndustryID = 0;
                            $scope.bifaIndustry.bifaIndustryNM = null;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            }
        };

        function openBifaAddress(id) {
            bifaCompanyService.getBifaAddress(
                id,
                null,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.bifaAddress = data.data;
                        if ($scope.bifaCompany != null) {
                            $scope.bifaAddress.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
                        }

                        jQuery('#frmBifaAddress').modal();
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function clearBifaAddress() {
            $scope.bifaAddress.addressLine1 = null;
            $scope.bifaAddress.addressLine2 = null;
            $scope.bifaAddress.bifaAddressID = 0;
            $scope.bifaAddress.bifaCityID = null;
            $scope.bifaAddress.remark = null;

            if ($scope.bifaCompany != null) {
                $scope.bifaAddress.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
            }
        };
        function closeBifaAddress() {
            jQuery('#frmBifaAddress').modal('hide');
        };
        function updateBifaAddress($event) {
            $event.preventDefault();
            bifaCompanyService.updateBifaAddress(
                $scope.bifaAddress.bifaAddressID,
                $scope.bifaAddress,
                function (data) {
                    if (data.message.type == 0) {
                        if ($scope.bifaCompany.bifaAddresses.length == 0 || $scope.bifaAddress.bifaAddressID == 0) {
                            $scope.bifaCompany.bifaAddresses.push(data.data);
                        } else {
                            angular.forEach($scope.bifaCompany.bifaAddresses, function (item) {
                                if (item.bifaAddressID == data.data.bifaAddressID) {
                                    item.addressLine1 = data.data.addressLine1;
                                    item.addressLine2 = data.data.addressLine2;
                                    item.bifaCityID = data.data.bifaCityID;
                                    item.remark = data.data.remark;
                                    item.updatedBy = data.data.updatedBy;
                                    item.updatedDate = data.data.updatedDate;
                                    item.employeeNM = data.data.employeeNM;
                                    item.bifaCityNM = data.data.bifaCityNM;
                                }
                            });
                        }

                        jQuery('#frmBifaAddress').modal('hide');
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function deleteBifaAddress($event, item) {
            $event.preventDefault();

            bifaCompanyService.deleteBifaAddress(
                item.bifaAddressID,
                item.addressLine1,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.bifaCompany.bifaAddresses.indexOf(item);
                        $scope.bifaCompany.bifaAddresses.splice(index, 1);
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };

        function openBifaClubMember(item) {
            if (item != null) {
                $scope.bifaClubMember.bifaClubMemberID = item.bifaClubMemberID;
                $scope.bifaClubMember.bifaClubID = item.bifaClubID;
                $scope.bifaClubMember.bifaClubUD = item.bifaClubUD;
                $scope.bifaClubMember.bifaClubNM = item.bifaClubNM;
                $scope.bifaClubMember.joinedDate = item.joinedDate;
                $scope.bifaClubMember.remark = item.remark;
                $scope.bifaClubMember.updatedBy = item.updatedBy;
                $scope.bifaClubMember.updatedDate = item.updatedDate;
                $scope.bifaClubMember.employeeNM = item.employeeNM;
            } else {
                $scope.bifaClubMember.bifaClubMemberID = 0;
                $scope.bifaClubMember.bifaClubID = null;
                $scope.bifaClubMember.bifaClubUD = null;
                $scope.bifaClubMember.bifaClubNM = null;
                $scope.bifaClubMember.joinedDate = null;
                $scope.bifaClubMember.remark = null;
                $scope.bifaClubMember.updatedBy = null;
                $scope.bifaClubMember.updatedDate = null;
                $scope.bifaClubMember.employeeNM = null;
            }

            if ($scope.bifaCompany != null) {
                $scope.bifaClubMember.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
            }

            jQuery('#frmBifaClubMember').modal();
        };
        function clearBifaClubMember() {
            $scope.bifaClubMember.bifaClubMemberID = 0;
            $scope.bifaClubMember.bifaClubID = null;
            $scope.bifaClubMember.bifaClubUD = null;
            $scope.bifaClubMember.bifaClubNM = null;
            $scope.bifaClubMember.joinedDate = null;
            $scope.bifaClubMember.remark = null;
            $scope.bifaClubMember.updatedBy = null;
            $scope.bifaClubMember.updatedDate = null;
            $scope.bifaClubMember.employeeNM = null;
        };
        function closeBifaClubMember() {
            jQuery('#frmBifaClubMember').modal('hide');
        };
        function updateBifaClubMember() {
            if ($scope.bifaClubMember.bifaClubID != null && $scope.bifaClubMember.bifaClubID != '') {
                bifaCompanyService.updateBifaClubMember(
                    $scope.bifaClubMember.bifaClubMemberID,
                    $scope.bifaClubMember,
                    function (data) {
                        if (data.message.type == 0) {
                            if ($scope.bifaCompany.bifaClubMembers.length == 0 || $scope.bifaClubMember.bifaClubMemberID == 0) {
                                $scope.bifaCompany.bifaClubMembers.push(data.data);
                            } else {
                                angular.forEach($scope.bifaCompany.bifaClubMembers, function (item) {
                                    if (item.bifaClubMemberID == data.data.bifaClubMemberID) {
                                        item.bifaClubID = data.data.bifaClubID;
                                        item.bifaClubUD = data.data.bifaClubUD;
                                        item.bifaClubNM = data.data.bifaClubNM;
                                        item.joinedDate = data.data.joinedDate;
                                        item.remark = data.data.remark;
                                        item.updatedBy = data.data.updatedBy;
                                        item.updatedDate = data.data.updatedDate;
                                        item.employeeNM = data.data.employeeNM;
                                    }
                                });
                            }

                            jQuery('#frmBifaClubMember').modal('hide');
                        } else {
                            jsHelper.showMessage('warning', data.message.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            }
        };
        function deleteBifaClubMember(item) {
            bifaCompanyService.deleteBifaClubMember(
                item.bifaClubMemberID,
                item.bifaClubNM,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.bifaCompany.bifaClubMembers.indexOf(item);
                        $scope.bifaCompany.bifaClubMembers.splice(index, 1);
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };

        function openBifaClub() {
            jQuery('#frmBifaClubMember').modal('hide');
            jQuery('#frmBifaClub').modal();
        };
        function clearBifaClub() {
            $scope.bifaClub.bifaClubID = 0;
            $scope.bifaClub.bifaClubUD = null;
            $scope.bifaClub.bifaClubNM = null;
            $scope.bifaClub.description = null;
            $scope.bifaClub.updatedBy = null;
            $scope.bifaClub.updatedDate = null;
            $scope.bifaClub.employeeNM = null;
        };
        function closeBifaClub() {
            jQuery('#frmBifaClub').modal('hide');
            jQuery('#frmBifaClubMember').modal();
        };
        function editBifaClub(item) {
            if (item != null) {
                $scope.bifaClub.bifaClubID = item.bifaClubID;
                $scope.bifaClub.bifaClubUD = item.bifaClubUD;
                $scope.bifaClub.bifaClubNM = item.bifaClubNM;
                $scope.bifaClub.description = item.description;
                $scope.bifaClub.updatedBy = item.updatedBy;
                $scope.bifaClub.updatedDate = item.updatedDate;
                $scope.bifaClub.employeeNM = item.employeeNM;
            }
        };
        function deleteBifaClub(item) {
            bifaCompanyService.deleteBifaClub(
                item.bifaClubID,
                item.bifaClubUD,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.support.bifaClubs.indexOf(item);
                        $scope.support.bifaClubs.splice(index, 1);

                        $scope.bifaClub.bifaClubID = 0;
                        $scope.bifaClub.bifaClubUD = null;
                        $scope.bifaClub.bifaClubNM = null;
                        $scope.bifaClub.description = null;
                        $scope.bifaClub.updatedBy = null;
                        $scope.bifaClub.updatedDate = null;
                        $scope.bifaClub.employeeNM = null;

                        if ($scope.bifaClubMember.bifaClubID == item.bifaClubID) {
                            $scope.bifaClubMember.bifaClubID = null;
                            $scope.bifaClubMember.bifaClubUD = null;
                            $scope.bifaClubMember.bifaClubNM = null;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function selectBifaClub(item) {
            if (item != null) {
                $timeout(function () {
                    $scope.bifaClubMember.bifaClubID = item.bifaClubID;
                    $scope.bifaClubMember.bifaClubUD = item.bifaClubUD;
                    $scope.bifaClubMember.bifaClubNM = item.bifaClubNM;
                }, 0);

                jQuery('#frmBifaClub').modal('hide');
                jQuery('#frmBifaClubMember').modal();
            }
        };
        function updateBifaClub() {
            if ($scope.bifaClub.bifaClubUD != null && $scope.bifaClub.bifaClubUD != '') {
                if ($scope.bifaClub.bifaClubNM != null && $scope.bifaClub.bifaClubNM != '') {
                    bifaCompanyService.updateBifaClub(
                        $scope.bifaClub.bifaClubID,
                        $scope.bifaClub,
                        function (data) {
                            if (data.message.type == 0) {
                                if ($scope.bifaClub.bifaClubID == 0) {
                                    $scope.support.bifaClubs.push(data.data);
                                } else {

                                    angular.forEach($scope.support.bifaClubs, function (item) {
                                        if (item.bifaClubID == data.data.bifaClubID) {
                                            item.bifaClubUD = data.data.bifaClubUD;
                                            item.bifaClubNM = data.data.bifaClubNM;
                                            item.description = data.data.description;
                                            item.updatedBy = data.data.updatedBy;
                                            item.updatedDate = data.data.updatedDate;
                                            item.employeeNM = data.data.employeeNM;
                                        }
                                    });
                                }

                                $scope.bifaClub.bifaClubID = 0;
                                $scope.bifaClub.bifaClubUD = null;
                                $scope.bifaClub.bifaClubNM = null;
                                $scope.bifaClub.description = null;
                                $scope.bifaClub.updatedBy = null;
                                $scope.bifaClub.updatedDate = null;
                                $scope.bifaClub.employeeNM = null;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.data.exceptionMessage);
                        });
                }
            }
        };

        function openBifaEmailAddress(item) {
            if (item != null) {
                $scope.emailAddress = {
                    bifaEmailAddressID: item.bifaEmailAddressID,
                    bifaCompanyID: item.bifaCompanyID,
                    bifaPersonID: item.bifaPersonID,
                    emailAddress: item.emailAddress,
                    updatedBy: item.updatedBy,
                    updatedDate: item.updatedDate,
                    employeeNM: item.employeeNM
                };
            } else {
                var newEmailAddress = {
                    bifaEmailAddressID: $scope.method.getNewID(),
                    bifaCompanyID: null,
                    bifaPersonID: null,
                    emailAddress: null,
                    updatedBy: null,
                    updatedDate: null,
                    employeeNM: null
                };

                $scope.emailAddress = newEmailAddress;

                if ($scope.person !== null) {
                    $scope.emailAddress.bifaPersonID = $scope.person.bifaPersonID;
                } else {
                    if ($scope.bifaCompany != null) {
                        $scope.emailAddress.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
                    }
                }
            }

            if ($scope.listEmailAddress.length > 0) {
                $scope.listEmailAddress = [];
            }

            if ($scope.person != null) {
                jQuery('#frmPerson').modal('hide');
                jQuery('#frmBifaEmailAddress').modal();
            } else {
                jQuery('#frmBifaEmailAddress').modal();
            }
        };
        function clearBifaEmailAddress() {
            var newEmailAddress = {
                bifaEmailAddressID: $scope.method.getNewID(),
                bifaCompanyID: null,
                bifaPersonID: null,
                emailAddress: null,
                updatedBy: null,
                updatedDate: null,
                employeeNM: null
            };

            $scope.emailAddress = newEmailAddress;

            if ($scope.listEmailAddress.length > 0) {
                $scope.listEmailAddress = [];
            }
        };
        function closeBifaEmailAddress() {
            jQuery('#frmBifaEmailAddress').modal('hide');

            if ($scope.person !== null) {
                jQuery('#frmPerson').modal();
            }
        };
        function updateBifaEmailAddress() {
            var listUpdate = [];

            if ($scope.emailAddress.emailAddress != null && $scope.emailAddress.emailAddress != '') {
                listUpdate.push($scope.emailAddress);
            }

            if ($scope.listEmailAddress.length > 0) {
                angular.forEach($scope.listEmailAddress, function (item) {
                    if (item.emailAddress != null && item.emailAddress != '') {
                        listUpdate.push(item);
                    }
                });
            }

            if (!$scope.person) {
                bifaCompanyService.updateBifaEmailAddress(
                    0,
                    listUpdate,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.bifaCompany.bifaEmailAddresses = [];

                            angular.forEach(data.data, function (item) {
                                if (item.bifaCompanyID == $scope.bifaCompany.bifaCompanyID) {
                                    $scope.bifaCompany.bifaEmailAddresses.push(item);
                                }
                            });

                            jQuery('#frmBifaEmailAddress').modal('hide');
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            } else {
                for (var i = 0; i < listUpdate.length; i++) {
                    var item = listUpdate[i];
                    var index = $scope.method.getElementExist(item, $scope.person.bifaEmailAddresses, 'email');

                    if (index != null) {
                        $scope.person.bifaEmailAddresses[index] = item;
                    } else {
                        $scope.person.bifaEmailAddresses.push(item);
                    }
                }

                jQuery('#frmBifaEmailAddress').modal('hide');
                jQuery('#frmPerson').modal();
            }
        };
        function deleteBifaEmailAddress(item) {
            if ($scope.person == null) {
                bifaCompanyService.deleteBifaEmailAddress(
                item.bifaEmailAddressID,
                item.emailAddress,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.bifaCompany.bifaEmailAddresses.indexOf(item);
                        $scope.bifaCompany.bifaEmailAddresses.splice(index, 1);
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
            } else {
                var index = $scope.person.bifaEmailAddresses.indexOf(item);
                $scope.person.bifaEmailAddresses.splice(index, 1);
            }
        };
        function addBifaEmailAddress() {
            var eleEmailAddress = {
                bifaEmailAddressID: $scope.method.getNewID(),
                bifaCompanyID: null,
                bifaPersonID: null,
                emailAddress: null,
            };

            if ($scope.bifaCompany != null) {
                eleEmailAddress.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
            }

            $scope.listEmailAddress.push(eleEmailAddress);
        };

        function openTelephone(item) {
            if (item !== null) {
                var getTelephone = {
                    bifaTelephoneID: item.bifaTelephoneID,
                    bifaPersonID: item.bifaPersonID,
                    bifaCompanyID: item.bifaCompanyID,
                    phoneTypeID: item.phoneTypeID,
                    phoneTypeNM: item.phoneTypeNM,
                    phoneNumber: item.phoneNumber,
                    phoneExtension: item.phoneExtension,
                    remark: item.remark,
                    isPrimary: item.isPrimary,
                    updatedBy: item.updatedBy,
                    updatedDate: item.updatedDate,
                    employeNM: item.employeNM,
                };

                $scope.telephone = getTelephone;
            } else {
                var newTelephone = {
                    bifaTelephoneID: 0,
                    bifaPersonID: null,
                    bifaCompanyID: null,
                    phoneTypeID: null,
                    phoneTypeNM: null,
                    phoneNumber: null,
                    phoneExtension: null,
                    remark: null,
                    isPrimary: null,
                    updatedBy: null,
                    updatedDate: null,
                    employeNM: null,
                };

                $scope.telephone = newTelephone;

                if ($scope.person) {
                    $scope.telephone.bifaPersonID = $scope.person.bifaPersonID;
                } else {
                    if ($scope.bifaCompany != null) {
                        $scope.telephone.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
                    }
                }
            }

            // Display form pop-up input data Telephone
            if ($scope.person != null) {
                jQuery('#frmPerson').modal('hide');
            }

            jQuery('#frmBifaTelephone').modal();
        };
        function clearTelephone() {
            var newTelephone = {
                bifaTelephoneID: 0,
                bifaPersonID: null,
                bifaCompanyID: null,
                phoneTypeID: null,
                phoneTypeNM: null,
                phoneNumber: null,
                phoneExtension: null,
                remark: null,
                isPrimary: null,
                updatedBy: null,
                updatedDate: null,
                employeNM: null,
            };

            $scope.telephone = newTelephone;
        };
        function closeTelephone() {
            jQuery('#frmBifaTelephone').modal('hide');
            if ($scope.person !== null) {
                jQuery('#frmPerson').modal();
            }
        };
        function updateTelephone() {
            if ($scope.person == null) {
                bifaCompanyService.updateTelephone(
                    $scope.telephone.bifaTelephoneID,
                    $scope.telephone,
                    function (data) {
                        if (data.message.type === 0) {
                            /// List bifa telephone not item and new item
                            if ($scope.bifaCompany.bifaTelephones.length === 0 || $scope.telephone.bifaTelephoneID === 0) {
                                $scope.bifaCompany.bifaTelephones.push(data.data);
                            } else { /// exist item
                                angular.forEach($scope.bifaCompany.bifaTelephones, function (item) {
                                    if (item.bifaTelephoneID == data.data.bifaTelephoneID) {
                                        item.bifaTelephoneID = data.data.bifaTelephoneID;
                                        item.bifaPersonID = data.data.bifaPersonID;
                                        item.bifaCompanyID = data.data.bifaCompanyID;
                                        item.phoneTypeID = data.data.phoneTypeID;
                                        item.phoneTypeNM = data.data.phoneTypeNM;
                                        item.phoneNumber = data.data.phoneNumber;
                                        item.phoneExtension = data.data.phoneExtension;
                                        item.remark = data.data.remark;
                                        item.isPrimary = data.data.isPrimary;
                                        item.updatedBy = data.data.updatedBy;
                                        item.updatedDate = data.data.updatedDate;
                                        item.employeNM = data.data.employeNM;
                                    }
                                });
                            }
                            jQuery('#frmBifaTelephone').modal('hide');
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            } else {
                if ($scope.telephone != null && $scope.telephone.bifaTelephoneID == 0) {
                    $scope.telephone.bifaTelephoneID = $scope.method.getNewID();
                }

                var index = $scope.method.getElementExist($scope.telephone, $scope.person.bifaTelephones, 'phone');

                if (index == null) {
                    $scope.person.bifaTelephones.push($scope.telephone);
                } else {
                    $scope.person.bifaTelephones[index] = $scope.telephone;
                }

                jQuery('#frmBifaTelephone').modal('hide');
                jQuery('#frmPerson').modal();
            }
        };
        function deleteTelephone(item) {
            if ($scope.person == null) {
                bifaCompanyService.deleteTelephone(
                    item.bifaTelephoneID,
                    item.phoneNumber,
                    function (data) {
                        if (data.message.type === 0) {
                            var index = $scope.bifaCompany.bifaTelephones.indexOf(item);
                            $scope.bifaCompany.bifaTelephones.splice(index, 1);
                        } else {
                            jsHelper.showMessage('warning', data.message.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            } else {
                var index = $scope.person.bifaTelephones.indexOf(item);
                $scope.person.bifaTelephones.splice(index, 1);
            }
        };

        function openPerson(item) {
            if (item !== null) { // Set item for $scope.person
                var getPerson = {
                    bifaPersonID: item.bifaPersonID,
                    fullName: item.fullName,
                    callingName: item.callingName,
                    dateOfBirth: item.dateOfBirth,
                    jobTitle: item.jobTitle,
                    jobDescription: item.jobDescription,
                    bifaCompanyID: item.bifaCompanyID,
                    positionGroupID: item.positionGroupID,
                    positionGroupNM: item.positionGroupNM,
                    remark: item.remark,
                    updatedBy: item.updatedBy,
                    updatedDate: item.updatedDate,
                    employeeNM: item.employeeNM,

                    bifaTelephones: item.bifaTelephones,
                    bifaEmailAddresses: item.bifaEmailAddresses,
                };

                $scope.person = getPerson;
            } else {
                var newPerson = {
                    bifaPersonID: 0,
                    fullName: null,
                    callingName: null,
                    dateOfBirth: null,
                    jobTitle: null,
                    jobDescription: null,
                    bifaCompanyID: null,
                    positionGroupID: null,
                    positionGroupNM: null,
                    remark: null,
                    updatedBy: null,
                    updatedDate: null,
                    employeeNM: null,

                    bifaTelephones: [],
                    bifaEmailAddresses: []
                };

                $scope.person = newPerson;

                if ($scope.bifaCompany !== null) { // $scope.bifaCompany is not null
                    $scope.person.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
                }
            }

            // Open form pop-up person
            jQuery('#frmPerson').modal();
        };
        function clearPerson() {
            var newPerson = {
                bifaPersonID: 0,
                fullName: null,
                callingName: null,
                dateOfBirth: null,
                jobTitle: null,
                jobDescription: null,
                bifaCompanyID: null,
                positionGroupID: null,
                positionGroupNM: null,
                remark: null,
                updatedBy: null,
                updatedDate: null,
                employeeNM: null,

                bifaTelephones: [],
                bifaEmailAddresses: []
            };

            $scope.person = newPerson;
        };
        function closePerson() {
            $scope.person = null;
            jQuery('#frmPerson').modal('hide');
        };
        function updatePerson() {
            bifaCompanyService.updatePerson(
                $scope.person.bifaPersonID,
                $scope.person,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.method.getPersonInCompany(data.data, $scope.bifaCompany.bifaPersons);
                        if (index != null) {
                            $scope.bifaCompany.bifaPersons[index] = data.data;
                        } else {
                            $scope.bifaCompany.bifaPersons.push(data.data);
                        }

                        $scope.person = null;
                        jQuery('#frmPerson').modal('hide');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function deletePerson(item) {
            bifaCompanyService.deletePerson(
                    item.bifaPersonID,
                    item.fullName,
                    function (data) {
                        if (data.message.type === 0) {
                            var index = $scope.bifaCompany.bifaPersons.indexOf(item);
                            $scope.bifaCompany.bifaPersons.splice(index, 1);
                        } else {
                            jsHelper.showMessage('warning', data.message.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
        };

        function openEvent(item) {
            var event = {
                bifaEventID: 0,
                bifaEventUD: null,
                bifaEventNM: null,
                bifaCompanyID: null,
                addressLine1: null,
                addressLine2: null,
                description: null,
                startDate: null,
                endDate: null,
                updatedBy: null,
                updatedDate: null,
                employeeNM: null,
                bifaEventFiles: [],
                bifaEventParticipants: []
            };

            if (item != null) {
                event = item;
                $scope.eventData = event;
            } else {
                $scope.eventData = event;
                if ($scope.bifaCompany != null) {
                    $scope.eventData.bifaCompanyID = $scope.bifaCompany.bifaCompanyID;
                }
            }

            jQuery('#frmEvent').modal();
        };
        function clearEvent() {
            var event = {
                bifaEventID: 0,
                bifaEventUD: null,
                bifaEventNM: null,
                bifaCompanyID: null,
                addressLine1: null,
                addressLine2: null,
                description: null,
                startDate: null,
                endDate: null,
                updatedBy: null,
                updatedDate: null,
                employeeNM: null,
                bifaEventFiles: [],
                bifaEventParticipants: []
            };
            $scope.eventData = event;
        };
        function closeEvent() {
            jQuery('#frmEvent').modal('hide');
        };
        function updateEvent(id, data, company) {
            bifaCompanyService.updateEvent(
                id,
                data,
                function (data) {
                    if (data.message.type == 0) {
                        var index = $scope.method.getEventCompany(data.data, company.bifaEvents);
                        if (index != null) {
                            company.bifaEvents[index] = data.data;
                        } else {
                            company.bifaEvents.push(data.data);
                        }

                        jQuery('#frmEvent').modal('hide');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function deleteEvent(id, data, company) {
            bifaCompanyService.deleteEvent(
                id,
                data.bifaEventUD,
                function (data) {
                    if (data.message.type === 0) {
                        var index = company.bifaEvents.indexOf(data);
                        company.bifaEvents.splice(index, 1);
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                });
        };
        function addAttachmentFile() {
            userFileMng.callback = function () {
                var newFiles = [];
                scope.$apply(function () {
                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        newFiles.push({
                            bifaEventFileID: $scope.method.getNewID(),
                            fileUD: '',
                            description: '',
                            updatedBy: '',
                            updatedDate: '',
                            friendlyName: value.fileName,
                            fileLocation: value.filePath,
                            hasChange: true,
                            newFile: value.fileName
                        });
                    }, null);

                    $scope.eventData.bifaEventFiles = newFiles;
                });
            };
            userFileMng.open();
        };
        function removeAttachmentFile(item) {
            if (item != null) {
                var index = $scope.eventData.bifaEventFiles.indexOf(item);
                $scope.eventData.bifaEventFiles.splice(index, 1);
            }
        };

        function getNewID() {
            return $scope.newID = $scope.newID - 1;
        };
        function getElementExist(ele, list, type) {
            if (list.length === 0) {
                return null;
            }

            for (var i = 0; i < list.length; i++) {
                if (type === 'email') {
                    if (ele.bifaEmailAddressID === list[i].bifaEmailAddressID) {
                        return i;
                    }
                }

                if (type === 'phone') {
                    if (ele.bifaTelephoneID === list[i].bifaTelephoneID) {
                        return i;
                    }
                }
            }

            return null;
        };
        function getPhoneTypeNM(phoneTypeID) {
            for (var i = 0; i < $scope.support.bifaPhoneTypes.length; i++) {
                var item = $scope.support.bifaPhoneTypes[i];
                if (phoneTypeID === item.phoneTypeID) {
                    return item.phoneTypeNM;
                }
            }

            return null;
        };
        function getPersonInCompany(person, persons) {
            if (persons.length == 0) {
                return null;
            }

            for (var i = 0; i < persons.length; i++) {
                var item = persons[i];
                if (person.bifaPersonID == item.bifaPersonID) {
                    return i;
                }
            }

            return null;
        };
        function getEventCompany(event, events) {
            if (events.length == 0) {
                return null;
            }

            for (var i = 0; i < events.length; i++) {
                var item = events[i];
                if (event.bifaEventID == item.bifaEventID) {
                    return i;
                }
            }

            return null;
        };
    };
})();