app.service('svcAppCar', ['$http', function ($http) {
    this.HCLYears = function () {
        return $http.get('/api/Cars/Years')
            .then(function (response) { return response.data; });
    };

    this.HCLMakes = function (year) {
        var options = { params: { year: year } };
        return $http.get('/api/Cars/Makes', options)
            .then(function (response) { return response.data; });
    };

    this.HCLModels = function (year, make) {
        var options = { params: { year: year, make: make } };
        return $http.get('/api/Cars/Models', options)
            .then(function (response) { return response.data; });
    };

    this.HCLTrims = function (year, make, model) {
        var options = { params: { year: year, make: make, model: model } };
        return $http.get('/api/Cars/Trims', options)
            .then(function (response) { return response.data; });
    };

    this.HCLCars = function (year, make, model, trim) {
        var options = { params: { year: year, make: make, model: model, trim: trim } };
        return $http.get('/api/Cars/Car', options)
            .then(function (response) {
                return response.data;
            });
    };

    this.HCLCar = function (year, make, model, trim) {
        var options = { params: { year: year, make: make, model: model, trim: trim } };
        return $http.get('/api/Cars/Car', options)
            .then(function (response) {
                return response.data;
            });
    };

}]);