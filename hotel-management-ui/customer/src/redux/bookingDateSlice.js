import { createSlice } from "@reduxjs/toolkit";

const bookingDateSlice = createSlice({
	name: "bookingDate",
	initialState: {
		bookingDate: {},
	},
	reducers: {
		setBookingDate: (state, action) => {
			state.bookingDate = action.payload;
		},
	},
});

export const { setBookingDate } = bookingDateSlice.actions;

export default bookingDateSlice.reducer;
