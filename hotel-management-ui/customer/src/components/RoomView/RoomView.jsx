import RoomGallery from "../RoomGallery/RoomGallery";

export default function RoomView({ room, hotelBenefit }) {
	return (
		<div className="d-flex flex-column flex-lg-row h-100">
			{/* left */}
			<div className="flex-grow-1 bg-light d-flex flex-column w-100 rounded">
				<div className="d-flex justify-content-between px-5 py-3">
					<div className="fs-4 fw-bold">{room.name}</div>
					<div>
						<button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
					</div>
				</div>
				<div className="d-flex flex-column">
					<RoomGallery room={room} />
				</div>
			</div>
			{/* right */}
			<div
				className="d-flex flex-row flex-lg-column bg-white rounded"
				style={{
					width: "300px",
				}}>
				<div className="flex-grow-1 d-flex overflow-auto">
					<div className="flex-grow-1 d-flex flex-column">
						<div className="d-flex flex-column p-3">
							<div className="d-flex flex-column">
								<div className="fw-bold">Thông tin phòng</div>
								<div className="d-flex flex-column">
									<div>
										<i className="text-info fa-solid fa-ruler"></i>
										<span className="ms-2">{room.square > 0 + " m2" || "22.0 m2"}</span>
									</div>
									<div>
										<i className="text-info fa-solid fa-user-group"></i>
										<span className="ms-2">{room.numOfPeope + " khách"}</span>
									</div>
								</div>
							</div>
							<div className="border-top border-3 my-2"></div>
							<div className="d-flex flex-column">
								<div className="fw-bold">Tiện nghi phòng</div>
								<div className="row">
									<div className="col-12">
										{hotelBenefit.resttaurant && (
											<div className="d-flex text-success align-items-center">
												<i className="fa-solid fa-store"></i>
												<div className="ms-2">Nhà hàng</div>
											</div>
										)}
									</div>
									<div className="col-12">
										{hotelBenefit.elevator && (
											<div className="d-flex text-success align-items-center">
												<i className="fa-solid fa-elevator"></i>
												<div className="ms-2">Thang máy</div>
											</div>
										)}
									</div>
									<div className="col-12">
										{hotelBenefit.pool && (
											<div className="d-flex text-success align-items-center">
												<i className="fa-solid fa-water-ladder"></i>
												<div className="ms-2">Bể bơi</div>
											</div>
										)}
									</div>
									<div className="col-12">
										{hotelBenefit.freeBreakfast && (
											<div className="d-flex text-success align-items-center">
												<i className="fa fa-utensils"></i>
												<div className="ms-2">Bữa sáng miễn phí</div>
											</div>
										)}
									</div>
									<div className="col-12">
										{hotelBenefit.airConditioner && (
											<div className="d-flex text-success align-items-center">
												<i className="fa fa-temperature-arrow-down"></i>
												<div className="ms-2">Máy điều hòa</div>
											</div>
										)}
									</div>
									<div className="col-12">
										{hotelBenefit.carBorrow && (
											<div className="d-flex text-success align-items-center">
												<i className="fa-sharp fa-solid fa-bicycle"></i>
												<div className="ms-2">Thuê xe</div>
											</div>
										)}
									</div>
									<div className="col-12">
										{hotelBenefit.wifiFree && (
											<div className="d-flex text-success align-items-center">
												<i className="fa-solid fa-wifi"></i>
												<div className="ms-2">Wifi miễn phí</div>
											</div>
										)}
									</div>
									<div className="col-12">
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
				<div
					className="d-flex flex-column p-3"
					style={{
						boxShadow: "rgba(27, 27, 27, 0.1) 0px -4px 8px 0px, rgba(27, 27, 27, 0.1) 0px 1px 3px 0px",
					}}>
					<div>Giá chỉ từ:</div>
					<div className="fs-4 fw-bold text-danger">{new Intl.NumberFormat().format(room.price) + " VNĐ"}</div>
					<div className="d-grid">
						<button type="button" className="btn btn-primary btn-sm" data-bs-dismiss="modal">
							Thêm lựa chọn phòng
						</button>
					</div>
				</div>
			</div>
		</div>
	);
}
