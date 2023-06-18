import { Link, NavLink } from "react-router-dom";
import { routes } from "../../routes";

export default function AccountPage() {
	const handleLogout = (e) => {
		console.log("t");
	};
	return (
		<div className="bg-light">
			<div className="Container">
				<div className="d-flex flex-column">
					<div className="d-flex gap-4 my-3">
						<div className="d-flex flex-column bg-white border rounded" style={{ minWidth: 300 }}>
							<div className="d-flex flex-column p-3 border-bottom mb-2">
								<div className="p-30">
									<div className="d-flex align-items-center">
										<div className="border border-2 rounded-circle p-1">
											<img src="/img/user-avatar.png" alt="user-avatar" style={{ width: 60, height: 60 }} />
										</div>
										<div className="ms-2">
											<h3 className="fs-5">name</h3>
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
						<div className="flex-grow-1">1</div>
					</div>
				</div>
			</div>
		</div>
	);
}
