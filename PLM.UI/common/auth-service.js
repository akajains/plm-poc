(function() {
    'use strict';

    angular.module('PLM.Services.Authorization', [
    ]).factory('AuthService',  ['ConfigService','localStorageService','$http','Session','$window', function (config,localStorageService, $http, Session,$window) {
        
        return {
            
            authorize: function(){
                console.log("AuthorizedController time to log on");

            //GET /authorize?
            //response_type=code%20id_token
            //&client_id=s6BhdRkqt3
            //&redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb
            //&scope=openid%20profile%data
            //&nonce=n-0S6_WzA2Mj
            //&state=af0ifjsldkj HTTP/1.1

            // var authorizationUrl = 'https://localhost:44345/connect/authorize';
            // var client_id = 'angularclient';
            // var redirect_uri = 'https://localhost:44347/authorized';
            // var response_type = "id_token token";
            // var scope = "dataEventRecords aReallyCoolScope securedFiles openid";
             var nonce = config.getOAuthConfig.nonce;
             var state = config.getOAuthConfig.state;
             
            localStorageService.set("authNonce", nonce);
            localStorageService.set("authStateControl", state);
            console.log("AuthorizedController created. adding myautostate: " + localStorageService.get("authStateControl"));

            var url =
                config.api.connectUri + "?" +
                "response_type=" + encodeURI(config.getOAuthConfig.response_type) + "&" +
                "client_id=" + encodeURI(config.getOAuthConfig.client_id) + "&" +
                "redirect_uri=" + (config.getOAuthConfig.redirect_uri) + "&" +
                "scope=" + encodeURI(config.getOAuthConfig.scope) + "&" +
                "nonce=" + encodeURI(nonce) + "&" +
                "state=" + encodeURI(state);

                $window.location = url;
            },
            
            login: function (credentials) {
                var user = { grant_type: 'password', username: "credentials.userName", password: "credentials.password", scope: 'openid read' };

                //required for Idsrv V3 call (required bys spec)
                var urlEncodedUrl = {
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                    // 'Authorization': 'Basic SWRlbnRpdHlXZWJVSTpzZWNyZXQ='
                };
                
                return $http({
                    method: 'POST', url: config.api.connect, headers:urlEncodedUrl, data: config.getOAuthConfig, transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    }
                }).success(function (data, status, headers, config) {
                    Session.create(data.access_token, credentials.userName, 'admin');
                    localStorageService.set('bearerToken', data.access_token);
                }).error(function (data, status, headers, config) {
                    Session.destroy();

                });
            },

            logout: function () {
                Session.destroy();
                localStorageService.remove('bearerToken');
                console.log('session destroyed');
            },
           
            register: function (credentials) {
                return $http.post(config.api.account + 'create',
                    {
                        Username: credentials.userName,
                        Password: credentials.password, Email: credentials.email
                    })
                .success(function (data, status, headers, config) {

                });
            },

            isAuthenticated: function () {
                console.log(Session);
                return !!Session.userId;
            },
           
        };
    }])
    
    
    
    .factory('authInterceptor', function ($rootScope, $q, localStorageService, AUTH_EVENTS) {
        return {
            request: function (config) {
                console.log('bearer : ');
                console.log(localStorageService.get('bearerToken'));
                config.headers = config.headers || {};
                if (localStorageService.get('bearerToken')) {
                    config.headers.Authorization = 'Bearer ' + localStorageService.get('bearerToken');
                }
                return config;
            },
            response: function (response) {
                if (response.status === 401) {
                    // handle the case where the user is not authenticated
                }
                return response || $q.when(response);
            },
            responseError: function (response) {
                if (response.status === 401) {
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated,
                                          response);
                }
                if (response.status === 403) {
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthorized,
                                          response);
                }
                if (response.status === 419 || response.status === 440) {
                    $rootScope.$broadcast(AUTH_EVENTS.sessionTimeout,
                                          response);
                }
                return $q.reject(response);
            }

        };
    })
    
    .service('Session', function (localStorageService) {
        this.create = function (sessionId, userId, userRole) {
            this.id = sessionId;
            this.userId = userId;
            this.userRole = userRole;
            localStorageService.set('sessionObject', this);
        };
        this.destroy = function () {
            this.id = null;
            this.userId = null;
            this.userRole = null;
            localStorageService.remove('sessionObject');
        };
        return this;
    })
    
     
    
})();