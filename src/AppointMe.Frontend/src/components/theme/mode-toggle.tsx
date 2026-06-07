import { useTheme } from '@/components/theme';
import { Button, Kbd, Tooltip, TooltipContent, TooltipTrigger } from '@/components/ui';
import { useHotkey } from '@tanstack/react-hotkeys';
import { Moon, Sun } from 'lucide-react';
import { useCallback } from 'react';

export const ModeToggle = () => {
    const { theme, setTheme } = useTheme();

    const toggleTheme = useCallback(() => {
        setTheme(theme === 'light' ? 'dark' : 'light');
    }, [theme, setTheme]);

    useHotkey('D', toggleTheme, {
        preventDefault: true,
    });

    return (
        <Tooltip>
            <TooltipTrigger asChild>
                <Button variant='outline' size='icon-sm' onClick={toggleTheme}>
                    <Sun className='h-[1.2rem] w-[1.2rem] scale-100 rotate-0 transition-all dark:scale-0 dark:-rotate-90' />
                    <Moon className='absolute h-[1.2rem] w-[1.2rem] scale-0 rotate-90 transition-all dark:scale-100 dark:rotate-0' />
                    <span className='sr-only'>Toggle theme</span>
                </Button>
            </TooltipTrigger>
            <TooltipContent side='bottom' className='flex items-center gap-2'>
                Toggle theme
                <Kbd>D</Kbd>
            </TooltipContent>
        </Tooltip>
    );
};
