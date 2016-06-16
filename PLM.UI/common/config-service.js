(function() {
    'use strict';

    angular.module('PLM.Services.Configuration', [])
    .service('ConfigService',[function(){
        
        var apiUrl = 'http://localhost:10073/';
        var authSrvrBaseUrl = 'http://localhost:64705/';
        var publicApiUrl = 'http://localhost:14117/'
       
        var api = {
        accountUri: apiUrl + 'api/account/',
        connectUri: authSrvrBaseUrl + 'connect/authorize',
        resourceUri : publicApiUrl + 'resource/'
        };
        
        
        var getOAuthConfig = 
                 {
                    client_id: "socialnetwork_implicit",
                    scope: "read",
                    response_type: "token",
                    redirect_uri: "http://localhost:8000/index.html/private",
                    state: Date.now() + "" + Math.random(),
                    nonce: "N" + Math.random() + "" + Date.now()
                };

          
        return{                   
        appErrorPrefix: '[ABS Error] ', //Configure the exceptionHandler decorator
        docTitle: 'Identity',
        apiUrl: apiUrl,
        getOAuthConfig: getOAuthConfig,
        api: api,
        version: '0.1'
            
        }
                
    }])
    
})();