import { ReactElement, createContext, useContext } from 'react';

export interface IModalProps<TResult> {
    resolve: (value: TResult | PromiseLike<TResult>) => void;
    visible: boolean;
}

export type ModalFactory<T> = (props: IModalProps<T>) => ReactElement;

export interface IModalDialogContext {
    open: <TResult = void>(modalFactory: ModalFactory<TResult>) => Promise<TResult>;
}

export const ModalDialogContext = createContext<IModalDialogContext>({} as IModalDialogContext);

export const useModalDialog = () => useContext(ModalDialogContext);
