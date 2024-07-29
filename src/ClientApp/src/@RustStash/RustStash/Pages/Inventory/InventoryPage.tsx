import React from 'react';
import {useLazyLoadQuery} from 'react-relay';
import {graphql} from 'relay-runtime';
import {InventoryPageQuery} from './__generated__/InventoryPageQuery.graphql';
import Grid2 from '@mui/material/Unstable_Grid2';
import {Typography} from '@mui/material';

const QUERY = graphql`
  query InventoryPageQuery($userId: String!) {
    userInventory(userId: $userId) {
      itemName
      id
      itemImage
      quantity
    }
  }
`;

export default function InventoryPage() {
  const data = useLazyLoadQuery<InventoryPageQuery>(QUERY, {userId: 'dsajdsi'});
  return (
    <Grid2>
      <Typography>{data.userInventory[0].itemName}</Typography>
    </Grid2>
  );
}
