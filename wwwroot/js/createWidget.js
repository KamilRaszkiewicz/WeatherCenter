export function createWidgetCallback(manager, createForm)
{
    var httprequest = new XMLHttpRequest();

    httprequest.open("POST", "/api/widget", true);

    httprequest.setRequestHeader(
        "Content-Type",
        "application/json"
    );

    httprequest.onload = () => {
        var json = JSON.parse(httprequest.response)
        
        manager.createWidget(json);
    }

    var data = {
        WidgetName: createForm.querySelector("[name=widgetName]").value,
        PlaceId: createForm.querySelector("[name=placeId]").value,
        City: createForm.querySelector("[name=city]").value,
        SecondPart: createForm.querySelector("[name=secondPart]").value
    }

    var json = JSON.stringify(data);

    httprequest.send(json);
}