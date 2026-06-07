export type ViewKey = 'day' | 'week' | 'month';

export const VIEW_MAP: Record<ViewKey, string> = {
    day: 'timeGridDay',
    week: 'timeGridWeek',
    month: 'dayGridMonth',
};
