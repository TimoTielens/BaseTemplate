import { toDate } from './to-date';
import { DateFormat, useFormattedDate } from './use-formatted-date';
import { HTMLAttributes } from 'react';

interface FormattedDateProps extends Omit<HTMLAttributes<HTMLTimeElement>, 'children' | 'dateTime'> {
    date: Date | string | number | undefined | null;
    format?: DateFormat;
}

export const FormattedDate = ({ date, format = 'dayMonthShortYear', ...rest }: FormattedDateProps) => {
    const formatDate = useFormattedDate();

    if (date == null) {
        return null;
    }

    const value = toDate(date);
    if (Number.isNaN(value.getTime())) {
        return null;
    }

    const result = formatDate(value, format);
    if (result == null) {
        return null;
    }

    return (
        <time dateTime={value.toISOString()} {...rest}>
            {result}
        </time>
    );
};
