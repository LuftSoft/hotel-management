import React from "react";
import ReactDOM from "react-dom/client";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import { QueryClient } from "@tanstack/query-core";
import { QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

import App from "./App.jsx";
import "./index.css";
import { store, persistor } from "./redux/store";
import { BookingDateProvider } from "./contexts/bookingDateContext.jsx";

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById("root")).render(
	<React.StrictMode>
		<Provider store={store}>
			<PersistGate loading={null} persistor={persistor}>
				<QueryClientProvider client={queryClient}>
					<BookingDateProvider>
						<App />
					</BookingDateProvider>
					<ReactQueryDevtools initialIsOpen={false} />
				</QueryClientProvider>
			</PersistGate>
		</Provider>
	</React.StrictMode>,
);
