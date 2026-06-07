import { MutationCache, QueryCache, QueryClient } from '@tanstack/react-query';
import { isAxiosError } from 'axios';
import { toast } from 'sonner';

// Network errors and 5xx responses get the global toast — they're systemic and
// the message is always "something on our side broke". 4xx responses are
// contextual (validation, not-found, conflict, …) and stay the call site's job.
const isServerError = (error: unknown): boolean => {
    if (!isAxiosError(error)) {
        return true;
    }
    const status = error.response?.status;
    return status === undefined || status >= 500;
};

// Backend returns ASP.NET ProblemDetails; ValidationException puts the actual
// message in extensions.error, so check that before falling back to detail/title.
const extractErrorMessage = (error: unknown): string => {
    if (isAxiosError(error)) {
        const data = error.response?.data;
        return data?.error ?? data?.detail ?? data?.title ?? error.message;
    }
    return error instanceof Error ? error.message : 'Something went wrong';
};

export const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            refetchOnWindowFocus: false,
            refetchOnReconnect: false,
        },
    },
    queryCache: new QueryCache({
        onError: error => {
            if (isServerError(error)) {
                toast.error(extractErrorMessage(error));
            }
        },
    }),
    mutationCache: new MutationCache({
        onError: error => {
            if (isServerError(error)) {
                toast.error(extractErrorMessage(error));
            }
        },
    }),
});
