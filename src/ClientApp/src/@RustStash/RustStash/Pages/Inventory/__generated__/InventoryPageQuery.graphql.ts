/**
 * @generated SignedSource<<361ed4e6f7c341a5d4b9d871f09d42f0>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type InventoryPageQuery$variables = {
  userId: string;
};
export type InventoryPageQuery$data = {
  readonly me: {
    readonly id: string;
  };
  readonly userInventory: ReadonlyArray<{
    readonly id: string;
    readonly itemImage: string;
    readonly itemName: string;
    readonly quantity: number;
  }>;
};
export type InventoryPageQuery = {
  response: InventoryPageQuery$data;
  variables: InventoryPageQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = [
  {
    "defaultValue": null,
    "kind": "LocalArgument",
    "name": "userId"
  }
],
v1 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "id",
  "storageKey": null
},
v2 = [
  {
    "alias": null,
    "args": [
      {
        "kind": "Variable",
        "name": "userId",
        "variableName": "userId"
      }
    ],
    "concreteType": "Inventory",
    "kind": "LinkedField",
    "name": "userInventory",
    "plural": true,
    "selections": [
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "itemName",
        "storageKey": null
      },
      (v1/*: any*/),
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "itemImage",
        "storageKey": null
      },
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "quantity",
        "storageKey": null
      }
    ],
    "storageKey": null
  },
  {
    "alias": null,
    "args": null,
    "concreteType": "User",
    "kind": "LinkedField",
    "name": "me",
    "plural": false,
    "selections": [
      (v1/*: any*/)
    ],
    "storageKey": null
  }
];
return {
  "fragment": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Fragment",
    "metadata": null,
    "name": "InventoryPageQuery",
    "selections": (v2/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Operation",
    "name": "InventoryPageQuery",
    "selections": (v2/*: any*/)
  },
  "params": {
    "cacheID": "bd8c945be619c81808abd2358a10382b",
    "id": null,
    "metadata": {},
    "name": "InventoryPageQuery",
    "operationKind": "query",
    "text": "query InventoryPageQuery(\n  $userId: String!\n) {\n  userInventory(userId: $userId) {\n    itemName\n    id\n    itemImage\n    quantity\n  }\n  me {\n    id\n  }\n}\n"
  }
};
})();

(node as any).hash = "d4a2152b446fa8e12278949d928b5fe1";

export default node;
