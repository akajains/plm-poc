(function() {
    'use strict';

    var app = angular.module('PLM.UI', [
		'ngRoute',
		'PLM.Controllers',
		'PLM.Services',
		'LocalStorageModule',
        "ui.router"
		]);
    
	//Route configuration
	app.config(['$routeProvider',
    '$httpProvider',
    '$locationProvider',
    '$stateProvider',
    '$urlRouterProvider',
    function($routeProvider,httpProvider,$locationProvider,$stateProvider,$urlRouterProvider) {            
            
           //ToDo: Turn off for time being
           
           // $locationProvider.html5Mode(true);                        
           //$locationProvider.hashPrefix('!');            
           // $urlRouterProvider.otherwise("/");
             
            $stateProvider.state('private',{
                url: "/private",
                templateUrl: "resource/private.html",
                controller: "UserController"
            })
            
            .state('status',{
                url: "/status", template: "The site is up and running"
            })
            
            .state('authorize',{
                url: '/authorize', templateUrl: 'account/authorize.html', controller: 'AuthorizeController'
            })
            
			// var route = {templateUrl: 'account/authorize.html', controller: 'AuthorizeController'};            
			// $routeProvider.when('/authorize', route);
			// $routeProvider.when('/status', {template:'The web server is up and running'});
            // $routeProvider.when('/authorized', {redirectTo: '/private'});
            
            // $routeProvider.when('/private', {templateUrl: 'resource/private.html', controller: 'UserController'});            
			//httpProvider.interceptors.push('authInterceptor');         
			 
	}]);
	
	//Constants
	app.constant('AUTH_EVENTS', {
        loginSuccess: 'auth-login-success',
        loginFailed: 'auth-login-failed',
        logoutSuccess: 'auth-logout-success',
        logoutFailed: 'auth-logout-failed',
        registrationSuccess: 'auth-registration-success',
        registrationFailed: 'auth-registration-failed',
        sessionTimeout: 'auth-session-timeout',
        notAuthenticated: 'auth-not-authenticated',
        notAuthorized: 'auth-not-authorized'
    });
	
	app.constant('USER_ROLES', {
        all: '*',
        admin: 'admin',
        editor: 'editor',
        guest: 'guest'
    });


})();