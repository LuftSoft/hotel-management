import NonLayout from "../layouts/NonLayout";
import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";

export const routes = {
	home: "/",
	signIn: "/sign-in",
};

export const publicRoutes = [
	{ path: routes.home, page: HomePage, layout: null },
	{ path: routes.signIn, page: LoginPage, layout: NonLayout },
];
