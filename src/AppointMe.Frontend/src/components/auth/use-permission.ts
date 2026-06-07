import { Permission } from '@/api/appointme.schemas';
import { useUserAccess } from '@/components/auth/user-access-context';

export const usePermission = (permission: Permission) => {
    const { permissions } = useUserAccess();
    return permissions.includes(permission);
};
