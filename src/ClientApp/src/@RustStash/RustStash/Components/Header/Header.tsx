import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {Button, Typography} from '@mui/material';
import {Link} from 'react-router-dom';
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
            <Grid container xs columnGap={2}>
              <>
                <Typography>Home</Typography>
                <Typography component={Link} to={'/inventory'}>
                  Inventory
                </Typography>
                <Typography>Map</Typography>
              </>
            </Grid>
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
