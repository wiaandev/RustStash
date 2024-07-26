import {yupResolver} from '@hookform/resolvers/yup';
import {Button, Typography} from '@mui/material';
import Grid2 from '@mui/material/Unstable_Grid2';
import Form from '@RustStash/RustStash/Components/Form';
import TextField from '@RustStash/RustStash/Components/TextField';
import {useAuthContext} from '@RustStash/RustStash/Context/AuthContext';
import {authenticationService} from '@RustStash/RustStash/Services/AuthenticationService';
import React, {useState} from 'react';
import {SubmitHandler, useForm} from 'react-hook-form';
import {Navigate} from 'react-router-dom';
import * as yup from 'yup';

const loginSchema = yup.object().shape({
  email: yup
    .string()
    .email('not a valid email')
    .required('This field is required'),
  password: yup.string().required('This field is required'),
});

type formValues = yup.InferType<typeof loginSchema>;

export default function Login() {
  const [error, setError] = useState<string>('');
  const {authenticated, handleLogin} = useAuthContext();
  const form = useForm<formValues>({
    resolver: yupResolver(loginSchema),
    defaultValues: {email: '', password: ''},
  });

  const onSubmit: SubmitHandler<formValues> = async (data: formValues) => {
    try {
      await authenticationService.login(data);
      handleLogin();
    } catch (ex) {
      setError('Credentials invalid');
    }
  };

  if (authenticated) {
    return <Navigate to='/home' />;
  }

  return (
    <Grid2 container spacing={2}>
      <Grid2 xs>
        <Form {...form} onSubmit={onSubmit}>
          <TextField control={form.control} fieldName='email' label='Email' />
          <TextField
            control={form.control}
            fieldName='password'
            type='password'
          />
          <Typography color='error'>{error}</Typography>
          <Button type='submit' variant='contained' color='primary'>
            Submit
          </Button>
        </Form>
      </Grid2>
    </Grid2>
  );
}
