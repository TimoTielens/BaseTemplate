import { STORAGE_KEYS } from '@/lib/storage-keys';
import { useSyncExternalStore } from 'react';

type Listener = () => void;

function createCurrentCompanyStore() {
    let companyId: string | null = localStorage.getItem(STORAGE_KEYS.currentCompanyId);

    const listeners = new Set<Listener>();

    const get = () => companyId;

    const set = (id: string | null) => {
        if (companyId === id) {
            return;
        }

        companyId = id;
        if (companyId) {
            localStorage.setItem(STORAGE_KEYS.currentCompanyId, companyId);
        } else {
            localStorage.removeItem(STORAGE_KEYS.currentCompanyId);
        }

        listeners.forEach(listener => listener());
    };

    const subscribe = (listener: Listener) => {
        listeners.add(listener);
        return () => listeners.delete(listener);
    };

    return { get, set, subscribe };
}

export const currentCompanyStore = createCurrentCompanyStore();

export const useCurrentCompanyId = () => {
    return useSyncExternalStore(currentCompanyStore.subscribe, currentCompanyStore.get);
};
