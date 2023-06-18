import { Link, NavLink } from "react-router-dom";

import { routes } from "../../routes";
import nulllIcon from "../../assets/null-icon.svg";
import BookedRoomCard from "../../components/BookedRoomCard";
import { useQuery } from "@tanstack/react-query";
import { axiosGet, axiosJWT, url } from "../../utils/httpRequest";
import { useDispatch, useSelector } from "react-redux";
import { selectAccessToken, selectRefreshToken } from "../../redux/selectors";
import { logout } from "../../services/userServices";

export default function BookedRoomPage() {
	const accessToken = useSelector(selectAccessToken);
	const refreshToken = useSelector(selectRefreshToken);
	const dispatch = useDispatch();
	const bookedRoomState = useQuery({
		queryKey: ["bookedRoom", accessToken],
		queryFn: async () => {
			const axiosJwt = axiosJWT(accessToken, refreshToken, dispatch);
			try {
				const res = await axiosJwt.get(url.bookedRooms, {
					headers: {
						Authorization: `Bearer ${accessToken}`,
					},
				});
				return res.data;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		staleTime: 3 * 60 * 1000,
	});
	let bookedRooms = [];
	if (bookedRoomState.isSuccess) {
		bookedRooms = bookedRoomState.data.bookingList;
	}
	const handleLogout = (e) => {
		e.preventDefault();
		logout(dispatch);
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
						<div className="flex-grow-1 d-flex flex-column gap-3">
							<div>
								<h1 className="fs-4">Danh sách phòng đã đặt</h1>
								{/* card wrapper, list */}
								<div className="d-flex flex-column gap-3">
									{bookedRooms.length > 0 ? (
										bookedRooms.map((bookedRoom) => <BookedRoomCard key={bookedRoom.id} bookedRoom={bookedRoom} />)
									) : (
										<div className="border rounded bg-white p-3 d-flex">
											<img src={nulllIcon} alt="null-icon" />
											<div className="d-flex flex-column mx-3">
												<h3 className="fs-6 fw-bold my-2">Không tìm thấy đặt chỗ</h3>
												<div>
													Mọi chỗ bạn đặt sẽ được hiển thị tại đây. Hiện bạn chưa có bất kỳ đặt chỗ nào, hãy đặt trên
													trang chủ ngay!
												</div>
											</div>
										</div>
									)}
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
