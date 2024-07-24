import {createTheme} from '@mui/material/styles';

// font list to specify to the browser the preferred fonts (if the first one is not available it will default to the next one and so on)
const fontList =
  `"Raleway", "Poppins", "Roboto", "Helvetica", "Arial", "sans-serif"`;

// #region Text and Colour defaults

const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#e63900',
    },
    secondary: {
      main: '#f6bba4',
    },
    text: {
      primary: '#ffebcb',
    },
    background: {
      default: '#272320',
      paper: '#3c3731',
    },
  },
  typography: {
    allVariants: {
      fontFamily: fontList,
      letterSpacing: 0.5,
    },
  },
  components: {
    MuiTextField: {
      defaultProps: {
        fullWidth: true,
      },
    },
    MuiTypography: {
      defaultProps: {
        color: '#ffebcb',
      },
    },
    MuiButton: {
      defaultProps: {
        disableElevation: true,
        disableRipple: true,
        variant: 'contained',
        sx: {textTransform: 'none'},
        size: 'large',
      },
    },
  },
});

// #endregion

export {theme};
