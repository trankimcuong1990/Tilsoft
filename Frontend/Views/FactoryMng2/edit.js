jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.filterResult();
    }
});//
// APP START
//
$("#factoryRawMaterialID").select2({
    closeOnSelect: true,
});

$("#personID").select2({
    closeOnSelect: true,
});

$("#pricingID").select2({
    closeOnSelect: true,
});

$("#directorID").select2({
    closeOnSelect: true,
});

$("#managerID").select2({
    closeOnSelect: true,
});

$("#sampleID").select2({
    closeOnSelect: true,
});


var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.filter("trustUrl", ['$sce', function ($sce) {
    return function (recordingUrl) {
        return $sce.trustAsResourceUrl(recordingUrl);
    };
}]);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;
    $scope.employees = null;
    $scope.employeeDepartmentDTOs = null;
    $scope.factoryRawMaterialSupplierList = null;
    $scope.locations = null;
    $scope.currentImage = [];
    $scope.currentCard = [];
    $scope.supplierList = null;
    $scope.productGroupDTOs = null;

    $scope.directorSelected = [];
    $scope.personSelected = [];
    $scope.pricingSelected = [];
    $scope.managerSelected = [];
    $scope.sampleSelected = [];
    $scope.supplierSelected = [];

    $scope.currentSeasonCapacity = [];
    $scope.previousSeasonCapacity = [];
    $scope.arrSeason = [];
    $scope.weekInforRange = [];
    $scope.arrDataByWeek = [];

    $scope.totalpage = 0;
    $scope.totalrows = 0;
    $scope.pageIndex = 1;
    $scope.turnOver = [];
    $scope.pic = [];
    $scope.sortedBy = '';
    $scope.sortedDirection = '';
    $scope.TotalTurnOver = 0;

    $scope.filters = {
        season: jsHelper.getCurrentSeason(),
        clientUD: '',
        factoryOrderUD: '',
        trackingStatusNM: '',
    };

    $scope.weekIndexs = [
        { 'weekIndexID': 35 }, { 'weekIndexID': 36 }, { 'weekIndexID': 37 }, { 'weekIndexID': 38 }, { 'weekIndexID': 39 }, { 'weekIndexID': 40 }, { 'weekIndexID': 41 }, { 'weekIndexID': 42 }, { 'weekIndexID': 43 },
        { 'weekIndexID': 44 }, { 'weekIndexID': 45 }, { 'weekIndexID': 46 }, { 'weekIndexID': 47 }, { 'weekIndexID': 48 }, { 'weekIndexID': 49 }, { 'weekIndexID': 50 }, { 'weekIndexID': 51 }, { 'weekIndexID': 52 },
        { 'weekIndexID': 53 }, { 'weekIndexID': 1 }, { 'weekIndexID': 2 }, { 'weekIndexID': 3 }, { 'weekIndexID': 4 }, { 'weekIndexID': 5 }, { 'weekIndexID': 6 }, { 'weekIndexID': 7 }, { 'weekIndexID': 8 },
        { 'weekIndexID': 9 }, { 'weekIndexID': 10 }, { 'weekIndexID': 11 }, { 'weekIndexID': 12 }, { 'weekIndexID': 13 }, { 'weekIndexID': 14 }, { 'weekIndexID': 15 }, { 'weekIndexID': 16 }, { 'weekIndexID': 17 },
        { 'weekIndexID': 18 }, { 'weekIndexID': 19 }, { 'weekIndexID': 20 }, { 'weekIndexID': 21 }, { 'weekIndexID': 22 }, { 'weekIndexID': 23 }, { 'weekIndexID': 24 }, { 'weekIndexID': 25 }, { 'weekIndexID': 26 },
        { 'weekIndexID': 27 }, { 'weekIndexID': 28 }, { 'weekIndexID': 29 }, { 'weekIndexID': 30 }, { 'weekIndexID': 31 }, { 'weekIndexID': 32 }, { 'weekIndexID': 33 }, { 'weekIndexID': 34 }, { 'weekIndexID': 35 },

    ];

    $scope.certificateTypes = [
        { 'typeID': 1, 'typeNM': 'BSCI' },
        { 'typeID': 2, 'typeNM': 'ISO-9001 / ISO-14001' },
        { 'typeID': 3, 'typeNM': 'QMS' },
        { 'typeID': 4, 'typeNM': 'SR' },
        { 'typeID': 5, 'typeNM': 'CTPAT' },
        { 'typeID': 6, 'typeNM': 'FSC' }
    ];

    //
    // event
    //
    $scope.event = {
        //loadMore: function () {
        //    if ($scope.pageIndex < $scope.totalPage) {
        //        $scope.pageIndex++;
        //        $scope.event.loadTurnover(true);
        //    }
        //},

        addContactQuickInfo: function () {
            $scope.data.factoryContactQuickInfos.push({
                factoryContactQuickInfoID: $scope.method.getNewID()
            });
        },

        removeContactQuickInfo: function (item) {
            var index = $scope.data.factoryContactQuickInfos.indexOf(item);
            $scope.data.factoryContactQuickInfos.splice(index, 1);
        },

        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            $scope.TotalTurnOver = 0;
            $scope.pageIndex = 1;
            $scope.sortedBy = sortedBy;
            $scope.sortedDirection = sortedDirection;
            $scope.event.loadTurnover(false);
        },

        clearFilter: function () {
            $scope.TotalTurnOver = 0;
            $scope.pageIndex = 1;
            $scope.filters = {
                season: jsHelper.getCurrentSeason(),
                clientUD: '',
                factoryOrderUD: '',
                trackingStatusNM: '',
            };
            $scope.event.loadTurnover(false);
        },

        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    console.log(data);
                    $scope.directorSelected = [];
                    $scope.managerSelected = [];
                    $scope.pricingSelected = [];
                    $scope.personSelected = [];
                    $scope.sampleSelected = [];
                    $scope.supplierSelected = [];
                    $scope.users = [];
                    $scope.data = data.data.data;
                    $scope.employees = data.data.employees;
                    $scope.employeeDepartmentDTOs = data.data.employeeDepartmentDTOs;
                    $scope.factoryRawMaterialSupplierList = data.data.factoryRawMaterialSupplierList;
                    $scope.supplierList = data.data.supplierList;
                    $scope.locations = data.data.locations;
                    $scope.productGroupDTOs = data.data.productGroupDTOs;
                    $scope.users = data.data.usersDTO;
                    //bind checked status for product group
                    angular.forEach($scope.productGroupDTOs, function (item) {
                        if ($scope.data.factoryProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID).length > 0) {
                            item.isChecked = true;
                        }
                    });

                    if ($scope.data.factoryExpectedCapacities !== undefined && $scope.data.factoryExpectedCapacities !== null) {
                        if ($scope.data.factoryExpectedCapacities.length >= 12) {
                            var tempIndex = $scope.data.factoryExpectedCapacities.length - 1;
                            if ($scope.data.factoryExpectedCapacities[tempIndex].month === 9) {
                                var tempPos = $scope.data.factoryExpectedCapacities[tempIndex];
                                $scope.data.factoryExpectedCapacities.splice(tempIndex, 1);
                                $scope.data.factoryExpectedCapacities.unshift(tempPos);
                            }
                        }
                    }

                    //asign factory
                    var x = [];
                    angular.forEach($scope.data.factoryRawMaterialSuppliers, function (item) {
                        x.push({ "id": item.factoryRawMaterialID, "text": item.factoryRawMaterialUD });
                        $scope.supplierSelected.push(item.factoryRawMaterialID);
                    })
                    $('#factoryRawMaterialID').select2('data', x);

                    var y = [];
                    angular.forEach($scope.data.factoryDirectors, function (item) {
                        y.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.directorSelected.push(item.employeeID);
                    })
                    $('#directorID').select2('data', y);

                    var l = [];
                    angular.forEach($scope.data.factoryPricings, function (item) {
                        l.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.pricingSelected.push(item.employeeID);
                    })
                    $('#pricingID').select2('data', l);

                    var z = [];
                    angular.forEach($scope.data.factoryResponsiblePersons, function (item) {
                        z.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.personSelected.push(item.employeeID);
                    })
                    $('#personID').select2('data', z);

                    var h = [];
                    angular.forEach($scope.data.factoryManagers, function (item) {
                        h.push({ "id": item.employeeID, "text": item.employeeNM, "selected": item.isReceivePriceRequest });
                        $scope.managerSelected.push(item.employeeID);
                    })
                    $('#managerID').select2('data', h);

                    var g = [];
                    angular.forEach($scope.data.factorySampleTechnicals, function (item) {
                        g.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.sampleSelected.push(item.employeeID);
                    })
                    $('#sampleID').select2('data', g);

                    jQuery('#content').show();

                    // Turnover input manually by Factory data
                    var arr = [context.CurrentSeason, context.PrevSeason1, context.PrevSeason2, context.PrevSeason3];

                    // Without turnover data in current Factory -> Create new data
                    if ($scope.data.factoryTurnovers.length == 0) {
                        for (i = 0; i < arr.length; i++) {
                            $scope.data.factoryTurnovers.push({
                                factoryTurnoverID: $scope.method.getNewID(),
                                factoryID: context.id,
                                season: arr[i],
                                amount: 0
                            });
                        }
                    } else {
                        var existingSeason = [];
                        angular.forEach($scope.data.factoryTurnovers, function (item) {
                            existingSeason.push(item.season);
                        })

                        for (i = 0; i < arr.length; i++) {
                            if (jQuery.inArray(arr[i], existingSeason) == -1) {
                                $scope.data.factoryTurnovers.push({
                                    factoryTurnoverID: $scope.method.getNewID(),
                                    factoryID: context.id,
                                    season: arr[i],
                                    amount: 0
                                });
                            }
                        }
                    }
                    // Retrieve factory monthly capacity
                    var monthArr = [9, 10, 11, 12, 1, 2, 3, 4, 5, 6, 7, 8];
                    var capacityData = {
                        month: 0,
                        totalOrderQnt40HC: 0
                    };

                    // Current Season
                    angular.forEach(monthArr, function (item) {
                        qnt40HC = 0;
                        angular.forEach($scope.data.monthlyCapacityByCurrentSeasons, function (item1) {
                            if (item1.monthlyOrder == item) {
                                qnt40HC = item1.totalOrderQnt40HC;
                            }
                        })

                        capacityData = { month: item, totalOrderQnt40HC: qnt40HC };
                        $scope.currentSeasonCapacity.push(capacityData);
                    })

                    // Previous Season
                    angular.forEach(monthArr, function (item) {
                        qnt40HC = 0;
                        angular.forEach($scope.data.monthlyCapacityByPreSeasons, function (item1) {
                            if (item1.monthlyOrder == item) {
                                qnt40HC = item1.totalOrderQnt40HC;
                            }
                        })

                        capacityData = { month: item, totalOrderQnt40HC: qnt40HC };
                        $scope.previousSeasonCapacity.push(capacityData);
                    })

                    // Previous of Previous Season
                    //angular.forEach(monthArr, function (item) {
                    //    qnt40HC = 0;
                    //    angular.forEach($scope.data.monthlyCapacityByPreSeasons1, function (item1) {
                    //        if (item1.monthlyOrder == item) {
                    //            qnt40HC = item1.totalOrderQnt40HC;
                    //        }
                    //    })

                    //    capacityData = { month: item, totalOrderQnt40HC: qnt40HC };
                    //    $scope.previousSeasonCapacity1.push(capacityData);
                    //})

                    // Expected monthly capacity
                    if ($scope.data.factoryExpectedCapacities.length < 12) {
                        $scope.data.factoryExpectedCapacities.length = [];
                        angular.forEach(monthArr, function (item) {
                            $scope.data.factoryExpectedCapacities.push({
                                factoryExpectedCapacityID: $scope.method.getNewID(),
                                factoryID: context.id,
                                season: context.NextSeason,
                                month: item,
                                qnt40HC: 0
                            });
                        });
                    }

                    if ($scope.data.factoryCapacityByWeeks.length == 0) {
                        angular.forEach(factoryCapacityByWeeks, function (item) {
                            $scope.data.factoryCapacityByWeeks.push({
                                factoryCapacityID: $scope.method.getNewID(),
                                factoryID: context.id,
                            });
                        })
                    } else {
                        for (var i = 0; i < $scope.data.factoryCapacityByWeeks.length; i++) {
                            var item = $scope.data.factoryCapacityByWeeks[i];
                            // set factoryCapacityID
                            if (item.factoryCapacityID === null) {
                                item.factoryCapacityID = $scope.method.getNewID();
                            }
                            // set factoryID
                            if (item.factoryID === null) {
                                item.factoryID = context.id;
                            }
                        }
                    }

                    for (var i = 0; i < $scope.data.factoryCapacityByWeeks.length; i++) {
                        var item = $scope.data.factoryCapacityByWeeks[i];

                        if ($scope.arrSeason.length == 0) {
                            $scope.arrSeason.push({
                                "season": item.season,
                            });

                        }
                        else {
                            if (!$scope.method.isCheckSeasonInArray(item)) {
                                $scope.arrSeason.push({
                                    "season": item.season,
                                });
                            }
                        }
                    }

                    for (var i = 0; i < $scope.data.weekInforRangeDTOs.length; i++) {
                        var item = $scope.data.weekInforRangeDTOs[i];

                        index = 0;
                        for (var j = 0; j < $scope.data.factoryCapacityByWeeks.length; j++) {
                            var wItem = $scope.data.factoryCapacityByWeeks[j];

                            if (item.season === wItem.season) {
                                if (index === 0 && wItem.weekIndex === 36) {
                                    nItem = {
                                        season: wItem.season,
                                        capacity: null,
                                        weekIndex: 35,
                                        isVisiable: false,
                                    };

                                    $scope.arrDataByWeek.push(nItem);
                                    index = index + 1;
                                }

                                if (j > 0 && $scope.data.factoryCapacityByWeeks[j - 1].weekIndex === 52 && wItem.weekIndex === 1 && !item.hasWeek53) {
                                    nItem = {
                                        season: wItem.season,
                                        capacity: null,
                                        weekIndex: 53,
                                        isVisiable: false,
                                    };

                                    $scope.arrDataByWeek.push(nItem);
                                    index = index + 1;
                                }

                                nItem = {
                                    season: wItem.season,
                                    capacity: wItem.capacity,
                                    weekIndex: wItem.weekIndex,
                                    isVisiable: true,
                                    factoryCapacityID: wItem.factoryCapacityID,
                                    factoryID: wItem.factoryID,
                                    weekInfoID: wItem.weekInfoID
                                };

                                $scope.arrDataByWeek.push(nItem);
                                index = index + 1;
                            }
                        }

                        if (index === 53) {
                            nItem = {
                                season: item.season,
                                capacity: null,
                                weekIndex: 35,
                                isVisiable: false,
                            };

                            $scope.arrDataByWeek.push(nItem);
                            index = index + 1;
                        }
                    }

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });


                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.employees = null;
                    $scope.employeeDepartmentDTOs = null;
                    $scope.factoryRawMaterialSupplierList = null;
                    $scope.locations = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {

                $scope.data.factoryRawMaterialSuppliers = [];
                //$scope.data.factorySuppliers = [];
                //$scope.data.factoryDirectors = [];
                //$scope.data.factoryResponsiblePersons = [];

                var selectedSupplier = $('#factoryRawMaterialID').select2('data');
                //var selectedDirector = $('#employeeID1').select2('data');
                //var selectedPerson = $('#employeeID2').select2('data');

                for (i = 0; i < selectedSupplier.length; i++) {
                    $scope.data.factoryRawMaterialSuppliers.push({
                        factoryRawMaterialSupplierID: $scope.method.getNewID(),
                        factoryRawMaterialID: selectedSupplier[i].id
                    });
                    //$scope.data.factorySuppliers.push({
                    //    factorySupplierID: $scope.method.getNewID(),
                    //    supplierID: selectedSupplier[i].id
                    //});
                }

                for (var kl = 0; kl < $scope.arrDataByWeek.length; kl++) {
                    var item = $scope.arrDataByWeek[kl];
                    $scope.data.factoryCapacityByWeeks.push({
                        capacity: item.capacity,
                        factoryCapacityID: item.factoryCapacityID,
                        factoryID: item.factoryID,
                        season: item.season,
                        weekIndex: item.weekIndex,
                        weekInfoID: item.weekInfoID
                    })
                }

                //bind checked status for product group
                angular.forEach($scope.productGroupDTOs, function (item) {
                    if (item.isChecked) {
                        // insert new
                        if ($scope.data.factoryProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID).length == 0) {
                            $scope.data.factoryProductGroupDTOs.push({
                                productGroupID: item.productGroupID
                            })
                        }
                    }
                    else {
                        //remove if unchecked
                        if ($scope.data.factoryProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID).length > 0) {
                            angular.forEach($scope.data.factoryProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID), function (itemFactoryProductGroup) {
                                $scope.data.factoryProductGroupDTOs.splice($scope.data.factoryProductGroupDTOs.indexOf(itemFactoryProductGroup), 1);
                            })
                        }
                    }

                });

                //angular.forEach($scope.data.factoryImages, function (item) {
                //    item.thumbnailLocation = item.fileLocation;
                //    item.hasChange = true;
                //    item.newfile = item.fileLocation;
                //});

                //console.log($scope.data.factoryImages);

                //for (i = 0; i < selectedDirector.length; i++) {
                //    $scope.data.factoryDirectors.push({
                //        factoryDirectorID: $scope.method.getNewID(),
                //        employeeID: selectedDirector[i].id
                //    });
                //}

                //for (i = 0; i < selectedPerson.length; i++) {
                //    $scope.data.factoryResponsiblePersons.push({
                //        factoryResponsiblePersonID: $scope.method.getNewID(),
                //        employeeID: selectedPerson[i].id
                //    });
                //}
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },


        uploadVideo: function () {
            //return;

            userFileMng.callback = function () {
                scope.$apply(function () {
                    // TO DO LIST
                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        scope.data.factoryGalleries.push({
                            factoryGalleryID: scope.method.getNewID(),
                            factoryGalleryUD: '',
                            FactoryID: context.id,
                            factoryGalleryHasChange: true,
                            factoryGalleryNewFile: value.fileName,
                            factoryGalleryThumbnail: context.backendUrl + 'avi.mp4'
                        });
                    });
                });
            };
            userFileMng.open();
        },

        removeGalleryItem: function (id) {
            //return;
            if (confirm('Are you sure ?')) {
                isExist = false;
                for (j = 0; j < $scope.data.factoryGalleries.length; j++) {
                    if ($scope.data.factoryGalleries[j].factoryGalleryID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.factoryGalleries.splice(j, 1);
                }
            }
        },

        //
        // Logo
        //
        changeLogo: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.logoImage_DisplayURL = img.fileURL;
                        scope.data.logoImage_NewFile = img.filename;
                        scope.data.logoImage_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeLogo: function ($event) {
            $scope.data.logoImage_DisplayURL = '';
            $scope.data.logoImage_NewFile = '';
            $scope.data.logoImage_HasChange = true;
        },

        // Select director
        addDirector: function () {
            jQuery('#directorForm').modal('show');
        },
        updateDirector: function (item) {
            $scope.data.factoryDirectors = [];
            var selectedDirector = $('#directorID').select2('data');
            angular.forEach(selectedDirector, function (item) {
                $scope.data.factoryDirectors.push({
                    factoryDirectorID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text
                });
            });
            jQuery('#directorForm').modal('hide');
        },
        removeDirector: function (item) {
            $scope.data.factoryDirectors.splice($scope.data.factoryDirectors.indexOf(item), 1);
        },

        // Select manager
        addManager: function () {
            jQuery('#managerForm').modal('show');
        },
        updateManager: function (item) {
            $scope.data.factoryManagers = [];
            var selectedManager = $('#managerID').select2('data');
            angular.forEach(selectedManager, function (item) {
                $scope.data.factoryManagers.push({
                    factoryManagerID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text,
                    isReceivePriceRequest: item.selected
                });
            });
            jQuery('#managerForm').modal('hide');
        },
        removeManager: function (item) {
            $scope.data.factoryManagers.splice($scope.data.factoryManagers.indexOf(item), 1);
        },

        // Select pricing
        addPricing: function () {
            jQuery('#pricingForm').modal('show');
        },
        updatePricing: function (item) {
            debugger;
            $scope.data.factoryPricings = [];
            var selectedPricing = $('#pricingID').select2('data');
            angular.forEach(selectedPricing, function (item) {
                $scope.data.factoryPricings.push({
                    factoryPricingID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text
                });
            });
            jQuery('#pricingForm').modal('hide');
        },
        removePricing: function (item) {
            $scope.data.factoryPricings.splice($scope.data.factoryPricings.indexOf(item), 1);
        },

        // Select person
        addPerson: function () {
            jQuery('#personForm').modal('show');
        },
        updatePerson: function (item) {
            $scope.data.factoryResponsiblePersons = [];
            var selectedPerson = $('#personID').select2('data');
            angular.forEach(selectedPerson, function (item) {
                $scope.data.factoryResponsiblePersons.push({
                    factoryResponsiblePersonID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text
                });
            });
            jQuery('#personForm').modal('hide');
        },
        removePerson: function (item) {
            $scope.data.factoryResponsiblePersons.splice($scope.data.factoryResponsiblePersons.indexOf(item), 1);
        },

        // Select sample/technical
        addSample: function () {
            jQuery('#sampleForm').modal('show');
        },
        updateSample: function (item) {
            $scope.data.factorySampleTechnicals = [];
            var selectedSample = $('#sampleID').select2('data');
            angular.forEach(selectedSample, function (item) {
                $scope.data.factorySampleTechnicals.push({
                    factorySampleTechnicalID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text
                });
            });
            jQuery('#sampleForm').modal('hide');
        },
        removeSample: function (item) {
            $scope.data.factorySampleTechnicals.splice($scope.data.factorySampleTechnicals.indexOf(item), 1);
        },

        //
        // IMAGE
        //
        addImage: function () {
            var newImage = {
                factoryImageID: $scope.method.getNewID(),
                fileUD: null,
                description: null,
                friendlyName: null,
                thumbnailLocation: null,
                fileLocation: null,
                hasChange: false,
                newfile: null
            };
            $scope.event.editImage(newImage);
        },
        editImage: function (item) {
            $scope.currentImage = JSON.parse(JSON.stringify(item));
            jQuery("#imageForm").modal();
        },
        updateImage: function () {
            var index = $scope.method.getImageIndex($scope.currentImage.factoryImageID);
            if (index >= 0) {
                $scope.data.factoryImages[index] = $scope.currentImage;
            }
            else {
                $scope.data.factoryImages.push($scope.currentImage);
            }
            jQuery("#imageForm").modal('hide');

            $scope.method.renderImage();
        },

        removeImage: function (item) {
            if (confirm('Are you sure ?')) {
                $scope.data.factoryImages.splice($scope.data.factoryImages.indexOf(item), 1);
            }
        },

        addPreviewImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentImage.hasChange = true;
                        scope.currentImage.thumbnailLocation = img.fileURL;
                        scope.currentImage.fileLocation = img.fileURL;
                        scope.currentImage.newFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },

        //
        // BUSINESS CARD
        //

        addCard: function () {
            var newCard = {
                factoryBusinessCardID: $scope.method.getNewID(),
                frontFileUD: null,
                behindFileUD: null,
                description: null,
                remark: null,

                frontFriendlyName: null,
                frontThumbnailLocation: null,
                frontFileLocation: null,
                frontHasChange: false,
                frontNewFile: null,

                behindFriendlyName: null,
                behindThumbnailLocation: null,
                behindFileLocation: null,
                behindHasChange: false,
                behindNewFile: null,
                controllerID: null,
                controllerName: null,
                otherName: null
            };
            $scope.event.editCard(newCard);
        },
        editCard: function (item) {
            debugger;
            $scope.currentCard = JSON.parse(JSON.stringify(item));
            jQuery("#cardForm").modal();
        },
        removeCard: function (item) {
            if (confirm('Are you sure ?')) {
                $scope.data.factoryBusinessCard.splice($scope.data.factoryBusinessCard.indexOf(item), 1);
            }
        },
        updateCard: function () {
            debugger;
            var index = $scope.method.getCardIndex($scope.currentCard.factoryBusinessCardID);
            if (index >= 0) {
                $scope.data.factoryBusinessCard[index] = $scope.currentCard;
            }
            else {
                $scope.data.factoryBusinessCard.push($scope.currentCard);
            }
            jQuery("#cardForm").modal('hide');

            $scope.method.renderCard();
        },
        addPreviewCardImage: function (pos) {
            if (pos === 1) {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.currentCard.frontHasChange = true;
                            scope.currentCard.frontThumbnailLocation = img.fileURL;
                            scope.currentCard.frontFileLocation = img.fileURL;
                            scope.currentCard.frontNewFile = img.filename;
                        }, null);
                    });
                };
                masterUploader.open();
            }
            if (pos === 2) {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.currentCard.behindHasChange = true;
                            scope.currentCard.behindThumbnailLocation = img.fileURL;
                            scope.currentCard.behindFileLocation = img.fileURL;
                            scope.currentCard.behindNewFile = img.filename;
                        }, null);
                    });
                };
                masterUploader.open();
            }
        },
        changeController: function (controllerID) {
            if ($scope.currentCard.controllerID) {
                angular.forEach($scope.users, function (item) {
                    if (item.userID == $scope.currentCard.controllerID) {
                        $scope.currentCard.controllerName = item.employeeNM + ' (' + item.internalCompanyNM + ')';
                    }
                });
            }
            else {
                $scope.currentCard.controllerName = '';
            }
        },

        //
        // Factory In House test
        //
        addTest: function ($event) {
            $event.preventDefault();
            $scope.data.factoryInHouseTests.push({
                factoryInHouseTestID: $scope.method.getNewID(),
            });
        },
        removeTest: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.factoryInHouseTests.splice(index, 1);
            }
        },

        //
        // Factory Certificate
        //
        addCertificate: function ($event) {
            $event.preventDefault();
            $scope.data.factoryCertificates.push({
                factoryCertificateID: $scope.method.getNewID(),
            });
        },
        removeCertificate: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.factoryCertificates.splice(index, 1);
            }
        },
        //
        // Certificate file
        //
        changeCertificateFile: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.factoryCertificates.filter(function (o) { return o.factoryCertificateID == controlID });
                        $(arr).each(function () {
                            this.certificateFileUrl = img.fileURL;
                            this.certificateFileFriendlyName = img.filename;
                            this.certificateFileHasChange = true;
                            this.newCertificateFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeCertificateFile: function ($event, controlID, typeID) {
            var arr = scope.data.factoryCertificates.filter(function (o) { return o.factoryCertificateID == controlID });
            $(arr).each(function () {
                this.certificateFileUrl = '';
                this.certificateFileFriendlyName = '';
                this.certificateFileHasChange = true;
                this.newCertificateFile = '';
            });
        },

        loadTurnover: function (isLoadMore) {
            
            if ($scope.turnOver.length === 0 || isLoadMore != null) {
                dataService.getTurnOverData(
                    context.id,
                    $scope.filters.season,
                    $scope.filters.clientUD /*clientUD*/,
                    $scope.filters.factoryOrderUD /*factoryOrderUD*/,
                    $scope.filters.trackingStatusNM /*trackingStatusNM*/,
                    context.pageSize,
                    $scope.pageIndex,
                    $scope.sortedBy/*orderBy*/,
                    $scope.sortedDirection/*orderDirection*/,
                    function (data) {
                        //$scope.totalPage = Math.ceil(data.totalRows / context.pageSize);
                        $scope.totalRows = data.totalRows;
                        if (isLoadMore) {
                            Array.prototype.push.apply($scope.turnOver, data.data);
                        }
                        else {
                            $scope.turnOver = data.data;
                        }
                        angular.forEach($scope.turnOver, function (item) {
                            $scope.TotalTurnOver += item.turnover;
                        })
                    },
                    function (error) {
                    });
            }
        },

        loadPIC: function () {

            dataService.getPIC(
                $scope.data.supplierID,
                function (data) {
                    $scope.totalRows = data.totalRows;
                    $scope.pic = data.data;
                },
                function (error) {
                });
        },
        seasonChange: function () {
            $scope.TotalTurnOver = 0;
            $scope.pageIndex = 1;
            $scope.event.loadTurnover(false);
        },

        filterResult: function () {
            $scope.TotalTurnOver = 0;
            $scope.pageIndex = 1;
            $scope.event.loadTurnover(false);
        },

        uploadDocument: function () {
            userFileMng.isMultiSelectActivated = true;
            userFileMng.callback = function () {
                scope.$apply(function () {
                    // TO DO LIST
                    var d = new Date();
                    var n = d.getMilliseconds();
                    if (scope.data.factoryDocuments === null) {
                        scope.data.factoryDocuments = [];
                    }
                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        scope.data.factoryDocuments.push({
                            factoryDocumentID: scope.method.getNewID(),
                            factoryID: context.id,
                            factoryDocumentFile: '',
                            name: '',
                            friendlyName: value.fileName,
                            fileLocation: value.filePath,
                            remark: '',
                            factoryDocumentHasChange: true,
                            factoryDocumentNewFile: value.fileName
                        });
                    });
                });
            };
            userFileMng.open();
        },
        removeDocument: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.factoryDocuments.splice(index, 1);
            }
        },
    };
    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        //Image
        getImageIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.factoryImages.length; index++) {
                if ($scope.data.factoryImages[index].factoryImageID == id) {
                    isExist = true;
                    break;
                }
            }

            if (!isExist) {
                return -1;
            }
            else {
                return index;
            }
        },
        renderImage: function () {
            $scope.images = [];
            angular.forEach($scope.data.factoryImages, function (value, key) {
                if ($scope.images.indexOf(value.draw) < 0) {
                    $scope.images.push(value.draw);
                }
            }, null);
        },

        //Card
        getCardIndex: function (id) {
            var isExist = false;
            var cardIndex = 0;
            for (cardIndex; cardIndex < $scope.data.factoryBusinessCard.length; cardIndex++) {
                if ($scope.data.factoryBusinessCard[cardIndex].factoryBusinessCardID === id) {
                    isExist = true;
                    break;
                }
            }

            if (!isExist) {
                return -1;
            }
            else {
                return cardIndex;
            }
        },
        renderCard: function () {
            $scope.cards = [];
            angular.forEach($scope.data.factoryBusinessCard, function (value, key) {
                if ($scope.cards.indexOf(value.draw) < 0) {
                    $scope.cards.push(value.draw);
                }
            }, null);
        },

        getTotal: function (data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.totalOrderQnt40HC);
                }, null);
            }
            return result;
        },
        getTotalExt: function (data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.qnt40HC);
                }, null);
            }
            return result;
        },

        getTotalCapacity: function (season, data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    if (season == item.season) {
                        if (item.capacity) {
                            result = parseFloat(result) + parseFloat(item.capacity);
                        }
                    }
                }, null);
            }
            return result;
        },

        isCheckSeasonInArray: function (item) {
            for (var i = 0; i < $scope.arrSeason.length; i++) {
                var subItem = $scope.arrSeason[i];

                if (subItem.season === item.season) {
                    return true;
                }
            }

            return false;
        },

    };

    //
    // init
    //
    $scope.event.init();
    //$scope.event.getTurnOverData();
}]);