import NonLayout from "../layouts/NonLayout";
import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";

export const routes = {
	home: "/",
	login: "/login",
};

export const publicRoutes = [
	{ path: routes.home, page: HomePage, layout: null },
	{ path: routes.login, page: LoginPage, layout: NonLayout },
];
