var SamgoGameApp = angular.module('BookApp', ['ui.bootstrap']);

SamgoGameApp.controller('SamgoGameController',
    function SamgoGameController($scope, $http, $uibModal) {
        /*def*/
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
        $scope.LegionArray = ['狂龙'];
        
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
            $scope.reqModel.Legion =  "狂龙";
        }
        //$scope.showTable = 'default';
        $scope.showTable = 'writeDetail';
        $scope.switchPage = function (data) {
            $scope.showTable = data;
            if (data == 'openRaider') {
                //window.navigator = location.origin + "/samgo/raider";
                location.href = location.origin + "/samgo/raider";
            }
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

        $scope.goAPIList = function () {
            document.location.href = $scope.host + "/swagger";
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

        $scope.open = function () {
            
        };
        $scope.send_user_data = function () {
            //$scope.result = '';
            //var modalInstance = $uibModal.open({
            //    templateUrl: "SamgoModal",
            //    controller: "ModalContentCtrl",
            //    size: '',
            //});
            
            if (!parseInt($scope.reqModel.Power)) { alert("請起碼輸入戰力資料"); return; }
            alert("資料輸入中");

            var req = {
                method: 'POST',
                url: $scope.samgoGameUserApiUrl,
                headers: { 'Content-Type': 'application/json' },
                data: $scope.reqModel
            }
            $http(req).then(function (response) {
                $scope.reqModel.SeqNo = response.data;
                alert("感謝你的提交！ 龍團愉快");
                //modalInstance.result.then(function (response) {
                //    $scope.result = `${response}`;
                //});

            });



        }
            /*ajax*/

            /*page */

        $scope.selectPage = function (page) {
            //不能小於1大於最大
            if (page < 1 || page > $scope.pages) return;
            //最多顯示分頁數5
            if (page > 2) {
                //因為只顯示5個頁數，大於2頁開始分頁轉換
                var newpageList =[];
                for (var i = (page - ($scope.pageSize - 2)) ; i < ((page + ($scope.pageSize - 3)) > $scope.pages ? $scope.pages : (page + 2)) ; i++) {
                    newpageList.push(i +1);
            }
                $scope.pageList = newpageList;
        }
            $scope.selPage = page;
            $scope.setData();
            $scope.isActivePage(page);
        };
        $scope.isActivePage = function (page) {
            return $scope.selPage == page;
        };
        $scope.pageData =[];
        $scope.Previous = function () {
            $scope.selectPage($scope.selPage -1);
        }
            //下一頁
        $scope.Next = function () {
            $scope.selectPage($scope.selPage +1);
        };
            /*auto run*/
        $scope.init();
     //   $scope.load();
        $scope.$watch('showTable', function (newValue, oldValue) {
            console.log(newValue +oldValue);
            var data =[];
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
            $scope.itemList =[]; $scope.selPage = 0;
        })
    });
SamgoGameApp.controller('ModalContentCtrl', function ($scope, $uibModalInstance) {
    $scope.ok = function () {
        $uibModalInstance.close("資料輸入");
    }
    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }
});
