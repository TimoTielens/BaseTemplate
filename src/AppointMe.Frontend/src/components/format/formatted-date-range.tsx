import { useFormattedDateRange } from './use-formatted-date-range';
import { HTMLAttributes } from 'react';

interface FormattedDateRangeProps extends Omit<HTMLAttributes<HTMLSpanElement>, 'children'> {
    from: Date | string | number | undefined | null;
    to: Date | string | number | undefined | null;
}

export const FormattedDateRange = ({ from, to, ...rest }: FormattedDateRangeProps) => {
    const formatRange = useFormattedDateRange();

    const result = formatRange(from, to);
    if (result == null) {
        return null;
    }

    return <span {...rest}>{result}</span>;
};
