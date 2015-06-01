(function () {

    var app = angular.module('MyApp', []);

    app.controller('ProductController', ['$http', '$log', function ($http, $log) {
        var product = this;
        product.items = [];
        $http.get('/api/products/list')
            .success(function (products) {
                product.items = products;
                $log.log(products);
            })
            .error(function (err) {
                $log.log("cannot load products : ", err);
            });

    }]);

    app.directive('productCatalog', function () {
        return {
            restrict: 'E',
            templateUrl: '/scripts/templates/product-catalog.html',
            controller: 'ProductController',
            controllerAs: 'product'
        };
    });

})();