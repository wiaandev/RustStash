import React from 'react';
import {useLazyLoadQuery} from 'react-relay';
import {graphql} from 'relay-runtime';
import Grid2 from '@mui/material/Unstable_Grid2';
import {Typography} from '@mui/material';
import {DashboardQuery} from './__generated__/DashboardQuery.graphql';

const QUERY = graphql`
  query DashboardQuery($userId: String!) {
    userInventory(userId: $userId) {
      itemName
      id
      itemImage
      quantity
    }
  }
`;

export default function DashboardPage() {
  const data = useLazyLoadQuery<DashboardQuery>(QUERY, {userId: 'dsajdsi'});
  return (
    <Grid2>
      <Typography>{data.userInventory[0].itemName}</Typography>
    </Grid2>
  );
}
