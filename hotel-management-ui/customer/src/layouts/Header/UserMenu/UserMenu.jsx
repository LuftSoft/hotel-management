import { useDispatch } from "react-redux";
import { logoutSuccess } from "../../../redux/authSlice";
import { updateUser } from "../../../redux/userSlice";

export default function UserMenu() {
	const dispatch = useDispatch();
	const handleLogout = async (e) => {
		e.preventDefault();
		// const axiosJwt = createAxiosJwt(accessToken, refreshToken, dispatch);
		// try {
		// 	const res = await axiosJwt.patch(
		// 		path.logout,
		// 		{},
		// 		{
		// 			headers: {
		// 				Authorization: `bearer ${accessToken}`,
		// 			},
		// 		},
		// 	);
		// 	if (res.data.isSuccess) {
		dispatch(logoutSuccess());
		dispatch(updateUser(null));
		// 	}
		// } catch (error) {
		// 	console.log(error);
		// }
	};
	return (
		<div className="d-flex">
			<div className="dropdown">
				<button
					type="button"
					className="btn btn-link text-decoration-none dropdown-toggle"
					data-bs-toggle="dropdown"
					aria-expanded="false">
					<img src="/img/user-avatar.png" alt="user-avatar" style={{ width: 40, height: 40 }} />
					<span className="ms-2">user name</span>
				</button>
				<div className="dropdown-menu dropdown-menu-end">
					<a href="#" className="dropdown-item">
						Tài khoản
					</a>
					<a href="#" className="dropdown-item" onClick={handleLogout}>
						Đăng xuất
					</a>
				</div>
			</div>
		</div>
	);
}
