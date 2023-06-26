import { combineReducers, configureStore } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import { persistStore, persistReducer, FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER } from "redux-persist";

import authSlice from "./authSlice";
import userSlice from "./userSlice";
import bookingDateSlice from "./bookingDateSlice";

const persistConfig = {
	key: "root",
	version: 1,
	storage,
	whitelist: ["auth", "user", "bookingDate"],
};

const rootReducer = combineReducers({
	auth: authSlice,
	user: userSlice,
	bookingDate: bookingDateSlice,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
	reducer: persistedReducer,
	middleware: (getDefaultMiddleware) =>
		getDefaultMiddleware({
			serializableCheck: {
				ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
			},
		}),
});

export const persistor = persistStore(store);
