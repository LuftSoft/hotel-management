import NonLayout from "../layouts/NonLayout";
import HomePage from "../pages/HomePage";
import SignInPage from "../pages/SignInPage";
import SignUpPage from "../pages/SignUpPage";
import HotelPage from "../pages/HotelPage";
import DetailHotelPage from "../pages/DetailHotelPage";
import BookedRoomPage from "../pages/BookedRoomPage";
import AccountPage from "../pages/AccountPage/AccountPage";
import ForgotPasswordPage from "../pages/ForgotPasswordPage";
import MenuUser from "../layouts/MenuUser";
import ResetPasswordPage from "../pages/ResetPasswordPage";

export const routes = {
	home: "/",
	signIn: "/sign-in",
	signUp: "/sign-up",
	rooms: "/rooms",
	hotel: "/hotel",
	detailHotel: "/hotel/:id",
	aboutUs: "/about-us",
	pages: "/pages",
	news: "/news",
	contact: "/contact",
	bookedRoom: "/user/booked-room",
	account: "/user/account",
	signOut: "/sign-out",
	forgotPassword: "/forgot-password",
	// resetPassword: "/reset-password/:token",
	resetPassword: "/reset-password",
};

export const publicRoutes = [
	{ path: routes.home, page: HomePage, layout: null },
	{ path: routes.signIn, page: SignInPage, layout: NonLayout },
	{ path: routes.signUp, page: SignUpPage, layout: NonLayout },
	{ path: routes.rooms, page: HomePage, layout: null },
	{ path: routes.hotel, page: HotelPage, layout: null },
	{ path: routes.detailHotel, page: DetailHotelPage, layout: null },
	{ path: routes.aboutUs, page: HomePage, layout: null },
	{ path: routes.pages, page: HomePage, layout: null },
	{ path: routes.news, page: HomePage, layout: null },
	{ path: routes.contact, page: HomePage, layout: null },
	{ path: routes.forgotPassword, page: ForgotPasswordPage, layout: NonLayout },
	{ path: routes.resetPassword, page: ResetPasswordPage, layout: null },
];

export const privateRoutes = [
	{ path: routes.bookedRoom, page: BookedRoomPage, layout: MenuUser },
	{ path: routes.account, page: AccountPage, layout: MenuUser },
];
