import star from "../../assets/star.svg";
import halfStar from "../../assets/half-star.svg";

export default function DetailHotelPage() {
	return (
		<div className="Container mt-3">
			<div className="bg-white rounded border">
				<div className="d-flex flex-column p-3">
					<div className="d-flex flex-column">
						<h4>ACE Hotel Saigon</h4>
						<div className="d-flex align-items-center mb-2">
							<div className="d-flex me-2">
								<span className="badge rounded-pill bg-primary">Hotel</span>
							</div>
							<div className="d-flex align-items-center">
								<div>
									<img src={star} style={{ width: 18, height: 18 }} />
								</div>
								<div>
									<img src={star} style={{ width: 18, height: 18 }} />
								</div>
								<div>
									<img src={star} style={{ width: 18, height: 18 }} />
								</div>
								<div>
									<img src={halfStar} style={{ width: 18, height: 18 }} />
								</div>
							</div>
						</div>
						<div className="d-flex">
							<i className="fa-solid fa-location-dot"></i>
							<div className="ms-1">
								139H Nguyen Trai, Ben Thanh Ward, District 1, Ho Chi Minh City, Vietnam, 711090
							</div>
						</div>
						<div className="border my-3"></div>
						<div className="d-flex">
							<div className="d-flex flex-grow-1 me-2" style={{ width: 768, height: 488 }}>
								<img src="/img/hotel-room.webp" alt="room" className="w-100 h-100" style={{ objectFit: "cover" }} />
							</div>
							<div className="d-flex flex-column" style={{ width: 152 }}>
								<img
									src="/img/hotel-room.webp"
									alt="room"
									className="w-100 h-100 mb-2"
									style={{ objectFit: "cover" }}
								/>
								<img
									src="/img/hotel-room.webp"
									alt="room"
									className="w-100 h-100 mb-2"
									style={{ objectFit: "cover" }}
								/>
								<img
									src="/img/hotel-room.webp"
									alt="room"
									className="w-100 h-100 mb-2"
									style={{ objectFit: "cover" }}
								/>
								<img src="/img/hotel-room.webp" alt="room" className="w-100 h-100" style={{ objectFit: "cover" }} />
							</div>
						</div>
						{/* button select room */}
						<div className="d-flex justify-content-end mt-2">
							<div className="d-flex flex-column align-items-end">
								<div>{"Price/room/night starts from"}</div>
								<div className="text-danger fs-4">{"450.000 VND"}</div>
								<button className="btn btn-primary">Chọn phòng</button>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div className="mt-3">
				<h2 className="fs-4">Available Room Types in ACE Hotel Saigon</h2>
				<div className="bg-light rounded border p-3">
					<div className="container p-0">
						<div className="d-flex">
							<div className="bg-white p-0">
								<div className="d-flex flex-column">
									<div>
										<div className="mb-1" style={{ height: 144 }}>
											<img
												src="/img/hotel-room.webp"
												alt="room"
												className="w-100 h-100"
												style={{ objectFit: "fill" }}
											/>
										</div>
									</div>
									<div className="d-flex justify-content-between">
										<div style={{ width: 94, height: 50 }}>
											<img
												src="/img/hotel-room.webp"
												alt="room"
												className="w-100 h-100"
												style={{ objectFit: "fill" }}
											/>
										</div>
										<div style={{ width: 94, height: 50 }}>
											<img
												src="/img/hotel-room.webp"
												alt="room"
												className="w-100 h-100"
												style={{ objectFit: "fill" }}
											/>
										</div>
										<div style={{ width: 94, height: 50 }}>
											<img
												src="/img/hotel-room.webp"
												alt="room"
												className="w-100 h-100"
												style={{ objectFit: "fill" }}
											/>
										</div>
									</div>
								</div>
							</div>
							<div className="w-100 bg-white ms-3 rounded">
								<div className="d-flex flex-column px-4 py-3">
									<div className="d-flex flex-column">
										<div className="fw-bold fs-6">Standard Double Room</div>
										<div className="d-flex justify-content-between text-secondary">
											<div>
												<i className="fa-solid fa-bed"></i>
												<span className="ms-2">1 Giường đôi</span>
											</div>
											<div>
												<i className="fa-solid fa-user-group"></i>
												<span className="ms-2">2 người</span>
											</div>
											<div className="text-danger">(3 phòng có sẵn)</div>
										</div>
										<div className="border my-3"></div>
									</div>
									<div className="d-flex">
										<div className="flex-grow-1">
											<div className="d-flex flex-column bg-light">
												<div className="row">
													<div className="col">1</div>
													<div className="col">2</div>
												</div>
											</div>
										</div>
										<div style={{ width: "25%" }}>2</div>
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
