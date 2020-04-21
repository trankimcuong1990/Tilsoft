angular.module('customFilters', []).filter('materialTypeFilter', function () {
    // use in product filter
    return function (materialTypeOptions, selectedMaterialID, materialStandard) {
        var items = {
            materialID: selectedMaterialID,
            isStandard: materialStandard,
            out: []
        }

        angular.forEach(materialTypeOptions, function (value, key) {
            if (value.materialID == this.materialID && value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('materialColorFilter', function () {
    // use in product filter
    return function (materialColorOptions, selectedMaterialID, selectedMaterialTypeID, materialStandard) {
        var items = {
            materialID: selectedMaterialID,
            materialTypeID: selectedMaterialTypeID,
            isStandard: materialStandard,
            out: []
        }

        angular.forEach(materialColorOptions, function (value, key) {
            if (value.materialID == this.materialID && value.materialTypeID == this.materialTypeID && value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('frameMaterialFilter', function () {
    // use in product filter
    return function (frameMaterialOptions, selectedMaterialOptionID, frameStandard) {
        var items = {
            materialOptionID: selectedMaterialOptionID,
            isStandard: frameStandard,
            out: []
        }

        angular.forEach(frameMaterialOptions, function (value, key) {
            if (value.materialOptionID == this.materialOptionID && value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('frameMaterialColorFilter', function () {
    // use in product filter
    return function (frameMaterialColorOptions, selectedMaterialOptionID, selectedFrameMaterialID, frameStandard) {
        var items = {
            materialOptionID: selectedMaterialOptionID,
            frameMaterialID: selectedFrameMaterialID,
            isStandard: frameStandard,
            out: []
        }
        
        angular.forEach(frameMaterialColorOptions, function (value, key) {
            if (value.materialOptionID == this.materialOptionID && value.frameMaterialID == this.frameMaterialID && value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('subMaterialFilter', function () {
    // use in product filter
    return function (subMaterialOptions, subMaterialStandard) {
        var items = {
            isStandard: subMaterialStandard,
            out: []
        }

        angular.forEach(subMaterialOptions, function (value, key) {
            if (value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('subMaterialColorFilter', function () {
    // use in product filter
    return function (subMaterialColorOptions, selectedSubMaterialID, subMaterialStandard) {
        var items = {
            subMaterialID: selectedSubMaterialID,
            isStandard: subMaterialStandard,
            out: []
        }

        angular.forEach(subMaterialColorOptions, function (value, key) {
            if (value.subMaterialID == this.subMaterialID && value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('cushionTypeFilter', function () {
    // use in product filter
    return function (cushionColorOptions, cushionColorStandard) {
        var items = {
            isStandard: cushionColorStandard,
            out: []
        }

        angular.forEach(cushionColorOptions, function (value, key) {
            if (value.isStandard == this.isStandard) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('cushionColorFilter', function () {
    // use in product filter
    return function (cushionColorOptions, selectedCushionTypeID, cushionColorStandard) {
        var items = {
            cushionTypeID: selectedCushionTypeID,
            isStandard: cushionColorStandard,
            out: []
        }

        angular.forEach(cushionColorOptions, function (value, key) {
            if (value.isStandard == this.isStandard && value.cushionTypeID == this.cushionTypeID) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
}).filter('cushionFilter', function () {
    // use in product filter
    return function (cushionOptions, cushionCurrentSeason) {
        var items = {
            currentSeason: cushionCurrentSeason,
            out: []
        }

        angular.forEach(cushionOptions, function (value, key) {
            if (value.isStandard == this.currentSeason) {
                this.out.push(value);
            }
            else {
                value.isSelected = false;
            }
        }, items);
        return items.out;
    };
});