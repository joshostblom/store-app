import { Counter } from "./components/template-examples/Counter/Counter";
import { FetchData } from "./components/template-examples/FetchData/FetchData";
import { Home } from "./components/template-examples/Home/Home";

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
  }
];

export default AppRoutes;
