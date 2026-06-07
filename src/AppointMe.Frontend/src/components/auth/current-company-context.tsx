import { useCurrentUser } from './current-user-context';
import { CompanyMembership } from '@/api/appointme.schemas';
import { currentCompanyStore, useCurrentCompanyId } from '@/components/auth';
import { useQueryClient } from '@tanstack/react-query';
import { type ReactNode, createContext, use, useCallback, useEffect, useMemo } from 'react';

interface CurrentCompanyContextValue {
    currentCompany: CompanyMembership;
    setCurrentCompany: (companyId: string) => Promise<void>;
}

const CurrentCompanyContext = createContext<CurrentCompanyContextValue | null>(null);

export const CurrentCompanyProvider = ({ children }: { children: ReactNode }) => {
    const queryClient = useQueryClient();
    const { companies } = useCurrentUser();
    const currentCompanyId = useCurrentCompanyId();

    const currentCompany = useMemo(() => {
        return companies?.find(company => company.companyId === currentCompanyId) || companies?.[0];
    }, [companies, currentCompanyId]);

    useEffect(() => {
        if (!companies?.length) {
            return;
        }

        const isValidCompanyId =
            currentCompanyId != null && companies.some(company => company.companyId === currentCompanyId);
        if (!isValidCompanyId) {
            currentCompanyStore.set(companies[0].companyId);
        }
    }, [companies, currentCompanyId, currentCompany]);

    const setCurrentCompany = useCallback(
        async (companyId: string) => {
            if (currentCompanyId === companyId) {
                return;
            }

            currentCompanyStore.set(companyId);
            await queryClient.invalidateQueries();
        },
        [currentCompanyId, queryClient],
    );

    if (!currentCompany) {
        return null;
    }

    return (
        <CurrentCompanyContext.Provider value={{ currentCompany, setCurrentCompany }}>
            {children}
        </CurrentCompanyContext.Provider>
    );
};

export const useCurrentCompany = () => {
    const context = use(CurrentCompanyContext);
    if (context === null) {
        throw new Error('useCurrentCompany must be used within a CurrentCompanyProvider');
    }
    return context;
};
