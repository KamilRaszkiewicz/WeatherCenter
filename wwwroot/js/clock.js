export function Clock(el, time = null){
    var [hrs, mins] = [0, 0];
    if(time == null)
    {
        [hrs, mins] = el.innerText.split(":");
    }
    else
    {

        [hrs, mins] = time.split(":");
    }
    

    this.el = el;
    this.hrs = hrs;
    this.mins = mins;

    updateHtml(this);

    var thisIsClock = this;

    this.innerInterval;
    this.outerInterval = setInterval(function (){

        if(new Date().getSeconds() == 0)
        {
            tick(thisIsClock);
            updateHtml(thisIsClock);

            thisIsClock.innerInterval = setInterval(function(){ 

                tick(thisIsClock);
                updateHtml(thisIsClock);

            }, 60 * 1000);

            clearInterval(thisIsClock.outerInterval);
        }
    }, 1000);

    function updateHtml(clock)
    {
        clock.el.innerText = clock.hrs.toString().padStart(2, "0") + ":" + clock.mins.toString().padStart(2, "0");
    }

    function tick(clock)
    {
        clock.mins++;

        if(clock.mins == 60)
        {
            clock.mins=0;
            clock.hrs++;
        }

        if(clock.hrs == 24)
        {
            clock.hrs = 0;
        }
    }
}