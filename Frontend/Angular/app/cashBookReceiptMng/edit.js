(function () {
    'use strict';

    angular
        .module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies'])
        .controller('tilsoftController', cashBookReceiptController);
    cashBookReceiptController.$inject = ['$scope', 'dataService'];

    function cashBookReceiptController($scope, cashBookReceiptService) {
        $scope.data = [];
        $scope.supportLocation = [];
        $scope.supportType = [];
        $scope.supportPostCost = [];
        $scope.supportCostItem = [];
        $scope.supportCostItemDetail = [];
        $scope.supportPaidBy = [];
        $scope.cashBookDocument = [];
        $scope.newid = -1;
        $scope.isEdit;

        var balanceMonth = null;
        var balanceYear = null;

        $scope.editPostCost = {
            cashBookPostCostID: 0,
            postCostNM: null,
            displayIndex: null
        };

        $scope.editCostItem = {
            cashBookCostItemID: 0,
            costItemNM: null,
            cashBookPostCostID: null,
            displayIndex: null
        };

        $scope.editCostItemDetail = {
            cashBookCostItemDetailID: 0,
            costItemDetailNM: null,
            cashBookCostItemID: null,
            displayIndex: null
        };

        $scope.event = {
            load: load,
            saveCashBookReceipt: saveCashBookReceipt,
            openPostCost: openPostCost,
            openCostItem: openCostItem,
            openCostItemDetail: openCostItemDetail,
            closePostCost: closePostCost,
            closeCostItem: closeCostItem,
            closeCostItemDetail: closeCostItemDetail,
            editPostCost: editPostCost,
            selectPostCost: selectPostCost,
            savePostCost: savePostCost,
            deletePostCost: deletePostCost,
            editCostItem: editCostItem,
            selectCostItem: selectCostItem,
            saveCostItem: saveCostItem,
            deleteCostItem: deleteCostItem,
            editCostItemDetail: editCostItemDetail,
            selectCostItemDetail: selectCostItemDetail,
            saveCostItemDetail: saveCostItemDetail,
            deleteCostItemDetail: deleteCostItemDetail,
            removeCashBook: removeCashBook,
            addCashBookDocument: addCashBookDocument,
            removeCashBookDocument: removeCashBookDocument,
            changeCashBookType: changeCashBookType,
        }

        $scope.method = {
            init: init,
            getNewID: getNewID
        }

        function load() {
            $scope.method.init();

            $scope.data = {
                vndAmount: null,
                vndusdExRate: null
            };

            cashBookReceiptService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data; console.log($scope.data);
                    $scope.supportLocation = data.data.cashBookLocations;
                    $scope.supportType = data.data.cashBookTypes;
                    $scope.supportSourceOfFlow = data.data.cashBookSourceOfFlows;
                    $scope.supportPostCost = data.data.cashBookPostCosts;
                    $scope.supportCostItem = data.data.cashBookCostItems;
                    $scope.supportCostItemDetail = data.data.cashBookCostItemDetails;
                    $scope.supportPaidBy = data.data.cashBookPaidBys;

                    if (context.id === 0) {
                        $scope.isEdit = false;
                    } else {
                        $scope.isEdit = true;
                    }

                    $('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                })
        };

        function changeCashBookType(id) {
            $scope.data.cashBookSourceOfFlowID = null;
            $scope.data.cashBookPaidByID = null;

            if (id !== null) {
                if (id === 1 || id === 4) {
                    $scope.data.cashBookSourceOfFlowID = 1;
                    $scope.data.cashBookPaidByID = 5;
                } 

                if (id === 2) {
                    $scope.data.cashBookSourceOfFlowID = 2;
                    $scope.data.cashBookPaidByID = 5;
                } 
            }
        };

        function saveCashBookReceipt() {
            if ($scope.editForm.$valid) {
                cashBookReceiptService.update(
                context.id,
                $scope.data,
                function (data) {
                    if (data.data.returnData === -1) {
                        if (confirm(data.message.message)) {
                            window.location = context.moveBalanceUrl;
                        }
                    } else {
                        window.location = context.refreshUrl + data.data.cashBookID;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);
                });
            }
        };

        function removeCashBook() {
            if (context.id > 0) {
                cashBookReceiptService.delete(
                    context.id,
                    null,
                    function (data) {
                        window.location = context.backUrl;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function openPostCost() {
            $('#frmPostCost').modal();
        };

        function openCostItem(cashBookPostCostId) {
            $scope.editCostItem.cashBookPostCostID = cashBookPostCostId;
            $('#frmCostItem').modal();
        };

        function openCostItemDetail(cashBookCostItemId) {
            $scope.editCostItemDetail.cashBookCostItemID = cashBookCostItemId;
            $('#frmCostItemDetail').modal();
        };

        function closePostCost() {
            $scope.editPostCost = {
                cashBookPostCostID: 0,
                postCostNM: null,
                displayIndex: null
            };

            $('#frmPostCost').modal('hide');
        };

        function closeCostItem() {
            $scope.editCostItem = {
                cashBookCostItemID: 0,
                costItemNM: null,
                cashBookPostCostID: null,
                displayIndex: null
            };

            $('#frmCostItem').modal('hide');
        };

        function closeCostItemDetail() {
            $scope.editCostItemDetail = {
                cashBookCostItemDetailID: 0,
                costItemDetailNM: null,
                cashBookCostItemID: null,
                displayIndex: null
            };

            $('#frmCostItemDetail').modal('hide');
        };

        function editPostCost(item, index) {
            $scope.editPostCost = {
                cashBookPostCostID: item.cashBookPostCostID,
                postCostNM: item.postCostNM,
                displayIndex: item.displayIndex
            };
        };

        function selectPostCost(item, index) {
            $scope.data.cashBookPostCostID = item.cashBookPostCostID;

            $('#frmPostCost').modal('hide');
        };

        function savePostCost() {
            if ($scope.editPostCost.postCostNM === null) {
                return false;
            }

            //if ($scope.editPostCost.displayIndex === null) {
            //    return false;
            //}

            cashBookReceiptService.updatePostCost(
                $scope.editPostCost,
                function (data) {
                    $scope.supportPostCost = data.data;

                    $scope.editPostCost = {
                        cashBookPostCostID: 0,
                        postCostNM: null,
                        displayIndex: null
                    };

                    $('#postCostIndex').val(null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function deletePostCost(id, item) {
            cashBookReceiptService.deletePostCost(
                id,
                null,
                function (data) {
                    var index = $scope.supportPostCost.indexOf(item);
                    $scope.supportPostCost.splice(index, 1);

                    $scope.editPostCost = {
                        cashBookPostCostID: 0,
                        postCostNM: null,
                        displayIndex: null
                    };

                    $('#postCostIndex').val(null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function editCostItem(item, index) {
            $scope.editCostItem = {
                cashBookCostItemID: item.cashBookCostItemID,
                costItemNM: item.costItemNM,
                cashBookPostCostID: item.cashBookPostCostID,
                displayIndex: item.displayIndex
            };
        };

        function selectCostItem(item, index) {
            $scope.data.cashBookCostItemID = item.cashBookCostItemID;

            $('#frmCostItem').modal('hide');
        };

        function saveCostItem() {
            if ($scope.editCostItem.costItemNM === null) {
                return false;
            }

            if ($scope.editCostItem.cashBookPostCostID === null) {
                return false;
            }

            cashBookReceiptService.updateCostItem(
                $scope.editCostItem,
                function (data) {
                    $scope.supportCostItem = data.data;

                    $scope.editCostItem = {
                        cashBookCostItemID: 0,
                        costItemNM: null,
                        cashBookPostCostID: null,
                        displayIndex: null
                    };

                    $('#costItemIndex').val(null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function deleteCostItem(id, item) {
            cashBookReceiptService.deleteCostItem(
                id,
                null,
                function (data) {
                    var index = $scope.supportCostItem.indexOf(item);
                    $scope.supportCostItem.splice(index, 1);
                    
                    $scope.editCostItem = {
                        cashBookCostItemID: 0,
                        costItemNM: null,
                        //cashBookPostCostID: null,
                        displayIndex: null
                    };

                    $('#costItemIndex').val(null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function editCostItemDetail(item) {
            $scope.editCostItemDetail = {
                cashBookCostItemDetailID: item.cashBookCostItemDetailID,
                costItemDetailNM: item.costItemDetailNM,
                cashBookCostItemID: item.cashBookCostItemID,
                displayIndex: item.displayIndex
            };
        };

        function selectCostItemDetail(item) {
            $scope.data.cashBookCostItemDetailID = item.cashBookCostItemDetailID;
            $('#frmCostItemDetail').modal('hide');
        };

        function saveCostItemDetail() {
            if ($scope.editCostItemDetail.costItemDetailNM === null) {
                return false;
            }

            if ($scope.editCostItemDetail.cashBookCostItemID === null) {
                return false;
            }

            cashBookReceiptService.updateCostItemDetail(
                $scope.editCostItemDetail,
                function (data) {
                    $scope.supportCostItemDetail = data.data;

                    $scope.editCostItemDetail = {
                        cashBookCostItemDetailID: 0,
                        costItemDetailNM: null,
                        cashBookCostItemID: null,
                        displayIndex: null
                    };

                    $('#costItemDetailIndex').val(null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function deleteCostItemDetail(id, item) {
            cashBookReceiptService.deleteCostItemDetail(
                id,
                null,
                function (data) {
                    var index = $scope.supportCostItemDetail.indexOf(item);
                    $scope.supportCostItemDetail.splice(index, 1);

                    $scope.editCostItemDetail = {
                        cashBookCostItemDetailID: 0,
                        costItemDetailNM: null,
                        //cashBookCostItemID: null,
                        displayIndex: null
                    };

                    $('#costItemDetailIndex').val(null);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function addCashBookDocument() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                if ($scope.data.cashBookDocuments === null) {
                    $scope.data.cashBookDocuments = [];
                }
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        $scope.data.cashBookDocuments.push({
                            cashBookDocumentID: $scope.method.getNewID(),
                            hasChange: true,
                            newFile: img.filename,
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        };

        function removeCashBookDocument(id, index) {
            $scope.data.cashBookDocuments[index].cashBookDocumentID = id;
            $scope.data.cashBookDocuments[index].hasChange = true;
            $scope.data.cashBookDocuments[index].newFile = null;
            $scope.data.cashBookDocuments[index].thumbnailLocation = null;
            $scope.data.cashBookDocuments[index].fileLocation = null;

            $scope.data.cashBookDocuments.splice(index, 1);
        };

        function init() {
            cashBookReceiptService.serviceUrl = context.serviceUrl;
            cashBookReceiptService.token = context.token;
        };

        function getNewID() {
            $scope.newid--;
            return $scope.newid;
        };

        $scope.event.load();
    }
})();