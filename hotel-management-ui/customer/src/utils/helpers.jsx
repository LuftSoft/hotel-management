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
					<Route key={index} path={route.path} element={<Outlet />}>
						<Route index element={<Layout></Layout>} />
						{children.map((childRoute) => {
							const ChildPage = childRoute.component;
							return (
								<Route
									key={childRoute.key}
									path={childRoute.path}
									element={
										<Layout>
											<ChildPage />
										</Layout>
									}
								/>
							);
						})}
					</Route>
				);
			} else {
				return (
					<Route
						key={index}
						path={route.path}
						element={
							<Layout>
								<Page />
							</Layout>
						}
					/>
				);
			}
		});
		return reactElements;
	} else {
		throw new Error("Routes must be an array!");
	}
};

export const formatDate = (date, pattern = "yyyy-mm-dd") => {
	let rs = null;
	switch (pattern) {
		case "yyyy-mm-dd":
			rs = `${date.getFullYear()}-${date.getMonth() < 9 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1}-${
				date.getDate() < 10 ? "0" + date.getDate() : date.getDate()
			}`;
			break;
		default:
			throw new Error("date pattern is not founded!");
	}
	return rs;
};
export const validateEmail = (errors, username) => {
	if (username === "") {
		errors.email = "Vui lòng nhập email!";
	}
};
export const validatePassword = (errors, password) => {
	const decimal = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{6,}$/;
	if (password === "") {
		errors.password = "Vui lòng nhập mật khẩu!";
	} else if (!decimal.test(password)) {
		errors.password = "Mật khẩu tối thiểu 6 ký tự. Chứa ít nhất 1 ký tự in hoa, 1 ký tự số và 1 ký tự đặc biệt";
	}
};
