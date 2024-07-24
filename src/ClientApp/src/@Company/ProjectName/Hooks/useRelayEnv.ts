import * as React from 'react';
import {Environment, Network, RecordSource, Store} from 'relay-runtime';
import {createFetchQuery} from '@stackworx.io/relay-network';

const network = Network.create(
  createFetchQuery({
    url: `/graphql`,
    timeout: 10000,
    retry: {
      statusCodes: [503],
      methods: ['get'],
      limit: 2,
    },
  }),
);

export function useRelayEnv() {
  // // TODO: handle reset when user logs out
  return React.useMemo(() => {
    const store = new Store(new RecordSource());

    return new Environment({
      network,
      store,
    });
  }, []);
}
