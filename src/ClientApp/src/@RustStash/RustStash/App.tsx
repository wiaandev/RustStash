import {RelayEnvironmentProvider} from 'react-relay';
import {useRelayEnv} from './Hooks/useRelayEnv';

export function App() {
  const environment = useRelayEnv();
  return (
    <RelayEnvironmentProvider environment={environment}>
      <>Hello World</>
    </RelayEnvironmentProvider>
  );
}
