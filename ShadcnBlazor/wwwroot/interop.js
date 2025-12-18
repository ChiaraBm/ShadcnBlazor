window.shadcnBlazor = {
    getBoundingBox: function (element) {
        if(element instanceof HTMLElement)
            return element.getBoundingClientRect();
        
        return {};
    },
    
    getViewport: function () {
        return {
            height: window.innerHeight,
            width: window.innerWidth
        }
    },
    
    getMatchMedia(query) {
        return window.matchMedia(query).matches;
    }
}