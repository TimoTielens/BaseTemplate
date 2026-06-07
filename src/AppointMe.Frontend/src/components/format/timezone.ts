const getGmtOffsetLabel = (timeZone: string, date = new Date()): string => {
    const parts = new Intl.DateTimeFormat('en-US', {
        timeZone,
        hour12: false,
        timeZoneName: 'shortOffset',
    }).formatToParts(date);

    const offsetPart = parts.find(p => p.type === 'timeZoneName')?.value;

    // Example values: "GMT+1", "GMT-5"
    if (!offsetPart || offsetPart === 'GMT') {
        return '(GMT+00:00)';
    }

    const match = offsetPart.match(/GMT([+-]\d+)/);
    if (!match) {
        return '(GMT+00:00)';
    }

    const hours = Number(match[1]);
    const sign = hours >= 0 ? '+' : '-';
    const absHours = Math.abs(hours).toString().padStart(2, '0');

    return `(GMT${sign}${absHours}:00)`;
};

export const formatTimeZoneLabel = (timeZone: string): string => {
    return `${getGmtOffsetLabel(timeZone)} ${timeZone}`;
};
