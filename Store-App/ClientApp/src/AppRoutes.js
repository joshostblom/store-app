import { DisplayProductBoxes } from "./components/Product/ProductBox/ProductBox";
import { APITest } from "./components/APITest/APITest";
import { FetchData } from "./components/template-examples/FetchData/FetchData";

const AppRoutes = [
    
  {
    index: true,
    element: <DisplayProductBoxes />
  },
  {
    path: '/apitest',
    element: <APITest />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
  
];

export default AppRoutes;
