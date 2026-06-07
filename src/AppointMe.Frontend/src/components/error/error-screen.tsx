import { Button, Empty, EmptyContent, EmptyDescription, EmptyHeader, EmptyMedia, EmptyTitle } from '@/components/ui';
import { useQueryClient } from '@tanstack/react-query';
import { TriangleAlertIcon } from 'lucide-react';

export const ErrorScreen = () => {
    const queryClient = useQueryClient();

    return (
        <div className='flex min-h-screen items-center justify-center p-6'>
            <Empty className='max-w-md'>
                <EmptyHeader>
                    <EmptyMedia variant='icon'>
                        <TriangleAlertIcon />
                    </EmptyMedia>
                    <EmptyTitle>Something went wrong</EmptyTitle>
                    <EmptyDescription>
                        An unexpected error occurred. You can try again, or return to the start page.
                    </EmptyDescription>
                </EmptyHeader>
                <EmptyContent>
                    <div className='flex gap-2'>
                        <Button
                            onClick={() => {
                                queryClient.clear();
                                globalThis.location.reload();
                            }}
                        >
                            Try again
                        </Button>
                        <Button variant='outline' asChild>
                            <a href='/'>Go home</a>
                        </Button>
                    </div>
                </EmptyContent>
            </Empty>
        </div>
    );
};
