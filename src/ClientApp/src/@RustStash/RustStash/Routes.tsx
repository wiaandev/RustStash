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

interface Props {
  relayEnv: IEnvironment;
}

const router = (_relayEnv: IEnvironment) => {
  return createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route element={<RootLayout />}>
          <Route index element={<Navigate to={'home'} replace />} />
          <Route path='home' element={<Landing />} />
        </Route>
        <Route path='register' element={<Register />} />
      </>,
    ),
  );
};

export function CustomRouterProvider({relayEnv}: Props) {
  return <RouterProvider router={router(relayEnv)} />;
}
