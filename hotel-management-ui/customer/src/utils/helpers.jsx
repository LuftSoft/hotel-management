import { Outlet, Route } from "react-router-dom";

import DefaultLayout from "../layouts/DefaultLayout";

export const renderRoutes = (routes) => {
  let reactElements = null;
  if (Array.isArray(routes)) {
    reactElements = routes.map((route, index) => {
      const Layout = route.layout ?? DefaultLayout; // null or undefined
      const Page = route?.page;
      if (!Layout) {
        throw new Error("Layout is undefined!");
      }
      if (!Page) {
        throw new Error("Page is undefined!");
      }
      const children = route.children;
      if (children?.length) {
        return (
          <Route key={index} path={route.path} element={<Outlet />} >
            <Route index element={<Layout></Layout>} />
            {children.map((childRoute) => {
              const ChildPage = childRoute.component;
              return (
                <Route
                  key={childRoute.key}
                  path={childRoute.path}
                  element={<Layout><ChildPage /></Layout>} />
              )
            })}
          </Route>
        )
      } else {
        return (
          <Route key={index} path={route.path}
            element={<Layout><Page /></Layout>} />
        )
      }
    })
    return reactElements;
  } else {
    throw new Error("Routes must be an array!");
  }
}