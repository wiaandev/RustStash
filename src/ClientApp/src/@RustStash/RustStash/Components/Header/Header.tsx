import React from 'react';
import Grid from '@mui/material/Unstable_Grid2';
import {Divider, Typography} from '@mui/material';

export default function Header() {
  return (
    <React.Fragment>
      <Grid
        container
        xs
        p={5}
        borderRadius={2}
        justifyContent={'space-between'}
      >
        <Grid>
          <Typography sx={{fontWeight: 'bold'}}>Rust Stash</Typography>
        </Grid>
        <Grid container xs={'auto'} columnGap={2}>
          <Typography>Home</Typography>
          <Typography>Inventory</Typography>
          <Typography>Map</Typography>
        </Grid>
      </Grid>
      <Divider />
    </React.Fragment>
  );
}
