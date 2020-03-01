var BookApp = angular.module('BookApp', []);

BookApp.controller('BookListController',
    function PhoneListController($scope, $http) {
        /*def*/
        $scope.host = location.origin;
        $scope.bookStoreApiUrl = location.origin + "/api/v1/BookStore";
        $scope.samgoPowerApiUrl = location.origin + "/api/v1/SamgoGame";
        $scope.historyApiUrl = location.origin + "/api/v1/History";
        $scope.exchangeRateApiUrl = location.origin + "/api/v1/ExchangeRate";
        $scope.boardGameApiUrl = location.origin + "/api/v1/BoardGame";
        $scope.bookStoreSearchApiUrl = $scope.bookStoreApiUrl + "Search";
        $scope.historySearchApiUrl = $scope.historyApiUrl + "Search";
        $scope.exchangeRateSearchApiUrl = $scope.exchangeRateApiUrl + "Search";
        $scope.boardGameSearchApiUrl = $scope.boardGameApiUrl + "Search";
        $scope.showMore = false;
        $scope.Books = [];
        $scope.Historys = [];
        $scope.ExchangeRates = [];
        $scope.BoardGames = [];
        $scope.showTable = 'default';
        //$scope.showTable = 'aboutSamgoGame';
        $scope.Margees = [{ 'image': '/image/BookStore/1.jpg', 'href': 'http://www.google.com/' },
        { 'image': '/image/BookStore/2.jpg', 'href': 'http://www.google.com/' },
        { 'image': '/image/BookStore/3.jpg', 'href': 'http://www.google.com/' },
        { 'image': '/image/BookStore/4.jpg', 'href': 'http://www.google.com/' },
        { 'image': '/image/BookStore/5.jpg', 'href': 'http://www.google.com/' },
        { 'image': '/image/BookStore/6.jpg', 'href': 'http://www.google.com/' }]

        $scope.load = function () {
            $http.get($scope.bookStoreApiUrl).then(function (response) {
                $scope.Books = response.data;
                if ($scope.Books)
                    for (i = 0; i < $scope.Books.length; i++) {
                        $scope.Books[i].mode = "Edit";
                    }
            });
            $http.get($scope.historyApiUrl).then(function (response) {
                $scope.Historys = response.data;
            });
            $http.get($scope.exchangeRateApiUrl).then(function (response) {
                $scope.ExchangeRates = response.data;
            });
            $http.get($scope.boardGameApiUrl).then(function (response) {
                $scope.BoardGames = response.data;
            });
           
        }

        $scope.goAPIList = function () {
            document.location.href = $scope.host + "/swagger";
        }
        $scope.addRow = function () {
            $scope.Books.push({});
        }

        $scope.showData = function (data) {
            return data.editMode == true
        }
        $scope.goEditMode = function (data) {
            for (i = 0; i < $scope.Books.length; i++) {
                if ($scope.Books[i].SeqNo == data.SeqNo) {
                    if ($scope.Books[i].editMode == true) {
                        $scope.Books[i].editMode = !$scope.Books[i].editMode;
                        $scope.Books[i].mode = "Edit";

                        break;
                    }
                    else {
                        $scope.Books[i].editMode = true;
                        $scope.Books[i].mode = "View";
                    }
                }
            }
        }
        $scope.goViewMode = function (data) {
            $scope.goEditMode(data);
        }

        /*ajax*/
        $scope.addData = function (data) {
            if (data.BookName) {
                var req = {
                    method: 'POST',
                    url: $scope.bookStoreApiUrl,
                    headers: { 'Content-Type': 'application/json' },
                    data: data
                }
                $http(req).then(function (response) {
                    $scope.Books.push(response.data);
                    $scope.inputData = {};
                });
            } else {
                alert("請輸入書名");
            }
        }
        $scope.myKeyup = function (e) {
            var keycode = window.event ? e.keyCode : e.which;
            if (keycode == 13) {
                $scope.addData($scope.inputData);
            }

        }
        $scope.editData = function (data) {
            var req = {
                method: 'PUT',
                url: $scope.bookStoreApiUrl,
                headers: { 'Content-Type': 'application/json' },
                data: data
            }
            $http(req).then(function (response) {
                $scope.goEditMode(data);
            });
        }
        $scope.deleteData = function (data) {
            if (!confirm("Are you sure?")) return;

            var req = {
                method: 'DELETE',
                url: $scope.bookStoreApiUrl,
                headers: { 'Content-Type': 'application/json' },
                data: data
            }
            $http(req).then(function (response) {
                for (i = 0; i < $scope.Books.length; i++) {
                    if ($scope.Books[i].SeqNo == data.SeqNo) {
                        $scope.Books.splice(i, 1);
                        //delete $scope.Books[i];
                    }
                }
            });

        }
        $scope.searchByKeyword = function (data) {

            var targetUrl;
            switch ($scope.showTable) {
                case 'book':
                    targetUrl = $scope.bookStoreSearchApiUrl;
                    break;
                case 'history':
                    targetUrl = $scope.historySearchApiUrl;
                    break;
                case 'exchangeRate':
                    targetUrl = $scope.exchangeRateSearchApiUrl;
                    break;
                case 'boardGame':
                    targetUrl = $scope.boardGameSearchApiUrl;
                    break;
                default:
                    break;
            }

            var req = {
                method: 'GET',
                url: targetUrl + "/?keyword=" + data,
            }
            $http(req).then(function (response) {
                if ($scope.showTable == 'book') {
                    $scope.Books = response.data;
                }
                else if ($scope.showTable == 'history') {
                    $scope.Historys = response.data;
                }
                else if ($scope.showTable == 'exchangeRate') {
                    $scope.ExchangeRates = response.data;
                }
                else if ($scope.showTable == 'boardGame') {
                    $scope.BoardGames = response.data;
                }


            });
        }
        /*page */
        $scope.changePageData = function (data) {
            $scope.pageList = [];
            $scope.pages = Math.ceil(data.length / $scope.pageSize); //分頁數
            $scope.newPages = $scope.pages > $scope.pageSize ? $scope.pageSize : $scope.pages;
            for (var i = 0; i < $scope.newPages; i++) {
                $scope.pageList.push(i + 1);
            }
        }
        $scope.pageList = [];
        $scope.pageSize = 5;
        $scope.pages = Math.ceil($scope.BoardGames.length / $scope.pageSize); //分頁數
        $scope.setData = function () {
            var data = [];
            switch ($scope.showTable) {
                case 'book':
                    data = $scope.Books;
                    break;
                case 'history':
                    data = $scope.Historys;
                    break;
                case 'exchangeRate':
                    data = $scope.ExchangeRates;
                    break;
                case 'boardGame':
                    data = $scope.BoardGames;
                    break;
                default:
                    break;
            }

            $scope.itemList = data.slice(($scope.pageSize * ($scope.selPage - 1)), ($scope.selPage * $scope.pageSize));//通過當前頁數篩選出表格當前顯示數據
        }
        $scope.selectPage = function (page) {
            //不能小於1大於最大
            if (page < 1 || page > $scope.pages) return;
            //最多顯示分頁數5
            if (page > 2) {
                //因為只顯示5個頁數，大於2頁開始分頁轉換
                var newpageList = [];
                for (var i = (page - ($scope.pageSize - 2)) ; i < ((page + ($scope.pageSize - 3)) > $scope.pages ? $scope.pages : (page + 2)) ; i++) {
                    newpageList.push(i + 1);
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
        $scope.pageData = [];
        $scope.Previous = function () {
            $scope.selectPage($scope.selPage - 1);
        }
        //下一頁
        $scope.Next = function () {
            $scope.selectPage($scope.selPage + 1);
        };
        /*auto run*/

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
            $scope.changePageData(data);
        })

    });