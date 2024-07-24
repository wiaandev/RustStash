import {defaultConfig} from '@stackworx.io/vite-react-scripts';
import path from 'path';

export default defaultConfig({
  importRoot: '@Stackworx/ProjectName',
  projectRoot: __dirname,
  resolve: {
    alias: [
      {
        find: '@BookAPharmacy',
        replacement: path.resolve(__dirname, 'src', '@BookAPharmacy'),
      },
      {
        find: /^@mui\/icons-material\/(.*)/,
        replacement: '@mui/icons-material/esm/$1',
      },
    ],
  },
  aspnetcore: {
    proxyPaths: ['/api', '/graphql'],
  },
});
