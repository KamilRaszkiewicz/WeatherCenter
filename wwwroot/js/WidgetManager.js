import { Widget } from "./widget.js";
import { deleteWidgetCallback } from "./deleteWidget.js";
import { editWidgetCallback, saveChangesCallback } from "./editWidget.js";
import { createWidgetCallback } from "./createWidget.js";

export function WidgetManager()
{
    this._widgetTemplate = document.querySelector("#widgetTemplateContainer").querySelector(".widget");
    this._widgetsRoot = document.querySelector("#widgetsContainer");

    this._editForm = document.querySelector("[name=editForm]");
    this._saveChangesButton = document.querySelector("#saveButton");

    this._createWidgetForm = document.querySelector("#widgetForm");
    this._createWidgetButton = document.querySelector("#submitWidget");

    this._elements = Array.from(this._widgetsRoot.querySelectorAll(".widget"));
    this._editButtons = Array.from(this._widgetsRoot.querySelectorAll("[data-name='editWidget'"));

    this._widgets = [];

    var thisIsManager = this;

    this._elements.forEach(function(element){
        element.querySelector("[data-name='editWidget'").addEventListener("click", function(){
            editWidgetCallback(thisIsManager._editForm, element)
        });
        element.querySelector("[data-name='deleteWidget'").addEventListener("click", function(){
            deleteWidgetCallback(thisIsManager, element)
        });

        this._widgets.push(new Widget(element))
    }, this);

    this._saveChangesButton.addEventListener("click", function(){
        saveChangesCallback(thisIsManager, thisIsManager._editForm);
    });

    this._createWidgetButton.addEventListener("click", function(){
        createWidgetCallback(thisIsManager, thisIsManager._createWidgetForm)
    });


    this.createWidget = function(data)
    {
        var node = this._widgetTemplate.cloneNode(true);

        node.querySelector("[data-name='editWidget'").addEventListener("click", function(){
            editWidgetCallback(thisIsManager._editForm, node);
        });
        node.querySelector("[data-name='deleteWidget'").addEventListener("click", function(){
            deleteWidgetCallback(thisIsManager, node);
        });

        var widget = new Widget(node, data);

        widget.updateWidget();

        this._elements.push(node);
        this._widgets.push(widget);
        this._widgetsRoot.prepend(node);
    }

    this.deleteWidget = function(widgetId)
    {
        var widget;

        this._widgets.forEach(function(w){
            if(w.id == widgetId)
            {
                widget = w;
            }
        }, this)

        clearInterval(widget.clock.innerInterval);
        clearInterval(widget.clock.outerInterval);
        clearInterval(widget.widgetInterval);

        var index = this._widgets.indexOf(widget);

        this._elements.splice(index, 1);
        this._widgets.splice(index, 1);

        widget.el.remove();
    }

    this.editWidget = function(widgetId, newName)
    {
        var widget;

        this._widgets.forEach(function(w){
            if(w.id == widgetId)
            {
                widget = w;
            }
        }, this)

        widget.name = newName;
    }
}
