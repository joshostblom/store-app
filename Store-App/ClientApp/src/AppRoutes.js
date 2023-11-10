import { DisplayProductBoxes } from "./components/Product/ProductBox/ProductBox";
import { APITest } from "./components/APITest/APITest";
import { FetchData } from "./components/template-examples/FetchData/FetchData";
import { LoginPage } from "./pages/LoginPage/LoginPage";

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
  },
  {
      path: '/login',
      element: <LoginPage />
  }
  
];

export default AppRoutes;
