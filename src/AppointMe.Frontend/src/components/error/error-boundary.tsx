import { ErrorScreen } from './error-screen';
import { Component, type ReactNode } from 'react';

interface ErrorBoundaryProps {
    children: ReactNode;
}

interface ErrorBoundaryState {
    hasError: boolean;
}

// Logging happens centrally in `createRoot`'s `onCaughtError` callback (React 19+),
// so this boundary only needs to derive fallback state and render the screen.
export class ErrorBoundary extends Component<ErrorBoundaryProps, ErrorBoundaryState> {
    state: ErrorBoundaryState = { hasError: false };

    static getDerivedStateFromError(): ErrorBoundaryState {
        return { hasError: true };
    }

    render() {
        if (this.state.hasError) {
            return <ErrorScreen />;
        }

        return this.props.children;
    }
}
