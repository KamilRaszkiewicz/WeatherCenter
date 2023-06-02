export function deleteWidgetCallback(manager, widget)
{

    var widgetId = widget.getAttribute("data-widgetId");

    var httprequest = new XMLHttpRequest();
    httprequest.open("DELETE", "/api/widget?widget_id="
        + encodeURIComponent(widgetId),
        true);

    httprequest.setRequestHeader(
        "Content-Type",
        "application/json"
    );

    httprequest.onload = () => {
        if(httprequest.status == 200)
        {
            manager.deleteWidget(widgetId);
        }
    }

    httprequest.send();
}
