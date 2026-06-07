import { ConfirmDialog, IModalProps } from '@/components/modal-dialog';

export const CancelAppointmentDialog = ({ resolve, visible }: IModalProps<boolean>) => {
    return (
        <ConfirmDialog
            resolve={resolve}
            visible={visible}
            title='Cancel appointment'
            description='This appointment will be cancelled. This action cannot be undone.'
            confirmLabel='Cancel appointment'
        />
    );
};
