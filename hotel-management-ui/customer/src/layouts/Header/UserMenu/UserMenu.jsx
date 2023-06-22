import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";

import { routes } from "../../../routes";
import { logout } from "../../../services/userServices";
import { selectUser } from "../../../redux/selectors";

export default function UserMenu() {
	const currentUser = useSelector(selectUser);
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
					<img
						className="rounded-circle"
						src={currentUser.avatar ? currentUser.avatar : ""}
						alt="user-avatar"
						style={{ width: 40, height: 40 }}
						onError={(e) => {
							e.target.src = "/img/user-avatar.png";
						}}
					/>
					<span className="ms-2">{currentUser.firstName}</span>
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
