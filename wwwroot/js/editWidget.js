export function editWidgetCallback(editForm, widget)
{
    var widgetId = widget.getAttribute("data-widgetId");

    editForm.querySelector("input[name=widget_id").value = widgetId;
    editForm.querySelector("input[name=newName]").value = widget.querySelector("[data-name=widgetName]").innerText;
}

export function saveChangesCallback(manager, editForm)
{
    var widgetId = editForm.querySelector("input[name=widget_id").value;
    var name = editForm.querySelector("input[name=newName]").value;

    var httprequest = new XMLHttpRequest();

    httprequest.open("PATCH", "/api/widget?widget_id="
        + encodeURIComponent(widgetId) + "&name=" + encodeURIComponent(name),
        true);

    httprequest.onload = () => 
    {
        if(httprequest.status == 200)
        {
            manager.editWidget(widgetId, name)
        }
    }

    httprequest.send();
}

