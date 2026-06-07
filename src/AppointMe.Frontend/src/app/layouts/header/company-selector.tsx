import { useCurrentCompany, useCurrentUser } from '@/components/auth';
import {
    Button,
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
    Popover,
    PopoverContent,
    PopoverTrigger,
} from '@/components/ui';
import { cn } from '@/lib/utils';
import { CheckIcon, ChevronsUpDownIcon } from 'lucide-react';
import { useState } from 'react';

export const CompanySelector = () => {
    const { companies } = useCurrentUser();
    const { currentCompany, setCurrentCompany } = useCurrentCompany();
    const [open, setOpen] = useState(false);

    return (
        <Popover open={open} onOpenChange={setOpen}>
            <PopoverTrigger asChild>
                <Button variant='outline' size='sm' className='gap-2'>
                    <span className='truncate'>{currentCompany.companyName}</span>
                    <ChevronsUpDownIcon className='text-muted-foreground size-4' />
                </Button>
            </PopoverTrigger>
            <PopoverContent align='end' className='w-56 p-0'>
                <Command>
                    <CommandInput placeholder='Search companies...' />
                    <CommandList>
                        <CommandEmpty>No companies found.</CommandEmpty>
                        <CommandGroup>
                            {companies?.map(company => (
                                <CommandItem
                                    key={company.companyId}
                                    value={company.companyName}
                                    onSelect={async () => {
                                        await setCurrentCompany(company.companyId);
                                        setOpen(false);
                                    }}
                                >
                                    {company.companyName}
                                    <CheckIcon
                                        className={cn(
                                            'ml-auto size-4',
                                            company.companyId === currentCompany.companyId
                                                ? 'opacity-100'
                                                : 'opacity-0',
                                        )}
                                    />
                                </CommandItem>
                            ))}
                        </CommandGroup>
                    </CommandList>
                </Command>
            </PopoverContent>
        </Popover>
    );
};
