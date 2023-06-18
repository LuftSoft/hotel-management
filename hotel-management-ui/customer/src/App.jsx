import { BrowserRouter, Navigate, Outlet, Route, Routes } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import { renderRoutes } from "./utils/helpers";
import { privateRoutes, publicRoutes } from "./routes";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import "./custom-bs.scss";
import { selectUser } from "./redux/selectors";
import { useSelector } from "react-redux";

const ProtectedRoute = ({ redirectPath = "/sign-in" }) => {
	console.log("ProtectedRoute");
	const currentUser = useSelector(selectUser);
	const currentPathName = window.location.pathname;
	if (!currentUser) {
		return <Navigate to={`${redirectPath}?next=${encodeURIComponent(currentPathName)}`} replace />;
	}
	return <Outlet />;
};

export default function App() {
	return (
		<BrowserRouter>
			<ToastContainer />
			<Routes>
				{renderRoutes(publicRoutes)}
				{<Route element={<ProtectedRoute />}>{renderRoutes(privateRoutes)}</Route>}
			</Routes>
		</BrowserRouter>
	);
}
