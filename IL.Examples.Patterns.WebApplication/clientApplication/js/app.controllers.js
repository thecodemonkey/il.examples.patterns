function GridCtrl($scope, $rootScope, $http, $dialog) {
    $scope.autoUpdate = false;
    $scope.Users = [];
    $scope.SelectedUser = {};
    $scope.ErrorMessage = null;
    $scope.IsAuthenticated = $rootScope.IsAuthenticated;


    $scope.Load = function () {
        $http.get("api/Users/?nocach=" + (new Date()).getTime(), { headers: { 'X-Token': $rootScope.Token }})
             .success(function (result) {
                 $scope.Users = result;
                 $scope.isEditorOpen = false;
                 $scope.ErrorMessage = null;
             }).error($scope.ShowError);
    }

    $scope.Edit = function (user, title)
    {
        $scope.EditorTitle = title;
        $scope.SelectedUser = (user != null) ? angular.copy(user) : null;
        $scope.isEditorOpen = true;
        $scope.ErrorMessage = null;
    }

    $scope.Save = function ()
    {
        $http({
                method: 'POST', 
                url: "api/Users/?nocach=" + (new Date()).getTime(),
                headers: { 'X-Token': $rootScope.Token },
                data: $scope.SelectedUser
            }).success(function (result) {
                $scope.isEditorOpen = false;
                $scope.Load();
            }).error(function (err) {
                $scope.ErrorMessage = "Error:" + err.Message + JSON.stringify(err.ModelState);
            });
    }

    $scope.Delete = function (id) {
        $http({
            method: 'DELETE',
            url: "api/Users/?id=" + id + "&nocach=" + (new Date()).getTime(),
            headers: { 'X-Token': $rootScope.Token },
        }).success(function (result) {
            $scope.Load();
        }).error($scope.ShowError);
    }

    $scope.ShowError = function (error) {
        $scope.isEditorOpen = false;
        $scope.ShowMessage("ein feher ist aufgetreten", error);
    };

    $scope.ShowMessage = function (title, message) {
        var btns = [{ result: 'ok', label: 'OK', cssClass: 'btn-primary' }];

        $dialog.messageBox(title, message, btns).open();
    }

    $scope.Login = function () {
        $http.post("api/Login/?nocach=" + (new Date()).getTime(), $scope.LoginModel)
             .success(function (result) {

                 $scope.IsAuthenticated = true;
                 $rootScope.IsAuthenticated = true;
                 $rootScope.Token = result.token;

                 $scope.ErrorMessage = null;

                 $scope.Load();
             }).error(function (error) {
                 $scope.ErrorMessage = error;
             });
    };

    $scope.AutoUpdate = function () {
        window.setTimeout(function () {
            if ($scope.autoUpdate) {
                $scope.Load();
            }
            $scope.AutoUpdate()
        }, 5000);
    }

    if($scope.IsAuthenticated)
        $scope.Load();

    $scope.AutoUpdate();
}