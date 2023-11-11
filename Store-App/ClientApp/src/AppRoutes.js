import { DisplayProductBoxes } from "./components/Product/ProductBox/ProductBox";
import { APITest } from "./components/APITest/APITest";
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
    path: '/login',
    element: <LoginPage />
  },
  
];

export default AppRoutes;
