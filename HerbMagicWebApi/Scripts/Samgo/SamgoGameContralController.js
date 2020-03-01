var SamgoGameApp = angular.module('BookApp',[]);


SamgoGameApp.controller('SamgoGameContralController',
    function SamgoGameContralController($scope, $http) {
        /*def*/
        var q = {};
        $scope.host = location.origin;
        $scope.samgoGameUserApiUrl = location.origin + "/api/v1/SamgoGameUser";
        $scope.samgoGameUserSearchApiUrl = location.origin + "/api/v1/SamgoGameUserSearch";
        $scope.samgoGameUserDetailApiUrl = location.origin + "/api/v1/SamgoGameUserDetail";
        $scope.showMore = false;
        $scope.Books = [];
        $scope.Historys = [];
        $scope.ExchangeRates = [];
        $scope.BoardGames = [];
        $scope.LevelArray = [];
        
        $scope.HighLevelArray = [];
        $scope.SuperHighLevelArray = [];
        $scope.ImageArray = [{ 'id': 0, 'path': 'girl1b.jpg', 'name': '女生1' }
        , { 'id': 1, 'path': 'girl2b.jpg', 'name': '女生2' }
    , { 'id': 2, 'path': 'man1b.jpg', 'name': '男生1' }
    , { 'id': 3, 'path': 'man2b.jpg', 'name': '男生2' }
        ];
        $scope.init = function () {
            $scope.reqModel = {};
            for (i = 0; i < 11; i++) {
                $scope.LevelArray.push(i);
            }
            for (i = 0; i < 51; i++) {
                $scope.HighLevelArray.push(i);
            }
            for (i = 0; i < 81; i++) {
                $scope.SuperHighLevelArray.push(i);
            }
            $scope.reqModel.Legion = "狂龙";
        }
        //$scope.showTable = 'default';
        $scope.showTable = 'showDetail';
        $scope.switchPage = function (data) {
            $scope.showTable = data;
         
            $scope.load();
        }
        $scope.load = function () {
            //var req = {
            //    method: 'POST',
            //    url: $scope.samgoGameUserApiUrl,
            //    headers: { 'Content-Type': 'application/json' },
            //    data: { 'secondTime': '2019-01-31', 'firstTime': '2019-01-22' }
            //}
            //$http(req).then(function (response) {
            //    $scope.SamgoPowers = response.data;
            //});

            var req = {
                method: 'Get',
                url: $scope.samgoGameUserDetailApiUrl,
                headers: { 'Content-Type': 'application/json' },
            }
            $http(req).then(function (response) {
                $scope.userInfoList = response.data;
            });
        }

        $scope.myKeyup = function (e) {
            var keycode = window.event ? e.keyCode : e.which;
            if (keycode == 13) {
                var req = {
                    method: 'POST',
                    url: $scope.samgoGameUserSearchApiUrl,
                    headers: { 'Content-Type': 'application/json' },
                    data: { 'Role': $scope.reqModel.Role, 'Legion': $scope.reqModel.Legion }
                }
                $http(req).then(function (response) {
                    $scope.reqModel = response.data;
                });
            }

        }
$scope.send_user_data = function () {
            $scope.result = '';
           
            var req = {
                method: 'POST',
                url: $scope.samgoGameUserApiUrl,
                headers: { 'Content-Type': 'application/json' },
                data: $scope.reqModel
            }
            $http(req).then(function (response) {
                $scope.reqModel.SeqNo = response.data;
             

            });



        }
        /*ajax*/

        /*page */
        /*auto run*/
        $scope.init();
        $scope.load();
        $scope.$watch('showTable', function (newValue, oldValue) {
            console.log(newValue + oldValue);
            var data = [];
            switch ($scope.showTable) {
                case "book":
                    data = $scope.Books;
                    break;
                case "history":
                    data = $scope.Historys;
                    break;
                case "exchangeRate":
                    data = $scope.ExchangeRates;
                    break;
                case "boardGame":
                    data = $scope.BoardGames;
                    break;
                default:
                    break;
            }
            $scope.itemList = []; $scope.selPage = 0;
        })
    });