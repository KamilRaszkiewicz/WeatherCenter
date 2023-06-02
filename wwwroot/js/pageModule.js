import { WidgetManager } from "./WidgetManager.js"

$( document ).ready(function() {
    configureKendoAutocomplete();
    setupFormValidating();

    var manager = new WidgetManager();
});



function configureKendoAutocomplete() {
    $("#autoComplete").kendoAutoComplete({
        minLength: 2,
        dataTextField: "fullName",
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    serverFiltering: true,
                    url: "/api/autocomplete",
                    data: {
                        q: function () {
                            return $("#autoComplete").data("kendoAutoComplete").value();
                        }
                    }
                }
            },
        }),
        select: function (e) {
            var form = $("#widgetForm")[0];
            var dataitem = this.dataItem(e.item.index());
            form.elements["placeId"].value = dataitem.placeId;
            form.elements["city"].value = dataitem.city;
            form.elements["secondPart"].value = dataitem.secondPart;
        }
    });

    $("#autoComplete")[0].addEventListener("input", function (e) {
        if(e.target.value.length > 2)
        {
            $("#autoComplete").data("kendoAutoComplete").dataSource.read();
        }
    });

}

function setupFormValidating(formElements) {
    var formElements = [
        $("[name=placeId]")[0],
        $("[name=city]")[0],
        $("[name=secondPart]")[0],
        $("[name=widgetName]")[0]
    ]

    formElements.forEach(function (el) {
        el.addEventListener("input", function () {
            for (var i = 0; i < formElements.length; i++) {
                if (formElements[i].value == "") {
                    $("#submitWidget")[0].disabled = true;
                    return;
                }
            }
            $("#submitWidget")[0].disabled = false;
        });
    });
}
