import {Button, Typography} from '@mui/material';
import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {theme} from '../../Theme/Theme';
import {Link} from 'react-router-dom';

export default function Landing() {
  return (
    <Grid container xs direction={'column'}>
      <Grid container>
        <Grid
          container
          xs
          rowGap={3}
          alignItems={'center'}
          direction={'column'}
        >
          <Grid>
            <Typography variant='h3' fontWeight={'bold'}>
              Visualise your Rust inventory
            </Typography>
          </Grid>
          <Typography variant='caption' color={theme.palette.primary.light}>
            With Rust Stash, take control of your inventory
          </Typography>
          <Button variant='outlined' component={Link} to={'/register'}>
            Create Account
          </Button>
        </Grid>
      </Grid>
    </Grid>
  );
}
