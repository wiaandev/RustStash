import React from 'react';
import {
  Control,
  Controller,
  FieldPath,
  FieldValues,
  RegisterOptions,
  useFormState,
} from 'react-hook-form';
import {
  StandardTextFieldProps,
  TextField as MUITextField,
  TextFieldProps as MUITextFieldProps,
} from '@mui/material';

export type Props<
  TFieldValues extends FieldValues,
  TName extends FieldPath<TFieldValues>,
  TextFieldProps extends MUITextFieldProps = StandardTextFieldProps,
> = TextFieldProps & {
  fieldName: TName;
  control: Control<TFieldValues>;
  rules?: Omit<
    RegisterOptions<TFieldValues, TName>,
    'valueAsNumber' | 'valueAsDate' | 'setValueAs' | 'disabled'
  >;
};

export default function TextField<
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPath<TFieldValues> = FieldPath<TFieldValues>,
  TextFieldProps extends MUITextFieldProps = MUITextFieldProps,
>({
  fieldName,
  control,
  ...props
}: Props<TFieldValues, TName, TextFieldProps>) {
  const {errors} = useFormState();
  const fieldError = errors[fieldName];
  return (
    <Controller
      render={({field}) => (
        <MUITextField
          helperText={fieldError ? (fieldError.message as string) : ' '}
          error={!!fieldError}
          fullWidth
          {...field}
          {...props}
        />
      )}
      name={fieldName}
      control={control}
    />
  );
}
