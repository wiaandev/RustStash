import React, {Suspense} from 'react';
import {CircularProgress, CssBaseline, ThemeProvider} from '@mui/material';
import {theme} from './Theme/Theme';
import {CustomRouterProvider} from './Routes';
import {useRelayEnv} from './Hooks/useRelayEnv';
import {LocalizationProvider} from '@mui/x-date-pickers';
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFnsV3';
import {RelayEnvironmentProvider} from 'react-relay';
import {enGB} from 'date-fns/locale';
import {AuthContextController} from './Context/AuthContext';

export function App() {
  const environment = useRelayEnv();
  return (
    <RelayEnvironmentProvider environment={environment}>
      <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={enGB}>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <Suspense fallback={<CircularProgress />}>
            <AuthContextController>
              <CustomRouterProvider relayEnv={environment} />
            </AuthContextController>
          </Suspense>
        </ThemeProvider>
      </LocalizationProvider>
    </RelayEnvironmentProvider>
  );
}
