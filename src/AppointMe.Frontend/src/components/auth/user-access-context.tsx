import { useGetCurrentUserAccessSuspense } from '@/api/appointme';
import { GetCurrentUserAccessResponse } from '@/api/appointme.schemas';
import { type ReactNode, createContext, use } from 'react';

const UserAccessContext = createContext<GetCurrentUserAccessResponse | null>(null);

export const UserAccessProvider = ({ children }: { children: ReactNode }) => {
    const { data } = useGetCurrentUserAccessSuspense();
    return <UserAccessContext value={data}>{children}</UserAccessContext>;
};

export const useUserAccess = () => {
    const context = use(UserAccessContext);
    if (context === null) {
        throw new Error('useUserAccess must be used within a UserAccessProvider');
    }

    return context;
};
