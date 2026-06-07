import { toDate } from './to-date';
import { useTimeZone } from './use-time-zone';
import { useCallback } from 'react';

const LOCALE = 'en-US';

export type DateFormat =
    // single parts
    | 'year' // "2026"
    | 'weekday' // "Wed"
    | 'weekdayLong' // "Wednesday"
    | 'monthShort' // "Mar"
    | 'time' // "2:30 PM" (locale default — 12h in en-US)
    | 'time24' // "14:30" (explicit 24h)

    // date-only
    | 'dayMonthShort' // "Mar 5"
    | 'monthShortYear' // "Mar 2026"
    | 'weekdayDayMonthShort' // "Thu, Mar 5"
    | 'dayMonthShortYear' // "Mar 5, 2026"
    | 'weekdayDayMonthShortYear' // "Thu, Mar 5, 2026"
    | 'dayMonthYear' // "March 5, 2026"
    | 'weekdayLongDayMonthYear' // "Thursday, March 5, 2026"

    // date + time
    | 'dayMonthShortYearTime24' // "Mar 5, 2026, 14:30"

    // numeric
    | 'numericDate'; // "03/05/2026"

const formatters: Record<DateFormat, (date: Date, tz: string) => string> = {
    // single parts
    year: (date, tz) => date.toLocaleDateString(LOCALE, { year: 'numeric', timeZone: tz }),
    weekday: (date, tz) => date.toLocaleDateString(LOCALE, { weekday: 'short', timeZone: tz }),
    weekdayLong: (date, tz) => date.toLocaleDateString(LOCALE, { weekday: 'long', timeZone: tz }),
    monthShort: (date, tz) => date.toLocaleDateString(LOCALE, { month: 'short', timeZone: tz }),
    time: (date, tz) => date.toLocaleTimeString(LOCALE, { hour: 'numeric', minute: '2-digit', timeZone: tz }),
    time24: (date, tz) =>
        date.toLocaleTimeString(LOCALE, {
            hour: '2-digit',
            minute: '2-digit',
            hour12: false,
            hourCycle: 'h23',
            timeZone: tz,
        }),

    // date-only
    dayMonthShort: (date, tz) => date.toLocaleDateString(LOCALE, { month: 'short', day: 'numeric', timeZone: tz }),
    monthShortYear: (date, tz) => date.toLocaleDateString(LOCALE, { month: 'short', year: 'numeric', timeZone: tz }),
    weekdayDayMonthShort: (date, tz) =>
        date.toLocaleDateString(LOCALE, { weekday: 'short', month: 'short', day: 'numeric', timeZone: tz }),
    dayMonthShortYear: (date, tz) =>
        date.toLocaleDateString(LOCALE, { month: 'short', day: 'numeric', year: 'numeric', timeZone: tz }),
    weekdayDayMonthShortYear: (date, tz) =>
        date.toLocaleDateString(LOCALE, {
            weekday: 'short',
            month: 'short',
            day: 'numeric',
            year: 'numeric',
            timeZone: tz,
        }),
    dayMonthYear: (date, tz) =>
        date.toLocaleDateString(LOCALE, { month: 'long', day: 'numeric', year: 'numeric', timeZone: tz }),
    weekdayLongDayMonthYear: (date, tz) =>
        date.toLocaleDateString(LOCALE, {
            weekday: 'long',
            month: 'long',
            day: 'numeric',
            year: 'numeric',
            timeZone: tz,
        }),

    // date + time
    dayMonthShortYearTime24: (date, tz) =>
        date.toLocaleString(LOCALE, {
            month: 'short',
            day: 'numeric',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
            hour12: false,
            hourCycle: 'h23',
            timeZone: tz,
        }),

    // numeric
    numericDate: (date, tz) =>
        date.toLocaleDateString(LOCALE, { month: '2-digit', day: '2-digit', year: 'numeric', timeZone: tz }),
};

type FormatDateFn = {
    (date: Date | string | number, format?: DateFormat): string;
    (date: undefined | null, format?: DateFormat): undefined;
    (date: Date | string | number | undefined | null, format?: DateFormat): string | undefined;
};

/**
 * Returns a memoised, timezone-aware single-date formatter bound to the current
 * company's IANA timezone.
 *
 * For date ranges use {@link useFormattedDateRange}.
 *
 * Format names spell out the parts that appear in the output (e.g.
 * `weekdayDayMonthShort` → "Wed, Mar 5"). Modifiers: `monthShort` vs implicit
 * full month, `weekday` (abbrev) vs `weekdayLong`, `time` (locale default) vs
 * `time24`.
 *
 * @example
 *   const formatDate = useFormattedDate();
 *   formatDate(appointment.start)                          // "Mar 5, 2026"  (default: 'dayMonthShortYear')
 *   formatDate(appointment.start, 'time24')                // "14:30"
 *   formatDate(appointment.start, 'weekdayDayMonthShort')  // "Wed, Mar 5"
 */
export const useFormattedDate = (): FormatDateFn => {
    const timeZone = useTimeZone();

    return useCallback(
        (
            date: Date | string | number | undefined | null,
            format: DateFormat = 'dayMonthShortYear',
        ): string | undefined => {
            if (date == null) return undefined;
            return formatters[format](toDate(date), timeZone);
        },
        [timeZone],
    ) as FormatDateFn;
};
