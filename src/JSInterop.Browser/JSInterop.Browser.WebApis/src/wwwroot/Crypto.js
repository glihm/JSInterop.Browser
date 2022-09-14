'use strict';


function getRandomValuesFromCount(byteCount) {
    try {
        const buf = new Uint8Array(byteCount);
        window.crypto.getRandomValues(buf);
        return buf;
    }
    catch (e) {
        console.error(e);
        return null;
    }
}


export { getRandomValuesFromCount }
