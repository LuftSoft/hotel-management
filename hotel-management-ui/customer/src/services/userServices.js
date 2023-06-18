import { logoutSuccess } from "../redux/authSlice";
import { updateUser } from "../redux/userSlice";

export const signUp = () => {};

export const logout = (dispatch) => {
	dispatch(logoutSuccess());
	dispatch(updateUser(null));
};
