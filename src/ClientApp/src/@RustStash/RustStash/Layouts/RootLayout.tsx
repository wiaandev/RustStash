import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {Outlet} from 'react-router';
import {theme} from '../Theme/Theme';
import Header from '@RustStash/RustStash/Components/Header/Header';

export default function RootLayout() {
  return (
    <Grid minHeight={'100vh'} bgcolor={theme.palette.background.default}>
      <Header />
      <Grid container xs wrap='nowrap' flex='1 1 auto' direction='column' p={5}>
        <Outlet />
      </Grid>
    </Grid>
  );
}
