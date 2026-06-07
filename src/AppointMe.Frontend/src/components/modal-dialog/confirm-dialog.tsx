import { IModalProps } from './context';
import { Button } from '@/components/ui';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from '@/components/ui/dialog';
import { ReactNode } from 'react';

interface ConfirmDialogProps extends IModalProps<boolean> {
    title: string;
    description?: string;
    confirmLabel?: string;
    cancelLabel?: string;
    children?: ReactNode;
}

export const ConfirmDialog = ({
    resolve,
    visible,
    title,
    description,
    confirmLabel = 'Confirm',
    cancelLabel = 'Cancel',
    children,
}: ConfirmDialogProps) => (
    <Dialog open={visible} onOpenChange={open => !open && resolve(false)}>
        <DialogTrigger asChild>
            <button type='button' aria-hidden tabIndex={-1} className='hidden' />
        </DialogTrigger>
        <DialogContent>
            <DialogHeader>
                <DialogTitle>{title}</DialogTitle>
                {description && <DialogDescription>{description}</DialogDescription>}
            </DialogHeader>
            {children}
            <DialogFooter>
                <Button variant='outline' onClick={() => resolve(false)}>
                    {cancelLabel}
                </Button>
                <Button variant='destructive' onClick={() => resolve(true)}>
                    {confirmLabel}
                </Button>
            </DialogFooter>
        </DialogContent>
    </Dialog>
);
