import { AppProviders } from '@/app/providers';
import { Outlet } from 'react-router-dom';

export const RootLayout = () => {
    return (
        <AppProviders>
            <Outlet />
        </AppProviders>
    );
};
