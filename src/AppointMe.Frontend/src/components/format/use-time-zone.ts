import { useCurrentCompany } from '@/components/auth/current-company-context';

export const useTimeZone = (): string => {
    const { currentCompany } = useCurrentCompany();
    return currentCompany.timeZone;
};
