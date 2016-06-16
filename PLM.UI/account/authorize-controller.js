(function() {
'use strict';

    angular
        .module('PLM.Controllers.Authorization',[])
        .controller('AuthorizeController',  AuthorizeController);

    AuthorizeController.$inject = [
            "$log",
            "$scope",
            "AuthService"];
    
    function AuthorizeController($log,$scope,authService) {
      
      	$log.info("AuthorizeController called");
		$scope.message = "redirecting to signle sign on";
        
        authService.authorize();
      
    }
})();





