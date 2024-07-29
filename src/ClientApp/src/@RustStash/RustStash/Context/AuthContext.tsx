import * as React from 'react';
import Loading from '../Components/Loading';

interface AuthContextType {
  authenticated: boolean;
  roles: Role[];
  me: {email: string; fullName: string; id: string} | null;
  handleLogin(): void;
  handleLogout(): void;
}

export interface Role {
  context: string;
  role: string;
  value: string;
}

export const AuthContext = React.createContext<AuthContextType>({
  authenticated: false,
  roles: [],
  me: null,
  handleLogin: () => {},
  handleLogout: () => {},
});

export function useAuthContext() {
  return React.useContext(AuthContext);
}

export const AuthContextController = React.memo(function AuthContextController({
  children,
}: {
  children: React.ReactNode;
}) {
  const [initialized, setInitialized] = React.useState(false);
  const [authenticated, setAuthenticated] = React.useState(false);
  const [roles, setRoles] = React.useState<Role[]>([]);
  const [me, setMe] = React.useState<
    {
      id: string;
      email: string;
      fullName: string;
    } | null
  >(null);

  React.useEffect(() => {
    async function profile() {
      if (initialized) {
        return;
      }
      try {
        const resp = await fetch('/api/account/profile');
        const json = await resp.json();

        if (!json.value) {
          setAuthenticated(false);
          setRoles([]);
          setMe(null);
        } else {
          setAuthenticated(true);
          setRoles(json.value.roles as Role[]);
          setMe(json.value);
          // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        }
      } catch {
        setAuthenticated(false);
        setRoles([]);
        setMe(null);
      } finally {
        setInitialized(true);
      }
    }

    profile();
  }, [setAuthenticated, setInitialized, initialized]);

  const handleLogin = React.useCallback(() => {
    setAuthenticated(true);
    setInitialized(false);
  }, []);

  const handleLogout = React.useCallback(() => {
    fetch('/api/account/logout', {
      method: 'GET',
    }).then(() => {
      setAuthenticated(false);
      setRoles([]);
    });
  }, [setAuthenticated]);

  const value = React.useMemo(
    () => ({
      authenticated,
      roles,
      me,
      handleLogin,
      handleLogout,
    }),
    [authenticated, handleLogin, handleLogout, me, roles],
  );

  if (!initialized) {
    return <Loading />;
  }

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
});
