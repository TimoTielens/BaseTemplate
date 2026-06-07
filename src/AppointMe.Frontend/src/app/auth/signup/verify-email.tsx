import { Card, CardContent, CardDescription, CardHeader, CardTitle, FieldDescription } from '@/components/ui';
import { MailCheck } from 'lucide-react';
import { useParams } from 'react-router';

export const VerifyEmail = () => {
    const { email } = useParams();
    return (
        <div className='flex min-h-[80vh] items-center justify-center px-4'>
            <Card className='w-full max-w-md'>
                <CardHeader className='space-y-2 text-center'>
                    <div className='bg-primary/10 mx-auto flex h-12 w-12 items-center justify-center rounded-full'>
                        <MailCheck className='text-primary h-6 w-6' />
                    </div>
                    <CardTitle className='text-2xl'>Check your email</CardTitle>
                    <CardDescription className='text-muted-foreground text-sm'>
                        We’ve started creating your account
                    </CardDescription>
                </CardHeader>
                <CardContent className='space-y-6'>
                    <div className='space-y-3 text-sm'>
                        <p>
                            We’ve sent an email
                            {email && (
                                <>
                                    {' '}
                                    to <span className='text-foreground font-medium'>{email}</span>
                                </>
                            )}
                            .
                        </p>
                        <p>Open the email and click the link to finish setting up your account and sign in.</p>
                    </div>

                    <FieldDescription className='px-6 text-center'>
                        Already have an account? <a href='/auth/login'>Sign in</a>
                    </FieldDescription>
                </CardContent>
            </Card>
        </div>
    );
};
