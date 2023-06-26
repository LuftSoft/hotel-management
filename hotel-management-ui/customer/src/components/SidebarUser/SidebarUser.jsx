import { Link, NavLink } from "react-router-dom";

import { routes } from "../../routes";
import { useDispatch, useSelector } from "react-redux";
import { selectUser } from "../../redux/selectors";
import { logout } from "../../services/userServices";

export default function SidebarUser() {
	const currentUser = useSelector(selectUser);
	const dispatch = useDispatch();
	const handleLogout = (e) => {
		e.preventDefault();
		logout(dispatch);
	};
	return (
		<div className="align-self-start d-flex flex-column bg-white border rounded" style={{ minWidth: 300 }}>
			<div className="d-flex flex-column p-3 border-bottom mb-2">
				<div className="p-30">
					<div className="d-flex align-items-center">
						<div className="border border-2 rounded-circle p-1 overflow-hidden">
							<img
								className="rounded-circle"
								src={currentUser.avatar ? currentUser.avatar : "/img/user-avatar.png"}
								alt="user-avatar"
								style={{ width: 60, height: 60, objectFit: "cover" }}
								onError={(e) => {
									e.target.src = "/img/user-avatar.png";
								}}
							/>
						</div>
						<div className="ms-2">
							<h3 className="fs-5">{currentUser.lastName + " " + currentUser.firstName}</h3>
						</div>
					</div>
				</div>
			</div>
			{/* menu */}
			<div className="d-flex flex-column">
				<div className="d-flex flex-column">
					<NavLink
						to={routes.bookedRoom}
						className={({ isActive }) => {
							return `${isActive ? "bg-primary text-white" : "text-black"}`;
						}}>
						<div className="d-flex px-4 py-2">
							<div className="d-flex fw-bold">
								{/* icon */}
								<div>
									<i className="fa-solid fa-rectangle-list"></i>
								</div>
								<div className="ms-3">Đặt chỗ của tôi</div>
							</div>
						</div>
					</NavLink>
				</div>
			</div>
			<div className="border my-2"></div>
			<div className="d-flex flex-column">
				<div className="d-flex flex-column">
					<NavLink
						to={routes.account}
						className={({ isActive }) => {
							return `${isActive ? "bg-primary text-white" : "text-black"}`;
						}}>
						<div className="d-flex px-4 py-2">
							<div className="d-flex fw-bold">
								{/* icon */}
								<div>
									<i className="fa-solid fa-gear"></i>
								</div>
								<div className="ms-3">Tài khoản</div>
							</div>
						</div>
					</NavLink>
				</div>
			</div>
			<div className="d-flex flex-column">
				<div className="d-flex flex-column">
					<Link onClick={handleLogout} className="text-black">
						<div className="d-flex px-4 py-2">
							<div className="d-flex fw-bold">
								{/* icon */}
								<div>
									<i className="fa-solid fa-power-off"></i>
								</div>
								<div className="ms-3">Đăng xuất</div>
							</div>
						</div>
					</Link>
				</div>
			</div>
		</div>
	);
}
