import {Hardware, Inventory, Map} from '@mui/icons-material';
import {
  alpha,
  Box,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from '@mui/material';
import Grid2 from '@mui/material/Unstable_Grid2';
import React from 'react';
import {Outlet, useLocation} from 'react-router-dom';
import {useLocalStorage} from '../Hooks/useLocalStorage';
import {theme} from '../Theme/Theme';
import logo from '@RustStash/RustStash/assets/logo.png';
import logoBig from '@RustStash/RustStash/assets/Rust_Branding_Rough-1080.png';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';

export function UserLayout() {
  const menuItems = [
    {text: 'Inventory', icon: <Inventory />, path: 'dashboard'},
    {text: 'Craft', icon: <Hardware />, path: 'craft'},
    {text: 'Map', icon: <Map />, path: 'map'},
  ];

  const drawerWidth = 240;
  const collapsedDrawerWidth = 60;
  const [open, setOpen] = useLocalStorage('navigation:layout:drawer', true);

  const location = useLocation();

  const handleDrawerToggle = () => {
    setOpen(!open);
  };

  // useEffect(() => {
  //   localStorage.setItem('drawerOpen', JSON.stringify(open));
  // }, [open]);

  const isActive = (path: string) => {
    const currentPath = location.pathname;
    if (currentPath === `/${path}`) {
      return true;
    }

    if (
      currentPath.includes(`/${path}`)
      && currentPath.split('/')[1] === path
    ) {
      return true;
    }
    return false;
  };
  return (
    <Grid2 container xs>
      <Drawer
        variant='permanent'
        anchor='left'
        open={open}
        sx={{
          width: open ? drawerWidth : collapsedDrawerWidth,
          '& .MuiDrawer-paper': {
            width: open ? drawerWidth : collapsedDrawerWidth,
            boxSizing: 'border-box',
            transition: theme.transitions.create('width', {
              easing: theme.transitions.easing.easeIn,
              duration: theme.transitions.duration.complex,
            }),
          },
        }}
      >
        {!open && (
          <IconButton onClick={handleDrawerToggle}>
            <img src={logo} alt='rust_stash' height={50} />
          </IconButton>
        )}
        {open && (
          <Box
            width='100%'
            display='flex'
            justifyContent='space-between'
            alignItems='center'
          >
            <img src={logoBig} alt='rust_stash_logo' height={100} />
            <IconButton onClick={handleDrawerToggle}>
              <ChevronLeftIcon />
            </IconButton>
          </Box>
        )}
        <Box flex='1'>
          <List sx={{mx: open ? 1 : 0}}>
            {menuItems.map((item) => (
              <ListItem key={item.text} disablePadding>
                <ListItemButton
                  href={`/${item.path}`}
                  sx={{
                    backgroundColor: isActive(item.path)
                      ? alpha(theme.palette.primary.main, 0.2)
                      : 'transparent',
                    '&:hover': {
                      backgroundColor: alpha(
                        theme.palette.primary.main,
                        0.3,
                      ),
                    },
                    borderRadius: 2,
                    justifyContent: open ? 'initial' : 'center',
                    px: 2.5,
                    mx: 1,
                  }}
                >
                  <ListItemIcon
                    sx={{
                      color: isActive(item.path)
                        ? theme.palette.primary.dark
                        : 'none',
                      minWidth: 0,
                      mr: open ? 3 : 'auto',
                      justifyContent: 'center',
                    }}
                  >
                    {item.icon}
                  </ListItemIcon>
                  <ListItemText
                    primary={item.text}
                    primaryTypographyProps={{noWrap: true}}
                  />
                </ListItemButton>
              </ListItem>
            ))}
          </List>
        </Box>
      </Drawer>
      <Grid2 p={2}>
        <Outlet />
      </Grid2>
    </Grid2>
  );
}
