import { useDispatch } from "react-redux";
import { Link } from "react-router-dom";

import { logoutSuccess } from "../../../redux/authSlice";
import { updateUser } from "../../../redux/userSlice";
import { routes } from "../../../routes";
import { logout } from "../../../services/userServices";

export default function UserMenu() {
	const dispatch = useDispatch();
	const handleLogout = async (e) => {
		e.preventDefault();
		logout(dispatch);
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
					<Link to={routes.account} className="dropdown-item">
						Tài khoản
					</Link>
					<Link to={routes.bookedRoom} className="dropdown-item">
						Đặt chỗ của tôi
					</Link>
					<a href="#" className="dropdown-item" onClick={handleLogout}>
						Đăng xuất
					</a>
				</div>
			</div>
		</div>
	);
}
