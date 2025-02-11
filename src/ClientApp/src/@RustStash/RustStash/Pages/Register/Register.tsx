import React from 'react';
import {Controller, useForm} from 'react-hook-form';
import * as yup from 'yup';
import {yupResolver} from '@hookform/resolvers/yup';
import {Button, Typography} from '@mui/material';
import Grid from '@mui/material/Unstable_Grid2';
import {theme} from '@RustStash/RustStash/Theme/Theme';
import Logo from '@RustStash/RustStash/assets/register-image.jpg';
import {DatePicker} from '@mui/x-date-pickers';
import Form from '@RustStash/RustStash/Components/Form';
import TextField from '@RustStash/RustStash/Components/TextField';
import {useNavigate} from 'react-router';

// Define the Yup validation schema
const registerFormSchema = yup.object().shape({
  username: yup
    .string()
    .min(2, 'Username is too short')
    .max(50, 'Username too long')
    .required('Username is required'),
  email: yup
    .string()
    .email('Invalid email format')
    .required('Email is required'),
  password: yup
    .string()
    .min(6, 'Password is too short')
    .max(20, 'Password too long')
    .required('Password is required'),
  confirmPassword: yup
    .string()
    .oneOf([yup.ref('password')], 'Passwords must match')
    .required('Confirm Password is required'),
  dateOfBirth: yup.date().required('Date of Birth is required'),
});

type FormValues = yup.InferType<typeof registerFormSchema>;

const YourComponent = () => {
  const navigate = useNavigate();
  const form = useForm<FormValues>({
    resolver: yupResolver(registerFormSchema),
  });

  const onSubmit = () => {
    const word = 'cool';
    word.substring(0, word.length - 1);
  };

  return (
    <Grid container xs minHeight={'100vh'}>
      <Grid
        sm={6}
        md
        p={2}
        bgcolor={theme.palette.background.default}
        direction={'column'}
        justifyContent={'center'}
        alignContent={'center'}
      >
        <Grid xs>
          <Typography variant='h3' sx={{fontWeight: 'bold'}} align='center'>
            Create Account
          </Typography>
        </Grid>
        <Grid container direction={'column'} spacing={2} p={4} columnGap={2}>
          <Form {...form} onSubmit={onSubmit}>
            <Grid xs gap={10}>
              <TextField
                control={form.control}
                fieldName='username'
                label='Username'
              />
            </Grid>

            <Grid xs>
              <TextField
                control={form.control}
                fieldName='email'
                label='Email'
              />
            </Grid>
            <Grid xs>
              <Controller
                control={form.control}
                name='dateOfBirth'
                rules={{required: true}}
                render={({field: {value, ref, onChange}}) => {
                  return (
                    <DatePicker
                      value={value || null}
                      inputRef={ref}
                      onChange={(date) => {
                        onChange(date);
                      }}
                      label='Date of Birth'
                    />
                  );
                }}
              />
              {form.formState.errors.dateOfBirth
                ? (
                  <Typography
                    variant='caption'
                    sx={{color: theme.palette.error.main}}
                  >
                    {form.formState.errors.dateOfBirth?.message}
                  </Typography>
                )
                : (
                  ' '
                )}
            </Grid>
            <Grid xs container columnGap={2}>
              <Grid xs>
                <TextField
                  control={form.control}
                  fieldName='password'
                  label='Password'
                />
              </Grid>
              <Grid xs>
                <TextField
                  control={form.control}
                  fieldName='confirmPassword'
                  label='Confirm Password'
                />
              </Grid>
            </Grid>
            <Grid xs container rowGap={2}>
              <Grid>
                <Button type='submit' variant='contained'>
                  Create Account
                </Button>
              </Grid>
              <Grid>
                <Button onClick={() => navigate('/login')} variant='outlined'>
                  Login
                </Button>
              </Grid>
            </Grid>
          </Form>
        </Grid>
      </Grid>
      <Grid xs container overflow={'hidden'}>
        <img
          src={Logo}
          alt='Logo'
          style={{maxHeight: '100vh', objectFit: 'fill'}}
        />
      </Grid>
    </Grid>
  );
};

export default YourComponent;
