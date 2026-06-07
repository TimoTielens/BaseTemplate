import { ComponentProps } from 'react';

export const AppLogo = (props: ComponentProps<'svg'>) => (
    <svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' aria-hidden='true' {...props}>
        <rect width='24' height='24' rx='6.5' fill='currentColor' />
        <path
            className='fill-background'
            fillRule='evenodd'
            clipRule='evenodd'
            d='M12 6 18 18 15.5 18 14 15 10 15 8.5 18 6 18ZM11 13 13 13 12 11Z'
        />
    </svg>
);
