import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router";
import LoginPage from "./pages/login";
import RegisterPage from "./pages/register";
import NotFoundPage from "./pages/not-found";
import "./index.css";
import ContactsPage from "./pages/contacts";
import { AuthProvider } from "./providers/auth-provider";
import ContactEditForm from "./pages/contact-edit-form";
import ContactDetailsPage from "./pages/contact-detailes";
import ContactForm from "./pages/contact-form";

const router = createBrowserRouter([
  {
    path: "/",
    element: <LoginPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
  {
    path: "/contacts",
    element: <ContactsPage />,
  },
  {
    path: "/contacts/detailed/:id",
    element: <ContactDetailsPage />,
  },
  {
    path: "/contacts/add",
    element: <ContactForm />,
  },
  {
    path: "/contacts/edit/:id",
    element: <ContactEditForm />,
  },
]);

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  </StrictMode>
);
