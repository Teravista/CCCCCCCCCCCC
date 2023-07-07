
import { Home } from "./components/Home";
import { ContactsList } from "./components/ContactsList";

const AppRoutes = [
  {
        index: true,
        element: <Home />
    },
    {
        path: '/ContactsList',
        element: <ContactsList />
  }
];

export default AppRoutes;
