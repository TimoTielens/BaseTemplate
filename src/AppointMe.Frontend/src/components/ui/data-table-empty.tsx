import { TableCell, TableRow } from '@/components/ui/table.tsx';

interface DataTableEmptyProps {
    colSpan: number;
    message?: string;
}

export function DataTableEmpty({ colSpan, message = 'No results.' }: DataTableEmptyProps) {
    return (
        <TableRow>
            <TableCell colSpan={Math.max(colSpan, 1)} className='text-muted-foreground h-24 text-center'>
                {message}
            </TableCell>
        </TableRow>
    );
}
