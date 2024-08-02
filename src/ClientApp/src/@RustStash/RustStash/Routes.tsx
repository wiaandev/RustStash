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
import {UserLayout} from './Layouts/UserLayout';
import DashboardPage from './Pages/Dashboard/Dashboard';
import Craft from './Pages/Craft/Craft';
import Map from './Pages/Map/Map';

interface Props {
  relayEnv: IEnvironment;
}

const router = (_relayEnv: IEnvironment) => {
  return createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route index element={<Navigate to={'welcome'} replace />} />
        <Route path='welcome' element={<Landing />} />
        <Route
          element={
            <AuthBlocker>
              <RootLayout />
            </AuthBlocker>
          }
        >
        </Route>
        <Route
          element={
            <AuthBlocker>
              <UserLayout />
            </AuthBlocker>
          }
        >
          <Route index element={<Navigate to='dashboard' replace />} />
          <Route path='dashboard' element={<DashboardPage />} />
          <Route path='craft' element={<Craft />} />
          <Route path='map' element={<Map />} />
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
