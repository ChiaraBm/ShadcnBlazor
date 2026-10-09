window.shadcnBlazor.scrollBar = {
    getScrollBarThumbSize: function (track) {
        if (track instanceof HTMLElement) return {
            scrollBarHorizontalSize: (track.offsetWidth - 2) * (track.offsetWidth / track.scrollWidth),
            scrollBarVerticalSize: (track.offsetHeight - 2) * (track.offsetHeight / track.scrollHeight)
        }

        return {};
    },

    getScrollBarScrollLength: function (area, type) {
        if (area instanceof HTMLElement) switch (type) {
            case 'horizontal': {
                const thumbLeft = (area.scrollLeft / (area.scrollWidth - area.clientWidth)) * (area.clientWidth - (Math.max(30, (area.clientWidth / area.scrollWidth) * area.clientWidth)));
                return {
                    thumbTop: 0, thumbLeft: thumbLeft
                }
            }
            case 'vertical': {
                const thumbTop = (area.scrollTop / (area.scrollHeight - area.clientHeight)) * (area.clientHeight - (Math.max(30, (area.clientHeight / area.scrollHeight) * area.clientHeight)));
                return {
                    thumbTop: thumbTop, thumbLeft: 0
                }
            }
        }

        return {};
    },

    isContentOverflowing: function (area, type) {
        if (area instanceof HTMLElement) switch (type) {
            case 'horizontal': {
                return area.scrollWidth > area.clientWidth
            }
            case 'vertical': {
                return area.scrollHeight > area.clientHeight
            }
        }
        return false;
    },

    attachScrollBar: (area, track, thumb, type) => {
        if (!(area instanceof HTMLElement) || !(track instanceof HTMLElement) || !(thumb instanceof HTMLElement)) return;
        const horizontal = String(type).toLowerCase() === 'horizontal';

        if (thumb._sbCleanup) thumb._sbCleanup();

        thumb.style.touchAction = 'none';
        track.style.touchAction = 'none';

        const metrics = () => {
            const pad = 2;
            const free = horizontal
                ? track.clientWidth - pad - thumb.offsetWidth
                : track.clientHeight - pad - thumb.offsetHeight;
            const max = horizontal ? area.scrollWidth - area.clientWidth : area.scrollHeight - area.clientHeight;
            return {free, max, ratio: free > 0 ? max / free : 0};
        };
        const getScroll = () => horizontal ? area.scrollLeft : area.scrollTop;
        const setScroll = v => {
            if (horizontal) area.scrollLeft = v; else area.scrollTop = v;
        };

        let startPointer = 0, startScroll = 0, ratio = 0, prevBehavior = '';

        const onDown = e => {
            if (e.button !== undefined && e.button !== 0) return;
            const m = metrics();
            ratio = m.ratio;
            startPointer = horizontal ? e.clientX : e.clientY;
            startScroll = getScroll();
            prevBehavior = area.style.scrollBehavior;
            area.style.scrollBehavior = 'auto';
            thumb.setPointerCapture(e.pointerId);
            e.preventDefault();
            e.stopPropagation();
        };
        const onMove = e => {
            if (!thumb.hasPointerCapture(e.pointerId) || ratio === 0) return;
            const pos = horizontal ? e.clientX : e.clientY;
            setScroll(startScroll + (pos - startPointer) * ratio);
        };
        const onUp = e => {
            if (thumb.hasPointerCapture(e.pointerId)) thumb.releasePointerCapture(e.pointerId);
            area.style.scrollBehavior = prevBehavior;
        };

        const onTrackDown = e => {
            if (e.target !== track) return;
            const {free, max} = metrics();
            if (free <= 0) return;
            const rect = track.getBoundingClientRect();
            const thumbSize = horizontal ? thumb.offsetWidth : thumb.offsetHeight;
            const offset = (horizontal ? e.clientX - rect.left : e.clientY - rect.top) - thumbSize / 2;
            const clamped = Math.min(Math.max(offset, 0), free);
            setScroll((clamped / free) * max);
        };

        thumb.addEventListener('pointerdown', onDown);
        thumb.addEventListener('pointermove', onMove);
        thumb.addEventListener('pointerup', onUp);
        thumb.addEventListener('pointercancel', onUp);
        track.addEventListener('pointerdown', onTrackDown);

        thumb._sbCleanup = () => {
            thumb.removeEventListener('pointerdown', onDown);
            thumb.removeEventListener('pointermove', onMove);
            thumb.removeEventListener('pointerup', onUp);
            thumb.removeEventListener('pointercancel', onUp);
            track.removeEventListener('pointerdown', onTrackDown);
            delete thumb._sbCleanup;
        };
    },

    // only used on-demand
    detachScrollBar: thumb => {
        if (thumb && thumb._sbCleanup) thumb._sbCleanup();
    },
}