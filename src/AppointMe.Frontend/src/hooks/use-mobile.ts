import { useEffect, useState } from 'react';

const MOBILE_BREAKPOINT = 768;

export function useIsMobile() {
    const [isMobile, setIsMobile] = useState(() => globalThis.innerWidth < MOBILE_BREAKPOINT);

    useEffect(() => {
        const mediaQueryList = globalThis.matchMedia(`(max-width: ${MOBILE_BREAKPOINT - 1}px)`);
        const onChange = () => {
            setIsMobile(globalThis.innerWidth < MOBILE_BREAKPOINT);
        };
        mediaQueryList.addEventListener('change', onChange);
        return () => mediaQueryList.removeEventListener('change', onChange);
    }, []);

    return isMobile;
}
