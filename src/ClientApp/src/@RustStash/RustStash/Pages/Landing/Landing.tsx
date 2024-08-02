import {Button, Typography} from '@mui/material';
import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {theme} from '../../Theme/Theme';
import {Link} from 'react-router-dom';
import Header from '@RustStash/RustStash/Components/Header/Header';
import landingImage from '@RustStash/RustStash/assets/image-removed.png';
import {useAuthContext} from '@RustStash/RustStash/Context/AuthContext';

export default function Landing() {
  const {me} = useAuthContext();
  return (
    <Grid container xs direction={'column'}>
      <Header />
      <Grid container>
        <Grid
          container
          xs
          alignItems={'center'}
          direction={'column'}
          bgcolor={theme.palette.background.paper}
          pt={5}
          rowGap={2}
        >
          <Grid>
            <Typography variant='h1' fontWeight={'bold'}>
              Visualise your Rust Stash
            </Typography>
          </Grid>
          <Typography variant='caption' color={theme.palette.text.primary}>
            Take control of your inventory
          </Typography>
          {!me && (
            <>
              <Button component={Link} to={'/register'}>
                Create Account
              </Button>
            </>
          )}
          <img src={landingImage} width={500} alt='an_image' />
        </Grid>
      </Grid>
      <Grid container height={'50vh'}>
        <Grid
          container
          xs
          alignItems={'center'}
          direction={'column'}
          bgcolor={'primary.main'}
        >
          <Grid>
            <Typography
              variant='h3'
              fontWeight={'bold'}
              color={theme.palette.background.paper}
            >
              Here will be some images
            </Typography>
            <Typography variant='body1'>
              With Rust Stash this can all be done under 1 place
            </Typography>
          </Grid>
        </Grid>
      </Grid>
      <Grid container>
        <Grid
          container
          xs
          rowGap={3}
          alignItems={'center'}
          direction={'column'}
          bgcolor={'primary.main'}
        >
          <Grid container justifyContent={'flex-end'}>
            <Typography>Rust Stash inc. 2024</Typography>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
