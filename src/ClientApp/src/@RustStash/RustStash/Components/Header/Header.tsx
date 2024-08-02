import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {Button, Typography} from '@mui/material';
import {useAuthContext} from '@RustStash/RustStash/Context/AuthContext';
import {AccountCircle, Logout} from '@mui/icons-material';
import Logo from '@RustStash/RustStash/assets/logo.png';

export default function Header() {
  const {authenticated, me} = useAuthContext();
  return (
    <React.Fragment>
      <Grid
        container
        xs
        p={5}
        borderRadius={2}
        alignItems={'center'}
        columnGap={2}
      >
        <Grid xs>
          <img src={Logo} width={50} alt='logo' />
        </Grid>
        {authenticated && (
          <>
            <Grid container xs={'auto'} columnGap={1}>
              <AccountCircle />
              <Typography>{me?.email}</Typography>
            </Grid>
            <Grid xs='auto'>
              <Button variant='outlined' endIcon={<Logout />}>
                Logout
              </Button>
            </Grid>
          </>
        )}
      </Grid>
    </React.Fragment>
  );
}
