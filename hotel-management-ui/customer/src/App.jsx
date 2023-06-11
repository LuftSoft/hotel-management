import { BrowserRouter, Routes } from "react-router-dom";

// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import { renderRoutes } from "./utils/helpers";
import { publicRoutes } from "./routes";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import "./custom-bs.scss";

export default function App() {
	return (
		<BrowserRouter>
			<Routes>{renderRoutes(publicRoutes)}</Routes>
		</BrowserRouter>
	);
}
