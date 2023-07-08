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

// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
	apiKey: "AIzaSyBJaBz4pPayjp0fq3hPzQZtRe3RL0gauYc",
	authDomain: "booking-hotel-ca412.firebaseapp.com",
	projectId: "booking-hotel-ca412",
	storageBucket: "booking-hotel-ca412.appspot.com",
	messagingSenderId: "52323841285",
	appId: "1:52323841285:web:d73e82f56483696f5bf36c",
	measurementId: "G-JLZQKZ6BP5",
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
console.log(analytics);

const ProtectedRoute = ({ redirectPath = "/sign-in" }) => {
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
