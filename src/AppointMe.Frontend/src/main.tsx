import './index.css';
import { App } from '@/app';
import { createRoot } from 'react-dom/client';

const reportError = (
    error: unknown,
    context: {
        kind: 'caught' | 'uncaught' | 'recoverable';
        componentStack?: string;
    },
) => {
    console.error(`[React ${context.kind} error]`, error, context);
};

const container = document.getElementById('root');

if (!container) {
    throw new Error('Root element not found');
}

const root = createRoot(container, {
    onCaughtError: (error, errorInfo) => {
        reportError(error, {
            kind: 'caught',
            componentStack: errorInfo.componentStack,
        });
    },
    onUncaughtError: (error, errorInfo) => {
        reportError(error, {
            kind: 'uncaught',
            componentStack: errorInfo.componentStack,
        });
    },
    onRecoverableError: (error, errorInfo) => {
        reportError(error, {
            kind: 'recoverable',
            componentStack: errorInfo.componentStack,
        });
    },
});

root.render(<App />);
