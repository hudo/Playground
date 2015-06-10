(function() {
    var app = {
        planets: [],
        init : function() {
            app.planets = $("#planets");

            $(document).on("click", "#planets li", function () {
                app.onPlanetClick($(this));
            });
        },
        loadPlanets : function() {
            catalog.getAllPlanets(function(data) {
                $(data).each(function() {
                    app.planets.append("<li data-id='" + this.Id + "'><strong>" + this.Name + "</strong></li>");
                });
            });
        },
        onPlanetClick : function($el) {
            var id = $el.data("id");
            catalog.getPlanet(id, function(data) {
                $(".distance").remove();
                $el.append("<div class='distance'>" + addCommas(data.DistanceFromSun) + " km from Sun</div>");
            });
        }
    };

    var catalog = {
        getAllPlanets : function(callback) {
            $.get("/api/planets", function (data) {
                callback(data);
            });
        },
        getPlanet : function(id, callback) {
            $.get("/api/planets/" + id, function (data) {
                callback(data);
            });
        }
    }

    // todo: error handling, templating, binding

    $(function () {
        app.init();
        app.loadPlanets();
    })

    // http://www.mredkj.com/javascript/numberFormat.html
    function addCommas(nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }
})();