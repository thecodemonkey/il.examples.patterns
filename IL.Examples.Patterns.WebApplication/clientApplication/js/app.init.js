angular.module('application', ['ui.bootstrap', 'ngGrid'])
       .run(function ($rootScope) {
           $rootScope.IsAuthenticated = false;

           if (typeof (_xToken) != 'undefined' && _xToken != null && _xToken != '')
           {
               $rootScope.IsAuthenticated = true;
               $rootScope.Token = _xToken;
           }
       });