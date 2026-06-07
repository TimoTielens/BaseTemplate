import { Button, Empty, EmptyDescription, EmptyHeader, EmptyMedia, EmptyTitle } from '@/components/ui';
import { TriangleAlertIcon } from 'lucide-react';

interface ErrorWidgetProps {
    title?: string;
    description?: string;
    onRetry?: () => void;
}

export const ErrorWidget = ({
    title = 'Unable to load',
    description = 'Something went wrong. Please try again later.',
    onRetry,
}: ErrorWidgetProps) => {
    return (
        <Empty>
            <EmptyHeader>
                <EmptyMedia variant='icon'>
                    <TriangleAlertIcon />
                </EmptyMedia>
                <EmptyTitle>{title}</EmptyTitle>
                <EmptyDescription>{description}</EmptyDescription>
            </EmptyHeader>
            {onRetry && (
                <Button variant='outline' size='sm' onClick={onRetry}>
                    Retry
                </Button>
            )}
        </Empty>
    );
};
