app.controller('ctrAppCar', ['$scope', 'svcAppCar', function ($scope, svcAppCar) {
    var vm = this;

    $scope.selectedYear = '';
    $scope.selectedMake = '';
    $scope.selectedModel = '';
    $scope.selectedTrim = '';
    $scope.carImage = '';

    $scope.years = [];
    $scope.makes = [];
    $scope.models = [];
    $scope.trims = [];
    $scope.cars = '';


    $scope.getYears = function () {
        svcAppCar.HCLYears().then(function (data) {
            $scope.years = data;
        });
    };
    //This line pre fills the years dropdown
    $scope.getYears();

    $scope.getMakes = function () {
        $scope.makes.length = 0;
        svcAppCar.HCLMakes($scope.selectedYear).then(function (data) {
            $scope.makes = data;
        });
        $scope.models.length = 0;
        $scope.getCars();
    };

    $scope.getModels = function () {
        svcAppCar.HCLModels($scope.selectedYear, $scope.selectedMake).then(function (data) {
            $scope.models = data;
        });
        $scope.trims.length = 0;
        $scope.getCars();
    };

    $scope.getTrims = function () {
        svcAppCar.HCLTrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel).then(function (data) {
            $scope.trims = data;
        });
    };

    $scope.getaCar = function () {
        svcAppCar.HCLCar($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) {
            $scope.car = data;
        });
    };

    $scope.getCars = function () {
        svcAppCar.HCLCars($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) {
            $scope.cars = data;
        });
    };


}]);