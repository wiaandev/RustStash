import {
  createBrowserRouter,
  createRoutesFromElements,
  Navigate,
  Route,
  RouterProvider,
} from 'react-router-dom';
import RootLayout from './Layouts/RootLayout';
import Register from './Pages/Register/Register';
import React from 'react';
import Landing from './Pages/Landing/Landing';
import {IEnvironment} from 'relay-runtime';
import Login from './Pages/Login/Login';
import {AuthContext} from './Context/AuthContext';
import InventoryPage from './Pages/Inventory/InventoryPage';

interface Props {
  relayEnv: IEnvironment;
}

const router = (_relayEnv: IEnvironment) => {
  return createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route path='home' element={<Landing />} />
        <Route
          element={
            <AuthBlocker>
              <RootLayout />
            </AuthBlocker>
          }
        >
          <Route path='inventory' element={<InventoryPage />} />
        </Route>
        <Route path='register' element={<Register />} />
        <Route path='login' element={<Login />} />
      </>,
    ),
  );
};

function AuthBlocker({children}: {children: React.ReactNode}) {
  const {authenticated} = React.useContext(AuthContext);

  if (!authenticated) {
    return <Navigate to={'/login'} />;
  }

  return <>{children}</>;
}

export function CustomRouterProvider({relayEnv}: Props) {
  return <RouterProvider router={router(relayEnv)} />;
}
