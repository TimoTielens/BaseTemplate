import { Button, Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui';
import { Table } from '@tanstack/react-table';
import { ChevronLeft, ChevronRight, ChevronsLeft, ChevronsRight } from 'lucide-react';

interface DataTablePaginationProps<TData> {
    table: Table<TData>;
}

export function DataTablePagination<TData>({ table }: DataTablePaginationProps<TData>) {
    return (
        <div className='flex items-center justify-end px-2 sm:justify-between'>
            <div className='text-muted-foreground hidden flex-1 text-sm sm:block'>
                {table.getFilteredSelectedRowModel().rows.length} of {table.getFilteredRowModel().rows.length} row(s)
                selected.
            </div>
            <div className='flex items-center gap-3 sm:gap-6 lg:gap-8'>
                <div className='flex items-center space-x-2'>
                    <p className='hidden text-sm font-medium sm:block'>Rows per page</p>
                    <Select
                        value={`${table.getState().pagination.pageSize}`}
                        onValueChange={value => {
                            table.setPageSize(Number(value));
                        }}
                    >
                        <SelectTrigger className='h-8 w-[70px]'>
                            <SelectValue />
                        </SelectTrigger>
                        <SelectContent side='top'>
                            {[10, 20, 25, 30, 40, 50].map(pageSize => (
                                <SelectItem key={pageSize} value={`${pageSize}`}>
                                    {pageSize}
                                </SelectItem>
                            ))}
                        </SelectContent>
                    </Select>
                </div>
                <div className='flex items-center justify-center text-sm font-medium sm:w-25'>
                    {table.getPageCount() > 0 && (
                        <>
                            Page {table.getState().pagination.pageIndex + 1} of {table.getPageCount()}
                        </>
                    )}
                </div>
                <div className='flex items-center space-x-2'>
                    <Button
                        variant='outline'
                        size='icon'
                        className='hidden size-8 lg:flex'
                        onClick={() => table.setPageIndex(0)}
                        disabled={!table.getCanPreviousPage()}
                    >
                        <span className='sr-only'>Go to first page</span>
                        <ChevronsLeft />
                    </Button>
                    <Button
                        variant='outline'
                        size='icon'
                        className='size-8'
                        onClick={() => table.previousPage()}
                        disabled={!table.getCanPreviousPage()}
                    >
                        <span className='sr-only'>Go to previous page</span>
                        <ChevronLeft />
                    </Button>
                    <Button
                        variant='outline'
                        size='icon'
                        className='size-8'
                        onClick={() => table.nextPage()}
                        disabled={!table.getCanNextPage()}
                    >
                        <span className='sr-only'>Go to next page</span>
                        <ChevronRight />
                    </Button>
                    <Button
                        variant='outline'
                        size='icon'
                        className='hidden size-8 lg:flex'
                        onClick={() => {
                            const lastPage = table.getPageCount() - 1;
                            if (lastPage >= 0) {
                                table.setPageIndex(lastPage);
                            }
                        }}
                        disabled={!table.getCanNextPage()}
                    >
                        <span className='sr-only'>Go to last page</span>
                        <ChevronsRight />
                    </Button>
                </div>
            </div>
        </div>
    );
}
