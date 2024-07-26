import Grid from '@mui/material/Unstable_Grid2';
import {CircularProgress, Paper} from '@mui/material';
import {theme} from '../Theme/Theme';

export default function Loading() {
  return (
    <Grid
      container
      xs
      justifyContent={'center'}
      alignItems={'center'}
      bgcolor={theme.palette.primary.contrastText}
      minHeight={'100vh'}
      component={Paper}
    >
      <CircularProgress color={'primary'} />
    </Grid>
  );
}
