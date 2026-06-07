import { Temporal } from 'temporal-polyfill';

const toInstant = (value: Date | string | number): Temporal.Instant => {
    const epochMs = value instanceof Date ? value.getTime() : new Date(value).getTime();
    return Temporal.Instant.fromEpochMilliseconds(epochMs);
};

/** Format a Date/ISO/timestamp for <input type="date"> (YYYY-MM-DD) in the given IANA timezone. */
export const toDateInputValue = (value: Date | string | number | null | undefined, timeZone: string): string => {
    if (value == null) {
        return '';
    }
    return toInstant(value).toZonedDateTimeISO(timeZone).toPlainDate().toString();
};

/** Format a Date/ISO/timestamp for <input type="time"> (24h HH:mm) in the given IANA timezone. */
export const toTimeInputValue = (value: Date | string | number | null | undefined, timeZone: string): string => {
    if (value == null) {
        return '';
    }
    return toInstant(value).toZonedDateTimeISO(timeZone).toPlainTime().toString({ smallestUnit: 'minute' });
};

/**
 * Combine "yyyy-MM-dd" + "HH:mm" picked by the user (interpreted as the
 * company-local wall clock) into a UTC ISO string for the API.
 *
 * Temporal's default disambiguation ('compatible') handles DST: non-existent
 * times during spring-forward shift to the "after" branch; ambiguous fall-back
 * times pick the first occurrence.
 */
export const parseInputDateTime = (date: string, time: string, timeZone: string): string => {
    return Temporal.PlainDateTime.from(`${date}T${time}`).toZonedDateTime(timeZone).toInstant().toString();
};
