import { STORAGE_KEYS } from '@/lib/storage-keys.ts';
import { ReactNode, createContext, use, useCallback, useEffect, useMemo, useState } from 'react';

export type Theme = 'dark' | 'light';

const isTheme = (value: unknown): value is Theme => value === 'light' || value === 'dark';

type ThemeProviderProps = {
    children: ReactNode;
    defaultTheme?: Theme;
};

type ThemeProviderState = {
    theme: Theme;
    setTheme: (theme: Theme) => void;
};

const ThemeProviderContext = createContext<ThemeProviderState | null>(null);

export function ThemeProvider({ children, defaultTheme = 'light', ...props }: ThemeProviderProps) {
    const [theme, setTheme] = useState<Theme>(() => {
        const stored = globalThis.localStorage.getItem(STORAGE_KEYS.theme);
        return isTheme(stored) ? stored : defaultTheme;
    });

    useEffect(() => {
        const root = globalThis.document.documentElement;

        root.classList.remove('light', 'dark');
        root.classList.add(theme);
    }, [theme]);

    const setThemePersisted = useCallback((theme: Theme) => {
        localStorage.setItem(STORAGE_KEYS.theme, theme);
        setTheme(theme);
    }, []);

    const value = useMemo(
        () => ({
            theme,
            setTheme: setThemePersisted,
        }),
        [theme, setThemePersisted],
    );

    return (
        <ThemeProviderContext.Provider {...props} value={value}>
            {children}
        </ThemeProviderContext.Provider>
    );
}

export const useTheme = () => {
    const context = use(ThemeProviderContext);
    if (!context) {
        throw new Error('useTheme must be used within a ThemeProvider');
    }

    return context;
};
