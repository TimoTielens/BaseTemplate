import { toDate } from './to-date';
import { useTimeZone } from './use-time-zone';
import { useCallback } from 'react';

const LOCALE = 'en-US';

/**
 * Render a date range as a single locale-aware string. The end is treated as
 * exclusive (end − 1 ms inclusive) so a half-open range like
 * [Mar 5 00:00, Mar 9 00:00) prints as "Mar 5 – 8, 2026".
 */
const formatDateRange = (start: Date, end: Date, tz: string): string => {
    const inclusiveEnd = new Date(end.getTime() - 1);
    const sameMonth = start.getMonth() === inclusiveEnd.getMonth();
    const sameYear = start.getFullYear() === inclusiveEnd.getFullYear();

    if (sameMonth && sameYear) {
        const startDay = start.toLocaleDateString(LOCALE, { day: 'numeric', timeZone: tz });
        const endDay = inclusiveEnd.toLocaleDateString(LOCALE, { day: 'numeric', timeZone: tz });
        const month = start.toLocaleDateString(LOCALE, { month: 'short', timeZone: tz });
        const year = start.toLocaleDateString(LOCALE, { year: 'numeric', timeZone: tz });
        return `${month} ${startDay} – ${endDay}, ${year}`;
    }
    if (sameYear) {
        const startStr = start.toLocaleDateString(LOCALE, { month: 'short', day: 'numeric', timeZone: tz });
        const endStr = inclusiveEnd.toLocaleDateString(LOCALE, {
            month: 'short',
            day: 'numeric',
            year: 'numeric',
            timeZone: tz,
        });
        return `${startStr} – ${endStr}`;
    }
    const options: Intl.DateTimeFormatOptions = { month: 'short', day: 'numeric', year: 'numeric', timeZone: tz };
    return `${start.toLocaleDateString(LOCALE, options)} – ${inclusiveEnd.toLocaleDateString(LOCALE, options)}`;
};

type FormatDateRangeFn = {
    (from: Date | string | number, to: Date | string | number): string;
    (from: undefined | null, to: Date | string | number | undefined | null): undefined;
    (from: Date | string | number | undefined | null, to: undefined | null): undefined;
    (
        from: Date | string | number | undefined | null,
        to: Date | string | number | undefined | null,
    ): string | undefined;
};

/**
 * Returns a memoised, timezone-aware date-range formatter bound to the current
 * company's IANA timezone.
 *
 * @example
 *   const formatDateRange = useFormattedDateRange();
 *   formatDateRange(range.from, range.to)   // "Mar 5 – 8, 2026"
 */
export const useFormattedDateRange = (): FormatDateRangeFn => {
    const timeZone = useTimeZone();

    return useCallback(
        (
            from: Date | string | number | undefined | null,
            to: Date | string | number | undefined | null,
        ): string | undefined => {
            if (from == null || to == null) return undefined;
            return formatDateRange(toDate(from), toDate(to), timeZone);
        },
        [timeZone],
    ) as FormatDateRangeFn;
};
