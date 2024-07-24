import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {
  FieldValues,
  FormProvider,
  FormProviderProps,
  SubmitHandler,
} from 'react-hook-form';
import {Typography} from '@mui/material';
import {theme} from '@RustStash/RustStash/Theme/Theme';

interface Props<TFormValues extends FieldValues>
  extends FormProviderProps<TFormValues>
{
  children: React.ReactNode;
  onSubmit: SubmitHandler<TFormValues>;
}

export default function Form<TFormValues extends FieldValues = FieldValues>({
  children,
  onSubmit,
  ...props
}: Props<TFormValues>) {
  const errors = props.formState.errors;

  return (
    <FormProvider {...props}>
      <form onSubmit={props.handleSubmit(onSubmit)}>
        <Grid container xs direction={'column'}>
          {children}
        </Grid>
        {errors && errors.root?.['serverError'] && (
          <Typography color={theme.palette.error.main}>
            {errors.root?.['serverError'].message}
          </Typography>
        )}
      </form>
    </FormProvider>
  );
}
