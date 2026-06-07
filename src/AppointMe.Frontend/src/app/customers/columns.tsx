import { EditCustomerDialog } from './edit-customer-dialog';
import { CustomerDto } from '@/api/appointme.schemas';
import { getGetCustomersQueryKey, useDeleteCustomer } from '@/api/appointme.ts';
import { Can } from '@/components/auth/can';
import { FormattedDate } from '@/components/format';
import { ConfirmDialog, useModalDialog } from '@/components/modal-dialog';
import {
    Avatar,
    AvatarFallback,
    Button,
    Checkbox,
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from '@/components/ui';
import { useQueryClient } from '@tanstack/react-query';
import { ColumnDef } from '@tanstack/react-table';
import { EllipsisVerticalIcon } from 'lucide-react';
import { toast } from 'sonner';

const ActionsCell = ({ row }: { row: { original: CustomerDto } }) => {
    const modalDialog = useModalDialog();
    const queryClient = useQueryClient();

    const { mutateAsync: deleteCustomer } = useDeleteCustomer({
        mutation: { onSuccess: () => queryClient.invalidateQueries({ queryKey: getGetCustomersQueryKey() }) },
    });

    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button
                    variant='ghost'
                    className='data-[state=open]:bg-muted text-muted-foreground flex size-8 cursor-pointer'
                    size='icon'
                >
                    <EllipsisVerticalIcon />
                    <span className='sr-only'>Open menu</span>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align='end' className='w-32'>
                <Can permission='customers:update'>
                    <DropdownMenuItem
                        onClick={async event => {
                            event.stopPropagation();
                            await modalDialog.open(props => (
                                <EditCustomerDialog {...props} customerId={row.original.id} />
                            ));
                        }}
                    >
                        Edit
                    </DropdownMenuItem>
                </Can>
                <Can permission='customers:delete'>
                    <DropdownMenuSeparator />
                    <DropdownMenuItem
                        variant='destructive'
                        onClick={async event => {
                            event.stopPropagation();
                            const confirmed = await modalDialog.open<boolean>(props => (
                                <ConfirmDialog
                                    {...props}
                                    title='Delete customer'
                                    description={`${row.original.fullName} will be permanently deleted and cannot be recovered.`}
                                    confirmLabel='Delete'
                                />
                            ));
                            if (confirmed) {
                                await deleteCustomer({ id: row.original.id });
                                toast.success(`${row.original.fullName} has been deleted.`);
                            }
                        }}
                    >
                        Delete
                    </DropdownMenuItem>
                </Can>
            </DropdownMenuContent>
        </DropdownMenu>
    );
};

export const Columns: ColumnDef<CustomerDto>[] = [
    {
        id: 'select',
        header: ({ table }) => (
            <Checkbox
                checked={table.getIsAllPageRowsSelected() || (table.getIsSomePageRowsSelected() && 'indeterminate')}
                onCheckedChange={value => table.toggleAllPageRowsSelected(!!value)}
                aria-label='Select all'
            />
        ),
        cell: ({ row }) => (
            <Checkbox
                checked={row.getIsSelected()}
                disabled={!row.getCanSelect()}
                onCheckedChange={value => row.toggleSelected(!!value)}
                aria-label='Select row'
                onClick={e => e.stopPropagation()}
            />
        ),
        enableSorting: false,
        enableHiding: false,
        meta: {
            className: 'w-0',
        },
    },
    {
        accessorKey: 'fullName',
        header: 'Name',
        cell: ({ row }) => (
            <div className='flex items-center gap-2'>
                <Avatar>
                    <AvatarFallback>{row.original.initials}</AvatarFallback>
                </Avatar>
                <div className='flex flex-col'>
                    <span>{row.getValue('fullName')}</span>
                    {row.original.email && (
                        <span className='text-muted-foreground text-xs break-all lowercase md:hidden'>
                            {row.original.email}
                        </span>
                    )}
                </div>
            </div>
        ),
        enableHiding: false,
        meta: {
            className: 'whitespace-normal md:whitespace-nowrap',
        },
    },
    {
        accessorKey: 'dateOfBirth',
        header: 'Date of Birth',
        cell: ({ row }) =>
            row.original.dateOfBirth ? (
                <FormattedDate date={row.getValue('dateOfBirth')} format='dayMonthShortYear' />
            ) : null,
        meta: {
            className: 'hidden lg:table-cell',
        },
    },
    {
        accessorKey: 'email',
        header: 'Email',
        cell: ({ row }) => <div className='lowercase'>{row.getValue('email')}</div>,
        meta: {
            className: 'hidden md:table-cell',
        },
    },
    {
        id: 'actions',
        enableResizing: false,
        enableSorting: false,
        enableHiding: false,
        meta: {
            className: 'w-0',
        },
        cell: ({ row }) => <ActionsCell row={row} />,
    },
];
