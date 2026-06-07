import { ViewKey } from './calendar-config';
import { Can } from '@/components/auth/can';
import { FormattedDateRange } from '@/components/format';
import { Button, Tabs, TabsList, TabsTrigger } from '@/components/ui';
import { ChevronLeftIcon, ChevronRightIcon, PlusIcon } from 'lucide-react';

interface CalendarToolbarProps {
    initialView: ViewKey;
    range: { start: Date | string | number; end: Date | string | number } | null;
    onToday: () => void;
    onPrev: () => void;
    onNext: () => void;
    onViewChange: (next: ViewKey) => void;
    onNew: () => void;
}

export const CalendarToolbar = ({
    initialView,
    range,
    onToday,
    onPrev,
    onNext,
    onViewChange,
    onNew,
}: CalendarToolbarProps) => (
    <div className='border-border bg-background -mx-4 -mt-4 flex h-13 shrink-0 items-center gap-3 border-b px-5'>
        <Button variant='outline' size='sm' onClick={onToday}>
            Today
        </Button>
        <div className='flex'>
            <Button variant='outline' size='sm' className='rounded-r-none' onClick={onPrev} aria-label='Previous'>
                <ChevronLeftIcon />
            </Button>
            <Button variant='outline' size='sm' className='-ml-px rounded-l-none' onClick={onNext} aria-label='Next'>
                <ChevronRightIcon />
            </Button>
        </div>
        <FormattedDateRange
            from={range?.start}
            to={range?.end}
            className='ml-1 hidden text-[15px] font-medium sm:inline'
        />
        <div className='flex-1' />
        <Tabs defaultValue={initialView}>
            <TabsList>
                {(['day', 'week', 'month'] as const).map(view => (
                    <TabsTrigger key={view} value={view} className='capitalize' onClick={() => onViewChange(view)}>
                        {view}
                    </TabsTrigger>
                ))}
            </TabsList>
        </Tabs>
        <Can permission='appointments:schedule'>
            <Button size='sm' onClick={onNew} aria-label='New appointment'>
                <PlusIcon />
                <span className='hidden sm:inline'>New appointment</span>
            </Button>
        </Can>
    </div>
);
