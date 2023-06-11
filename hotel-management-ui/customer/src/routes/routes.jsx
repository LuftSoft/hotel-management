import NonLayout from "../layouts/NonLayout";
import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";

export const routes = {
	home: "/",
	signIn: "/sign-in",
	rooms: "/rooms",
	aboutUs: "/about-us",
	pages: "/pages",
	news: "/news",
	contact: "/contact",
};

export const publicRoutes = [
	{ path: routes.home, page: HomePage, layout: null },
	{ path: routes.signIn, page: LoginPage, layout: NonLayout },
	{ path: routes.rooms, page: HomePage, layout: null },
	{ path: routes.aboutUs, page: HomePage, layout: null },
	{ path: routes.pages, page: HomePage, layout: null },
	{ path: routes.news, page: HomePage, layout: null },
	{ path: routes.contact, page: HomePage, layout: null },
];
