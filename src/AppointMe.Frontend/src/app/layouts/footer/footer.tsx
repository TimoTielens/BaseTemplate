import { FormattedDate } from '@/components/format';

export const Footer = () => {
    return (
        <footer className='p-4 text-center text-xs text-gray-500'>
            © <FormattedDate date={new Date()} format='year' />{' '}
            <span className='font-extralight'>
                Appoint<span className='font-semibold'>Me</span>
            </span>
        </footer>
    );
};
