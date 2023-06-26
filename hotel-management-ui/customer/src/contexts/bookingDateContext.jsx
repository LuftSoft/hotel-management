import { createContext, useContext, useRef, useState } from "react";

const initValue = {};
const BookingDateContext = createContext();

export const BookingDateProvider = ({ children }) => {
	// const bookingDateRef = useRef(initValue);
	const [bookingDate, setBookingDate] = useState(initValue);
	return <BookingDateContext.Provider value={{ bookingDate, setBookingDate }}>{children}</BookingDateContext.Provider>;
};

export const useBookingDate = () => {
	const context = useContext(BookingDateContext);
	if (context) {
		return context;
	} else {
		throw new Error("Context not found!");
	}
};
