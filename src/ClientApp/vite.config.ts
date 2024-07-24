import {defaultConfig} from '@stackworx.io/vite-react-scripts';

export default defaultConfig({
  importRoot: '@Stackworx/ProjectName',
  projectRoot: __dirname,
  aspnetcore: {
    proxyPaths: [
      '/api',
      '/graphql',
    ],
  },
});
