(function() {
'use strict';

    angular
        .module('PLM.Controllers.User',[])
        .controller('UserController', UserController);

    UserController.$inject = ['$scope'];
    
    function UserController($scope) {
        
        $scope.message = "User Controller";
        

      
    }
})();