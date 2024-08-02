/**
 * @generated SignedSource<<8f086eb3147b508f661050c50eae968a>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type DashboardQuery$variables = {
  userId: string;
};
export type DashboardQuery$data = {
  readonly userInventory: ReadonlyArray<{
    readonly id: string;
    readonly itemImage: string;
    readonly itemName: string;
    readonly quantity: number;
  }>;
};
export type DashboardQuery = {
  response: DashboardQuery$data;
  variables: DashboardQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = [
  {
    "defaultValue": null,
    "kind": "LocalArgument",
    "name": "userId"
  }
],
v1 = [
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
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "id",
        "storageKey": null
      },
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
  }
];
return {
  "fragment": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Fragment",
    "metadata": null,
    "name": "DashboardQuery",
    "selections": (v1/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Operation",
    "name": "DashboardQuery",
    "selections": (v1/*: any*/)
  },
  "params": {
    "cacheID": "19efbfea9d8281a0245baf90fd6ddf0c",
    "id": null,
    "metadata": {},
    "name": "DashboardQuery",
    "operationKind": "query",
    "text": "query DashboardQuery(\n  $userId: String!\n) {\n  userInventory(userId: $userId) {\n    itemName\n    id\n    itemImage\n    quantity\n  }\n}\n"
  }
};
})();

(node as any).hash = "da5d80729054be4d02006dc36f0a92c8";

export default node;
