var SamgoGameApp = angular.module('BookApp', []);


SamgoGameApp.controller('RaiderController',
    function RaiderController($scope, $http) {
        /*def*/
        var q = {};
        $scope.host = location.origin;
        $scope.samgoGameUserApiUrl = location.origin + "/api/v1/SamgoGameUser";
        $scope.samgoGameUserSearchApiUrl = location.origin + "/api/v1/SamgoGameUserSearch";
        $scope.samgoGameUserDetailApiUrl = location.origin + "/api/v1/SamgoGameUserDetail";
        $scope.showgrid = '1';
    });