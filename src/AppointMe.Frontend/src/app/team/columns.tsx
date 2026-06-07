import { EditEmployeeRolesDialog } from './edit-employee-roles-dialog';
import { TeamMemberDto, TeamMemberType } from '@/api/appointme.schemas.ts';
import { getGetTeamQueryKey, useCancelInvitation, useDeleteEmployee, useResendInvitation } from '@/api/appointme.ts';
import { usePermission } from '@/components/auth';
import { Can } from '@/components/auth/can';
import { ConfirmDialog, useModalDialog } from '@/components/modal-dialog';
import {
    Avatar,
    AvatarFallback,
    Badge,
    Button,
    Checkbox,
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
    Tooltip,
    TooltipContent,
    TooltipTrigger,
} from '@/components/ui';
import { useQueryClient } from '@tanstack/react-query';
import { ColumnDef } from '@tanstack/react-table';
import { EllipsisVerticalIcon } from 'lucide-react';
import { toast } from 'sonner';

const ActionsCell = ({ row }: { row: { original: TeamMemberDto } }) => {
    const modalDialog = useModalDialog();
    const queryClient = useQueryClient();

    const canUpdateRoles = usePermission('employees:update_roles');
    const canResend = usePermission('invitations:resend');
    const canRemoveEmployee = usePermission('employees:remove');
    const canCancelInvitation = usePermission('invitations:cancel');

    const { mutateAsync: cancelInvitation } = useCancelInvitation({
        mutation: { onSuccess: () => queryClient.invalidateQueries({ queryKey: getGetTeamQueryKey() }) },
    });
    const { mutateAsync: resendInvitation } = useResendInvitation({
        mutation: { onSuccess: () => queryClient.invalidateQueries({ queryKey: getGetTeamQueryKey() }) },
    });
    const { mutateAsync: deleteEmployee } = useDeleteEmployee({
        mutation: { onSuccess: () => queryClient.invalidateQueries({ queryKey: getGetTeamQueryKey() }) },
    });

    const isInvitation = row.original.type === TeamMemberType.Invitation;
    const canRemove = isInvitation ? canCancelInvitation : canRemoveEmployee;
    const hasAnyAction = canUpdateRoles || (isInvitation && canResend) || canRemove;

    if (!hasAnyAction) return null;

    const handleResendInvitation = async () => {
        await resendInvitation({ id: row.original.id });
        toast.success(`Invitation resent to ${row.original.email}.`);
    };

    const handleCancelInvitation = async () => {
        const confirmed = await modalDialog.open<boolean>(props => (
            <ConfirmDialog
                {...props}
                title='Cancel invitation'
                description='The recipient will no longer be able to use this invitation to join your company.'
                confirmLabel='Cancel invitation'
                cancelLabel='Keep'
            />
        ));
        if (confirmed) {
            await cancelInvitation({ id: row.original.id });
            toast.success(`Invitation for ${row.original.email} has been canceled.`);
        }
    };

    const handleRemoveEmployee = async () => {
        const confirmed = await modalDialog.open<boolean>(props => (
            <ConfirmDialog
                {...props}
                title='Remove employee'
                description={`${row.original.fullName ?? row.original.email} will no longer be able to access this company.`}
                confirmLabel='Remove'
            />
        ));
        if (confirmed) {
            await deleteEmployee({ id: row.original.id });
            toast.success(`${row.original.fullName ?? row.original.email} has been removed.`);
        }
    };

    const showTopSection = canUpdateRoles || (isInvitation && canResend);
    const showSeparator = showTopSection && canRemove;

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
            <DropdownMenuContent align='end' className='w-56'>
                <Can permission='employees:update_roles'>
                    <Tooltip>
                        <TooltipTrigger asChild>
                            <span className='inline-block w-full'>
                                <DropdownMenuItem
                                    disabled={isInvitation}
                                    onClick={async () => {
                                        await modalDialog.open(props => (
                                            <EditEmployeeRolesDialog {...props} employee={row.original} />
                                        ));
                                    }}
                                >
                                    Manage roles
                                </DropdownMenuItem>
                            </span>
                        </TooltipTrigger>
                        {isInvitation && (
                            <TooltipContent side='left' className='max-w-56'>
                                You can't manage roles for invitations. Cancel this invitation and send a new one with
                                the desired roles.
                            </TooltipContent>
                        )}
                    </Tooltip>
                </Can>

                {isInvitation && (
                    <Can permission='invitations:resend'>
                        <DropdownMenuItem onClick={handleResendInvitation}>Resend invitation</DropdownMenuItem>
                    </Can>
                )}

                {showSeparator && <DropdownMenuSeparator />}

                {canRemove && (
                    <Tooltip>
                        <TooltipTrigger asChild>
                            <span className='inline-block w-full'>
                                <DropdownMenuItem
                                    variant='destructive'
                                    disabled={row.original.isCurrentUser}
                                    onClick={async () => {
                                        await (isInvitation ? handleCancelInvitation() : handleRemoveEmployee());
                                    }}
                                >
                                    {isInvitation ? 'Cancel invitation' : 'Remove'}
                                </DropdownMenuItem>
                            </span>
                        </TooltipTrigger>
                        {row.original.isCurrentUser && <TooltipContent>You can't remove yourself</TooltipContent>}
                    </Tooltip>
                )}
            </DropdownMenuContent>
        </DropdownMenu>
    );
};

export const Columns: ColumnDef<TeamMemberDto>[] = [
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
            <div className='flex flex-wrap items-center gap-2'>
                {row.original.type === TeamMemberType.Employee && (
                    <>
                        <Avatar>
                            <AvatarFallback>{row.original.initials}</AvatarFallback>
                        </Avatar>
                        <div className='flex flex-col'>
                            <span>{row.getValue('fullName')}</span>
                            <span className='text-muted-foreground text-xs break-all md:hidden'>
                                {row.original.email}
                            </span>
                        </div>
                    </>
                )}
                {row.original.type === TeamMemberType.Invitation && (
                    <div className='flex flex-col'>
                        <Badge variant='outline'>Pending invite</Badge>
                        <span className='text-muted-foreground mt-1 text-xs break-all md:hidden'>
                            {row.original.email}
                        </span>
                    </div>
                )}
                {row.original.isCurrentUser && <Badge variant='outline'>You</Badge>}
                {row.original.isPrimaryOwner && <Badge variant='outline'>Owner</Badge>}
            </div>
        ),
        enableHiding: false,
        meta: {
            className: 'whitespace-normal md:whitespace-nowrap',
        },
    },
    {
        accessorKey: 'email',
        header: 'Email',
        cell: ({ row }) => <div>{row.getValue('email')}</div>,
        meta: {
            className: 'hidden md:table-cell',
        },
    },
    {
        accessorKey: 'roles',
        header: 'Roles',
        cell: ({ row }) => (
            <div className='flex gap-1'>
                {row.original.roles.map(role => (
                    <Badge key={role} variant='secondary'>
                        {role}
                    </Badge>
                ))}
            </div>
        ),
        enableSorting: false,
        meta: {
            className: 'hidden lg:table-cell',
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
