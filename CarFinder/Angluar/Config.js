app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state('cars', {
            url: '/',
            templateUrl: 'NgApps/Templates/cars.html',
            controller: 'ctrAppCar'
        })
        .state('home', {
            url: '/home',
            templateUrl: 'NgApps/Templates/home.html',
            controller: 'HomeController' 
        })
        .state('about', {
            url: '/aboutpage',
            templateUrl: 'NgApps/Templates/about.html',
            controller: 'AboutController'
        })
        .state('contact', {
            url: '/contactpage',
            templateUrl: 'NgApps/Templates/contact.html',
            controller: 'ContactController'
        })
        .state('apiHelp', {
            url: '/apihelp',
            templateUrl: 'App_Data/XmlDocument.xml',
            controller: 'ApiHelpController'
        })

}]);