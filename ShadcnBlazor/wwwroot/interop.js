window.interop = {
    getBoundingBox: function (element) {
        return element.getBoundingClientRect();
    },
    
    getViewport: function () {
        return {
            height: window.innerHeight,
            width: window.innerWidth
        }
    },
    
    getMatchMedia(query) {
        console.log(query);
        return window.matchMedia(query).matches;
    }
}