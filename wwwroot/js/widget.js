import { Clock } from "./clock.js";

export function Widget(el, data = null)
{
    this.el = el;
    //this._edit_el = el.querySelector("[data-name='editWidget']");
    //this._delte_el = el.querySelector("[data-name='deleteWidget']");

    this._name_el = el.querySelector("[data-name='widgetName']");
    this._fullLocation_el = el.querySelector("[data-name='fullLocation']");
    this._temperature_el = el.querySelector("[data-name='temperature']");
    this._windSpeed_el = el.querySelector("[data-name='windSpeed']");
    this._cloudPercentage_el = el.querySelector("[data-name='cloudPercentage']");
    this._summaryIcon_el = el.querySelector("[data-name='summaryIcon']");

    this._id;
    this._name;
    this._fullLocation;

    this._temperature;
    this._windSpeed;
    this._cloudPercentage;
    this._summaryIcon;

    this.clock = new Clock(el.querySelector("[data-name='localTime']"), data?.localTime)

    Object.defineProperty(this, 'id', {
        get: function() {
          return this._id;
        },
        set: function(value) {
          this._id = value;
          this.el.setAttribute("data-widgetId", value);
        }
    });
    
    Object.defineProperty(this, 'name', {
        get: function() {
          return this._name;
        },
        set: function(value) {
          this._name = value;
          this._name_el.innerText = value;
        }
    });

    Object.defineProperty(this, 'fullLocation', {
      get: function() {
        return this._fullLocation;
      },
      set: function(value) {
        this._fullLocation = value;
        this._fullLocation_el.innerText = value;
      }
  });

    Object.defineProperty(this, 'windSpeed', {
      get: function() {
        return this._windSpeed;
      },
      set: function(value) {
        this._windSpeed = value;
        this._windSpeed_el.innerText = value;
      }
  });

    Object.defineProperty(this, 'temperature', {
        get: function() {
          return this._temperature;
        },
        set: function(value) {
          this._temperature = value;
          this._temperature_el.innerText = value;
        }
    });
    
    Object.defineProperty(this, 'cloudPercentage', {
        get: function() {
          return this._cloudPercentage;
        },
        set: function(value) {
          this._cloudPercentage = value;
          this._cloudPercentage_el.innerText = value;
        }
    });

    Object.defineProperty(this, 'summaryIcon', {
        get: function() {
          return this._summaryIcon;
        },
        set: function(value) {
          this._summaryIcon = value;
          this._summaryIcon_el.setAttribute("src", value);
        }
    });

    if(data == null)
    {
        this.id = this.el.getAttribute("data-widgetId");
        this.name = this._name_el.innerText;
    }
    else
    {
        this.id = data.widgetId;
        this.name = data.widgetName;
        this.fullLocation = data.fullLocation;
    }

    this.updateWidget = function(){
      var httprequest = new XMLHttpRequest();
          httprequest.open("GET", "/api/weather?widget_id="
              + encodeURIComponent(this.id),
              true);

          httprequest.setRequestHeader(
              "Content-Type",
              "application/json"
          );

          httprequest.onload = () => {
              var json = JSON.parse(httprequest.response)
              
              this.temperature = json.temperature
              this.windSpeed = json.windSpeed
              this.cloudPercentage = json.cloudPercentage
              this.summaryIcon = json.summaryIconUrl;
          }

          httprequest.send();
    }

    this.updateWidget();
    
    this.widgetInterval = setInterval(() => {
        this.updateWidget();   
    }, 60 * 1000);

}