import { Counter } from "./components/template-examples/Counter/Counter";
import { FetchData } from "./components/template-examples/FetchData/FetchData";
import { Home } from "./components/template-examples/Home/Home";
import { LoginPage } from "./pages/LoginPage/LoginPage";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
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
