import {createTheme} from '@mui/material/styles';

// font list to specify to the browser the preferred fonts (if the first one is not available it will default to the next one and so on)
const fontList =
  `"Raleway", "Poppins", "Roboto", "Helvetica", "Arial", "sans-serif"`;

// #region Text and Colour defaults

const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#CD412B',
    },
    secondary: {
      main: '#708C45',
    },
    text: {
      primary: '#FBECEA',
    },
    background: {
      default: '#333333',
      paper: '#242424',
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
        color: '#FFF3F0',
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
