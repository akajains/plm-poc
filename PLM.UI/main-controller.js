(function() {
    'use strict';

    angular.module('PLM.Controllers.Main', []).
    controller('MainController',['$scope','AuthService','localStorageService', function($scope, AuthService,localStorageService){
        
        $scope.test = "Successfull " 
        
        //AuthService.authorize();
        
    }])
})();