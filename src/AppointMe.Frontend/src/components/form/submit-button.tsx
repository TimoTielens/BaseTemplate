import { Button, Spinner } from '@/components/ui';
import { ComponentProps } from 'react';

interface SubmitButtonProps extends ComponentProps<typeof Button> {
    isSubmitting: boolean;
    submittingLabel: string;
}

export const SubmitButton = ({ isSubmitting, submittingLabel, disabled, children, ...props }: SubmitButtonProps) => (
    <Button type='submit' disabled={disabled || isSubmitting} {...props}>
        {isSubmitting ? (
            <>
                <Spinner />
                {submittingLabel}
            </>
        ) : (
            children
        )}
    </Button>
);
