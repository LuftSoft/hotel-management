import { useSelector } from "react-redux";
import { axiosPost, url } from "../../utils/httpRequest";
import { selectAccessToken, selectBookingDate, selectUser } from "../../redux/selectors";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { routes } from "../../routes";
import { useState } from "react";
import RoomView from "../RoomView/RoomView";

export default function RoomCard({ room, hotelBenefit, onCardClick = () => {} }) {
	const bookingDate = useSelector(selectBookingDate);
	const currentUser = useSelector(selectUser);
	const accessToken = useSelector(selectAccessToken);

	const [showDetail, setShowDetail] = useState(false);
	const currentPathName = window.location.pathname;

	const navigate = useNavigate();

	const handleBook = async () => {
		console.log(room.id);
		if (currentUser) {
			const toastId = toast.loading("Đang xử lý!");
			try {
				const res = await axiosPost(
					url.createBooking,
					{
						id: "0",
						roomId: room.id,
						status: "create",
						returned: false,
						fromDate: new Date(bookingDate.FromDate).toISOString(),
						toDate: new Date(bookingDate.ToDate).toISOString(),
						// fromDate: "2023-06-19T15:52:09.895Z",
						// toDate: "2023-06-20T15:52:09.895Z",
					},
					{
						headers: {
							Authorization: "Bearer " + accessToken,
						},
					},
				);
				// console.log(res); //res.success
				onCardClick();
				if (res.success) {
					toast.update(toastId, {
						render: "Đặt phòng thành công!",
						type: "success",
						closeButton: true,
						autoClose: 1000,
						isLoading: false,
					});
				}
			} catch (error) {
				console.log(error);
				toast.update(toastId, {
					render: "Không thể đặt phòng!",
					type: "error",
					closeButton: true,
					autoClose: 1000,
					isLoading: false,
				});
			}
		} else {
			navigate(`${routes.signIn}?next=${encodeURIComponent(currentPathName)}`);
		}
	};
	const handleShowDetail = () => {
		setShowDetail(!showDetail);
	};
	return (
		<div className="bg-light rounded border p-3">
			{true && (
				<>
					<div
						className="modal fade"
						id={`room-${room.id}`}
						tabIndex={-1}
						aria-labelledby={`room-${room.id}Label`}
						aria-hidden="true">
						<div className="modal-dialog modal-lg">
							<div className="modal-content">
								<div
									className="modal-body p-0"
									style={{
										// height: "90vh",
										height: "90vh",
										maxHeight: "685px",
									}}>
									<RoomView room={room} hotelBenefit={hotelBenefit} />
								</div>
							</div>
						</div>
					</div>
				</>
			)}
			<div className="container p-0">
				<div className="d-flex">
					<div className="flex-grow-1 d-flex flex-column bg-white p-0">
						<div className="d-flex flex-column ">
							<div className="d-flex flex-column mb-1">
								<div className="" style={{ height: 144 }}>
									<img
										src={room.hotelImageGalleries[0].link}
										alt="room"
										className="w-100 h-100"
										style={{ objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
								</div>
							</div>
							<div className="d-flex justify-content-between">
								<div style={{ width: 90, height: 50 }} className="me-1">
									<img
										src={room.hotelImageGalleries[0].link}
										alt="room"
										className="w-100 h-100"
										style={{ objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
								</div>
								<div style={{ width: 90, height: 50 }} className="me-1">
									<img
										src={room.hotelImageGalleries[1].link}
										alt="room"
										className="w-100 h-100"
										style={{ objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
								</div>
								<div style={{ width: 90, height: 50 }}>
									<img
										src={room.hotelImageGalleries[2].link}
										alt="room"
										className="w-100 h-100"
										style={{ objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
								</div>
							</div>
							<div className="d-flex flex-column py-2">
								<button
									type="button"
									onClick={handleShowDetail}
									className="btn btn-outline-info"
									data-bs-toggle="modal"
									data-bs-target={`#room-${room.id}`}>
									Xem chi tiết
								</button>
							</div>
						</div>
					</div>
					<div className="w-100 bg-white ms-3 rounded">
						<div className="d-flex flex-column px-4 py-3">
							<div className="d-flex flex-column">
								<div className="fw-bold fs-6">{room.name}</div>
								<div className="d-flex justify-content-between text-secondary">
									<div>
										<i className="fa-solid fa-bed"></i>
										<span className="ms-2">{room.numOfBed + " Giường"}</span>
									</div>
									<div>
										<i className="fa-solid fa-user-group"></i>
										<span className="ms-2">{room.numOfPeope + " khách"}</span>
									</div>
									<div className="text-danger">{`(${room.emptyRoom} phòng có sẵn)`}</div>
								</div>
								<div className="border my-3"></div>
							</div>
							<div className="d-flex">
								<div className="flex-grow-1">
									<div className="d-flex flex-column">
										<div className="row">
											<div className="col">
												<div className="d-flex flex-column">
													<div className="d-flex flex-column">
														{hotelBenefit.resttaurant && (
															<div className="d-flex text-success align-items-center">
																<i className="fa-solid fa-store"></i>
																<div className="ms-2">Nhà hàng</div>
															</div>
														)}
													</div>
													<div className="d-flex flex-column">
														{hotelBenefit.elevator && (
															<div className="d-flex text-success align-items-center">
																<i className="fa-solid fa-elevator"></i>
																<div className="ms-2">Thang máy</div>
															</div>
														)}
													</div>
													<div className="d-flex flex-column">
														{hotelBenefit.pool && (
															<div className="d-flex text-success align-items-center">
																<i className="fa-solid fa-water-ladder"></i>
																<div className="ms-2">Bể bơi</div>
															</div>
														)}
													</div>
													<div className="d-flex flex-column">
														{hotelBenefit.freeBreakfast && (
															<div className="d-flex text-success align-items-center">
																<i className="fa fa-utensils"></i>
																<div className="ms-2">Bữa sáng miễn phí</div>
															</div>
														)}
													</div>
												</div>
											</div>
											<div className="col">
												<div className="d-flex flex-column">
													<div className="d-flex flex-column">
														{hotelBenefit.airConditioner && (
															<div className="d-flex text-success align-items-center">
																<i className="fa fa-temperature-arrow-down"></i>
																<div className="ms-2">Máy điều hòa</div>
															</div>
														)}
													</div>
													<div className="d-flex flex-column">
														{hotelBenefit.carBorrow && (
															<div className="d-flex text-success align-items-center">
																<i className="fa-sharp fa-solid fa-bicycle"></i>
																<div className="ms-2">Thuê xe</div>
															</div>
														)}
													</div>
													<div className="d-flex flex-column">
														{hotelBenefit.wifiFree && (
															<div className="d-flex text-success align-items-center">
																<i className="fa-solid fa-wifi"></i>
																<div className="ms-2">Wifi miễn phí</div>
															</div>
														)}
													</div>
													<div className="d-flex flex-column">
														{hotelBenefit.parking && (
															<div className="d-flex text-success align-items-center">
																<i className="fa-solid fa-parking"></i>
																<div className="ms-2">Chỗ đậu xe</div>
															</div>
														)}
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
								<div className="d-flex flex-column align-items-end" style={{ width: "25%" }}>
									<div className="d-flex flex-column text-end">
										<div
											className="text-secondary text-decoration-line-through"
											style={{
												fontSize: 12,
											}}>
											{new Intl.NumberFormat().format(room.price + 100000) + " VNĐ"}
										</div>
										<div className="fw-bold text-warning">{new Intl.NumberFormat().format(room.price) + " VNĐ"}</div>
										<div className="text-secondary">{"/ phòng / đêm"}</div>
										<div className="text-primary">{"Giá cuối cùng"}</div>
									</div>
									<div>
										<button type="button" onClick={handleBook} className="btn btn-primary">
											Đặt ngay
										</button>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
