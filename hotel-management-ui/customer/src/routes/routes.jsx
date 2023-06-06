import NonLayout from "../layouts/NonLayout";
import HomePage from "../pages/HomePage";
import SignInPage from "../pages/SignInPage";
import SignUpPage from "../pages/SignUpPage";

export const routes = {
	home: "/",
	signIn: "/sign-in",
	signUp: "/sign-up",
	rooms: "/rooms",
	aboutUs: "/about-us",
	pages: "/pages",
	news: "/news",
	contact: "/contact",
};

export const publicRoutes = [
	{ path: routes.home, page: HomePage, layout: null },
	{ path: routes.signIn, page: SignInPage, layout: NonLayout },
	{ path: routes.signUp, page: SignUpPage, layout: NonLayout },
	{ path: routes.rooms, page: HomePage, layout: null },
	{ path: routes.aboutUs, page: HomePage, layout: null },
	{ path: routes.pages, page: HomePage, layout: null },
	{ path: routes.news, page: HomePage, layout: null },
	{ path: routes.contact, page: HomePage, layout: null },
];
