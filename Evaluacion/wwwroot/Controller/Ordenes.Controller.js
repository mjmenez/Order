'use strict';

var app = angular.module("MyApp", ['ngMaterial', 'ngMessages']);

app.config(function($mdThemingProvider) {
    // $mdThemingProvider.theme('default')
    //    .primaryPalette('pink')
    //     .accentPalette('orange');
});

app.controller("MyController", function($scope, $http, $mdDialog) {
    $scope.Nombre = "Producto";
    $scope.Result = {};

    $http({
        method: 'GET',
        url: 'http://localhost:40304/api/Ordenes',
        withCredentials: false,
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Accept': 'application/json'
        }
    }).then(function(response) {
        $scope.Result = response.data;
    }, function(error) {

    });

    $scope.FiltrarTab = function(Estatus) {
        $http({
            method: 'POST',
            url: 'http://localhost:40304/api/Ordenes/Consultar?IdCatEstatus=' + Estatus,
            withCredentials: false,
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Accept': 'application/json'
            }
        }).then(function(response) {
            $scope.Result = response.data;
            console.log(response);
        }, function(error) {
            console.log(error);

        });
    };

    $scope.Modificar = function(IdOrden, Estatus) {

        $http({
            method: 'POST',
            url: 'http://localhost:40304/api/Ordenes/Modificar?IdOrden=' + IdOrden + '&IdCatEstatus=' + (Estatus + 1),
            withCredentials: false,
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Accept': 'application/json'
            }
        }).then(function(response) {
            $scope.FiltrarTab(Estatus);
        }, function(error) {
            console.log(error);

        });
    };

    $scope.Cancelar = function(IdOrden, Estatus) {

        $http({
            method: 'POST',
            url: 'http://localhost:40304/api/Ordenes/Modificar?IdOrden=' + IdOrden + '&IdCatEstatus=' + 5,
            withCredentials: false,
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Accept': 'application/json'
            }
        }).then(function(response) {
            $scope.FiltrarTab(Estatus);
        }, function(error) {
            console.log(error);

        });
    };

    $scope.Template = `
    <md-dialog aria-label="Agregar orden" id="ModalAgregar">
    <form>
        <md-toolbar>
            <div class="md-toolbar-tools">
                <h2>Agregar orden</h2>
            </div>
        </md-toolbar>
        <md-dialog-content style="min-width:400px;max-width:800px;max-height:600px; ">
            <div class="container">
                <div class="row">
                    <div class="col" style="max-width: 500;">
                        <md-select ng-model="Productos" placeholder="Productos" multiple>
                            <md-option ng-value="producto" ng-disabled="producto.CantidadAlmacen == 0" ng-repeat="producto in ListProductos">{{producto.Descripcion}}</md-option>
                        </md-select>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <table class="table table-striped table-hover">
                            <thead>
                                <tr>
                                    <td>Producto</td>
                                    <td>Cantidad</td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="pro in Productos">
                                    <td>{{pro.Descripcion}}</td>
                                    <td>
                                        <div class="row">
                                            <div class="col-sm m-0">
                                                <input type="number" class="form-control" ng-model="pro.Cantidad" placeholder="Cantidad" ng-keydown="Validacion($event,pro)">
                                            </div>
                                            <div class="col-sm m-0">/{{pro.CantidadAlmacen}}</div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </md-dialog-content>

        <md-dialog-actions style="justify-content: center;">
            <div class="container">
                <div class="flex">
                    <button class="btn btn-primary p-1 btn-sm" ng-click="GuardarProducto()"> Guardar </button>
                    <button class="btn btn-secondary p-1 btn-sm" ng-click="Cerrar()"> Cerrar </button>
                </div>
            </div>


        </md-dialog-actions>
    </form>
</md-dialog>
    `;

    $scope.Agregar = function() {
        $mdDialog.show({
                controllerAs: 'dialogCtrl',
                controller: DialogController,
                //templateUrl: './Modal/Dialog.tmpl.html',
                template: $scope.Template,
                parent: angular.element(document.body),
                clickOutsideToClose: false,
                bindToController: true,
                autoWrap: false,
                multiple: true,
                preserveScope: true,

            })
            .then(function(answer) {
                $scope.FiltrarTab(1);
            }, function() {
                $scope.FiltrarTab(1);

            });
    };

    function DialogController($scope, $mdDialog) {
        $scope.ListProductos = [];
        $scope.Productos = [];

        $http({
            method: 'GET',
            url: 'http://localhost:40304/api/Productos',
            withCredentials: false,
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Accept': 'application/json'
            }
        }).then(function(response) {
            $scope.ListProductos = response.data;
        }, function(error) {

        });

        $scope.GuardarProducto = function() {
            var data = $scope.Productos.map(function(obj) {
                var rObj = { IdProducto: obj.IdProducto, Cantidad: parseInt(obj.Cantidad) };
                return rObj;
            });
            $http({
                method: 'POST',
                url: 'http://localhost:40304/api/Ordenes/Agregar',
                withCredentials: false,
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Accept': 'application/json'
                },
                data: data
            }).then(function(response) {
                $scope.Cerrar();
            }, function(error) {
                console.log(error);

            });
        };

        $scope.Validacion = function(event, pro) {
            if (parseInt(event.key) > pro.CantidadAlmacen) {
                $mdDialog.show(
                    $mdDialog.alert()
                    .clickOutsideToClose(true)
                    .title('Aviso')
                    .textContent('No existen productos suficientes')
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Aceptar')
                    .multiple(true)
                ).then(function() {
                    pro.Cantidad = '';
                }, function() {

                });

            }
            if ((pro.CantidadAlmacen - parseInt(event.key)) == 0) {
                $mdDialog.show(
                    $mdDialog.alert()
                    .clickOutsideToClose(true)
                    .title(pro.Descripcion)
                    .textContent('El producto se agotara con esta orden')
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Aceptar')
                    .multiple(true)
                );

            }

        };
        $scope.Abrir = function() {
            $mdDialog.show();

        };

        $scope.Cerrar = function() {
            $mdDialog.cancel();
        };


    }



});