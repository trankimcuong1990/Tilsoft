//
//APP START
//
$("#factoryID").select2({
    closeOnSelect: true,
});
var tilsoftApp = angular.module('tilsoftApp', ['customFilters', 'furnindo-directive', 'ui.select2', 'avs-directives', 'ui']);

tilsoftApp.controller('tilsoftController', ['$scope', '$filter', '$timeout', function ($scope, $filter, $timeout) {
    $scope.dataContainer = null;
    $scope.newID = -1;
    $scope.seasons = null;
    $scope.jobLevels = null;
    $scope.companies = null;
    $scope.departments = null;
    $scope.locations = null;
    $scope.directors = null;
    $scope.yesNoValues = null;

    $scope.users = null;
    $scope.factories = null;
    $scope.factorySelected = null;

    $scope.responsibleFor = null;
    $scope.sensitiveData = null;
    $scope.verifyInfo = {
        username: '',
        password: ''
    }

    $scope.event = {
        init: function () {
            employeeMngService.load(
                context.id,
                function (data) {
                    $scope.factorySelected = [];
                    $scope.dataContainer = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.companies = data.data.companies;
                    $scope.branches = data.data.branches;
                    $scope.departments = data.data.departments;
                    $scope.jobLevels = data.data.jobLevels;
                    $scope.locations = data.data.locations;
                    $scope.directors = data.data.directors;
                    $scope.users = data.data.users;
                    $scope.factories = data.data.factories;
                    $scope.yesNoValues = data.data.yesNoStatuses;
                    $scope.dataContainer.annualLeaveSettings = data.data.data.annualLeaveSettings;
                    $scope.typeOfContracts = data.data.data.typeOfContractDTOs;
                    $scope.responsibleFor = data.data.responsibleFor;

                    //asign factory
                    var x = [];
                    angular.forEach($scope.dataContainer.employeeFactorys, function (item) {
                        x.push({ "id": item.factoryID, "text": item.factoryUD });
                        $scope.factorySelected.push(item.factoryID);
                    })

                    $('#factoryID').select2('data', x);

                    $('#page-title').html(context.pageTitle + ': ' + $scope.dataContainer.employeeNM);

                    //apply data
                    $scope.$apply();



                    //get factory selected
                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    // render high chart
                    //var chart = Highcharts.chart('weeklyChart', {
                    //    chart: {
                    //        type: 'column'
                    //    },
                    //    title: {
                    //        text: 'Weekly report'
                    //    },
                    //    subtitle: {
                    //        text: 'Click the columns to view detail'
                    //    },
                    //    xAxis: {
                    //        type: 'category'
                    //    },
                    //    yAxis: {
                    //        title: {
                    //            text: 'Total hit'
                    //        },
                    //        labels: {
                    //            format: '{value:,.0f}'
                    //        }
                    //    },
                    //    legend: {
                    //        enabled: false
                    //    },
                    //    plotOptions: {
                    //        series: {
                    //            borderWidth: 0,
                    //            dataLabels: {
                    //                enabled: true,
                    //                format: '{point.y:,.0f}'
                    //            }
                    //        }
                    //    },
                    //    tooltip: {
                    //        headerFormat: '',
                    //        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.0f}</b>'
                    //    },
                    //    series: [{
                    //        name: 'Week',
                    //        colorByPoint: true,
                    //        data: []
                    //    }],
                    //    drilldown: {
                    //        series: []
                    //    }
                    //});

                    //var seriData = [];
                    //var drilldownData = [];
                    //angular.forEach($scope.dataContainer.userDetailWeeklyOverviews, function (item) {
                    //    // processing seri data
                    //    seriData.push({ name: 'Week ' + item.weekIndex + '/' + item.yearIndex, y: item.totalHit, drilldown: 'week_' + item.weekIndex + '_' + item.yearIndex });

                    //    // processing drilldown data
                    //    var drilldownItemData = { name: 'Week ' + item.weekIndex + '/' + item.yearIndex, id: 'week_' + item.weekIndex + '_' + item.yearIndex, data: [] };
                    //    angular.forEach($scope.dataContainer.userDetailWeeklyDetails, function (subItem) {
                    //        if (item.weekIndex == subItem.weekIndex && item.yearIndex == subItem.yearIndex) {
                    //            drilldownItemData.data.push([subItem.moduleNM, subItem.totalHit]);
                    //        }
                    //    }, null);
                    //    drilldownData.push(angular.copy(drilldownItemData));
                    //}, null);
                    //chart.options.drilldown.series = drilldownData;
                    //chart.series[0].setData(seriData, true);

                    //chart.redraw();

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }

                    $timeout(function () {
                        $(".yr-datePicker").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: "-70:+0" });
                    }, 0);

                    angular.forEach($scope.dataContainer.employeeResponsibleForDTOs, function (item) {

                        if (item.displayText==="") {
                            $scope.dataContainer.employeeResponsibleForDTOs = [];
                        }
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.datdataContainer = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {

                $scope.dataContainer.employeeFactorys = [];
                var selectedFactory = $('#factoryID').select2('data');

                for (i = 0; i < selectedFactory.length; i++)
                {
                    $scope.dataContainer.employeeFactorys.push({
                        employeeFactoryID: $scope.method.getNewID(),
                        factoryID: selectedFactory[i].id
                    });
                }

                //update data
                employeeMngService.update(
                    context.id,
                    $scope.dataContainer,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.employeeID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function () {
            if (confirm('Are you sure ?')) {
                employeeMngService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        changePersonalImage: function ($event) {
            $event.preventDefault();
            //tmpFileManager.callback = function () {
            //    var scope = angular.element(jQuery('body')).scope();
            //    scope.$apply(function () {
            //        scope.dataContainer.personalPhoto_DisplayURL = tmpFileManager.selectedFileUrl;
            //        scope.dataContainer.personalPhoto_NewFile = tmpFileManager.selectedFileName;
            //        scope.dataContainer.personalPhoto_HasChange = true;
            //    });
            //};
            //tmpFileManager.open();

            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.dataContainer.personalPhoto_DisplayURL = fileUploader2.selectedFileUrl;
                    scope.dataContainer.personalPhoto_NewFile = fileUploader2.selectedFileName;
                    scope.dataContainer.personalPhoto_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removePersonalImage: function ($event) {
            $scope.dataContainer.personalPhoto_DisplayURL = '';
            $scope.dataContainer.personalPhoto_NewFile = '';
            $scope.dataContainer.personalPhoto_HasChange = true;
        },

        //
        //Annual Leave Setting
        //
        addSetting: function () {
            var newSetting = {
                annualLeaveSettingID: $scope.method.getNewID(),
                affectedYear: null,
                numberOfDay: null,
                comment: null
            };
            $scope.event.editSetting(newSetting);
        },
        editSetting: function (item) {
            $scope.currentSetting = JSON.parse(JSON.stringify(item));
            jQuery("#settingForm").modal();
        },
        updateSetting: function () {
            if ($scope.currentSetting.affectedYear == null|| $scope.currentSetting.numberOfDay == null) {
                alert("Please enter required fill ! ");
                return false;
            }
            var index = $scope.method.getSettingIndex($scope.currentSetting.annualLeaveSettingID)
            if (index >= 0) {
                $scope.dataContainer.annualLeaveSettings[index] = $scope.currentSetting;
            }
            else {
                $scope.dataContainer.annualLeaveSettings.push($scope.currentSetting);
            }
            jQuery("#settingForm").modal('hide');

            $scope.method.renderSetting();
        },

        removeSetting: function (item) {
            if (confirm('Are you sure ?')) {
                $scope.dataContainer.annualLeaveSettings.splice($scope.dataContainer.annualLeaveSettings.indexOf(item), 1);
            }
        },
        //ResumeFile
        changeResumeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.dataContainer.resumeFileFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.dataContainer.resumeFileLocation = fileUploader2.selectedFileUrl;
                    $scope.dataContainer.resumeFile_NewFile = fileUploader2.selectedFileName;
                    $scope.dataContainer.resumeFile_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeResumeFile: function () {
            $scope.dataContainer.resumeFileFriendlyName = '';
            $scope.dataContainer.resumeFileLocation = '';
            $scope.dataContainer.resumeFile_NewFile = '';
            $scope.dataContainer.resumeFile_HasChange = true;
        },
        skypeChat: function (value) {
            window.location = 'skype:' + value + '?chat';
        },
        openCall: function (value) {
            window.location = 'tel:' + value;
        },

        // manager files
        addFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.dataContainer.employeeFileDTOs.push({
                        employeeFileID: $scope.method.getNewID(),
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.open();
        },
        removeFile: function (item) {
            $scope.dataContainer.employeeFileDTOs.splice($scope.dataContainer.employeeFileDTOs.indexOf(item), 1);
        },

        // manager contracts
        addContract: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.dataContainer.employeeContractDTOs.push({
                        employeeContractID: $scope.method.getNewID(),
                        validFrom: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                    setTimeout(function () {
                        $(".yr-datePicker").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: "-70:+0" });
                    }, 500);
                });
            };
            fileUploader2.open();
        },
        removeContract: function (item) {
            $scope.dataContainer.employeeContractDTOs.splice($scope.dataContainer.employeeContractDTOs.indexOf(item), 1);
        },

        // account verification
        verifyAccount: function () {
            $('#frmVerifyAccount').modal('show');
        },
        verifyAccountPost: function () {
            if ($scope.verifyInfo.username && $scope.verifyInfo.password) {
                $('#frmVerifyAccount').modal('hide');
                employeeMngService.getSensitiveData(
                    context.id,
                    $scope.verifyInfo.username,
                    $scope.verifyInfo.password,
                    function (data) {
                        if (data.message.type == 0) {
                            // success
                            //$scope.sData = data.data.sensitiveData;
                            $scope.dataContainer.managerNote = data.data.data.managerNote;
                            $scope.dataContainer.employeeContractDTOs = data.data.data.employeeContractDTOs;
                            $scope.dataContainer.employeeFileDTOs = data.data.data.employeeFileDTOs;
                            $scope.dataContainer.needToUpdateManagerData = true;
                        }
                        else {
                            // error
                            jsHelper.processMessageEx(data.message);

                            $scope.dataContainer.managerNote = null;
                            $scope.dataContainer.employeeContractDTOs = null;
                            $scope.dataContainer.employeeFileDTOs = null;
                            $scope.dataContainer.needToUpdateManagerData = false;
                        }
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.dataContainer.managerNote = null;
                        $scope.dataContainer.employeeContractDTOs = null;
                        $scope.dataContainer.employeeFileDTOs = null;
                        $scope.dataContainer.needToUpdateManagerData = false;
                        $scope.$apply();
                    }
                );
                $scope.verifyInfo.username = '';
                $scope.verifyInfo.password = '';
            }
        },

        addResponsibleFor: function (responsibleFor, $event) {

            $event.preventDefault();

            //check list employeeResponsibleForDTOs null
            if ($scope.dataContainer.employeeResponsibleForDTOs == null) {
                $scope.dataContainer.employeeResponsibleForDTOs = [];
            }
        
            //check null value
            if (responsibleFor == null) {
                bootbox.alert("Please choose a type !!");
                return false;
            }

            if ($scope.dataContainer.employeeResponsibleForDTOs === null || $scope.dataContainer.employeeResponsibleForDTOs.length == 0) {
                $scope.dataContainer.employeeResponsibleForDTOs.push({
                    reposibleForValue: responsibleFor,
                    displayText: $scope.method.getResponsibleText(responsibleFor),
                });
            } else {
                //check value duplicate
                if (!$scope.method.checkDuplicateValue(responsibleFor)) {
                    $scope.dataContainer.employeeResponsibleForDTOs.push({
                        reposibleForValue: responsibleFor,
                        displayText: $scope.method.getResponsibleText(responsibleFor),
                    });
                }
            }
        },

        removeResponsibleFor: function ($event, index) {
            angular.forEach($scope.dataContainer.employeeResponsibleForDTOs.filter(s => s.reposibleForValue == $scope.dataContainer.employeeResponsibleForDTOs[index].reposibleForValue), function (item) {
                $scope.dataContainer.employeeResponsibleForDTOs.splice($scope.dataContainer.employeeResponsibleForDTOs.indexOf(item), 1);
            });
        },

        addqaqcFactory: function ($event) {
            $scope.dataContainer.factoryAccesses.push({
                qaqcFactoryAccessID: $scope.method.getNewID(),
                userAccessID: $scope.dataContainer.userID,
                factoryID: null,
                approvalID: null,
                remark: ""
            });
        },

        removeqaqcFactory: function ($event, id) {
            angular.forEach($scope.dataContainer.factoryAccesses.filter(s => s.qaqcFactoryAccessID === id), function (item) {
                $scope.dataContainer.factoryAccesses.splice($scope.dataContainer.factoryAccesses.indexOf(item), 1);
            });
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

        // Age
        getYear: function (value) {
            if (value) {
                var date = value.split('/');
                date = new Date(date[2] + '-' + date[1] + '-' + date[0]);
                var now = new Date();
                var nowYear = now.getFullYear();
                var dataYear = date.getFullYear();
                var age = nowYear - dataYear;
                return age;
            }
        },

        //Get year, month, day
        getDiffDate: function (dateString) {
            if (dateString) {
                var now = new Date();
                var today = new Date(now.getYear(), now.getMonth(), now.getDate());

                var yearNow = now.getYear();
                var monthNow = now.getMonth();
                var dateNow = now.getDate();

                var date = dateString.split('/');
                date = new Date(date[2] + '-' + date[1] + '-' + date[0]);

                var yearOfDate = date.getYear();
                var monthOfDate = date.getMonth();
                var dateOfDate = date.getDate();
                var result = {};

                year = yearNow - yearOfDate;

                if (monthNow >= monthOfDate)
                    var month = monthNow - monthOfDate;
                else {
                    year--;
                    var month = 12 + monthNow - monthOfDate;
                }

                if (dateNow >= dateOfDate)
                    var date = dateNow - dateOfDate;
                else {
                    month--;
                    var date = 31 + dateNow - dateOfDate;

                    if (month < 0) {
                        month = 11;
                        year--;
                    }
                }

                result = {
                    years: year,
                    months: month,
                    days: date
                };

                var yearString = " years";
                var monthString = " months";

                if (result.years == 1)
                {
                    yearString = " year";
                }
                if (result.months == 1)
                {
                    monthString = " month";
                }

                var resultString = "";

                if (result.years > 0 && result.months > 0)
                    resultString = result.years + yearString + ', ' + result.months + monthString;
                else if (result.years > 0)
                    resultString = result.years + yearString;
                else if (result.months > 0)
                    resultString = result.months + monthString;

                return resultString;
            }
        },

        //Annual Leave Setting
        getSettingIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.dataContainer.annualLeaveSettings.length; index++) {
                if ($scope.dataContainer.annualLeaveSettings[index].annualLeaveSettingID == id) {
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

        renderSetting: function () {
            $scope.settings = [];
            angular.forEach($scope.data.annualLeaveSettings, function (value, key) {
                if ($scope.settings.indexOf(value.setting) < 0) {
                    $scope.settings.push(value.setting);
                }
            }, null);
        },

        printNote: function () {
            var printWindow = window.open();
            printWindow.document.open('text/plain')
            printWindow.document.write($scope.dataContainer.managerNote);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        },

        getEndDay: function (dd, numOfMonth) {
            //debugger
            if (dd === undefined || dd === null || dd === '') return null;
            //get day && month && year		
            var day = parseInt(dd.substr(0, 2));
            var mm = parseInt(dd.substr(3, 2));
            var yyyy = parseInt(dd.substr(6, 4));
            var dates = [];

            numOfMonth = parseInt(numOfMonth);

            for (var i = 1; i <= numOfMonth; i++) {
                var _monthInput = parseInt(mm) + i;

                //month
                var _month = _monthInput % 12;
                if (_month === 0) _month = 12;

                //year
                var _year = parseInt(parseInt(_monthInput - 1) / 12);
                _year = _year + parseInt(yyyy);

                var dateAdded = day.toString().padStart(2, "0") + '/' + _month.toString().padStart(2, "0") + '/' + _year;

                if (i === numOfMonth) {
                    return $scope.dataContainer.contractPeriod = dateAdded;
                }
            }
        },

        getResponsibleText: function (responsibleFor) {
            for (var i = 0; i < $scope.responsibleFor.length; i++) {
                var item = $scope.responsibleFor[i];
                if (parseInt(item.entryValue) == responsibleFor) {
                    return item.entryText;
                }
            }
        },

        checkDuplicateValue: function (responsibleFor) {
            var isChecked = false
            angular.forEach($scope.dataContainer.employeeResponsibleForDTOs, function (value) {
                if (parseInt(value.reposibleForValue) == responsibleFor) {
                    bootbox.alert("Value duplicate !!!")
                    isChecked = true;
                }
            });
            return isChecked;
        }
    };
    //
    // init
    //
    $scope.event.init();
}]);

//DATE PICKER
$(function () {
    $(".yr-datePicker").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: "-70:+0" });
});